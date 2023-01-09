using SanProject.Application.Models;
using SanProject.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanProject.Application.Services
{
    public class FakeEmailService : IEmailService
    {
        public Task SendEmailAsync(MailRequest mailrequest)
        {
            Console.WriteLine("Email is ready");
            Console.WriteLine($"Address is {mailrequest.ToEmail}, subject is {mailrequest.Subject}");
            return Task.CompletedTask;
        }
    }
}
