using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class EnderecoRepository : IEndereco {
        public async Task<Endereco> Alterar (Endereco endereco) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Entry (endereco).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();

            }
            return endereco;
        }

        public async Task<Endereco> BuscarPorID (int id) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.Endereco.FindAsync (id);

            }

        }

        public async Task<Endereco> Excluir (Endereco endereco) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Endereco.Remove (endereco);
                await _contexto.SaveChangesAsync ();
                return endereco;
            }
        }

        public async Task<List<Endereco>> Listar () {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.Endereco.ToListAsync ();

            }

        }

        public async Task<Endereco> Salvar (Endereco endereco) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                await _contexto.AddAsync (endereco);
                await _contexto.SaveChangesAsync ();
                return endereco;
            }

        }
    }
}