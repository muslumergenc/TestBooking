using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SanProject.Application.Models;
using SanProject.Application.Services.Interfaces;
using SanProject.Data;
using SanProject.Domain;
using SanProject.Domain.Hotel;
using SanProject.Shared.HotelModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SanProject.Application.Services
{
    public class HotelService : IHotelService
    {

        public readonly IUnitOfWork _unitofwork;
        public readonly IEmailService _emailService;
        public string tokne { get; set; }
        private readonly ILogger<UsersService> _logger;
        public readonly IAuthenticationService _authenticationService;
        public HotelService(IUnitOfWork unitofwork, IEmailService emailservice,
            ILogger<UsersService> logger, IAuthenticationService authenticationService)
        {
            _unitofwork = unitofwork;
            _emailService = emailservice; ;
            _logger = logger;
            _authenticationService = authenticationService;

        }
        //for get /pricesearch
        public async Task<HotelDetailDTO> GetDetails(string querys, int adultnum, string checkinstr)
        {
            if (tokne == null)
            {
                tokne = await _authenticationService.Login();
            }
            RootQ searchobj = new RootQ();
            searchobj.Products.Add(querys); searchobj.roomCriteria[0].adult = adultnum; searchobj.checkIn = checkinstr;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokne);
            var content = JsonConvert.SerializeObject(searchobj);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            var req = await client.PostAsync("http://service.stage.paximum.com/v2/api/productservice/pricesearch", byteContent);
            var contents = await req.Content.ReadAsStringAsync();
            Root obj = JsonConvert.DeserializeObject<Root>(contents);
            HotelDetailDTO t = new HotelDetailDTO();
            Hotel hotelobj=new Hotel();
            if (obj.body == null)
            {
                return null;
            }
            hotelobj = obj.body.hotels[0];

            t.name = hotelobj.name;
            t.description = hotelobj.description; 
            t.price = hotelobj.offers[0].price;
            t.rating = Math.Round(hotelobj.rating,1);
            t.rooms = hotelobj.offers[0].rooms;
            t.locationName = hotelobj.location.name;
            if (hotelobj.offers[0].cancellationPolicies != null)
            {
                t.cancellationDueDate = hotelobj.offers[0].cancellationPolicies[0].dueDate;
                t.cancellationPrice = hotelobj.offers[0].cancellationPolicies[0].price.amount;
                t.cancellationCurrency = hotelobj.offers[0].cancellationPolicies[0].price.currency;
            }
            t.offerId = hotelobj.offers[0].offerId;
            t.offerCheckIn = hotelobj.offers[0].checkIn;
            t.address = hotelobj.address;
            t.hotelCategory = hotelobj.hotelCategory;
            t.thumbnail = hotelobj.thumbnailFull;
            t.travellernum = hotelobj.offers[0].rooms[0].travellers.Count;
            t.facilities = hotelobj.facilities;
            t.thumbnailFull= hotelobj.thumbnailFull;
            return t;
        }
        public async Task<List<HotelDetailDTO>> GetAllDetails(AllHotelQueryDTO qu)
        {
            if (tokne == null)
            {
                tokne = await _authenticationService.Login();
            }

            EarlyRoot searchobj = new EarlyRoot();
            var st =  new { id = qu.LocationId, type = 2 } ;
            searchobj.roomCriteria[0].adult = qu.NumberOfTravellers; searchobj.arrivalLocations[0]=st;
            searchobj.checkIn=qu.ChcekIn.ToString("yyyy-MM-dd");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokne);
            var content = JsonConvert.SerializeObject(searchobj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            var req = await client.PostAsync("http://service.stage.paximum.com/v2/api/productservice/pricesearch", byteContent);
            var contents = await req.Content.ReadAsStringAsync();
            Root obj = JsonConvert.DeserializeObject<Root>(contents);
            if (obj.body == null)
            {
                return null;
            }
            List<HotelDetailDTO> tt= new List<HotelDetailDTO>();
            List<Hotel> hotelobj = new List<Hotel>();
            //try
            if (obj.body == null)
            {
                return null;
            }
            //{
            hotelobj = obj.body.hotels;
            //}
            for(int i=0; i < hotelobj.Count; i++)
            {
                HotelDetailDTO t = new HotelDetailDTO();
                t.HotelId = hotelobj[i].id;
                t.name = hotelobj[i].name;
                t.description = hotelobj[i].description;
                t.price = hotelobj[i].offers[0].price;
                t.rating = Math.Round(hotelobj[i].rating, 1);
                t.rooms = hotelobj[i].offers[0].rooms;
                t.locationName = hotelobj[i].location.name;
                if (hotelobj[i].offers[0].cancellationPolicies != null)
                {
                    t.cancellationDueDate = hotelobj[i].offers[0].cancellationPolicies[0].dueDate;
                    t.cancellationPrice = hotelobj[i].offers[0].cancellationPolicies[0].price.amount;
                    t.cancellationCurrency = hotelobj[i].offers[0].cancellationPolicies[0].price.currency;
                }

                t.offerId = hotelobj[i].offers[0].offerId;
                t.offerCheckIn = hotelobj[i].offers[0].checkIn;
                t.address = hotelobj[i].address;
                t.hotelCategory = hotelobj[i].hotelCategory;
                t.thumbnail = hotelobj[i].thumbnailFull;
                t.travellernum = hotelobj[i].offers[0].rooms[0].travellers.Count;
                //t.facility = hotelobj[i].facilitiesCategory;
                tt.Add(t);
            }

            return tt;



        }


        public async Task<HotelDetailDTO> GetDetailsTest(string querys, int adultnum)
        {
            if (tokne == null)
            {
                tokne = await _authenticationService.Login();
            }
            
            RootQ searchobj = new RootQ();
            searchobj.Products.Add(querys); searchobj.roomCriteria[0].adult = adultnum;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokne);
            var content = JsonConvert.SerializeObject(searchobj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            var req = await client.PostAsync("http://service.stage.paximum.com/v2/api/productservice/pricesearch", byteContent);
            var contents = await req.Content.ReadAsStringAsync();
            Root obj = JsonConvert.DeserializeObject<Root>(contents);
            HotelDetailDTO t = new HotelDetailDTO();
            Hotel hotelobj = new Hotel();
            //try
            if (obj.body == null)
            {
                return null;
            }
            else
            {
                return new HotelDetailDTO();
            }
        }

    }
}
