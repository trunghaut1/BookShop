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
    public class CustomerController : Controller
    {
        ICustomerRepository _cusRepo;

        public CustomerController(ICustomerRepository cusRepo)
        {
            _cusRepo = cusRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _cusRepo.SelectAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _cusRepo.SelectByID(id);
        }

        [HttpGet("email/{email}")]
        public Customer Get(string email)
        {
            return _cusRepo.SelectByEmail(email);
        }

        // POST api/values
        [HttpPost]
        public Customer Post([FromBody]Customer value)
        {
            if (_cusRepo.Add(value)) return value;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Customer Put(int id, [FromBody]Customer value)
        {
            if (_cusRepo.Update(value)) return value;
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _cusRepo.Delete(id);
        }
    }
}
