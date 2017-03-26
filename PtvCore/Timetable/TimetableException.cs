using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    public class TimetableException : Exception
    {
        public TimetableException() { }
        public TimetableException(string message) : base(message) { }
        public TimetableException(string message, Exception inner) : base(message, inner) { }

        public string Json { get; set; }
    }
}
