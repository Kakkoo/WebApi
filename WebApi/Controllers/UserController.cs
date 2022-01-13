using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private NestleDbContext _nestleDbContext;

        public UserController(NestleDbContext nestleDbContext)
        {
            _nestleDbContext = nestleDbContext;
        }

        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            try
            {
                var users = _nestleDbContext.tblUsers.ToList();
                if (users.Count == 0)
                    return StatusCode(404, "No user found!");

                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] User user)
        {
            tblUsers usert = new tblUsers();
            usert.UserName = user.UserName;
            usert.FirstName = user.FirstName;
            usert.LastName = user.LastName;
            usert.City = user.City;
            usert.State = user.State;
            usert.Country = user.Country;

            try
            {
                _nestleDbContext.tblUsers.Add(usert);
                _nestleDbContext.SaveChanges();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
            // get all users
            var users = _nestleDbContext.tblUsers.ToList();
            return Ok(users);
        }

        [HttpPut("UpdateUser")]
        public IActionResult Update([FromBody] User request)
        {
            try
            {
                var usert = _nestleDbContext.tblUsers.FirstOrDefault(x => x.Id == request.Id);
                if (usert == null)
                    return StatusCode(404, "User NOT Found!");

                usert.UserName = request.UserName;
                usert.FirstName = request.FirstName;
                usert.LastName = request.LastName;
                usert.City = request.City;
                usert.State = request.State;
                usert.Country = request.Country;

                _nestleDbContext.Entry(usert).State = EntityState.Modified;
                _nestleDbContext.SaveChanges();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

            // get all users
            var users = _nestleDbContext.tblUsers.ToList();
            return Ok(users);
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            try
            {
                var usert = _nestleDbContext.tblUsers.FirstOrDefault(x => x.Id == id);
                if (usert == null)
                    return StatusCode(404, "User NOT Found!");

                _nestleDbContext.Entry(usert).State = EntityState.Deleted;
                _nestleDbContext.SaveChanges();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message); ;
            }
            // get all users
            var users = _nestleDbContext.tblUsers.ToList();
            return Ok(users);
        }
    }
}
