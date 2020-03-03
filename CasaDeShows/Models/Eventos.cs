using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace CasaDeShows.Models
{
    public class Eventos
    { 
        [Required]
        public int Id { get; set; }
        [Required]
        
        [StringLength(100, ErrorMessage = "� Nome muito grande, tente um nome menor.")]
        [MinLength(2, ErrorMessage = "� Nome muito pequeno, tente um nome maior.")]
        public string Nome { get; set; }
        [Required]
        public CasasDeShow CasaDeShows { get; set; }
        [Required]
        public double Preco { get; set; }
        [Required]
        public int Genero { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        [Required]
        public int Ingressos { get; set; }
        public string Imagem { get; set; }
        public IdentityUser User { get; set; }
    }
}