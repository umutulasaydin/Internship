using BaseRequest.BaseRequest;
using BaseRequest.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController<T,V> : Controller 
    {
        private BaseRequest<T, V> _request;
        private string _clientName;

        public TemplateController(BaseRequest<T,V> request, string clientName)
        {
            _request = request;
            _clientName = clientName;
        }
        // GET: api/<GenericController>
        [HttpGet]
        public T Get()
        {
            return _request.GetAll(_clientName);
        }

        // GET api/<GenericController>/5
        [HttpGet("{id}")]
        public T Get(int id)
        {
            return _request.GetById(id, _clientName);
        }

        // POST api/<GenericController>
        [HttpPost]
        public T Post([FromBody] V value)
        {
            return _request.Insert(value, _clientName);
        }

      

        // DELETE api/<GenericController>/5
        [HttpDelete("{id}")]
        public T Delete(int id)
        {
            return _request.Delete(id, _clientName);
        }
    }
}
