using EspacioPedidos;
using System.Text.Json;

namespace EspacioAccesoDatosPedidos
{
    using EspacioPedidos;
    public class accesoDatosPedidos
    {
        private readonly string filePath = "Pedidos.json";

        public List<Pedido> Obtener()
        {
            if (!File.Exists(filePath))
                return new List<Pedido>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
        }

        public void Guardar(List<Pedido> pedidos)
        {
            var json = JsonSerializer.Serialize(pedidos);
            File.WriteAllText(filePath, json);
        }
    }
}