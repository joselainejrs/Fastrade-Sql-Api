using System.ComponentModel.DataAnnotations;

namespace backend.ViewModels {
    public class LoginViewModel {

       [Required]
        public string Email { get; set; }

        [Required]
        [StringLength (255)]
        public string Senha { get; set; }
    }
}