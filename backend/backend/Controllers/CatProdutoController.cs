using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class CatProdutoController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        CatProdutoRepository _repositorio = new CatProdutoRepository ();

        //Get: Api/Catproduto
        /// <summary>
        /// Aqui é todas Categorias dos produtos
        /// </summary>
        /// <returns>Lista de categorias de produtos</returns>
        [HttpGet]
        // [Authorize (Roles = "3")]
        public async Task<ActionResult<List<CatProduto>>> Get () {

            var catprodutos = await _repositorio.Listar ();

            if (catprodutos == null) {
                return NotFound ();
            }
            return catprodutos;
        }
        //Get: Api/Catproduto
        /// <summary>
        /// Aqui Pegamos apenas Uma Categoria de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unico Id de categoria produtos</returns>
        [HttpGet ("{id}")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<CatProduto>> Get (int id) {
            var catproduto = await _repositorio.BuscarPorID (id);

            if (catproduto == null) {
                return NotFound ();
            }
            return catproduto;
        }
        //Post: Api/CatProduto
        /// <summary>
        /// Aqui enviamos mais categorias de produtos
        /// </summary>
        /// <param name="catproduto"></param>
        /// <returns>Envia uma categoria produto</returns>
        [HttpPost]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<CatProduto>> Post (CatProduto catproduto) {
            try {
                await _repositorio.Salvar (catproduto);

            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return catproduto;
        }
        //Put: Api/CatProduto
        /// <summary>
        /// Aqui alteramos dados das categorias de produtos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="catproduto"></param>
        /// <returns>Alteramento de categoria produto</returns>
        [HttpPut ("{id}")]
        // [Authorize (Roles = "3")]
        public async Task<ActionResult> Put (int id, CatProduto catproduto) {
            if (id != catproduto.IdCatProduto) {

                return BadRequest ();
            }
            _contexto.Entry (catproduto).State = EntityState.Modified;
            try {
                await _repositorio.Alterar (catproduto);
            } catch (DbUpdateConcurrencyException) {
                var catproduto_valido = await _repositorio.BuscarPorID (id);

                if (catproduto_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            return NoContent ();
        }
        // DELETE api/CatProduto/id
        /// <summary>
        /// Aqui deletamos uma categoria de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleta uma categoria</returns>
        [HttpDelete ("{id}")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<CatProduto>> Delete (int id) {

            var catproduto = await _repositorio.BuscarPorID (id);
            if (catproduto == null) {
                return NotFound ();
            }

            await _repositorio.Excluir (catproduto);

            return catproduto;
        }
    }
}