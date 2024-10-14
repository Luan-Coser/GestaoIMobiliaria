using Imobiliaria.Dominio.ModuloImovel;
using Imobiliarias;
using Microsoft.EntityFrameworkCore;

namespace DAO.Repositorios.EF.ModuloImovel
{
	public class ImovelRepositorio : IImovelRepositorio
	{

		public ImobiliariaDbContext _context { get; }

        public ImovelRepositorio(ImobiliariaDbContext context)
        {
			_context = context;

		}
		public void CriarImovel(Imovel imovel)
		{
			_context.Add(imovel);
			_context.SaveChanges();
		}

		public void RemoverImovel(int id)
		{
			var imovel = _context.Imoveis.Find(id);

			if (imovel != null)
			{
				_context.Imoveis.Remove(imovel);
			}
			_context.SaveChanges();
		}

		public void SalvarImovel(Imovel imovel)
		{
			_context.Update(imovel);
			_context.SaveChanges();
		}

		public Imovel TragaImovelId(int id)
		{

			var imovel = _context.Imoveis.Include(i => i.ClienteDono).Include(i => i.CorretorGestor).Include(i => i.CorretorNegocio). FirstOrDefault(m => m.ImovelId == id);
			return imovel;
		}

		public List<Imovel> TrazerImoveis()
		{
            var imoveis = _context.Imoveis.Include(i => i.ClienteDono).Include(i => i.CorretorGestor).Include(i => i.CorretorNegocio).ToList();
			return imoveis;
		}
	}
}
