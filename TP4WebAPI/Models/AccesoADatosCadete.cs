using EspacioCadete;
using EspacioCadeteria;
using System.Text.Json;

// Clase de acceso a datos para Cadetes: lectura/escritura desde `Cadetes.json`.
// Provee una única operación pública `Obtener` que devuelve la lista completa
// de cadetes. Es usada por el controlador para poblar el modelo en memoria.

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