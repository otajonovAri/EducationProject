using Serilog;

namespace EducationApp.Application.ErrorLogs;

public class LogException
{
    public static void LogExceptions(Exception ex)
    {
        // Log write File 
        LogToFil(ex.Message);

        // Log write Console
        LogToConsole(ex.Message);

        // Log write Dubugger
        LogToDebugger(ex.Message);
    }

    private static void LogToDebugger(string exMessage) => Log.Debug(exMessage);

    private static void LogToConsole(string exMessage) => Log.Warning(exMessage);

    private static void LogToFil(string exMessage) => Log.Information(exMessage);
}