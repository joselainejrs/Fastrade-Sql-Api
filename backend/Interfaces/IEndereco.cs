using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces {
    public interface IEndereco {
        Task<List<Endereco>> Listar ();
        
        Task<Endereco> BuscarPorID (int id);
        
        Task<Endereco> Salvar (Endereco endereco);

        Task<Endereco> Alterar (Endereco endereco);

        Task<Endereco> Excluir (Endereco endereco);
    }
}