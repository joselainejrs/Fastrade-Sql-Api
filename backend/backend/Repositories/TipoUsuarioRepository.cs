using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class TipoUsuarioRepository : ITipoUsuario {
        public async Task<TipoUsuario> Alterar (TipoUsuario tipousuario) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Entry (tipousuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();
            }
            return tipousuario;
        }

        public async Task<TipoUsuario> BuscarPorID (int id) {
            using (fastradeContext _contexto = new fastradeContext ()) {

                return await _contexto.TipoUsuario.FindAsync (id);
            }
        }

        public async Task<TipoUsuario> Excluir (TipoUsuario tipousuario) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.TipoUsuario.Remove (tipousuario);
                await _contexto.SaveChangesAsync ();

                return tipousuario;
            }

        }

        public async Task<List<TipoUsuario>> Listar () {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.TipoUsuario.ToListAsync ();
            }
        }

        public async Task<TipoUsuario> Salvar (TipoUsuario tipousuario) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                await _contexto.AddAsync (tipousuario);
                await _contexto.SaveChangesAsync ();
                return tipousuario;
             }
        }
    }
}