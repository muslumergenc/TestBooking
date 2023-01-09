using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SanProject.Application.Models;
using SanProject.Application.Services.Interfaces;
using SanProject.Data;
using SanProject.Domain;
using SanProject.Domain.City;
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
    public class CitySearchService : ICitySearchService
    {
        public readonly IUnitOfWork _unitofwork;
        public readonly IEmailService _emailService;
        public string tokne { get; set; }
        private readonly ILogger<UsersService> _logger;
        public readonly IAuthenticationService _authenticationService;
        public readonly IHotelService _hotelservice;
        public CitySearchService(IUnitOfWork unitofwork, IEmailService emailservice,
            ILogger<UsersService> logger, IAuthenticationService authenticationService, IHotelService hotelservice)
        {
            _unitofwork = unitofwork;
            _emailService = emailservice; ;
            _logger = logger;
            _authenticationService = authenticationService;
            _hotelservice = hotelservice;

        }

        public async Task<List<CityObject>> Search(string querys)
        {
            if (tokne == null)
            {
                tokne = await _authenticationService.Login();
            }
            var searchobj = new
            {
                ProductType = "2",
                Query = querys,
                Culture = "en-US"
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokne);
            var content = JsonConvert.SerializeObject(searchobj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            var req = await client.PostAsync("http://service.stage.paximum.com/v2/api/productservice/getarrivalautocomplete", byteContent);
            var contents = await req.Content.ReadAsStringAsync();
            Root obj = JsonConvert.DeserializeObject<Root>(contents);
            List<Item> t = obj.body.items;
            //List<FullCity.Item>conten=new List<FullCity.Item>();
            List<CityObject> conten = new List<CityObject>();
            if (t.Count != 0)
            {
                for (int i = 0; i < t.Count; i++)
                {
                    if (t[i].country.name == "Turkey" && !(t[i].hotelCount == 0 || t[i].hotelCount == null))
                    {
                        CityObject c = new CityObject(); c.Name = t[i].city.name; c.HotelCount = t[i].hotelCount;
                        c.Id = t[i].city.id;
                        if (!conten.Contains(c))
                        {
                            conten.Add(c);
                        }

                    }
                }
            }
            _logger.LogInformation("City results for the query " + querys + " has been fetched");
            return conten;
        }
        //there is a bit of redundancy but is needed at the moment
        public async Task<List<HotelObject>> CityHotelSearch(string querys)
        {
            if (tokne == null)
            {
                tokne = await _authenticationService.Login();
            }
            var searchobj = new
            {
                ProductType = "2",
                Query = querys,
                Culture = "en-US"
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokne);
            var content = JsonConvert.SerializeObject(searchobj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            var req = await client.PostAsync("http://service.stage.paximum.com/v2/api/productservice/getarrivalautocomplete", byteContent);
            var contents = await req.Content.ReadAsStringAsync();
            Root obj = JsonConvert.DeserializeObject<Root>(contents);
            List<Item> t = obj.body.items;
            List<HotelObject> conten = new List<HotelObject>();
            if (t.Count != 0)
            {
                for (int i = 0; i < t.Count; i++)
                {
                    if ((t[i].hotelCount == 0 || t[i].hotelCount == null) && t[i].city.name == querys)
                    {
                        HotelObject c = new HotelObject(); c.HotelName = t[i].hotel.name; c.HotelId = t[i].hotel.id;
                        //var ch = await _hotelservice.GetDetailsTest(c.HotelId, 1);
                        //if (ch !=null)
                        //{
                        if (!conten.Contains(c))
                        {
                            conten.Add(c);
                        }
                        //}


                    }
                }
            }
            _logger.LogInformation("Hotel results for the city " + querys + " has been fetched");
            return conten;

        }

        public async Task<List<CityObject>> SearchAll()
        {
            if (tokne == null)
            {
                tokne = await _authenticationService.Login();
            }
            var searchobj = new
            {
                ProductType = "2",
                Culture = "en-US"
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokne);
            var content = JsonConvert.SerializeObject(searchobj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            var req = await client.PostAsync("http://service.stage.paximum.com/v2/api/productservice/getarrivalautocomplete", byteContent);
            var contents = await req.Content.ReadAsStringAsync();
            Root obj = JsonConvert.DeserializeObject<Root>(contents);
            List<Item> t = obj.body.items;
            //List<FullCity.Item>conten=new List<FullCity.Item>();
            List<CityObject> conten = new List<CityObject>();
            if (t.Count != 0)
            {
                for (int i = 0; i < t.Count; i++)
                {
                    if (t[i].country.name == "Turkey" && !(t[i].hotelCount == 0 || t[i].hotelCount == null))
                    {
                        CityObject c = new CityObject(); c.Name = t[i].city.name; c.HotelCount = t[i].hotelCount;
                        c.Id = t[i].city.id;
                        if (!conten.Contains(c))
                        {
                            conten.Add(c);
                        }

                    }
                }
            }
            _logger.LogInformation("Hotel results for the city has been fetched");
            return conten;
        }
    }
}
