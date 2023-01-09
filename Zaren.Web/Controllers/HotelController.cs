using Microsoft.AspNetCore.Mvc;
using SanProject.Application.Services.Interfaces;
using SanProject.Data;
using SanProject.Domain;
using SanProject.Shared.HotelModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanProject.Web.Controllers
{
    public class HotelController : Controller
    {
        public SanProjectDBContext _context;
        public readonly IUnitOfWork _unitofwork;
        public readonly IUsersService _userservice;
        public readonly IAuthenticationService _authenticationservice;
        public readonly ICitySearchService _citysearchservice;
        public IHotelService _hotelservice;
        public HotelController(IUnitOfWork unitofwork, SanProjectDBContext context,
          IUsersService userservice, IAuthenticationService authenticationservice,
          ICitySearchService citysearchservice, IHotelService hotelservice)
        {
            _unitofwork = unitofwork;
            _context = context;
            _userservice = userservice;
            _authenticationservice = authenticationservice;
            _citysearchservice = citysearchservice;
            _hotelservice = hotelservice;
        }
        public IActionResult HotelsSearchPage(string id)
        {
            AllHotelQueryDTO t = new AllHotelQueryDTO();
            t.LocationId = id;
            return View(t);
        }
        [HttpPost]
        public async Task<IActionResult> Index(AllHotelQueryDTO qu)
        {

            QueryDetailBundleDTO a = new QueryDetailBundleDTO();
            List<HotelDetailDTO> t = await _hotelservice.GetAllDetails(qu);
            if (t == null)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return View(t);
        }


        public async Task<IActionResult> AlternateHotelDetail(string hotelId, int numtrav, string checkinstr)
        {
            HotelQueryDTO dto = new HotelQueryDTO(); dto.HotelId = hotelId; dto.NumberOfTravellers = numtrav;
            HotelDetailDTO dt = await _hotelservice.GetDetails(dto.HotelId, dto.NumberOfTravellers, checkinstr);
            //HotelDetailDTO dt = await _hotelservice.GetDetails(dt.HotelId, dt.NumberOfTravellers);
            
            if (dt == null)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return View("HotelDetail", dt);
        }





        //The previous user number related code that initiated the hotel details afterwards

        /* public async Task<IActionResult> NumberofTravelers(string id)
         {

             HotelQueryDTO a=new HotelQueryDTO();
             a.HotelId = id;
             a.NumberOfTravellers = 1;
             HotelDetailDTO dt = await _hotelservice.GetDetails(a.HotelId, a.NumberOfTravellers);
             if (dt == null)
             {
                 //return View("~/Views/SpecificView.cshtml");
                 return Redirect(Request.Headers["Referer"].ToString());
             }
             return View(a);
         }*/
        /*public async Task<IActionResult> HotelDetail(HotelQueryDTO dto)
        {
            HotelDetailDTO dt = await _hotelservice.GetDetails(dto.HotelId, dto.NumberOfTravellers);
            //HotelDetailDTO dt = await _hotelservice.GetDetails(dt.HotelId, dt.NumberOfTravellers);
            if (dt == null)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return View(dt);
        }*/


    }
}
