using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces {
    public interface IPedido {
        Task<List<Pedido>> Listar ();
        
        Task<Pedido> BuscarPorID (int id);        
        Task<Pedido> Salvar (Pedido pedido);
        Task<Pedido> Alterar (Pedido pedido);
        
        Task<Pedido> Excluir (Pedido pedido);
    }
}