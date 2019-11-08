using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_fastread.Controllers {

    //Definimos nossa rota do controller e dizemos que é um controller de API
    [Route ("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        TipoUsuarioRepository _repositorio = new TipoUsuarioRepository ();

        //GET: api/TipoUsuario
        /// <summary>
        /// Aqui são todos os Tipos de Usuario
        /// </summary>
        /// <returns>Lista de tipo de usuario</returns>
        [HttpGet]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<List<TipoUsuario>>> Get () {

            var TipoUsuarios = await _repositorio.Listar ();

            if (TipoUsuarios == null) {
                return NotFound ();
            }

            return TipoUsuarios;

        }
        //GET: api/TipoUsuario/2
        /// <summary>
        /// Pegamos um tipo de usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unico ID de um tipo de usuario</returns>
        [HttpGet ("{id}")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<TipoUsuario>> Get (int id) {

            var TipoUsuario = await _repositorio.BuscarPorID(id);
            if (TipoUsuario == null) {
                return NotFound ();
            }
            return TipoUsuario;
        }

        //POST api/TipoUsuario
        /// <summary>
        /// Envia dados de tipo de usuario
        /// </summary>
        /// <param name="tipousuario"></param>
        /// <returns>Envia dados de tipo de usuario</returns>
        [HttpPost]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<TipoUsuario>> Post (TipoUsuario tipousuario) {

            try {
               
                await _repositorio.Salvar (tipousuario);
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return tipousuario;
        }
        /// <summary>
        /// Alteramos dados de tipo de usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipousuario"></param>
        /// <returns>Alteração de dados de tipo de usuario</returns>
        [HttpPut ("{id}")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult> Put (int id, TipoUsuario tipousuario) {

            //Se o id do objeto não existir
            //ele retorna erro 401

            if (id != tipousuario.IdTipoUsuario) {
                return BadRequest ();
            }
            //comparamos os atributos que foram modificado atraves do EF
            _contexto.Entry (tipousuario).State = EntityState.Modified;

            try {
                await _repositorio.Alterar(tipousuario);

            } catch (DbUpdateConcurrencyException) {
                //verificamos se o objeto inserido realmente existe no banco
                var tipousuario_valido = await _repositorio.BuscarPorID(id);
                if (tipousuario_valido == null) {
                    return NotFound ();

                } else {
                    throw;
                }
            }
            //Nocontent = Retorna 204, sem nada
            return NoContent ();

        }
        //DELETE api/tipousuario/id
        /// <summary>
        /// Excluimos dados de tipo de usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Excluir dados de tipo de usuario</returns>
        [HttpDelete ("{id}")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<TipoUsuario>> Delete (int id) {

            var tipousuario = await _repositorio.BuscarPorID(id);
            if (tipousuario == null) {
                return NotFound ();
            }
            await _repositorio.Excluir(tipousuario);

            return tipousuario;
        }
    }
}