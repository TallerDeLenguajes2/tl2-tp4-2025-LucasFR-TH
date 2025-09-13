namespace EspacioCliente;

using System;

// creo la clase cleinte con properties quines de manera implicita generan los campos
public class Cliente
{
    public int ClienteID { get; set; }

    public string Nombre { get; set; }

    public string Direccion { get; set; }

    public string Telefono { get; set; }

    public string ReferenciaDireccion { get; set; }

    // constructora para instanciar clientes
    public Cliente(int id, string nombre, string direc, string tel, string refDirec)
    {
        ClienteID = id;
        Nombre = nombre;
        Direccion = direc;
        Telefono = tel;
        ReferenciaDireccion = refDirec;
    }
}