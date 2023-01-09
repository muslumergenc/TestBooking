using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SanProject.Application.Services;
using SanProject.Application.Services.Interfaces;
using SanProject.Data;
using SanProject.Domain;
using SanProject.Web.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SanProject.Web.Controllers
{
    public class UsersController : Controller
    {

        public SanProjectDBContext _context;
        public readonly IUnitOfWork _unitofwork;
        public readonly IUsersService _userservice;

        public UsersController(IUnitOfWork unitofwork, SanProjectDBContext context, IUsersService userservice)
        {
            _unitofwork = unitofwork;
            _context = context;
            _userservice = userservice;
        }
        public IActionResult Index()
        {
            var users = _unitofwork.UsersRepository.GetUsers();
            return View(users);
        }

        public IActionResult Edit(int id)
        {
            User us = _unitofwork.UsersRepository.FindUser(id);
            if(us!=null)
                return View(us);
            return BadRequest("User is not found");
        }
        [HttpPost]
        public IActionResult Edit(User us)
        {
            
            if (us != null)
                _userservice.EditUser(us);
                return RedirectToAction("Index");
            return BadRequest("User is not found");
        }
        
        //[HttpDelete]
        public IActionResult SoftDelete(int id)
        {
            User user=_unitofwork.UsersRepository.FindUser(id);
               
                //user.IsDeleted = true;
                //_context.SaveChanges();
                _userservice.SoftDelete(user);
                return RedirectToAction("Index");
            
            
        }
        public IActionResult Delete(int id)
        {
            //User user = _unitofwork.UsersRepository.FindUser(id);

            //user.IsDeleted = true;
            //_context.SaveChanges();
            _userservice.DeleteUser(id);
            return RedirectToAction("Index");
        }
        public IActionResult Registration()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            
           await _userservice.AddUser(user);

           return RedirectToAction("Index");
          
        }





        [HttpPut]
        public IActionResult ActivateUser(int id)
        {
            if (ModelState.IsValid)
            {
                //bool stat = user.IsActive;
                //user.IsActive = !stat;
                //_context.SaveChanges();
                _userservice.ActivateUser(id);
                return RedirectToAction("Index");
            }
            
            return BadRequest();
            
        }


        //might be placed onto some other Page, currently deprecated
        [HttpPost]
        public IActionResult Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                //string jsonuser = JsonConvert.SerializeObject(myReturnData);
                var client = new HttpClient();
                //var req = client.PostAsync("http://service.stage.paximum.com/v2", user);

            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
