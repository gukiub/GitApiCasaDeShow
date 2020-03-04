using System;
using System.Linq;
using CasaDeShows.Data;
using Microsoft.AspNetCore.Mvc;
using CasaDeShows.Models;
using System.ComponentModel.DataAnnotations;

namespace CasaDeShows.Controllers
{
    [Route("api/v1/vendas")]
    [ApiController]
    public class ApiVendasController : ControllerBase
    {
        readonly ApplicationDbContext _context;

        public ApiVendasController(ApplicationDbContext context){
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(){
            return Ok();
        }
    }
}