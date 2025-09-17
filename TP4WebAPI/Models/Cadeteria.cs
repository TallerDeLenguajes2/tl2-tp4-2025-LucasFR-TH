namespace EspacioCadeteria;

using System.IO;
using EspacioCadete;
using EspacioPedidos;

public class Cadeteria
{
    public int CUIL { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    private List<Cadete> Cadetes { get; set; } = new List<Cadete>();

    // Agregar ListadoPedidos en la clase Cadeteria que contenga todo los pedidos que se vayan generando.
    private List<Pedido> Pedidos { get; set; } = new List<Pedido>();

    // construtiora
    public Cadeteria(int cuil, string nombre, string telefono)
    {
        CUIL = cuil;
        Nombre = nombre;
        Telefono = telefono;
    }

    // metodo estático para leer la cadetería desde CSV
    public static Cadeteria LeerCadeteriaCSV(string archivo)
    {
        var lineas = File.ReadAllLines(archivo);
        var datos = lineas[1].Split(','); // salteo cabecera
        return new Cadeteria(int.Parse(datos[0]), datos[1], datos[2]);
    }

    // metodo para cargar cadetes desde CSV
    public void LeerCadetesCSV(string archivo)
    {
        var lineas = File.ReadAllLines(archivo).Skip(1); // salteo cabecera
        foreach (var linea in lineas)
        {
            var datos = linea.Split(',');
            Cadete c = new Cadete(int.Parse(datos[0]), datos[1], datos[2], datos[3]);
            Cadetes.Add(c);
        }
    }

    // metodo para agregar cadete
    public void AgregarCadete(Cadete cadete) => Cadetes.Add(cadete);

    // metodo para devolver un cadete mediante su busqueda de ID
    public Cadete BuscarCadete(int id) => Cadetes.FirstOrDefault(c => c.IDCadete == id);

    // Agregar el método JornalACobrar en la clase Cadeteria que recibe como parámetro el id del cadete y devuelve el monto a cobrar para dicho cadete 
    // Método para calcular el jornal de un cadete
    public double JornalACobrar(int idCadete)
    {
        // Buscamos al cadete correspondiente
        var cadete = Cadetes.FirstOrDefault(c => c.IDCadete == idCadete);

        if (cadete == null)
        {
            Console.WriteLine("Cadete no encontrado.");
            return 0; // Si no existe, no cobra nada
        }

        // Contamos los pedidos entregados de ese cadete
        int pedidosEntregados = Pedidos.Count(p =>
            p.Cadete != null &&
            p.Cadete.IDCadete == idCadete &&
            p.Estado == "Entregado");

        // Guardamos en el cadete la cantidad de pedidos entregados (opcional)
        cadete.PedidosEntregados = pedidosEntregados;

        // Cada pedido entregado equivale a $500
        double jornal = pedidosEntregados * 500;

        return jornal;
    }


    // metodo para agregar pedido
    public void AgregarPedido(Pedido pedido) => Pedidos.Add(pedido);

    // metodo para quitar pedido
    public void QuitarPedido(Pedido pedido) => Pedidos.Remove(pedido);

    // asignar el pedido
    public void AsignarCadeteAPedido(int idPedido, int idCadete)
    {
        var pedido = Pedidos.FirstOrDefault(p => p.NPedido == idPedido);
        var cadete = Cadetes.FirstOrDefault(c => c.IDCadete == idCadete);

        if (pedido != null && cadete != null)
        {
            pedido.Cadete = cadete; // asignamos el cadete al pedido
        }
    }


    // metodo para reasignar un pedido
    public void ReasignarPedido(int idPedido, int idCadeteDestino)
    {
        var pedido = Pedidos.FirstOrDefault(p => p.NPedido == idPedido);
        var cadeteDestino = Cadetes.FirstOrDefault(c => c.IDCadete == idCadeteDestino);

        if (pedido != null && cadeteDestino != null)
        {
            pedido.Cadete = cadeteDestino;
        }
    }

    public List<Pedido> ObtenerPedidos() => Pedidos;

    public List<Cadete> ObtenerCadetes() => Cadetes;

    public object GenerarInforme()
    {
        int totalPedidos = Pedidos.Count;
        int entregados = Pedidos.Count(p => p.Estado == "Entregado");
        return new
        {
            NombreCadeteria = Nombre,
            TotalPedidos = totalPedidos,
            Entregados = entregados,
            PromedioPorCadete = Cadetes.Count > 0 ? (double)entregados / Cadetes.Count : 0
        };
    }
    
    public bool CambiarEstadoPedido(int idPedido, string nuevoEstado)
    {
        var pedido = Pedidos.FirstOrDefault(p => p.NPedido == idPedido);
        if (pedido == null) return false;
        pedido.CambiarEstado(nuevoEstado);
        return true;
    }
}
