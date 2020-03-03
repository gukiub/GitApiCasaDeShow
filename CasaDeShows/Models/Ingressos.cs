using System;
using System.Collections.Generic;

namespace CasaDeShows.Models
{
    public class Ingressos
    {
        public int Id{get;set;}
        public Eventos Preco{get;set;}
        public DateTime Data{get;set;}
        public CasasDeShow Local{get;set;}
        public bool Status{get;set;}
        
    }
}