using Imobiliaria.Dominio.ModuloCorretor;
using Imobiliarias;

namespace Imobiliaria.Dominio.ModuloUsuario
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public DateTime DataCriacao { get; set; }

        //Mapeamento
        public int? CorretorId { get; set; }
        public int? ClienteId { get; set; }
        public int PerfilId { get; set; }

        public Cliente? Cliente { get; set; }
        public Corretor? Corretor { get; set; }
        public Perfil Perfil { get; set; }
    }
}
