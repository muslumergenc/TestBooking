using SanProject.Domain;
using SanProject.Shared.HotelModels;
using System.Collections.Generic;

namespace Zaren.Web.ViewModels
{
    public class CityHotelVM
    {
        public AllHotelQueryDTO query { get; set; }
        public List<CityObject> cityObjectList { get;set; }
    }
}
