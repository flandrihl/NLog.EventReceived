using NLog.Targets;
using System;

namespace NLog.EventReceived
{
    public class EventLogTarget : Target
    {
        /// <summary>
        /// Occurs when [on received].
        /// </summary>
        public event Action<LogEventInfo> OnReceived;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLogTarget" /> class.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="loggerName">Name of the logger.</param>
        /// <param name="ruleName">Name of the rule.</param>
        public EventLogTarget(LogLevel? logLevel = null, string loggerName = "*", string? ruleName = null) => _ = LogManager
            .Setup()
            .LoadConfiguration(configBuilder: _ => _
            .ForLogger(logLevel ?? LogLevel.Trace, loggerName, ruleName)
            .WriteTo(this));

        private EventLogTarget() { }

        /// <summary>
        /// Writes logging event to the target destination
        /// </summary>
        /// <param name="logEvent">Logging event to be written out.</param>
        protected override void Write(LogEventInfo logEvent) => OnReceived?.Invoke(logEvent);
    }
}
