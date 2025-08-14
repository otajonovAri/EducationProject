using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace EducationApp.TelegramBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            var bot = new TelegramBotClient("8489802573:AAF5MDmT2QUwlIHgBSVuCjO3_ARW7iBUAqw", cancellationToken: cts.Token);
            var me = await bot.GetMe();
            bot.OnError += OnError;
            bot.OnMessage += OnMessage;
            bot.OnUpdate += OnUpdate;

            Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
            Console.ReadLine();
            cts.Cancel(); // stop the bot

            // Methods to handle events from the bot client

            // method to handle errors in polling or in your OnMessage/OnUpdate code
            async Task OnError(Exception exception, HandleErrorSource source)
            {
                Console.WriteLine(exception); // just dump the exception to the console
            }

            // method that handle messages received by the bot:
            async Task OnMessage(Message msg, UpdateType type)
            {
                if (msg.Text == "/start")
                {   
                    await bot.SendMessage(msg.Chat, "Welcome! Pick one direction",
                        replyMarkup: new InlineKeyboardButton[] { "Left", "Right" });
                }
            }

            // method that handle other types of updates received by the bot:
            async Task OnUpdate(Update update)
            {
                if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
                {
                    await bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
                    await bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
                }
            }
        }
    }
}
