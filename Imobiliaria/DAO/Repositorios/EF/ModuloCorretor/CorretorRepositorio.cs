using Imobiliaria.Dominio.ModuloCorretor;
using Imobiliarias;

namespace DAO.Repositorios.EF.ModuloCorretor
{
	public class CorretorRepositorio : ICorretorRepositorio
	{
		public ImobiliariaDbContext _context { get; }

        public CorretorRepositorio(ImobiliariaDbContext context)
        {
			_context = context;
				 
		}
        public bool CorretorCpf(string cpf, int id)
		{

			var corretor = _context.Corretores.FirstOrDefault(m => m.CorretorId != id && string.Compare(m.Cpf, cpf) == 0);
			if (corretor == null)
			{
				return false;
			}
			return true;
		}

		public bool CorretorCreci(string creci, int id)
		{

			var corretor = _context.Corretores.FirstOrDefault(m => m.CorretorId != id && string.Compare(m.Creci, creci) == 0);
			if (corretor == null)
			{
				return false;
			}
			return true;
		}

		public void CriarCorretor(Corretor corretor)
		{
			_context.Add(corretor);
			_context.SaveChanges();
		}

		public void RemoverCorretor(int id)
		{
			var corretor = _context.Clientes.Find(id);

			if (corretor != null)
			{
				_context.Clientes.Remove(corretor);
			}
			_context.SaveChanges();
		}

		public void SalvarCorretor(Corretor corretor)
		{
			_context.Update(corretor);
			_context.SaveChanges();
		}

		public Corretor TragaCorretorId(int id)
		{
			var corretor = _context.Corretores.FirstOrDefault(m => m.CorretorId == id);
			return corretor;
		}

		public List<Corretor> TrazerCorretores()
		{
			var corretor = _context.Corretores.ToList();
			return corretor;
		}
	}
}
