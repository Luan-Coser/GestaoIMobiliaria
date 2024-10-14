using Imobiliaria.Dominio.ModuloCorretor;

namespace Imobiliaria.Dominio.ModuloImovel
{
    public interface IImovelRepositorio
    {
        void CriarImovel(Imovel imovel);
        List<Imovel> TrazerImoveis();
        void SalvarImovel(Imovel imovel);
		Imovel TragaImovelId(int id);
        void RemoverImovel(int id);
	}
}