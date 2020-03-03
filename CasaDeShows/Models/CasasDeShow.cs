using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasaDeShows.Models
{
    public class CasasDeShow
    {
        [Required]
        public int Id{get; set;}
        [Required(ErrorMessage = "Digite o nome da casa de show")]
        [StringLength(100, ErrorMessage = "• Nome muito grande, tente um nome menor.")]
        [MinLength(2, ErrorMessage = "• Nome muito pequeno, tente um nome maior.")]
        public string Nome{get;set;}
        [Required(ErrorMessage = "Digite o local da casa de show")]
        [StringLength(100, ErrorMessage = "• Nome do local muito grande, tente um nome menor.")]
        [MinLength(2, ErrorMessage = "• Nome do local muito pequeno, tente um nome maior.")]
        public string Local {get;set;}
        public IdentityUser User { get; set; }
    }
}