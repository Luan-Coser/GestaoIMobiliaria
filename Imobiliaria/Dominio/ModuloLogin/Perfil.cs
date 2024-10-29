namespace Imobiliaria.Dominio.ModuloLogin
{
    public class Perfil
    {
        public int PerfilId { get; set; }
        public string Nome { get; set; }

        //Mapeamento 
        public virtual List<Usuario> Usuarios { get; set; }

    }
}
