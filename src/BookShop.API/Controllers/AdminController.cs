﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        IAdminRepository _adminRepo;

        public AdminController(IAdminRepository adminRepo)
        {
            _adminRepo = adminRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Admin> Get()
        {
            return _adminRepo.SelectAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Admin Get(int id)
        {
            return _adminRepo.SelectByID(id);
        }

        [HttpGet("email/{email}")]
        public Admin Get(string email)
        {
            return _adminRepo.SelectByEmail(email);
        }

        // POST api/values
        [HttpPost]
        public Admin Post([FromBody]Admin value)
        {
            if (_adminRepo.Add(value)) return value;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Admin Put(int id, [FromBody]Admin value)
        {
            if (_adminRepo.Update(value)) return value;
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _adminRepo.Delete(id);
        }
    }
}