using CasaDeShows.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasaDeShows.Models;
using CasaDeShows.DTO;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CasaDeShows.Controllers
{
    public class UserController : Controller
    {
        readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(){
            var eventos = _context.Eventos.Include(cs => cs.CasaDeShows).ToList();
            return View(eventos);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Historico(){
            var historico = _context.Compras.Where(batata => batata.User.UserName == User.Identity.Name).ToList();
            return View(historico);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Comprando(int id) {
            // encontra o usuário e casa de show quando clicado no botão, converte um model em DTO e retorna a view com os dados já nos inputs
            var evento = _context.Eventos.Include(cs => cs.CasaDeShows).FirstOrDefault(eve => eve.Id == id);
            EventoDTO eveDTO = new EventoDTO();
            eveDTO.Id = evento.Id;
            eveDTO.Nome = evento.Nome;
            eveDTO.Preco = evento.Preco.ToString();
            eveDTO.Ingressos = evento.Ingressos.ToString();
            eveDTO.GeneroId = evento.Genero.ToString();
            eveDTO.Data = evento.Data;
            eveDTO.CasaDeShowsId = evento.CasaDeShows.Id.ToString();
            eveDTO.CaminhoImagem = evento.Imagem;
            return View("Compra", eveDTO);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ConfirmarCompra(int quantidade, [Bind("Id")] EventoDTO eventos) {
            var compra = _context.Eventos.Where(eve => eve.Id == eventos.Id).FirstOrDefault();
            var casa = _context.casasDeShow.ToList();
            
            var email = User.Identity.Name;
            var user = _context.Users.Where(use => use.Email.Equals(email)).FirstOrDefault();
            eventos.User = user;

            Compras hist = new Compras();
            hist.Nome = compra.Nome;
            hist.Data = compra.Data;
            hist.Preco = compra.Preco * quantidade;
            hist.Genero = compra.Genero;
            hist.Imagem = compra.Imagem;
            hist.User = eventos.User;
            hist.Quantidade = quantidade;
            hist.Local = compra.CasaDeShows.Local;
             
            _context.Add(hist);

            compra.Ingressos -= quantidade; // ao realizar a compra desconta o ingresso
            _context.Attach(compra).State = EntityState.Modified; // diz que o objeto foi modificado
            _context.SaveChanges(); // sobe para o banco
            return RedirectToAction("Index");
        }
    }
}