namespace Imobiliaria.Dominio.ModuloLogin
{
	public interface IServiceModel<T>
	{
		void Criar(T model);
		List<T> TragaTodos();
		void Salvar(T model);
		T TragaPorId(int id);
		void Remover(int id);
	}
}
