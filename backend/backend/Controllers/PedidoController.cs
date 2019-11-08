using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Para adicionar a árvore de objetos adicionamos uma nova biblioteca JSON
//dotnet add package Microsoft.AspNetCore.Mvc.NewtonSoftJson

namespace backend.Controllers {
    //Definimos nossa rota do controller e dizemos que é um controller de API   
    [Route ("API/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase {

        fastradeContext _contexto = new fastradeContext ();

        PedidoRepository _repositorio = new PedidoRepository ();

        //GET: api/Pedido
        /// <summary>
        /// Aqui são todos os pedidos
        /// </summary>
        /// <returns>Lista de pedidos</returns>
        [HttpGet]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<List<Pedido>>> Get () {

            //Include ("") = Adiciona o 
            var pedidos = await _repositorio.Listar();

            if (pedidos == null) {
                return NotFound ();
            }

            return pedidos;

        }

        //GET: api/Pedido/2
        /// <summary>
        /// Pegamos um pedido
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unico ID de pedido</returns>
        [HttpGet ("{id}")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<Pedido>> Get (int id) {

            // FindAsync = procura algo especifico no banco 
            var pedido = await _repositorio.BuscarPorID(id);

            if (pedido == null) {
                return NotFound ();
            }

            return pedido;

        }

        // POST api/Pedido
        /// <summary>
        /// Enviamos os dados de um pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns>Envia dados de pedido</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Pedido>> Post (Pedido pedido) {

            try {
              await _repositorio.Salvar(pedido);
            } catch (DbUpdateConcurrencyException) {
                throw;
            }

            return pedido;
        }
        /// <summary>
        /// Alteramos dados do pedido
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pedido"></param>
        /// <returns>Alteração de pedidos</returns>
        [HttpPut ("{id}")]
        [Authorize]
        public async Task<ActionResult> Put (int id, Pedido pedido) {

            //Se o Id do objeto não existir ele retorna 404 
            if (id != pedido.IdPedido) {
                return BadRequest ();
            }

            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry (pedido).State = EntityState.Modified;

            try {
                    await _repositorio.Alterar(pedido);
            } catch (DbUpdateConcurrencyException) {

                //Verificamos se o objeto inserido realmente existe no banco
                var pedido_valido = await _repositorio.BuscarPorID(id);

                if (pedido_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }

            }

            //NoContent = retorna 204, sem nada
            return NoContent ();

        }

        /// <summary>
        /// Excluimos dados de pedidos
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Excluir pedidos</returns>
        [HttpDelete ("{id}")]
        [Authorize]
        public async Task<ActionResult<Pedido>> Delete (int id) {

            var pedido = await _repositorio.BuscarPorID(id);
            if (pedido == null) {
                return NotFound ();
            }

            await _repositorio.Excluir(pedido);
            
            return pedido;
        }
    }
}