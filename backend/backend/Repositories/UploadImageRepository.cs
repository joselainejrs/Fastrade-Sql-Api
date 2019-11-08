using System.IO;
using System.Net.Http.Headers;
using backend.Domains;
using Microsoft.AspNetCore.Http;

namespace backend.Repositories {
    public class UploadImageRepository {
        public string Upload (IFormFile arquivo, string savingFolder) {

            var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), "ResourceImage", savingFolder);

            if (arquivo != null) {

                var fileName = ContentDispositionHeaderValue.Parse (arquivo.ContentDisposition).FileName.Trim ('"');
                var fullPath = Path.Combine (pathToSave, fileName);
                var folder = Path.Combine( "ResourceImage", savingFolder, fileName);

                using (var stream = new FileStream (fullPath, FileMode.Create)) {
                    arquivo.CopyTo (stream);
                }

                return folder;
            } else {
                //Entrar aqui caso o usuario não coloque nenhuma imagem
                return "ResourceImage\\Usuarios\\AvatarPadrao.png";
            }

        }
    }
}

