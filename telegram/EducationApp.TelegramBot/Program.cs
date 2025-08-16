using System.Collections.Concurrent;
using System.Net.Http.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    private static TelegramBotClient bot;
    private static HttpClient http = new HttpClient();
    private static ConcurrentDictionary<long, UserSession> sessions = new();

    public static async Task Main(string[] args)
    {
        var token = "8489802573:AAF5MDmT2QUwlIHgBSVuCjO3_ARW7iBUAqw";
        var apiBase = "https://localhost:7004";
        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(apiBase))
        {
            Console.WriteLine("Please set BOT_TOKEN and API_BASE env variables.");
            return;
        }

        http.BaseAddress = new Uri(apiBase);
        bot = new TelegramBotClient(token);

        using var cts = new CancellationTokenSource();
        bot.StartReceiving(UpdateHandler, ErrorHandler, new ReceiverOptions { AllowedUpdates = { } }, cts.Token);
        var me = await bot.GetMe();
        Console.WriteLine($"Bot {me.Username} is running...");
        Console.ReadLine();
    }

    private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken ct)
    {
        if (update.Type == UpdateType.Message && update.Message!.Text is not null)
        {
            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text.Trim();
            var session = sessions.GetOrAdd(chatId, _ => new UserSession());

            if (text.StartsWith("/start"))
            {
                await bot.SendMessage(chatId, "👋 Welcome! Please choose:", replyMarkup: HomeKeyboard());
            }
            else if (text == "/register")
            {
                session.State = UserState.AskFirstName;
                await bot.SendMessage(chatId, "Enter your first name:");
            }
            else if (text == "/login")
            {
                session.State = UserState.AskLoginEmail;
                await bot.SendMessage(chatId, "Enter your email:");
            }
            else if (session.State != UserState.None)
            {
                if (session.State.ToString().StartsWith("AskLogin"))
                    await HandleLoginFlow(chatId, text, session);
                else
                    await HandleRegistrationFlow(chatId, text, session);
            }
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;
            var data = update.CallbackQuery.Data;

            if (data == "/register")
            {
                sessions[chatId].State = UserState.AskFirstName;
                await bot.SendMessage(chatId, "Enter your first name:");
            }
            else if (data == "/login")
            {
                sessions[chatId].State = UserState.AskLoginEmail;
                await bot.SendMessage(chatId, "Enter your email:");
            }
        }
    }

    private static async Task HandleRegistrationFlow(long chatId, string text, UserSession session)
    {
        switch (session.State)
        {
            case UserState.AskFirstName:
                session.Register.FirstName = text;
                session.State = UserState.AskLastName;
                await bot.SendMessage(chatId, "Enter your last name:");
                break;
            case UserState.AskLastName:
                session.Register.LastName = text;
                session.State = UserState.AskPhone;
                await bot.SendMessage(chatId, "Enter your phone number:");
                break;
            case UserState.AskPhone:
                session.Register.PhoneNumber = text;
                session.State = UserState.AskEmail;
                await bot.SendMessage(chatId, "Enter your email:");
                break;
            case UserState.AskEmail:
                session.Register.Email = text;
                session.State = UserState.AskPassword;
                await bot.SendMessage(chatId, "Enter your password:");
                break;
            case UserState.AskPassword:
                session.Register.Password = text;
                session.State = UserState.AskBirthDate;
                await bot.SendMessage(chatId, "Enter your birth date (yyyy-MM-dd):");
                break;
            case UserState.AskBirthDate:
                if (DateTime.TryParse(text, out var birth))
                {
                    session.Register.BirthDate = birth;
                    session.State = UserState.AskGender;
                    await bot.SendMessage(chatId, "Enter your gender (0=Male, 1=Female):");
                }
                else
                {
                    await bot.SendMessage(chatId, "❌ Invalid date format. Please use yyyy-MM-dd.");
                }
                break;
            case UserState.AskGender:
                if (int.TryParse(text, out var gender) && (gender == 0 || gender == 1))
                {
                    session.Register.Gender = gender;
                    session.Register.IsAdminSite = false;
                    await SubmitRegistration(chatId, session);
                }
                else
                {
                    await bot.SendMessage(chatId, "❌ Invalid gender. Enter 0 or 1.");
                }
                break;
        }
    }

    private static async Task HandleLoginFlow(long chatId, string text, UserSession session)
    {
        switch (session.State)
        {
            case UserState.AskLoginEmail:
                session.Login.Email = text;
                session.State = UserState.AskLoginPassword;
                await bot.SendMessage(chatId, "Enter your password:");
                break;
            case UserState.AskLoginPassword:
                session.Login.Password = text;
                await SubmitLogin(chatId, session);
                break;
        }
    }

    private static async Task SubmitRegistration(long chatId, UserSession session)
    {
        var response = await http.PostAsJsonAsync("/api/Auth/register", session.Register);
        if (response.IsSuccessStatusCode)
        {
            await bot.SendMessage(chatId, "✅ Registration successful! Please check your email for verification.", replyMarkup: HomeKeyboard());
            session.State = UserState.None;
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            await bot.SendMessage(chatId, $"❌ Registration failed: {error}");
        }
    }

    private static async Task SubmitLogin(long chatId, UserSession session)
    {
        var response = await http.PostAsJsonAsync("/api/Auth/login", session.Login);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDto>>();

            if (apiResponse != null && apiResponse.Succeeded)
            {
                var loginResult = apiResponse.Result;
                await bot.SendMessage(
                    chatId,
                    $"✅ Welcome back {loginResult.FirstName} {loginResult.LastName}!\nYour token: {loginResult.AccessToken}",
                    replyMarkup: HomeKeyboard()
                );
                session.State = UserState.None;
            }
            else
            {
                var errorMessage = apiResponse?.Errors != null && apiResponse.Errors.Any()
                    ? string.Join(", ", apiResponse.Errors)
                    : "Unknown error";
                await bot.SendMessage(chatId, $"❌ Login failed: {errorMessage}");
            }
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            await bot.SendMessage(chatId, $"❌ Login failed: {error}");
        }
    }

    private static InlineKeyboardMarkup HomeKeyboard() => new InlineKeyboardMarkup(new[]
    {
        new[] { InlineKeyboardButton.WithCallbackData("Register", "/register") },
        new[] { InlineKeyboardButton.WithCallbackData("Login", "/login") },
    });

    private static Task ErrorHandler(ITelegramBotClient bot, Exception ex, CancellationToken ct)
    {
        Console.WriteLine(ex);
        return Task.CompletedTask;
    }
}

class UserSession
{
    public UserState State { get; set; } = UserState.None;
    public RegisterDto Register { get; set; } = new();
    public LoginRequestDto Login { get; set; } = new();
}

enum UserState
{
    None,
    AskFirstName,
    AskLastName,
    AskPhone,
    AskEmail,
    AskPassword,
    AskBirthDate,
    AskGender,
    AskLoginEmail,
    AskLoginPassword
}

class RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
    public int Gender { get; set; }
    public bool IsAdminSite { get; set; }
}

class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

// Wrapper class for API response
public class ApiResponse<T>
{
    public bool Succeeded { get; set; }
    public T Result { get; set; }
    public List<string> Errors { get; set; }
}

// Login response structure
public class LoginResponseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public List<string> Roles { get; set; }
    public List<string> Permissions { get; set; }
}
