using LidgrenServer.MyEventArgs;
using LidgrenServer.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidgrenServer.Managers
{
    internal class ManagerLogger
    {
        private List<LogMessage> _logMessages;
        public event EventHandler<LogMessageEventArgs> NewLogMessageEvent;

        public ManagerLogger()
        {
            _logMessages = new List<LogMessage>();
        }

        public void AddLogMessage(LogMessage logMessage)
        {
            _logMessages.Add(logMessage);

            if (NewLogMessageEvent != null)
            {
                NewLogMessageEvent(this, new LogMessageEventArgs(logMessage));
            }
        }

        public void AddLogMessage(string id, string message)
        {
            AddLogMessage(new LogMessage { Id = id, Message = message });
        }
    }

}
