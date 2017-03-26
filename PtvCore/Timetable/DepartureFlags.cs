using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    [Flags]
    public enum DepartureFlags
    {
        None,
        HasFlags,
        ReservationsRequired,
        GuaranteedConnection,
        DropOffOnly,
        PickUpOnly,
        MondaysOnly,
        TuesdaysOnly,
        WednesdaysOnly,
        ThursdaysOnly,
        FridaysOnly,
        SchoolDaysOnly
    }
}
