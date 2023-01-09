using SanProject.Domain.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanProject.Shared.HotelModels
{
    public class HotelDetailDTO
    {
        public string HotelId { get; set; }
        public int stars { get; set; }
        public string name { get; set; }
        public double rating { get; set; }
        public string locationName { get; set; }
        public List<Room> rooms { get; set; }
        public DateTime cancellationDueDate { get; set; }
        public double cancellationPrice { get; set; }
        public string cancellationCurrency { get; set; }
        public string offerId { get; set; }
        public DateTime offerCheckIn { get; set; }
        public Price price { get; set; }
        public string address { get; set; }
        public HotelCategory hotelCategory { get; set; }
        public string thumbnail { get; set; }
        public string thumbnailFull { get; set; }
        public Description description { get; set; }
        public int travellernum { get; set; }
        public List<object> facilities { get; set; }
    }
}
