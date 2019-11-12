using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces
{
    public interface ICatProduto
    {
        Task<List<CatProduto>> Listar();

        Task<CatProduto> BuscarPorID(int id);

        Task<CatProduto> Salvar(CatProduto catproduto);

        Task<CatProduto> Alterar(CatProduto catproduto);
        
        Task<CatProduto> Excluir(CatProduto catproduto);

    }
}