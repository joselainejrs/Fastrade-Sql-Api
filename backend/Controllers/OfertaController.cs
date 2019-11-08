using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase {

        fastradeContext _contexto = new fastradeContext ();

        OfertaRepository _repositorio = new OfertaRepository ();

        UploadImageRepository _Upload = new UploadImageRepository ();

        //Get: Api/Oferta
        /// <summary>
        /// Aqui são todas as ofertas
        /// </summary>
        /// <returns>Lista de Ofertas</returns>
        [HttpGet]
        // [Authorize (Roles = "3")]
        public async Task<ActionResult<List<Oferta>>> Get () {
            var oferta = await _repositorio.Listar ();
            if (oferta == null) {
                return NotFound ();
            }
            return oferta;
        }
        //Get: Api/Oferta/2
        /// <summary>
        /// Mostramos uma oferta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Mostramos unico ID de oferta</returns>
        [HttpGet ("{id}")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<Oferta>> Get (int id) {
            var oferta = await _repositorio.BuscarPorID (id);

            if (oferta == null) {
                return NotFound ();
            }
            return oferta;
        }
        //Post: Api/Oferta
        /// <summary>
        /// Enviamos mais dados de oferta
        /// </summary>
        /// <param name="oferta"></param>
        /// <returns>Envia uma oferta</returns>
        [HttpPost]
        // [Authorize(Roles = "3")]
        // [Authorize(Roles = "2")]
        public async Task<ActionResult<Oferta>> Post ([FromForm] Oferta oferta) {
            try {
                //Cadastro de oferta com imagem
                var arquivo = Request.Form.Files[0];
                oferta.IdProduto = Convert.ToInt32 (Request.Form["IdProduto"]);
                oferta.IdUsuario = Convert.ToInt32 (Request.Form["IdUsuario"]);
                oferta.Quantidade = Convert.ToInt32 (Request.Form["Quantidade"]);
                oferta.Preco = Request.Form["Preco"];
                oferta.FotoUrlOferta = _Upload.Upload (arquivo, "Resources");             

                await _repositorio.Salvar (oferta);

            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return oferta;
        }
        //Put: Api/Oferta
        /// <summary>
        /// Alteramos os dados de uma oferta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oferta"></param>
        /// <returns>Alteração de dados de uma oferta</returns>
        [HttpPut ("{id}")]
        [Authorize (Roles = "3")]
        [Authorize (Roles = "2")]
        public async Task<ActionResult> Put (int id, Oferta oferta) {
            if (id != oferta.IdOferta) {

                return BadRequest ();
            }
            _contexto.Entry (oferta).State = EntityState.Modified;
            try {
                var arquivo = Request.Form.Files[0];
                oferta.IdProduto = Convert.ToInt32 (Request.Form["IdProduto"]);
                oferta.IdUsuario = Convert.ToInt32 (Request.Form["IdUsuario"]);
                oferta.Quantidade = Convert.ToInt32 (Request.Form["Quantidade"]);
                oferta.Preco = Request.Form["Preco"];
                oferta.FotoUrlOferta = _Upload.Upload (arquivo, "Resources");

                await _repositorio.Alterar (oferta);
            } catch (DbUpdateConcurrencyException) {
                var oferta_valido = await _repositorio.BuscarPorID (id);

                if (oferta_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            return NoContent ();
        }
        // DELETE api/Oferta/id
        /// <summary>
        /// Excluimos uma oferta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Excluir uma oferta</returns>
        [HttpDelete ("{id}")]
        [Authorize (Roles = "3")]
        [Authorize (Roles = "2")]
        public async Task<ActionResult<Oferta>> Delete (int id) {

            var oferta = await _contexto.Oferta.FindAsync (id);
            if (oferta == null) {
                return NotFound ();
            }

            _contexto.Oferta.Remove (oferta);
            await _contexto.SaveChangesAsync ();

            return oferta;
        }
    }
}