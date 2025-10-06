using EspacioCadeteria;
using System.Text.Json;
using System.IO;

namespace EspacioAccesoDatosCadeteria
{
    // Clase responsable de leer/escribir la configuración básica de la cadetería
    // desde/hacia el archivo JSON `Cadeteria.json`.
    public class accesoDatosCadeteria
    {
        // Ruta del archivo donde se guarda la información de la cadetería
        private readonly string filePath = "Cadeteria.json";

        // Devuelve una instancia de Cadeteria cargada desde JSON.
        // Si el archivo no existe retorna una Cadeteria por defecto.
        public Cadeteria Obtener()
        {
            if (!File.Exists(filePath))
                return new Cadeteria(0, "", "");
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Cadeteria>(json) ?? new Cadeteria(0, "", "");
        }
    }
}