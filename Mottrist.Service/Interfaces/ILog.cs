using System.Diagnostics;

namespace Mottrist.Service.Interfaces
{
    public interface ILog
    {
        void Log(Exception ex, EventLogEntryType entryType = EventLogEntryType.Error);
    }
}
