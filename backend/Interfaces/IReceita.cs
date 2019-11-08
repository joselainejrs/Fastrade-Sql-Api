using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces {
    public interface IReceita {
        Task<List<Receita>> Listar ();
        Task<Receita> BuscarPorID (int id);
        Task<Receita> Salvar (Receita receita);
        Task<Receita> Alterar (Receita receita);
        Task<Receita> Excluir (Receita receita);
    }
}