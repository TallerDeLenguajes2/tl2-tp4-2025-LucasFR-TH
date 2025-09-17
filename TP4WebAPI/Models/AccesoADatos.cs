using EspacioCadeteria;
using EspacioCadete;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EspacioAccesoDatos
{
    using EspacioCadeteria;

    public interface IAccesoADatos
    {
        Cadeteria LeerCadeteria(string archivoCadeteria, string archivoCadetes);
        void GuardarCadeteria(Cadeteria cadeteria, string archivoDestino);
    }

    // particularizado para CSV
    public class AccesoADatosCSV : IAccesoADatos
    {
        public Cadeteria LeerCadeteria(string archivoCadeteria, string archivoCadetes)
        {
            var lineasCadeteria = File.ReadAllLines(archivoCadeteria);
            var datosCadeteria = lineasCadeteria[1].Split(',');
            var cadeteria = new Cadeteria(int.Parse(datosCadeteria[0]), datosCadeteria[1], datosCadeteria[2]);

            var lineasCadetes = File.ReadAllLines(archivoCadetes).Skip(1);
            foreach (var linea in lineasCadetes)
            {
                var datos = linea.Split(',');
                Cadete c = new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]);
                cadeteria.AgregarCadete(c);
            }

            return cadeteria;
        }

        public void GuardarCadeteria(Cadeteria cadeteria, string archivoDestino)
        {
            using (StreamWriter sw = new StreamWriter(archivoDestino))
            {
                sw.WriteLine("CUIL,Nombre,Telefono");
                sw.WriteLine($"{cadeteria.CUIL},{cadeteria.Nombre},{cadeteria.Telefono}");
            }
        }
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
}