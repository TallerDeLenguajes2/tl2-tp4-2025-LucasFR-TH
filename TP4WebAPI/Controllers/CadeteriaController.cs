using Microsoft.AspNetCore.Mvc;
// Controller que expone los endpoints HTTP para operar sobre la cadetería.
// Recibe las peticiones (GET/POST/PUT) y delega la lógica al modelo Cadeteria
// y a las clases de acceso a datos para persistencia en JSON.
using EspacioCadeteria;
using EspacioPedidos;
using EspacioCadete;
using EspacioAccesoDatos;
using EspacioAccesoDatosCadeteria;
using EspacioAccesoDatosCadete;
using EspacioAccesoDatosPedidos;

namespace TP4WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    // API controller para Cadeteria: rutas y acciones públicas
    public class CadeteriaController : ControllerBase

    {
        private Cadeteria cadeteria;
        private accesoDatosCadeteria ADCadeteria;
        private accesoDatosCadetes ADCadetes;
        private accesoDatosPedidos ADPedidos;

    // Constructor: inicializa la cadetería cargando datos desde JSON
    // mediante las clases específicas de acceso a datos.
    public CadeteriaController()
        {
            ADCadeteria = new accesoDatosCadeteria();
            ADCadetes = new accesoDatosCadetes();
            ADPedidos = new accesoDatosPedidos();
            // Cargar datos desde los archivos JSON y poblar el modelo en memoria
            cadeteria = ADCadeteria.Obtener();
            // Agregar listas completas de cadetes y pedidos cargadas desde JSON
            if (cadeteria != null)
            {
                var listaCadetes = ADCadetes.Obtener();
                if (listaCadetes != null && listaCadetes.Count > 0)
                {
                    foreach (var cadete in listaCadetes)
                        cadeteria.AgregarCadete(cadete); // agrega cada cadete al modelo
                }
                var listaPedidos = ADPedidos.Obtener();
                if (listaPedidos != null && listaPedidos.Count > 0)
                {
                    foreach (var pedido in listaPedidos)
                        cadeteria.AgregarPedido(pedido); // agrega cada pedido al modelo
                }
            }
        }


        // ==========================
        // [GET] Listar todos los pedidos
        // ==========================
        [HttpGet("GetPedidos")]
        // Devuelve la lista de pedidos en memoria (no modifica datos)
        public ActionResult<List<Pedido>> GetPedidos()
        {
            return Ok(cadeteria.ObtenerPedidos());
        }

        // ==========================
        // [GET] Listar todos los cadetes
        // ==========================
        [HttpGet("GetCadetes")]
        // Devuelve la lista de cadetes en memoria
        public ActionResult<List<Cadete>> GetCadetes()
        {
            return Ok(cadeteria.ObtenerCadetes());
        }

        // ==========================
        // [GET] Obtener informe general
        // ==========================
        [HttpGet("GetInforme")]
        // Genera un informe resumido (totales y promedios)
        public ActionResult<object> GetInforme()
        {
            var informe = cadeteria.GenerarInforme();
            return Ok(informe);
        }

        // ==========================
        // [POST] Agregar un pedido
        // ==========================
        [HttpPost("AgregarPedido")]
        // Alta de un nuevo pedido: lo agrega al modelo y persiste en JSON
        public ActionResult AgregarPedido([FromBody] Pedido nuevoPedido)
        {
            cadeteria.AgregarPedido(nuevoPedido);
            ADPedidos.Guardar(cadeteria.ObtenerPedidos()); // persistir cambios
            return Ok(new { mensaje = "Pedido agregado correctamente." });
        }

        // ==========================
        // [PUT] Asignar un pedido a un cadete
        // ==========================
        [HttpPut("AsignarPedido/{idPedido}/{idCadete}")]
        // Asigna un cadete a un pedido y persiste el cambio
        public ActionResult AsignarPedido(int idPedido, int idCadete)
        {
            cadeteria.AsignarCadeteAPedido(idPedido, idCadete);
            ADPedidos.Guardar(cadeteria.ObtenerPedidos()); // persistir cambios
            return Ok("Pedido asignado al cadete correctamente.");
        }

        // ==========================
        // [PUT] Cambiar estado de un pedido
        // ==========================
        [HttpPut("CambiarEstadoPedido/{idPedido}/{nuevoEstado}")]
        // Cambia el estado de un pedido (ej: "En camino", "Entregado") y guarda
        public ActionResult CambiarEstadoPedido(int idPedido, string nuevoEstado)
        {
            var resultado = cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado);
            if (!resultado)
                return NotFound("Pedido no encontrado.");
            ADPedidos.Guardar(cadeteria.ObtenerPedidos()); // persistir cambios
            return Ok($"Estado del pedido {idPedido} actualizado a {nuevoEstado}.");
        }

        // ==========================
        // [PUT] Cambiar cadete asignado a un pedido
        // ==========================
        [HttpPut("CambiarCadetePedido/{idPedido}/{idNuevoCadete}")]
        // Reasigna un pedido a otro cadete y guarda los cambios
        public ActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
        {
            cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
            ADPedidos.Guardar(cadeteria.ObtenerPedidos()); // persistir cambios
            return Ok($"Pedido {idPedido} reasignado al cadete {idNuevoCadete}.");
        }
    }
}
