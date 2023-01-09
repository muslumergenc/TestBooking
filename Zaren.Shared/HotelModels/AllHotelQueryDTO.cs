using System;
using System.Collections.Generic;
using System.Text;

namespace SanProject.Shared.HotelModels
{
    public  class AllHotelQueryDTO
    {
        public string LocationId { get; set; }
        public int NumberOfTravellers { get; set; }

        public DateTime ChcekIn { get; set; }
    }
}
