using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domains
{
    public partial class Endereco
    {
        public Endereco()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        [Column("Id_Endereco")]
        public int IdEndereco { get; set; }
        [Required]
        [Column("Rua_AV")]
        [StringLength(255)]
        public string RuaAv { get; set; }
        public int Numero { get; set; }
        [Required]
        [StringLength(1)]
        public string Complemento { get; set; }
        [Required]
        [Column("CEP")]
        [StringLength(9)]
        public string Cep { get; set; }
        [Required]
        [StringLength(255)]
        public string Bairro { get; set; }
        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [InverseProperty("IdEnderecoNavigation")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
