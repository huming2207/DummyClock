using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    public class DetailedResult
    {
        public static LineArray LineResultArray { get; set; }
        public static Line LineResult { get; set; }
        public static Direction[] DirectionResultArray { get; set; }
        public static Direction DirectionResult { get; set; } 
    }
}
