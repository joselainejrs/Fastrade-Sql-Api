using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class ReceitaRepository : IReceita {
        public async Task<Receita> Alterar (Receita receita) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Entry (receita).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();
            }
            return receita;
        }

        public async Task<Receita> BuscarPorID (int id) {
            using (fastradeContext _contexto = new fastradeContext ()) {

                return await _contexto.Receita.FindAsync (id);
            }

        }
        public async Task<Receita> Excluir (Receita receita) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Receita.Remove (receita);
                await _contexto.SaveChangesAsync ();

                return receita;
            }

        }

        public async Task<List<Receita>> Listar () {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.Receita.ToListAsync ();
            }

        }

        public async Task<Receita> Salvar (Receita receita) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                await _contexto.AddAsync (receita);
                await _contexto.SaveChangesAsync ();
                return receita;
            }
         }
    }
}