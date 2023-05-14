using Serilog;

namespace blazor_todo.Server.Utility
{
    public class AppSettingsHelper
    {
        public static void EnableLogger()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("logs" + "/log_.txt", rollOnFileSizeLimit: true,
            fileSizeLimitBytes: 10000000, rollingInterval: RollingInterval.Day).CreateLogger();
            Log.Information("START PROCESS");
        }
    }
}
