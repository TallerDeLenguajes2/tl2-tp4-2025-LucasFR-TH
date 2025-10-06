using EspacioCadete;
using EspacioCadeteria;
using System.Text.Json;

namespace EspacioAccesoDatosCadete
{
    public class ADCadetes 
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