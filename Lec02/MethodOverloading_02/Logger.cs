using System;

namespace MethodOverloading_02
{
    public class Logger
    {
        /// <summary>
        /// readonly property
        /// </summary>
        public DateTime LoggedTime
        {
            get;
        }

        #region CTORs

        /// <summary>
        /// bezparametricky CTOR, jakou hodnotu by mel field
        /// LoggedTime, pokud by CTOR neobsahoval inicializaci ?
        /// </summary>
        public Logger()
        {
            this.LoggedTime = DateTime.Now;
        }

        /// <summary>
        /// pretizena varianta konstruktoru
        /// </summary>
        /// <param name="timeToLog"></param>
        public Logger(DateTime timeToLog)
        {
            this.LoggedTime = timeToLog;
        }

        #endregion

        #region simple overloaded methods

        public void WriteLoggedTime()
        {
            // pripomenuti interpolace stringu:
            // https://msdn.microsoft.com/en-us/library/dn961160.aspx?f=255&MSPPError=-2147217396
            Console.WriteLine($"Logged time: {this.LoggedTime.ToShortTimeString()}");
        }

        /// <summary>
        /// Pretizena metoda s volitelnym parametrem
        /// http://msdn.microsoft.com/en-us/library/dd264739.aspx
        /// </summary>
        public void WriteLoggedTime(string description, bool includeDate = true)
        {
            if (includeDate)
            {
                Console.WriteLine(
                    $"{description ?? "General event has occurred at: "} {this.LoggedTime.ToShortDateString()} at: {this.LoggedTime.ToShortTimeString()}");
            }
            else
            {
                Console.WriteLine(
                   $"{description ?? "General event has occurred at: "} at: {this.LoggedTime.ToShortTimeString()}");
            }           
        }

        #endregion
    }
}
