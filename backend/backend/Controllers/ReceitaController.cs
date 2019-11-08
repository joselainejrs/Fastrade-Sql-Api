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
    public class ReceitaController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        ReceitaRepository _repositorio = new ReceitaRepository ();


        //Get: Api/Receita
        /// <summary>
        /// Aqui são todas as receitas
        /// </summary>
        /// <returns>Lista de receita</returns>
        [HttpGet]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<List<Receita>>> Get () {

            var receitas = await _repositorio.Listar ();

            if (receitas == null) {
                return NotFound();
            }
            return receitas;    
        }
        //Get: Api/Receita
        /// <summary>
        /// Mostramos uma unica receita
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unico ID de uma receita</returns>
        [HttpGet ("{id}")]
        [Authorize(Roles = "3")]

        public async Task<ActionResult<Receita>> Get(int id){
            var receita = await _repositorio.BuscarPorID (id);

            if (receita == null){
                return NotFound ();
            }
            return receita;
        }
        //Post: Api/Receita
        /// <summary>
        /// Enviamos dados de uma receita
        /// </summary>  
        /// <param name="receita"></param>
        /// <returns>Envia dados de uma receita</returns>
        [HttpPost]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<Receita>> Post (Receita receita){
            try{
                
                await _repositorio.Salvar(receita);

                }catch (DbUpdateConcurrencyException){
                    throw;
            }
            return receita;
        }
        //Put: Api/Receita
        /// <summary>
        /// Alteramos dados de uma receita
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receita"></param>
        /// <returns>Alteração de dados de uma receita</returns>
        [HttpPut ("{id}")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult> Put (int id, Receita receita){
            if(id != receita.IdReceita){
                
                return BadRequest ();
            }
            _contexto.Entry (receita).State = EntityState.Modified;
            try{
                await _repositorio.Alterar(receita);
            }catch (DbUpdateConcurrencyException){
                var receita_valido = await _repositorio.BuscarPorID(id);

                if(receita_valido == null) {
                    return NotFound ();
                }else{
                    throw;
                }
            }
            return NoContent();
        }
         // DELETE api/Receita/id
         /// <summary>
         /// Excluimos dados de uma receita
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Exclui dados de uma receita</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<Receita>> Delete(int id){

            var receita = await _repositorio.BuscarPorID(id);
            if(receita == null){
                return NotFound();
            }

            await _repositorio.Excluir(receita);

            return receita;
        }  
    }
}   