using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CasaDeShows.Models
{
    public class Compras 
    {   
        public int Id { get; set; }
        
        public string Nome { get; set; }
       
        public double Preco { get; set; }
        public int Quantidade { get; set; }
    
        public int Genero { get; set; }
        public string Local { get;set;}
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        public string Imagem { get; set; }
        public IdentityUser User { get; set; }

    }
}