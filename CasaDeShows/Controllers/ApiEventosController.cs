using System;
using System.Linq;
using CasaDeShows.Data;
using Microsoft.AspNetCore.Mvc;
using CasaDeShows.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace CasaDeShows.Controllers
{
    [Route("api/v1/eventos")]
    [ApiController]
    public class ApiEventosController : ControllerBase
    {
        readonly ApplicationDbContext _context;

        public ApiEventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recupera todos os eventos
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.ToList();
            var aux = eventos.Count();

            if (aux > 0)
            {
                return Ok(new { eventos });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhum evento cadastrado" });
            }

        }

        /// <summary>
        /// Recupera os eventos pela capacidade (crescente) 
        /// </summary>
        /// <remarks>
        /// observação: capacidade está com o nome de ingressos
        /// </remarks>
        [HttpGet("capacidade/asc")]
        public IActionResult GetCapAsc()
        {
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderBy(eve => eve.Ingressos).ToList();
            var aux = eventos.Count();

            if (aux > 0)
            {
                return Ok(new { eventos });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhum evento cadastrado" });
            }
        }

        /// <summary>
        /// Recupera os eventos pela capacidade (decrescente) 
        /// </summary>
        /// <remarks>
        /// observação: capacidade está com o nome de ingressos
        /// </remarks>
        [HttpGet("capacidade/desc")]
        public IActionResult GetCapDesc()
        {
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderByDescending(eve => eve.Ingressos).ToList();
            var aux = eventos.Count();

            if (aux > 0)
            {
                return Ok(new { eventos });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhum evento cadastrado" });
            }
        }

        /// <summary>
        /// Recupera os eventos pela data (crescente) 
        /// </summary>
        [HttpGet("data/asc")]
        public IActionResult GetDataAsc()
        {
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderBy(eve => eve.Data).ToList();
            var aux = eventos.Count();

            if (aux > 0)
            {
                return Ok(new { eventos });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhum evento cadastrado" });
            }
        }

        /// <summary>
        /// Recupera os eventos pela data (decrescente) 
        /// </summary>
        [HttpGet("data/desc")]
        public IActionResult GetDataDesc()
        {
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderByDescending(eve => eve.Data).ToList();
            var aux = eventos.Count();

            if (aux > 0)
            {
                return Ok(new { eventos });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhum evento cadastrado" });
            }
        }

        /// <summary>
        /// Recupera os eventos pelo nome (crescente) 
        /// </summary>
        [HttpGet("nome/asc")]
        public IActionResult GetNomeAsc()
        {
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderBy(nome => nome.Nome);
            var aux = eventos.Count();

            if (aux > 0)
            {
                return Ok(new { eventos });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhum evento cadastrado" });
            }
        }

        /// <summary>
        /// Recupera os eventos pelo nome (decrescente) 
        /// </summary>
        [HttpGet("nome/desc")]
        public IActionResult GetNomedesc()
        {
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderByDescending(nome => nome.Nome);
            var aux = eventos.Count();

            if (aux > 0)
            {
                return Ok(new { eventos });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhum evento cadastrado" });
            }
        }

        /// <summary>
        /// Recupera os eventos pelo preço (crescente) 
        /// </summary>
        [HttpGet("preco/asc")]
        public IActionResult GetPrecoAsc()
        {
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderBy(pr => pr.Preco);
            var aux = eventos.Count();

            if (aux > 0)
            {
                return Ok(new { eventos });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhum evento cadastrado" });
            }
        }

        /// <summary>
        /// Recupera os eventos pelo preço (decrescente) 
        /// </summary>
        [HttpGet("preco/desc")]
        public IActionResult GetPrecodesc()
        {
            var casas = _context.casasDeShow.ToList();
            var eventos = _context.Eventos.OrderByDescending(pr => pr.Preco);
            var aux = eventos.Count();

            if (aux > 0)
            {
                return Ok(new { eventos });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhum evento cadastrado" });
            }
        }

        /// <summary>
        /// Recupera um evento pelo id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var evento = _context.Eventos.First(p => p.Id == id);
                var casa = _context.casasDeShow.ToList();
                return Ok(evento);
            }
            catch (System.Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "id invalido" });
            }
        }


        /// <summary>
        /// Cria um novo evento.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] Eventotemp eventos)
        {
            try
            {
                if (eventos.Nome == null || eventos.Nome.Length < 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O nome do evento precisa conter mais de 1 caracter." });
                }

                if (eventos.Ingressos < 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "A quantidade de ingressos precisa ser superior a 0." });
                }

                if (eventos.Preco < 1)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O preço precisa ser superior a 0." });
                }

                if (eventos.Genero == 0 || eventos.Genero > 16)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "digite um genero entre 1 e 16 para ser válido." });
                }

                if (eventos.Data == null)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "digite uma data válida." });
                }


                Eventos c = new Eventos();
                c.Nome = eventos.Nome;
                c.Preco = eventos.Preco;
                c.CasaDeShows = _context.casasDeShow.First(cs => cs.Id == eventos.CasaDeShowId);

                if (c.CasaDeShows == null || c.CasaDeShows.Id == 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "Id inválido" });
                }

                c.Data = eventos.Data;
                c.Genero = eventos.Genero;
                c.Ingressos = eventos.Ingressos;
                _context.Eventos.Add(c);
                _context.SaveChanges();



                Response.StatusCode = 201;
                return new ObjectResult("");

            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "não envie um corpo vazio." });
            }
        }

        /// <summary>
        /// Deleta um evento.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
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
                return new ObjectResult(new { msg = "id inválido." });
            }
        }

        /// <summary>
        /// Altera um evento
        /// </summary>
        [HttpPatch]
        public IActionResult Patch([FromBody] EventoParaAtualizar evento)
        {
        try{
            if (evento.Id > 0)
            {
                try
                {
                    var p = _context.Eventos.First(ptemp => ptemp.Id == evento.Id);
                    var casas = _context.casasDeShow.ToList();

                    if (p != null)
                    {
                        //editar 
                        //operador ternario: condição ? faz algo : faz outra coisa
                        p.Nome = evento.Nome != null || evento.Nome.Length != 0 ? evento.Nome : p.Nome;
                        

                        p.Preco = evento.Preco != 0 ? evento.Preco : p.Preco;
                        

                        p.Data = evento.Data != null ? evento.Data : p.Data;
                        

                        p.Genero = evento.Genero != 0 || evento.Genero <= 16 ? evento.Genero : p.Genero;
                        

                        p.Imagem = evento.Imagem != null ? evento.Imagem : p.Imagem;
                        

                        p.Ingressos = evento.Ingressos != 0 ? evento.Ingressos : p.Ingressos;
                        
                        p.CasaDeShows.Id = evento.CasaDeShowId != 0 ? evento.CasaDeShowId : p.CasaDeShows.Id;
                        
                        if(p.CasaDeShows.Id == 0){
                            Response.StatusCode = 400;
                            return new ObjectResult(new { msg = "casa de show encontrada." });
                        }


                        _context.SaveChanges();
                        return Ok();

                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "evento não encontrado" });
                    }
                }
                catch (Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "evento não encontrado" });
                }

            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "O Id do evento é invalido" });
            }
        } catch(Exception){
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "corpo vazio" });
            }
        }


        public class Eventotemp
        {
            public string Nome { get; set; }

            public int CasaDeShowId { get; set; }

            public double Preco { get; set; }

            public int Genero { get; set; }

            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime Data { get; set; }

            public int Ingressos { get; set; }

            public string Imagem { get; set; }
        }

        public class EventoParaAtualizar
        {
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