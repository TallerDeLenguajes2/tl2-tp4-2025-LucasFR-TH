using EspacioPedidos;
using System.Text.Json;
using System.IO;

namespace EspacioAccesoDatosPedidos
{
    // Clase encargada de la persistencia de pedidos en `Pedidos.json`.
    // Provee `Obtener` para leer la lista completa y `Guardar` para
    // serializar la lista actual y sobrescribir el archivo.
    public class accesoDatosPedidos
    {
        private readonly string filePath = "Pedidos.json";

        // Devuelve la lista de pedidos desde JSON o una lista vacía si no existe.
        public List<Pedido> Obtener()
        {
            if (!File.Exists(filePath))
                return new List<Pedido>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
        }

        // Guarda la lista de pedidos en disco (reemplaza el contenido del archivo).
        public void Guardar(List<Pedido> pedidos)
        {
            var json = JsonSerializer.Serialize(pedidos);
            File.WriteAllText(filePath, json);
        }
    }
}