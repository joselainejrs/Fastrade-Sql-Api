using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace backend.Repositories {
    public class UploadImageRepository  {
        public string Upload (IFormFile arquivo, string savingFolder) {

            var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), savingFolder);

            if (arquivo.Length > 0) {
                var fileName = ContentDispositionHeaderValue.Parse (arquivo.ContentDisposition).FileName.Trim ('"');
                var fullPath = Path.Combine (pathToSave, fileName);

                using (var stream = new FileStream (fullPath, FileMode.Create)) {
                    arquivo.CopyTo (stream);
                }

                return fullPath;
            } else {
                return null;
            }
        }
    }
}