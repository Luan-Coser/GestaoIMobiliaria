using Imobiliaria.Dominio.ModuloCliente;
using Imobiliarias;

namespace Imobiliaria.Dominio.ModuloCorretor
{
    public interface IServiceCorretor
    {
        void CriarCorretor(Corretor Corretor);
        List<Corretor> TrazerCorretores();
        void SalvarCorretor(Corretor Corretor);
        Corretor TragaCorretorId(int Id);
        void RemoverCorretores(int Id);
    }

	public class ServiceCorretor : IServiceCorretor
	{
		private ICorretorRepositorio _corretorRepositorio { get; }

        public ServiceCorretor(ICorretorRepositorio corretorRepositorio)
        {
			_corretorRepositorio = corretorRepositorio;

		}

		public void CriarCorretor(Corretor corretor)
		{
			ValidarDados(corretor);

			_corretorRepositorio.CriarCorretor(corretor);
		}

		public void RemoverCorretores(int Id)
		{
			_corretorRepositorio.RemoverCorretor(Id);
		}

		public void SalvarCorretor(Corretor corretor)
		{
			ValidarDados(corretor);
			_corretorRepositorio.SalvarCorretor(corretor);
		}


		private void ValidarDados(Corretor corretor)
		{
			bool existeClienteCpf = _corretorRepositorio.CorretorCpf(corretor.Cpf, corretor.CorretorId);


			bool existeClienteCreci = _corretorRepositorio.CorretorCreci(corretor.Creci, corretor.CorretorId);

			if (existeClienteCpf || existeClienteCreci)
			{
				throw new Exception("Corretor com este dado já existe!");
			}
		}

		public Corretor TragaCorretorId(int Id)
		{
			return _corretorRepositorio.TragaCorretorId(Id);
		}

		public List<Corretor> TrazerCorretores()
		{
			return _corretorRepositorio.TrazerCorretores();
		}
	}



}
