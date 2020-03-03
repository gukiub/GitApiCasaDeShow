using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDeShows.DTO
{
    public class ImagemEventoDTO
    {
        public ImagemEventoDTO()
        {
        }

        public ImagemEventoDTO(int eventoId, IFormFile imagemEvento)
        {
            EventoId = eventoId;
            ImagemEvento = imagemEvento;
        }

        public int EventoId { get; set; }
        public IFormFile ImagemEvento { get; set; }
    }
}
