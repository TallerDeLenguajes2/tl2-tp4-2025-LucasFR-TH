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
    public double JornalACobrar(int id)
    {
        float pagar = 0;
        for (int i = 0; i < Pedidos.Count; i++)
        {
            if (Pedidos[i].Cadete.IDCadete == id && Pedidos[i].Estado == "Entregado")
            {
                Cadetes[id].PedidosEntregados++;
                pagar += 500;
            }
        }
        return pagar;
    }

    
    // metodo para agregar pedido
    public void AgregarPedido(Pedido pedido) => Pedidos.Add(pedido);

    // metodo para quitar pedido
    public void QuitarPedido(Pedido pedido) => Pedidos.Remove(pedido);

    // asignar el pedido

    // metodo para reasignar un pedido
    public void ReasignarPedido(Pedido pedido, Cadete cadeteOrigen, Cadete cadeteDestino)
    {
        QuitarPedido(pedido);
        AgregarPedido(pedido);
    }

    // INFORME // dejo comentado el metodod para crear un informar, en el tp4 creare una clase para poder mostrar estos datos
    // public void InformeFinal()
    // {
    //     Console.WriteLine("========= INFORME FINAL =========");
    //     int totalPedidos = 0;
    //     foreach (var cad in Cadetes)
    //     {
    //         int entregados = Pedidos.Count(p => p.Estado == "Entregado");
    //         totalPedidos += entregados;
    //         Console.WriteLine($"{cad.Nombre} → {cad.PedidosEntregados} entregados | Jornal: ${JornalACobrar(cad.IDCadete)}");
    //     }
    //     Console.WriteLine($"Total entregados: {totalPedidos}");
    //     Console.WriteLine($"Promedio por cadete: {(double)totalPedidos / Cadetes.Count}");
    // }
}
