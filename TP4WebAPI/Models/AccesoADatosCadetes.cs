using System.Text.Json;
using System.IO;
using EspacioCadete;

namespace EspacioAccesoDatosCadete
{
    public class accesoDatosCadetes
    {
        private readonly string filePath = "Cadetes.json";

        public List<Cadete> Obtener()
        {
            if (!File.Exists(filePath))
                return new List<Cadete>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Cadete>>(json) ?? new List<Cadete>();
        }
    }
}
