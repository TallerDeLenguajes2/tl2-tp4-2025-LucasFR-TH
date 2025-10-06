using Microsoft.AspNetCore.Mvc;
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

    public class CadeteriaController : ControllerBase

    {
        private Cadeteria cadeteria;
        private accesoDatosCadeteria ADCadeteria;
        private accesoDatosCadetes ADCadetes;
        private accesoDatosPedidos ADPedidos;

        public CadeteriaController()
        {
            ADCadeteria = new accesoDatosCadeteria();
            ADCadetes = new accesoDatosCadetes();
            ADPedidos = new accesoDatosPedidos();
            cadeteria = ADCadeteria.Obtener();
            // Agregar listas completas de cadetes y pedidos
            if (cadeteria != null)
            {
                var listaCadetes = ADCadetes.Obtener();
                if (listaCadetes != null && listaCadetes.Count > 0)
                {
                    foreach (var cadete in listaCadetes)
                        cadeteria.AgregarCadete(cadete);
                }
                var listaPedidos = ADPedidos.Obtener();
                if (listaPedidos != null && listaPedidos.Count > 0)
                {
                    foreach (var pedido in listaPedidos)
                        cadeteria.AgregarPedido(pedido);
                }
            }
        }


        // ==========================
        // [GET] Listar todos los pedidos
        // ==========================
        [HttpGet("GetPedidos")]
        public ActionResult<List<Pedido>> GetPedidos()
        {
            return Ok(cadeteria.ObtenerPedidos());
        }

        // ==========================
        // [GET] Listar todos los cadetes
        // ==========================
        [HttpGet("GetCadetes")]
        public ActionResult<List<Cadete>> GetCadetes()
        {
            return Ok(cadeteria.ObtenerCadetes());
        }

        // ==========================
        // [GET] Obtener informe general
        // ==========================
        [HttpGet("GetInforme")]
        public ActionResult<object> GetInforme()
        {
            var informe = cadeteria.GenerarInforme();
            return Ok(informe);
        }

        // ==========================
        // [POST] Agregar un pedido
        // ==========================
        [HttpPost("AgregarPedido")]
        public ActionResult AgregarPedido([FromBody] Pedido nuevoPedido)
        {
            cadeteria.AgregarPedido(nuevoPedido);
            ADPedidos.Guardar(cadeteria.ObtenerPedidos());
            return Ok(new { mensaje = "Pedido agregado correctamente." });
        }

        // ==========================
        // [PUT] Asignar un pedido a un cadete
        // ==========================
        [HttpPut("AsignarPedido/{idPedido}/{idCadete}")]
        public ActionResult AsignarPedido(int idPedido, int idCadete)
        {
            cadeteria.AsignarCadeteAPedido(idPedido, idCadete);
            ADPedidos.Guardar(cadeteria.ObtenerPedidos());
            return Ok("Pedido asignado al cadete correctamente.");
        }

        // ==========================
        // [PUT] Cambiar estado de un pedido
        // ==========================
        [HttpPut("CambiarEstadoPedido/{idPedido}/{nuevoEstado}")]
        public ActionResult CambiarEstadoPedido(int idPedido, string nuevoEstado)
        {
            var resultado = cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado);
            if (!resultado)
                return NotFound("Pedido no encontrado.");
            ADPedidos.Guardar(cadeteria.ObtenerPedidos());
            return Ok($"Estado del pedido {idPedido} actualizado a {nuevoEstado}.");
        }

        // ==========================
        // [PUT] Cambiar cadete asignado a un pedido
        // ==========================
        [HttpPut("CambiarCadetePedido/{idPedido}/{idNuevoCadete}")]
        public ActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
        {
            cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
            ADPedidos.Guardar(cadeteria.ObtenerPedidos());
            return Ok($"Pedido {idPedido} reasignado al cadete {idNuevoCadete}.");
        }
    }
}
