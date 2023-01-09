using System;
using System.Collections.Generic;
using System.Text;

namespace SanProject.Shared.BookingModels
{
    public class BookingFormsViewModel
    {
        public List<SanProject.Domain.SetReservation.Traveller> travellers { get; set; }
        public int travellernumber { get; set; }
        public string offerId { get; set; }
        public string currency { get; set; }
        public string culture { get; set; }
    }
}
