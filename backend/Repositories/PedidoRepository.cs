using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class PedidoRepository : IPedido {
        public async Task<Pedido> Alterar (Pedido pedido) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Entry (pedido).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();
            }
            return pedido;

        }

        public async Task<Pedido> BuscarPorID (int id) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.Pedido.Include ("IdProdutoNavigation").Include ("IdUsuarioNavigation").FirstOrDefaultAsync (e => e.IdPedido == id);
            }
        }

        public async Task<Pedido> Excluir (Pedido pedido) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Pedido.Remove (pedido);
                await _contexto.SaveChangesAsync ();
                return pedido;
            }
        }

        public async Task<List<Pedido>> Listar () {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.Pedido.Include ("IdProdutoNavigation").Include ("IdUsuarioNavigation").ToListAsync ();
            }
        }

        public async Task<Pedido> Salvar (Pedido pedido) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                await _contexto.AddAsync (pedido);
                await _contexto.SaveChangesAsync ();
                return pedido;
            }
        }
    }
}