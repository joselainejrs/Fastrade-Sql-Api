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
    public class ProdutoController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        ProdutoRepository _repositorio = new ProdutoRepository();

        //Get: Api/Produtoreceita
        /// <summary>
        /// Aqui são todos os produtos
        /// </summary>
        /// <returns>Lista de Produtos</returns>
        [HttpGet]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<List<Produto>>> Get () {

            var produtos = await _repositorio.Listar();

            if (produtos == null) {
                return NotFound();
            }
            return produtos;
        }
        //Get: Api/Produtoreceita
        /// <summary>
        /// Pegamos um dado de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Pegamos um unico ID de produto</returns>
        [HttpGet ("{id}")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<Produto>> Get(int id){
            var produto = await _repositorio.BuscarPorID(id);

            if (produto == null){
                return NotFound ();
            }
            return produto;
        }
        //Post: Api/Produto
        /// <summary>
        /// Enviamos os dados de produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>Envia dados de produto</returns>
        [HttpPost]
        [Authorize(Roles = "3")]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<Produto>> Post (Produto produto){
            try{
                await _repositorio.Salvar(produto);
                

                }catch (DbUpdateConcurrencyException){
                    throw;
            }
            return produto;
        }
        //Put: Api/Produto
        /// <summary>
        /// Alteramos os dados de produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns>Alteração de produto</returns>
        [HttpPut ("{id}")]
        [Authorize(Roles = "3")]
        [Authorize(Roles = "2")]
        public async Task<ActionResult> Put (int id, Produto produto){
            if(id != produto.IdProduto){
                
                return BadRequest ();
            }
            _contexto.Entry (produto).State = EntityState.Modified;
            try{
                await _repositorio.Alterar (produto);
            }catch (DbUpdateConcurrencyException){
                var produto_valido = await _repositorio.BuscarPorID(id);

                if(produto_valido == null) {
                    return NotFound ();
                }else{
                    throw;
                }
            }
            return NoContent();
        }
         // DELETE api/Produto/id
         /// <summary>
         /// Excluimos dados de um produto
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Exclui produto</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "3")]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<Produto>> Delete(int id){

            var produto = await _repositorio.BuscarPorID(id);
            if(produto == null){
                return NotFound();
            }

            await _repositorio.Excluir(produto);

            return produto;
        }  
    }
}   