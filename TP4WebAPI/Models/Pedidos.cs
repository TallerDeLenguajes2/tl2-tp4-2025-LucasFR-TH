namespace EspacioPedidos;

using EspacioCliente;
using EspacioCadete;

// Representa un pedido realizado por un cliente.
// Contiene referencia al cliente, posible cadete asignado y el estado.
public class Pedido
{
    public int NPedido { get; set; }

    public string Observacion { get; set; }

    // agego campo privado
    private Cliente Cliente { get; set; }

    public string Estado { get; set; }

    // agrego una refenerencia a cadete en la clase
    public Cadete Cadete { get; set; }

    // constructora para instanciar la clase pedido
    public Pedido(int n, string obs, int Cid, string Cnombre, string Cdirec, string Ctel, string Cref, string estado)
    {
        NPedido = n;
        Observacion = obs;
        // agrego en la cosntructora los datos del lciente un opor uno isntancia y cargo
        Cliente = new Cliente(Cid, Cnombre, Cdirec, Ctel, Cref);
        Estado = estado;
    }

    // Devuelve algunos datos del cliente asociados al pedido
    public string VerDireccionCliente() => Cliente.Direccion;

    public string VerNombreCliente() => Cliente.Nombre;

    // Cambia el estado interno del pedido (persistir fuera si corresponde)
    public void CambiarEstado(string nuevoEstado) => Estado = nuevoEstado;
}