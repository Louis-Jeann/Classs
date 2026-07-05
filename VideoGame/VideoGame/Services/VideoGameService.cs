using System.Collections.Generic;
using System.Linq;
using VideoGame.Models;

namespace VideoGame.Services
{
    public class VideoGameService
    {
        private List<Juego> juegos = new List<Juego>()
        {
            new Juego
            {
                Id = 1,
                Nombre = "Minecraft",
                Genero = "Sandbox",
                Precio = 30
            },

            new Juego
            {
                Id = 2,
                Nombre = "GTA V",
                Genero = "Acción",
                Precio = 45
            }
        };

        public List<Juego> FindAll()
        {
            return juegos;
        }

        public void Create(Juego juego)
        {
            juegos.Add(juego);
        }

        public bool Delete(int id)
        {
            var juego = juegos.FirstOrDefault(x => x.Id == id);

            if (juego == null)
                return false;

            juegos.Remove(juego);
            return true;
        }
    }
}