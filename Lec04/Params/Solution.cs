using System;

namespace Params
{
    public static class Solution
    {
        #region solution testing

        public static void TestSolution()
        {
            TestMethod();
        }

        private static void TestMethod()
        {
            // prepare test data
            var logTime = new EventLog("Underlying provider failed to open...", DateTime.Now.AddMinutes(-1));                
            var logTimes = new[]
            {
                new EventLog("Fatal error in AppCore.dll..." , DateTime.Now.AddMinutes(-15)),
                new EventLog("An unexpected AggregateException occurred in user code...", DateTime.Now.AddSeconds(-120)),
                new EventLog(null, DateTime.Now.AddHours(-2))
            };

            // no output
            WriteLogsWithDescription();
            // optional parameters must be stated explicitly when using params
            WriteLogsWithDescription(true, logTime);
            WriteLogsWithDescription(false, logTimes);

            Console.ReadKey();
        }

        #endregion

        /// <summary>
        /// Writes out variable number of logs
        /// each log consists of log time and
        /// log specific message
        /// </summary>
        /// <param name="includeDate">
        /// determines if date should be also
        /// included in the log output, this
        /// parameter is optional (with default
        /// value set to true)
        /// </param>
        /// <param name="logsToWriteOut">
        /// variable number of DateTimes 
        /// each having a specific message,
        /// which contains recorded events
        /// </param>
        private static void WriteLogsWithDescription(bool includeDate = true, params EventLog[] logsToWriteOut)
        {
            if (logsToWriteOut == null)
            {
                return;
            }
            foreach (var eventLog in logsToWriteOut)
            {
                Console.Write($"{eventLog.EventDescription} ");
                if (includeDate)
                {
                    Console.Write($"{eventLog.LoggeDateTime.ToShortDateString()} ");
                }
                Console.WriteLine(eventLog.LoggeDateTime.ToShortTimeString());
            }
        }
    }

    /// <summary>
    /// Simple class that represents log event
    /// </summary>
    public class EventLog
    {
        public string EventDescription { get; }
        public DateTime LoggeDateTime { get; }

        public EventLog(string description, DateTime logTime)
        {
            EventDescription = description ?? "General event has occurred at: ";
            LoggeDateTime = logTime;
        }
    }
}
