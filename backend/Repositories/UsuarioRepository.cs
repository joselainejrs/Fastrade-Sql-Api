using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class UsuarioRepository : IUsuario {
        public async Task<Usuario> Alterar (Usuario usuario) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Entry (usuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();
            }
            return usuario;

        }

        public async Task<Usuario> BuscarPorID (int id) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.Usuario.Include ("IdEnderecoNavigation").Include ("IdTipoUsuarioNavigation").FirstOrDefaultAsync (e => e.IdUsuario == id);
            }
        }

        public async Task<Usuario> Excluir (Usuario usuario) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                _contexto.Usuario.Remove (usuario);
                await _contexto.SaveChangesAsync ();
                return usuario;
            }
        }

        public async Task<List<Usuario>> Listar () {
            using (fastradeContext _contexto = new fastradeContext ()) {
                return await _contexto.Usuario.Include ("IdEnderecoNavigation").Include ("IdTipoUsuarioNavigation").ToListAsync ();
            }
        }

        public async Task<Usuario> Salvar (Usuario usuario) {
            using (fastradeContext _contexto = new fastradeContext ()) {
                await _contexto.AddAsync (usuario);
                await _contexto.SaveChangesAsync ();
                return usuario;
            }
        }

        //Validação  CPF
         public bool ValidaCPF( string cpfUsuario ){
            using (fastradeContext _contexto = new fastradeContext ()) {


            bool resultado = false;
            int[] v1       = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cpfCalculo = "";
            int resto         = 0;
            int calculo       = 0;

            string digito_v1 = "";
            string digito_v2 = "";
            
           cpfCalculo = cpfUsuario.Substring(0, 9);

            for(int i= 0; i <= 8; i++){

                calculo += int.Parse(cpfCalculo[i].ToString()) * v1[i];
            }

            resto   = calculo % 11;
            calculo = 11 - resto;

            if(calculo > 9){
                digito_v1 = "0";                
            }else{
                digito_v1 = calculo.ToString();
            }

            if( digito_v1 == cpfUsuario[9].ToString() ){
                resultado = true;
            }

            int[] v2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            resto    = 0;
            
            
            cpfCalculo = cpfCalculo + calculo.ToString();

            calculo = 0;

            for(int i= 0; i <= 8; i++){

                calculo += int.Parse(cpfCalculo[i].ToString()) * v2[i];
            }

            resto   = calculo % 11;
            calculo = 11 - resto;

            if(calculo > 9){
                digito_v2 = "0";                
            }else{
                digito_v2 = calculo.ToString();
            }

            if( digito_v2 == cpfUsuario[10].ToString() ){
                resultado = true;
            }

            return resultado;
            }
        }

        
        //Validação do CNPJ           
        public bool ValidaCNPJ( string cnpjUsuario){
                 using (fastradeContext _contexto = new fastradeContext ()) {
            bool resultado = false;
            int[]v1        = {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};

            string cnpjCalculo = "";
            int resto          = 0;
            int calculo        = 0;

            string digito_v1 = "";
            string digito_v2 = "";

           
            cnpjCalculo = cnpjUsuario.Substring(0, 12);

            for(int i = 0; i <= 11; i++){

                calculo += int.Parse(cnpjCalculo[i].ToString() ) * v1[i];
            }
            
            resto   = calculo % 11;
            calculo = 11 - resto;

            if(resto < 2){
                digito_v1 = "0";
            }else{
                digito_v1 = calculo.ToString();
            }

            if( digito_v1 == cnpjUsuario[12].ToString() ){
                resultado = true;
            }

            int[] v2 = {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
            resto    = 0;

            cnpjCalculo = cnpjCalculo = calculo.ToString();

            calculo = 0;

            for(int i = 0; i <= 11; i++){

                calculo += int.Parse(cnpjCalculo[i].ToString() ) * v2[i];
            }
            
            resto   = calculo % 11;
            calculo = 11 - resto;

            if(resto < 2){
                digito_v2 = "0";
            }else{
                digito_v2 = calculo.ToString();
            }

            if( digito_v2 == cnpjUsuario[12].ToString() ){
                resultado = true;
            }

            return resultado;
        }
    }
}
}