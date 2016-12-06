using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.ServerRepository;
using Repository.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    public class RecommendController : Controller
    {
        private IRecommendRepository recommendRepo;

        public RecommendController(IRecommendRepository recommendRepo)
        {
            this.recommendRepo = recommendRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Recommend> Get()
        {
            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<Recommend> Get(int id)
        {
            return recommendRepo.GetListByID(id).Select(o => new Recommend(o));
        }
        [HttpGet("count/{id}")]
        public int GetCount(int id)
        {
            return recommendRepo.CountRecommend(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]IEnumerable<Recommend> value)
        {
            return recommendRepo.UpdateList(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
