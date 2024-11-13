namespace Imobiliaria.Dominio.ModuloUsuario
{
    public class Perfil
    {
        public int PerfilId { get; set; }
        public string Nome { get; set; }

        //Mapeamento 
        public virtual List<Usuario> Usuarios { get; set; }
        public static IEnumerable<Perfil> Perfis { get; set; }

    }
}
