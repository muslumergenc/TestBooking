using System;
using System.Collections.Generic;
using System.Text;

namespace SanProject.Domain
{
   
        public class RoomCriterion
        {
            public int adult { get; set; }
        }

    public class RootQ
    {
        public bool checkAllotment { get; set; } = true;
        public bool checkStopSale { get; set; } = true;
        public bool getOnlyDiscountedPrice { get; set; } = false;
        public bool getOnlyBestOffers { get; set; } = true;
        public int productType { get; set; } = 2;
        public List<string> Products { get; set; }=new List<string>() { };
        public List<RoomCriterion> roomCriteria { get; set; } = new List<RoomCriterion>() { new RoomCriterion { adult=2} };
        public string nationality { get; set; } = "DE";
        public string checkIn { get; set; }
        public int night { get; set; } = 7;
        public string currency { get; set; } = "EUR";
        public string culture { get; set; } = "en-US";
    }

    
}
