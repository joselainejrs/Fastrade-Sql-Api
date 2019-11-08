using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class ProdutoReceitaRepository : IProdutoReceita {
        public async Task<ProdutoReceita> Alterar (ProdutoReceita produtoreceita) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Entry (produtoreceita).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();
            }
            return produtoreceita;
        }

        public async Task<ProdutoReceita> BuscarPorID (int id) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.ProdutoReceita.Include ("IdProdutoNavigation").Include ("IdReceitaNavigation").FirstOrDefaultAsync (e => e.IdProdutoReceita == id);
            }

        }

        public async Task<ProdutoReceita> Excluir (ProdutoReceita produtoreceita) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.ProdutoReceita.Remove (produtoreceita);
                await _contexto.SaveChangesAsync ();
                return produtoreceita;
            }

        }

        public async Task<List<ProdutoReceita>> Listar () {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.ProdutoReceita.Include ("IdProdutoNavigation").Include ("IdReceitaNavigation").ToListAsync ();
            }

        }

        public async Task<ProdutoReceita> Salvar (ProdutoReceita produtoreceita) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                await _contexto.AddAsync (produtoreceita);
                await _contexto.SaveChangesAsync ();
                return produtoreceita;
            }

        }
    }
}