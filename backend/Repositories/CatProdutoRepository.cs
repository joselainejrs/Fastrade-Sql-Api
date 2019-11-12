using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class CatProdutoRepository : ICatProduto {
        public async Task<CatProduto> Alterar (CatProduto catproduto) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Entry (catproduto).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();

            }
            return catproduto;

        }

        public async Task<CatProduto> BuscarPorID (int id) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.CatProduto.FindAsync (id);

            }
        }

        public async Task<CatProduto> Excluir (CatProduto catproduto) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.CatProduto.Remove (catproduto);
                await _contexto.SaveChangesAsync ();
                return catproduto;
            }

        }

        public async Task<List<CatProduto>> Listar () {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.CatProduto.ToListAsync ();

            }

        }

        public async Task<CatProduto> Salvar (CatProduto catproduto) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                await _contexto.AddAsync (catproduto);
                await _contexto.SaveChangesAsync ();
                return catproduto;
            }

        }
    }
}