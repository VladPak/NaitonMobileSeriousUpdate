using NaitonGPS.Models;
using System.Collections.Generic;

namespace NaitonGPS.ViewModels
{
    public class RacksViewModel
    {
        public List<Rack> Racks { get; set; }

        public RacksViewModel(List<Rack> racks)
        {
            Racks = racks;
        }

    }
}
