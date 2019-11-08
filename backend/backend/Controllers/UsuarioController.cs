using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        UsuarioRepository _Repositorio = new UsuarioRepository ();

        UploadImageRepository _Upload = new UploadImageRepository ();

        //Get: Api/Produtoreceita
        /// <summary>
        /// Aqui são todos os usuarios
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        [HttpGet]
        // [Authorize(Roles = "3")]
        public async Task<ActionResult<List<Usuario>>> Get () {

            var usuarios = await _Repositorio.Listar ();
            if (usuarios == null) {
                return NotFound ();
            }

            // usuario.EmaiL = null;
            // usuarios.Senha = null;

            return usuarios;

        }
        //Get: Api/Produtoreceita
        /// <summary>
        /// Mostramos os dados de um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unico ID de um usuario</returns>
        [HttpGet ("{id}")]
        [Authorize (Roles = "3")]
        public async Task<ActionResult<Usuario>> Get (int id) {
            var usuario = await _Repositorio.BuscarPorID (id);

            if (usuario == null) {
                return NotFound ();
            }
            return usuario;
        }
        //Post: Api/Usuario
        /// <summary>
        /// Enviamos dados de um usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Envia dados de um usuario</returns>
        [HttpPost]
        // [Authorize (Roles = "3")]
        public async Task<ActionResult<Usuario>> Post ([FromForm] Usuario usuario) {
            try {
                usuario.IdEndereco = Convert.ToInt32 (Request.Form["IdEndereco"]);
                usuario.IdTipoUsuario = Convert.ToInt32 (Request.Form["IdTipoUsuario"]);
                usuario.NomeRazaoSocial = Request.Form["NomeRazaoSocial"];
                usuario.CpfCnpj = Request.Form["CpfCnpj"];


                    // Remoção de caracteres especifico
                 usuario.CpfCnpj =  usuario.CpfCnpj.Replace ("", "");
                 usuario.CpfCnpj =  usuario.CpfCnpj.Replace ("/", "");
                 usuario.CpfCnpj =  usuario.CpfCnpj.Replace ("-", "");
                 usuario.CpfCnpj =  usuario.CpfCnpj.Replace (".", "");

                usuario.Email = Request.Form["Email"];
                usuario.Senha = Request.Form["Senha"];
                usuario.Celular = Request.Form["Celular"];

                await _Repositorio.Salvar (usuario);

                try {
                    var arquivo = Request.Form.Files[0];
                    if (arquivo != null) {
                        // Entrar aqui se o usuario enviar uma imagem no form.
                        usuario.FotoUrlUsuario = _Upload.Upload (arquivo, "Usuarios");
                    }

                    if (usuario.IdTipoUsuario == 1) {
                        if (_Repositorio.ValidaCPF (usuario.CpfCnpj)) {
                            await _Repositorio.Salvar (usuario);
                        } else {
                            return BadRequest ("O CPF digitado está incorreto");
                        }
                    }

                    if (usuario.IdTipoUsuario == 2) {
                        if (_Repositorio.ValidaCNPJ (usuario.CpfCnpj)) {
                            await _Repositorio.Salvar (usuario);
                        } else {
                            return BadRequest ("O CNPJ digitado está incorreto");
                        }
                    }

                } catch {
                    IFormFile arquivo = null;
                    usuario.FotoUrlUsuario = _Upload.Upload (arquivo, "Usuarios");
                }

            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return usuario;
        }
        //Put: Api/Usuario
        /// <summary>
        /// Alteramos dados de um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns>Envia dados de um usuario</returns>
        [HttpPut ("{id}")]
        // [Authorize]
        public async Task<ActionResult> Put (int id, Usuario usuario) {
            if (id != usuario.IdUsuario) {

                return BadRequest ();
            }
            _contexto.Entry (usuario).State = EntityState.Modified;
            try {
                var arquivo = Request.Form.Files[0];
                usuario.IdUsuario = Convert.ToInt32 (Request.Form["IdUsuario"]);
                usuario.IdEndereco = Convert.ToInt32 (Request.Form["IdEndereco"]);
                usuario.IdTipoUsuario = Convert.ToInt32 (Request.Form["IdTipoUsuario"]);
                usuario.NomeRazaoSocial = Request.Form["NomeRazaoSocial"];
                usuario.CpfCnpj = Request.Form["CpfCnpj"];
                usuario.Email = Request.Form["Email"];
                usuario.Senha = Request.Form["Senha"];
                usuario.Celular = Request.Form["Celular"];
                usuario.FotoUrlUsuario = _Upload.Upload (arquivo, "Usuarios");
                await _Repositorio.Alterar (usuario);
            } catch (DbUpdateConcurrencyException) {
                var usuario_valido = await _Repositorio.BuscarPorID (id);

                if (usuario_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            return NoContent ();
        }
        // DELETE api/Usuario/id
        /// <summary>
        /// Excluimos dados de um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Excluir dados de um usuario</returns>
        [HttpDelete ("{id}")]
        // [Authorize (Roles = "3")]
        public async Task<ActionResult<Usuario>> Delete (int id) {

            var usuario = await _Repositorio.BuscarPorID (id);
            if (usuario == null) {
                return NotFound ();
            }

            await _Repositorio.Excluir (usuario);

            return usuario;
        }
    }
}