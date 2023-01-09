using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using SanProject.Application.Services.Interfaces;
using SanProject.Data;
using SanProject.Domain;
using SanProject.Shared.BookingModels;
using SanProject.Shared.HotelModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SanProject.Web.Controllers
{
    public class BookingController : Controller
    {
        public SanProjectDBContext _context;
        public readonly IUnitOfWork _unitofwork;
        public readonly IUsersService _userservice;
        public readonly IAuthenticationService _authenticationservice;
        public readonly ICitySearchService _citysearchservice;
        public IHotelService _hotelservice;
        public IBookingService _bookingservice;
        public string currency { get; set; }
        public string culture { get; set; }
        public string offerId { get; set; }

        public BookingController(IUnitOfWork unitofwork, SanProjectDBContext context,
            IUsersService userservice, IAuthenticationService authenticationservice,
            ICitySearchService citysearchservice, IHotelService hotelservice, IBookingService bookingservice)
        {
            _unitofwork = unitofwork;
            _context = context;
            _userservice = userservice;
            _authenticationservice = authenticationservice;
            _citysearchservice = citysearchservice;
            _hotelservice = hotelservice;
            _bookingservice = bookingservice;
        }

        public IActionResult Index(string offid, string currenc, int travnum)
        {

            currency = currenc;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BookingFormsViewModel m = new BookingFormsViewModel();
            m.travellers = new List<Domain.SetReservation.Traveller>();
            for (int i = 0; i < travnum; i++)
            {
                m.travellers.Add(new Domain.SetReservation.Traveller());
            }
            m.travellernumber = travnum;
            m.offerId = offid;
            m.currency = currenc;
            m.culture = "en-US";
            return View(m);
        }
        [Route("BookingDetails")]
        public async Task<IActionResult> Booking(BookingFormsViewModel model)
        {
            List<SanProject.Domain.SetReservation.Traveller> travls = model.travellers;
            for(int i=0; i < model.travellers.Count; i++)
            {
                if (model.travellers[i].address.email == null)
                {
                    model.travellers[i].address.email = "";
                }
                if(model.travellers[i].name==null || model.travellers[i].surname == null)
                {
                    return BadRequest("Fill in the name and Surname Fields properly");
                }
            }
            ReservationDetailDTO res = await _bookingservice.FullBooking(model.offerId, model.currency, model.culture, travls);
            if (res == null)
            {
                return BadRequest("Reservation Could Not Be Made");
            }
            return View(res);
        }

    }
}
