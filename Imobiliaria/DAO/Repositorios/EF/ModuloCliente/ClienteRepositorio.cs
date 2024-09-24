using Imobiliaria.Dominio.ModuloCliente;
using Imobiliarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Repositorios.EF.ModuloCliente
{
	public class ClienteRepositorio : IClienteRepositorio
	{
		public ImobiliariaDbContext _context { get; }

		public ClienteRepositorio(ImobiliariaDbContext context)
		{
			_context = context;
		}


		public bool ClienteCpf(string cpf, int id)
		{
			var cliente = _context.Clientes.FirstOrDefault(m => m.ClienteId != id && string.Compare(m.Cpf, cpf) == 0);
			if (cliente == null)
			{
				return false;
			}
			return true;
		}

		public bool ClienteEmail(string email, int id)
		{
			var cliente = _context.Clientes.FirstOrDefault(m => m.ClienteId != id && string.Compare(m.Email, email) == 0);
			if (cliente == null)
			{
				return false;
			}
			return true;
		}

		public void CriarCliente(Cliente cliente)
		{
			_context.Add(cliente);
			_context.SaveChanges();
		}

		public void RemoverCliente(int id)
		{

			var cliente = _context.Clientes.Find(id);

			if (cliente != null)
			{
				_context.Clientes.Remove(cliente);
			}
			_context.SaveChanges();
		}

		public void SalvarCliente(Cliente cliente)
		{

			_context.Update(cliente);
			_context.SaveChanges();
		}

		public Cliente TragaClienteId(int id)
		{
			var cliente = _context.Clientes.FirstOrDefault(m => m.ClienteId == id);
			return cliente;
		}


		public List<Cliente> TrazerClientes()
		{
			var clientes = _context.Clientes.ToList();
   	        return clientes;
		}
	}
}
