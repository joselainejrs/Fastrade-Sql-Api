using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using backend.Domains;
using backend.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        // Chamamos nosso contexto do banco
        fastradeContext _contexto = new fastradeContext ();

        // Definimos uma variável para percorrer nossos métodos com as configurações obtidas no appsettings.json
        private IConfiguration _config;

        // Definimos um método construtor para poder passar essas configs
        public LoginController (IConfiguration config) {
            _config = config;
        }

        // Chamamos nosso método para validar nosso usuário da aplicação
        public Usuario AuthenticateUser (LoginViewModel login) {
            var usuario = _contexto.Usuario.FirstOrDefault (u => u.Email == login.Email && u.Senha == login.Senha);

            return usuario;

        }

        // Criamos nosso método que vai gerar nosso Token
        private string GenerateJSONWebToken (Usuario userInfo) {
            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config["Jwt:Key"]));
            var credentials = new SigningCredentials (securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new [] {

                // Definimos nossas Claims (dados da sessão) para poderem ser capturadas
                // a qualquer momento enquanto o Token for ativo  
                new Claim (JwtRegisteredClaimNames.NameId, userInfo.NomeRazaoSocial),
                new Claim (JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ()),
                //Define uma Claim atribuindo o perfil do usuário pelo Tipo de usuario
                new Claim (ClaimTypes.Role, userInfo.IdTipoUsuario.ToString ()),
                new Claim ("Role", userInfo.IdTipoUsuario.ToString ())

            };

            // Configuramos nosso Token e seu tempo de vida
            var token = new JwtSecurityToken (_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires : DateTime.Now.AddMinutes (120),
                signingCredentials : credentials);

            return new JwtSecurityTokenHandler ().WriteToken (token);
        }

        // Usamos essa anotação para ignorar a autenticação neste método, já que é ele quem fará isso  
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login ([FromBody] LoginViewModel login) {
            IActionResult response = Unauthorized ();
            var user = AuthenticateUser (login);

            if (user != null) {
                var tokenString = GenerateJSONWebToken (user);
                response = Ok (new { token = tokenString });
            }

            return response;
        }

    }
}