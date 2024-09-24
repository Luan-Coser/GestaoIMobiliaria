using Imobiliarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imobiliaria.Dominio.ModuloCliente
{
    public interface IServiceCliente
    {
        void CriarCliente(Cliente cliente);
        List<Cliente> TrazerClientes();
        void SalvarCliente (Cliente cliente);
        Cliente TragaClienteId(int Id);
        void RemoverCliente(int Id);
    }

	public class ServiceCliente : IServiceCliente
	{
		private IClienteRepositorio _clienteRepositorio { get; }

		public ServiceCliente(IClienteRepositorio clienteRepositorio)
        {
			_clienteRepositorio = clienteRepositorio;
		}


		public void CriarCliente(Cliente cliente)
		{
			ValidarDados(cliente);

			_clienteRepositorio.CriarCliente(cliente);
			
		}

		public void RemoverCliente(int Id)
		{
			_clienteRepositorio.RemoverCliente(Id);
		}

		public void SalvarCliente(Cliente cliente)
		{
			ValidarDados(cliente);
			_clienteRepositorio.SalvarCliente(cliente);
		}

		private void ValidarDados(Cliente cliente)
		{
			bool existeClienteCpf = _clienteRepositorio.ClienteCpf(cliente.Cpf, cliente.ClienteId);


			bool existeClienteEmail = _clienteRepositorio.ClienteEmail(cliente.Email, cliente.ClienteId);

			if (existeClienteCpf || existeClienteEmail)
			{
				throw new Exception("Cliente com este dado já existe!");
			}
		}

		public Cliente TragaClienteId(int Id)
		{
			return _clienteRepositorio.TragaClienteId(Id);
		}

		public List<Cliente> TrazerClientes()
		{
			return _clienteRepositorio.TrazerClientes();
		}
	}
}
