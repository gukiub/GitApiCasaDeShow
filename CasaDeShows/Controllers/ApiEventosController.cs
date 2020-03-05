using System;
using System.Linq;
using CasaDeShows.Data;
using Microsoft.AspNetCore.Mvc;
using CasaDeShows.Models;
using System.ComponentModel.DataAnnotations;

namespace CasaDeShows.Controllers
{
    [Route("api/v1/eventos")]
    [ApiController]
    public class ApiEventosController : ControllerBase
    {
        readonly ApplicationDbContext _context;

        public ApiEventosController(ApplicationDbContext context){
            _context = context;
        }

        /// <summary>
        /// Recupera todos os eventos
        /// </summary>
        [HttpGet]
        public IActionResult Get(){
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.ToList();
            return Ok(new{eventos});
        }

        /// <summary>
        /// Recupera os eventos em ordem alfabética pela capacidade (crescente) 
        /// </summary>
        /// <remarks>
        /// observação: capacidade está com o nome de ingressos
        /// </remarks>
        [HttpGet("capacidade/asc")]
        public IActionResult GetCapAsc(){
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderBy(eve => eve.Ingressos).ToList();
            return Ok(new{eventos});
        }

        /// <summary>
        /// Recupera os eventos em ordem alfabética pela capacidade (decrescente) 
        /// </summary>
        /// <remarks>
        /// observação: capacidade está com o nome de ingressos
        /// </remarks>
        [HttpGet("capacidade/desc")]
        public IActionResult GetCapDesc(){
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderByDescending(eve => eve.Ingressos).ToList();
            return Ok(new{eventos});
        }
        
        /// <remarks>
        /// Recupera os eventos em ordem alfabética (crescente) 
        /// </remarks>
        [HttpGet("data/asc")]
        public IActionResult GetDataAsc(){
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderBy(eve => eve.Data).ToList();
            return Ok(new{eventos});
        }

        [HttpGet("data/desc")]
        public IActionResult GetDataDesc(){
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderByDescending(eve => eve.Data).ToList();
            return Ok(new{eventos});
        }
        

        [HttpGet("nome/asc")]
        public IActionResult GetNomeAsc(string nome){
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.Where(eve => eve.Nome == nome).OrderBy(nome => nome.Nome);
            return Ok(new{eventos});
        }

        [HttpGet("nome/desc")]
        public IActionResult GetNomedesc(string nome){
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.Where(eve => eve.Nome == nome).OrderByDescending(nome => nome.Nome);
            return Ok(new{eventos});
        }

        [HttpGet("preco/asc")]
        public IActionResult GetPrecoAsc(){
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderBy(pr => pr.Preco);
            return Ok(new{eventos});
        }

        [HttpGet("preco/desc")]
        public IActionResult GetPrecodesc(){
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderByDescending(pr => pr.Preco);
            return Ok(new{eventos});
        }

        /// <summary>
        /// Recupera uma casa de show
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            try
            {
                var evento = _context.Eventos.First(p => p.Id == id);
                var casa = _context.casasDeShow.ToList(); 
                return Ok(evento);
            }
            catch (System.Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }


        /// <summary>
        /// Cria uma nova casa de show.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] Eventotemp eventos){
            if(eventos.Nome == null){
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O nome do evento precisa conter mais de 1 caracter."});
            }

            if(eventos.Ingressos < 1){
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "A quantidade de ingressos precisa ser superior a 0."});
            }
            
            Eventos c = new Eventos();
            c.Nome = eventos.Nome;
            c.Preco = eventos.Preco;
            c.CasaDeShows = _context.casasDeShow.First(cs => cs.Id == eventos.CasaDeShowId);
            c.Data = eventos.Data;
            c.Genero = eventos.Genero;
            c.Ingressos = eventos.Ingressos; 
            _context.Eventos.Add(c);
            _context.SaveChanges();

            Response.StatusCode = 201;
            return new ObjectResult("");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            try
            {
                var evento = _context.Eventos.First(p => p.Id == id);
                _context.Eventos.Remove(evento);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }

        /// <summary>
        /// Altera uma casa de show
        /// </summary>
        [HttpPatch]
        public IActionResult Patch([FromBody] EventoParaAtualizar evento){
            if (evento.Id > 0){
                try
                {
                    var p = _context.Eventos.First(ptemp => ptemp.Id == evento.Id);
                    var casas = _context.casasDeShow.ToList();

                    if(p != null){
                        //editar 
                        //operador ternario: condição ? faz algo : faz outra coisa
                        p.Nome = evento.Nome != null ? evento.Nome : p.Nome;
                        p.Preco = evento.Preco != 0 ? evento.Preco : p.Preco;
                        p.Data = evento.Data != null ? evento.Data : p.Data;
                        p.Genero = evento.Genero != 0 ? evento.Genero : p.Genero;
                        p.Imagem = evento.Imagem != null ? evento.Imagem : p.Imagem;

                        _context.SaveChanges();
                        return Ok();

                    } else {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "evento não encontrado"});
                    }
                }
                catch (Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new {msg = "evento não encontrado"});
                }
                
            }else {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O Id do evento é invalido"});
            }
        }


        public class Eventotemp{
            public string Nome { get; set; }
        
            public int CasaDeShowId { get; set; }
        
            public double Preco { get; set; }
        
            public int Genero { get; set; }
       
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime Data { get; set; }
        
            public int Ingressos { get; set; }

            public string Imagem { get; set; }
        }

        public class EventoParaAtualizar{
            public int Id { get; set; }

            public string Nome { get; set; }
        
            public int CasaDeShowId { get; set; }
        
            public double Preco { get; set; }
        
            public int Genero { get; set; }
       
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime Data { get; set; }
        
            public int Ingressos { get; set; }

            public string Imagem { get; set; }
        }
    }
}