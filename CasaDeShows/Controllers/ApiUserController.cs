using System;
using System.Linq;
using CasaDeShows.Data;
using Microsoft.AspNetCore.Mvc;
using CasaDeShows.Models;
using System.ComponentModel.DataAnnotations;

namespace CasaDeShows.Controllers
{
    [Route("api/v1/Users")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        readonly ApplicationDbContext _context;

        public ApiUserController(ApplicationDbContext context){
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(){
            var user = _context.Users.Select(p => p.UserName).ToList();
            return Ok(new{user});
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username){
            try{
                var user = _context.Users.Select(
                    x => new UserTemp{id = x.Id, 
                    email = x.Email, 
                    username = x.UserName
                }).First(user => user.username == username);
                return Ok(new{user});
            }catch(Exception){
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Usuário não encontrado"});
            }
        }


        public class UserTemp{
            public string id{get;set;}
            public string email { get; set; }
            public string username { get; set; }
        }
    }
}