using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanProject.Domain
{
    public class ReservationDBDTO
    {
        [Key]
        public string reservationNumber { get; set; }
        public string buyerId { get; set; }
        public int travellerNumber { get; set; }
        public int paymentNo { get; set; }
        public double paymentAmount { get; set; }
        public string paymetCurrency { get; set; }
        public string bookingNumber { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public string serviceId { get; set; }
        public string hotelName { get; set; }
        public string hotelPhoneNumber { get; set; }
        public string hotelHomePage { get; set; }
        public string hotelCity { get; set; }
        public string hotelCountry { get; set; }
    }
}
