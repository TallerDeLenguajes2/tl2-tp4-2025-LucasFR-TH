namespace EspacioCadete;

public class Cadete
{
    public int IDCadete { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }

    public int PedidosEntregados { get; set; }

    // construtora
    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        IDCadete = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
}

