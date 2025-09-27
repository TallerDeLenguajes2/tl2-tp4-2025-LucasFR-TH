using EspacioCadeteria;
using System.Text.Json;

namespace EspacioAccesoDatosCadeteria
{
    public interface IAccesoADatos
    {
        Cadeteria LeerCadeteria(string archivoCadeteria, string archivoCadetes);
        void GuardarCadeteria(Cadeteria cadeteria, string archivoDestino);
    }

    // particularizado para JSON
    public class AccesoADatosJSON : IAccesoADatos
    {
        public Cadeteria LeerCadeteria(string archivoCadeteria, string archivoCadetes)
        {
            string jsonString = File.ReadAllText(archivoCadeteria);
            Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonString);

            return cadeteria;
        }

        public void GuardarCadeteria(Cadeteria cadeteria, string archivoDestino)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(cadeteria, options);
            File.WriteAllText(archivoDestino, jsonString);
        }
    }

    public class ADCadeteria 
    {
        // public Cadeteria Obtener() {
            
        // } 
    }
}