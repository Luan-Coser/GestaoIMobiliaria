using Imobiliarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imobiliaria.Dominio.ModuloCliente
{
	public interface IClienteRepositorio
	{
		void CriarCliente(Cliente cliente);
		List<Cliente> TrazerClientes();
		void SalvarCliente(Cliente cliente);
		Cliente TragaClienteId(int id);
		void RemoverCliente(int id);
		bool ClienteCpf(string cpf, int id);
		bool ClienteEmail(string email, int id);
	}

}
