using CasaDeShows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Função para autoCriar o banco


namespace CasaDeShows.Config
{
    public interface IDataService
    {
        void InicializaDB();
    }

    public class DataService : IDataService
    {
        private readonly ApplicationDbContext _context;

        public DataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InicializaDB()
        {
            // verifica se o banco está criado se ele não estiver ele cria
            _context.Database.EnsureCreated();
        }
    }
}
