using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuslanMessager
{
    [Serializable]
    public class DayMessageJournalSerializable
    {
        public string CurrentDate;
        public List<Message> Messages;
        public DayMessageJournalSerializable()
        {
            CurrentDate = DateTime.Now.ToShortDateString();
            Messages = new List<Message>();
        }
    }
}
