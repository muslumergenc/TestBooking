using SanProject.Domain.ReservationDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanProject.Shared.BookingModels
{
    public  class ReservationDetailDTO
    {
        public List<Traveller> travellers { get; set; }
        public DateTime expiresOn { get; set; }
        public string bookingNumber { get; set; }
        public Agency agency { get; set; }
        public AgencyUser agencyUser { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public string agencyReservationNumber { get; set; }
        public PaymentDetail paymentDetail { get; set; }
        public string documenturl { get; set; }
        public ServiceDetails serviceDetails { get; set; }


    }
}
