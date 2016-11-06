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
    public class SubCatController : Controller
    {
        ISubCatRepository _subCatRepo;

        public SubCatController(ISubCatRepository subCatRepo)
        {
            _subCatRepo = subCatRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<SubCat> Get()
        {
            return _subCatRepo.SelectAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public SubCat Get(int id)
        {
            return _subCatRepo.SelectByID(id);
        }

        [HttpGet("cat/{id}")]
        public IEnumerable<SubCat> GetByCat(int id)
        {
            return _subCatRepo.SelectByCat(1);
        }

        // POST api/values
        [HttpPost]
        public SubCat Post([FromBody]SubCat value)
        {
            if (_subCatRepo.Add(value)) return value;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public SubCat Put(int id, [FromBody]SubCat value)
        {
            if (_subCatRepo.Update(value)) return value;
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _subCatRepo.Delete(id);
        }
    }
}