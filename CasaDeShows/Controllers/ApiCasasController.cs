using System;
using System.Linq;
using CasaDeShows.Data;
using Microsoft.AspNetCore.Mvc;
using CasaDeShows.Models;

namespace CasaDeShows.Models
{

    [Route("api/v1/casas")]
    [ApiController]
    public class ApiCasasController : ControllerBase
    {
        readonly ApplicationDbContext _context;

        public ApiCasasController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recupera todas as casas
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            var casas = _context.casasDeShow.ToList();
            var aux = casas.Count();
            if (aux > 0)
            {
                return Ok(new { casas });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhuma casa de show cadastrada." });
            }
        }

        /// <summary>
        /// Recupera as casas de show em ordem alfabética (crescente) 
        /// </summary>
        [HttpGet("asc")]
        public IActionResult GetAsc()
        {
            var casas = _context.casasDeShow.OrderBy(cs => cs.Nome).ToList();
            var aux = casas.Count();
            if (aux > 0)
            {
                return Ok(new { casas });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhuma casa de show cadastrada." });
            }
        }

        /// <summary>
        /// Recupera as casas de show em ordem alfabética (decrescente) 
        /// </summary>
        [HttpGet("desc")]
        public IActionResult GetDesc()
        {
            var casas = _context.casasDeShow.OrderByDescending(cs => cs.Nome).ToList();
            var aux = casas.Count();
            if (aux > 0)
            {
                return Ok(new { casas });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhuma casa de show cadastrada." });
            }
        }

        /// <summary>
        /// Recupera uma casa de show pelo id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var casa = _context.casasDeShow.First(p => p.Id == id);
                return Ok(casa);
            }
            catch (System.Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "id inválido." });
            }
        }

        /// <summary>
        /// Recupera as casas de show pelo nome
        /// </summary>
        [HttpGet("nome/{nome}")]
        public IActionResult GetNomeAsc(string nome)
        {
            var casas = _context.casasDeShow.Where(cs => cs.Nome == nome);
            var aux = casas.Count();
            if (aux > 0)
            {
                return Ok(new { casas });
            }
            else
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "nenhuma casa de show encontrada" });
            }
        }

        /// <summary>
        /// Cria uma nova casa de show.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] casaTemp CasaTemp)
        {
            try
            {
                if (CasaTemp.Nome == null || CasaTemp.Nome.Length <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O nome da casa precisa conter mais de 1 caracter." });
                }

                if (CasaTemp.Local == null || CasaTemp.Local.Length <= 0)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "O nome do local precisa conter mais de 1 caracter." });
                }

                CasasDeShow c = new CasasDeShow();
                c.Nome = CasaTemp.Nome;
                c.Local = CasaTemp.Local;
                _context.casasDeShow.Add(c);
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
        /// Deleta uma casa de show
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var casa = _context.casasDeShow.First(p => p.Id == id);
                _context.casasDeShow.Remove(casa);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new { msg = "id inválido" });
            }
        }


        /// <summary>
        /// Altera uma casa de show 
        /// </summary>

        [HttpPatch]
        public IActionResult Patch([FromBody] casaParaAtualizar casa)
        {
        try{
            if (casa.Id > 0)
            {
                try
                {
                    var p = _context.casasDeShow.First(ptemp => ptemp.Id == casa.Id);

                    if (p != null)
                    {

                        //editar 
                        //operador ternario: condição ? faz algo : faz outra coisa
                        p.Nome = casa.Nome != null || casa.Nome.Length > 0 ? casa.Nome : p.Nome;
                        p.Local = casa.Local != null || casa.Local.Length > 0 ? casa.Local : p.Local;

                        _context.SaveChanges();
                        return Ok();

                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "preencha todos os campos" });
                    }
                }
                catch (Exception)
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "preencha todos os campos corretamente." });
                }

            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "O Id da casa é invalido" });
            }
        } catch (Exception){
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "corpo vazio" });
            }
        }


        public class casaTemp
        {
            public string Nome { get; set; }
            public string Local { get; set; }
        }

        public class casaParaAtualizar
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Local { get; set; }
        }
    }
}