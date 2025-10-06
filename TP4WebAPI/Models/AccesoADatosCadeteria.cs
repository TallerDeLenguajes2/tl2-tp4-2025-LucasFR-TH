using EspacioCadeteria;
using System.Text.Json;
using System.IO;

namespace EspacioAccesoDatosCadeteria
{
    using EspacioCadeteria;
    public class accesoDatosCadeteria
    {
        private readonly string filePath = "Cadeteria.json";

        public Cadeteria Obtener()
        {
            if (!File.Exists(filePath))
                return new Cadeteria(0, "", "");
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Cadeteria>(json) ?? new Cadeteria(0, "", "");
        }
    }
}