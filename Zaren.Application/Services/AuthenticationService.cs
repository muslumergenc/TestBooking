using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SanProject.Application.Models;
using SanProject.Application.Services.Interfaces;
using SanProject.Data;
using SanProject.Domain;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace SanProject.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly IUnitOfWork _unitofwork;
        public readonly IEmailService _emailService;
        private readonly ILogger<UsersService> _logger;
        public AuthenticationService(IUnitOfWork unitofwork, IEmailService emailservice, ILogger<UsersService> logger)
        {
            _unitofwork = unitofwork;
            _emailService = emailservice; ;
            _logger = logger;
        }

        public async Task<string> Login()
        {
            var user = new
            {
                Agency = "PXM25520",
                User = "USR1",
                Password = "zaren!23"
            };
            var client = new HttpClient();
            var content = JsonConvert.SerializeObject(user);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            var req =  client.PostAsync("http://service.stage.paximum.com/v2/api/authenticationservice/login", byteContent).Result;
            //var cont = JsonConvert.DeserializeObject<dynamic>(req);
            var contents = await req.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<dynamic>(contents);
            var t= obj.body.token;
            return t;

        }
    }
}
