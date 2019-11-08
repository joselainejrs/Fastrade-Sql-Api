using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces {
    public interface IProdutoReceita {
        Task<List<ProdutoReceita>> Listar ();

        Task<ProdutoReceita> BuscarPorID (int id);

        Task<ProdutoReceita> Salvar (ProdutoReceita produtoreceita);

        Task<ProdutoReceita> Alterar (ProdutoReceita produtoreceita);
        
        Task<ProdutoReceita> Excluir (ProdutoReceita produtoreceita);
    }
}