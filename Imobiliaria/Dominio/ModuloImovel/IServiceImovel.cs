using Imobiliaria.Dominio.ModuloImovel;

namespace Imobiliaria.Dominio.ModuloImovel
{
    public interface IServiceImovel
    {
        int CriarImovel(Imovel Imovel);
        List<Imovel> TrazerImoveis();
        void SalvarImovel(Imovel Imovel);
        Imovel TragaImovelId(int Id);
        void RemoverImovel(int Id);
    }


    public class ServiceImovel : IServiceImovel
    {
        private IImovelRepositorio _imovelRepositorio { get; }
        public ServiceImovel(IImovelRepositorio imovel) {
			_imovelRepositorio = imovel;
        }

		public int CriarImovel(Imovel imovel)
		{
			_imovelRepositorio.CriarImovel(imovel);
			return imovel.ImovelId;
		}

		public List<Imovel> TrazerImoveis()
		{
			return _imovelRepositorio.TrazerImoveis();
		}

		public void SalvarImovel(Imovel imovel)
		{
			_imovelRepositorio.SalvarImovel(imovel);
		}

		public Imovel TragaImovelId(int Id)
		{
			return _imovelRepositorio.TragaImovelId(Id);
		}

		public void RemoverImovel(int Id)
		{
			_imovelRepositorio.RemoverImovel(Id);
		}
	}
}
