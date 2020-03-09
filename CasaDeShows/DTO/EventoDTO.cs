using CasaDeShows.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDeShows.DTO
{
    public class EventoDTO
    {

        public int Id{ get; set; }
        [Required(ErrorMessage = "• preencha o nome")]
        [StringLength(100, ErrorMessage = "• Nome muito grande, tente um nome menor.")]
        [MinLength(2, ErrorMessage = "• Nome muito pequeno, tente um nome maior.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "preencha a casa de show")]
        public string CasaDeShowsId { get; set; }
        [Required(ErrorMessage = "preencha o Preço")]
        public string Preco { get; set; }
        [Required(ErrorMessage = "preencha a data")]
        [DataType(DataType.DateTime)]
       
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "preencha a quantidade de ingressos")]
        public string Ingressos { get; set; }
        [Required(ErrorMessage = "preencha o genero")]
        public string GeneroId { get; set; }
        public string CaminhoImagem { get; set; }
        public IdentityUser User { get; set; }
    }
}
