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
    public class ProdutoReceitaController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        ProdutoReceitaRepository _repositorio = new ProdutoReceitaRepository ();

        //Get: Api/Produtoreceita
        /// <summary>
        /// Aqui são todos os produtos de uma receita
        /// </summary>
        /// <returns>Lista de produtos de uma receita</returns>
        [HttpGet]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<List<ProdutoReceita>>> Get () {

            var produtoreceitas = await _repositorio.Listar();

            if (produtoreceitas == null) {
                return NotFound();
            }
            return produtoreceitas;
        }
        //Get: Api/Produtoreceita
        /// <summary>
        /// Mostramos apenas uma ID de um produto receita
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unico ID de um produto receita</returns>
        [HttpGet ("{id}")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<ProdutoReceita>> Get(int id){
            var produtoreceita = await _repositorio.BuscarPorID(id);

            if (produtoreceita == null){
                return NotFound ();
            }
            return produtoreceita;
        }
        //Post: Api/ProdutoReceita
        /// <summary>
        /// Enviamos os dados de um produto receita
        /// </summary>
        /// <param name="produtoreceita"></param>
        /// <returns>Envia dados de um produto receita</returns>
        [HttpPost]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<ProdutoReceita>> Post (ProdutoReceita produtoreceita){
            try{
               await _repositorio.Salvar(produtoreceita);
                
                }catch (DbUpdateConcurrencyException){
                    throw;
            }
            return produtoreceita;
        }
        //Put: Api/ProdutoReceita
        /// <summary>
        /// Alteramos dados de um produto receita
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produtoreceita"></param>
        /// <returns>Alteração de dados produto receita</returns>
        [HttpPut ("{id}")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult> Put (int id, ProdutoReceita produtoreceita){
            if(id != produtoreceita.IdProdutoReceita){
                
                return BadRequest ();
            }
            _contexto.Entry (produtoreceita).State = EntityState.Modified;
            try{
                await _repositorio.Alterar(produtoreceita);
            }catch (DbUpdateConcurrencyException){
                var produtoreceita_valido = await _repositorio.BuscarPorID(id);

                if(produtoreceita_valido == null) {
                    return NotFound ();
                }else{
                    throw;
                }
            }
            return NoContent();
        }
         // DELETE api/ProdutoReceita/id
         /// <summary>
         /// Excluimos dados de uma produto receita
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Exclui dado de produto receita</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<ProdutoReceita>> Delete(int id){

            var produtoreceita = await _repositorio.BuscarPorID(id);
            if(produtoreceita == null){
                return NotFound();
            }

           
            await _repositorio.Excluir(produtoreceita);

            return produtoreceita;
        }  
    }
}   