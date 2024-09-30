using Imobiliarias;

namespace Imobiliaria.Dominio.ModuloCorretor
{
	public interface ICorretorRepositorio
	{
		void CriarCorretor(Corretor corretor);
		List<Corretor> TrazerCorretores();
		void SalvarCorretor(Corretor corretor);
		Corretor TragaCorretorId(int id);
		void RemoverCorretor(int id);
		bool CorretorCpf(string cpf, int id);
		bool CorretorCreci(string creci, int id);
	}
}