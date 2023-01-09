using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SanProject.Application.Services;
using SanProject.Application.Services.Interfaces;
using SanProject.Data;
using SanProject.Domain;
using SanProject.Domain.City;
using SanProject.Domain.Hotel;
using SanProject.Shared.HotelModels;
using SanProject.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Zaren.Web.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SanProject.Web.Controllers
{
    public class HomeController : Controller
    {
        public SanProjectDBContext _context;
        public readonly IUnitOfWork _unitofwork;
        public readonly IUsersService _userservice;
        public readonly IAuthenticationService _authenticationservice;
        public readonly ICitySearchService _citysearchservice;
        public readonly IHotelService _hotelService;

        public HomeController(IUnitOfWork unitofwork, SanProjectDBContext context,
            IUsersService userservice, IAuthenticationService authenticationservice,
            ICitySearchService citysearchservice, IHotelService hotelService)
        {
            _unitofwork = unitofwork;
            _context = context;
            _userservice = userservice;
            _authenticationservice = authenticationservice;
            _citysearchservice = citysearchservice;
            _hotelService = hotelService;
        }

        public IActionResult Index(string id)
        {
            if (id!="" && id!=null)
            {
                AllHotelQueryDTO t = new AllHotelQueryDTO();
                t.LocationId = id;
                TempData["city"]= t;
                return View(t);
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Index(string quer, string tst)
        {
            if ((quer == null || quer == "") || quer.Length < 3)
            {
                return BadRequest("The City field requires at least 3 characters");
            }
            List<CityObject> t = await _citysearchservice.Search(quer);

            AllHotelQueryDTO hotels = new AllHotelQueryDTO
            {
                LocationId = quer
            };
            List<HotelDetailDTO> detailDTOs= await _hotelService.GetAllDetails(hotels);
            var list = new CityHotelVM() 
            {
                query= hotels,
                cityObjectList=t
            };
            //ViewBag.hotels = hotels;
            //List<HotelDetailDTO> tlist = await _hotelService.GetAllDetails(hotels);
            //ViewBag.hotels = tlist;
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> Cities(string quer)
        {
            if ((quer == null || quer == "") || quer.Length < 3)
            {
                return BadRequest("The City field requires at least 3 characters");
            }
            List<CityObject> t = await _citysearchservice.Search(quer);

            return View(t);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> CityQuery(string quer)
        {
            /*
            string t = _authenticationservice.Login().Result;
            Token to=new Token();
            to.token=t;*/
            //var t=await _authenticationservice.Login();
            //string quer = HttpContext.Request.Form["citysearch"];
            List<CityObject> t = await _citysearchservice.Search(quer);

            //return RedirectToAction("Cities", t);
            return Ok();
        }


        //[HttpPost]
        //public async Task<JsonResult> AutoComplete(string prefix)
        //{
        //    var customers = await _citysearchservice.Search(prefix); 


        //    return Json(customers);
        //}

    }
}
