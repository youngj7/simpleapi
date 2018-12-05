using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> _users { get; set; }

        public UsersController()
        {
            if (_users == null)
            {
                _users = new List<User>();

                var name = Environment.GetEnvironmentVariable("SIMPLEAPI_DEFAULT_USERNAME");
                if (name == null)
                    name = "Default User";

                _users.Add(new User(){ID = 1, Username = name});

                Console.WriteLine(string.Format("SIMPLEAPI_DEFAULT_USERNAME = {0}", name));
            }
        }

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _users;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            foreach(User user in _users)
            {
                if (user.ID == id)
                    return user;
            }

            return null;
        }

        // POST api/users
        [HttpPost]
        public ActionResult<User> Post([FromBody] User value)
        {
            if (value == null) return null;

            foreach(User user in _users)
            {
                if (user.ID == value.ID)
                    return null;
            }

            _users.Add(value);

            return value;
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            if (value == null) return;

            foreach(User user in _users)
            {
                if (user.ID == value.ID)
                {
                    user.Username = value.Username;
                }
            }
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id < 1) return;

            for(int i=_users.Count - 1; i >= 0; i++)
            {
                if (_users[i].ID == id)
                {
                    _users.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
