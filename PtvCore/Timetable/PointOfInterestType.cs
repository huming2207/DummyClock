using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PtvCore.Timetable
{
    public enum PointOfInterestType : uint
    {
        Train = 0,
        Tram = 1,
        Bus = 2,
        Vline = 3,
        NightRider = 4,
        TicketOutlet = 100
    }
}
