using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsBookingSystem.Core.Enum
{
    public enum BookingState
    {
        Arrived = 0,

        Loading = 1,

        Unloading = 2,

        Completed = 3,

        Cancelled = 4
    }
}
