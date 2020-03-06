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

        /// <summary>
        /// lista as compras.
        /// </summary>
        [HttpGet]
        public IActionResult Get(){
            var compras = _context.Compras.ToList();
            var casa = _context.casasDeShow.ToList();
            return Ok(new{compras});
        }

        /// <summary>
        /// localiza uma compra pelo id.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            try
            {
                var compras = _context.Compras.First(compra => compra.Id == id);
                var casa = _context.casasDeShow.ToList();
                return Ok(new {compras});
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "Id não encontrado"});
            }
        }
    }
}