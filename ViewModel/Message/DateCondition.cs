using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Message
{
    public class DateTimeCondition
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; } = DateTime.Now;
    }
}
