
namespace What2Watch.Services.Logging.Configuration
{
    public static class LoggingConfiguration
    {
        public static IServiceCollection RegisterLoggingInterfaces(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppLogging<>), typeof(AppLogging<>));
            return services;
        }

        internal static readonly string OutputTemplate =
            @"[{Timestamp:yy-MM-dd HH:mm:ss} {Level}]{ApplicationName}:{SourceContext}{NewLine}Message:{Message}{NewLine}in method {MemberName} at {FilePath}:{LineNumber}{NewLine}{Exception}{NewLine}";


        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            var config = builder.Configuration;
            var settings = config.GetSection(nameof(AppLoggingSettings)).Get<AppLoggingSettings>();         
            string restrictedToMinimumLevel = settings.General.RestrictedToMinimumLevel;
            if (!Enum.TryParse<LogEventLevel>(restrictedToMinimumLevel, out var logLevel))
            {
                logLevel = LogEventLevel.Debug;
            }

            var log = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
                .Enrich.FromLogContext()
                .Enrich.With(new PropertyEnricher("ApplicationName", config.GetValue<string>("ApplicationName")))
                .Enrich.WithMachineName()
                .WriteTo.File(
                    path: builder.Environment.IsDevelopment()
                        ? settings.File.FileName
                        : settings.File.FullLogPathAndFileName,
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: logLevel,
                    outputTemplate: OutputTemplate)
                .WriteTo.Console(restrictedToMinimumLevel: logLevel);
               
            builder.Logging.AddSerilog(log.CreateLogger(), false);
        }
    }
}
