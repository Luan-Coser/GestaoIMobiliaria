using Imobiliaria.Dominio.ModuloCliente;
using System.ComponentModel.DataAnnotations;
namespace Imobiliarias.Models

{
	public class ClienteViewModel
    {
        public int ClienteId { get; set; }

        public string Nome { get; set; } = null!;

        public string Cpf { get; set; } = null!;

        public string? Telefone { get; set; }

        public string? Email { get; set; }
    }

    public class CreateClienteViewModel 
    {
        [Required]
        public string Nome { get; set; } = null!;
        [Required]
        [MaxLength(11,ErrorMessage = "O tamanho maximo é 11!")]
        [MinLength(11,ErrorMessage = "O tamanho minimo é 11!")]
        public string Cpf { get; set; } = null!;

        public string? Telefone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email incorreto!")]
        public string? Email { get; set; }
    }

    public static class ClienteViewModelExtensions
    {
        public static ClienteViewModel ToClienteViewModel(this Cliente cliente)
        {
            ClienteViewModel clienteViewModel = new ClienteViewModel();
            clienteViewModel.ClienteId = cliente.ClienteId;
            clienteViewModel.Nome = cliente.Nome;
            clienteViewModel.Cpf = cliente.Cpf;
            clienteViewModel.Email = cliente.Email;
            clienteViewModel.Telefone = cliente.Telefone;
            return clienteViewModel;
        }
        public static List<ClienteViewModel> ToClientesViewModelList(this List<Cliente> clientes)
        {
            List<ClienteViewModel> clientesViewModel = new List<ClienteViewModel>();
            foreach (Cliente cliente in clientes)
            {
                ClienteViewModel clienteViewModel = cliente.ToClienteViewModel();
                clientesViewModel.Add(clienteViewModel);
            }
            return clientesViewModel;
        }

        public static Cliente ToClienteModel(this CreateClienteViewModel clienteViewModel)
        {
            Cliente cliente = new Cliente();
            cliente.Nome = clienteViewModel.Nome;
            cliente.Cpf = clienteViewModel.Cpf;
            cliente.Email = clienteViewModel.Email;
            cliente.Telefone = clienteViewModel.Telefone;
            return cliente;
        }

        public static Cliente ToClienteModel(this ClienteViewModel clienteViewModel)
        {
            Cliente cliente = new Cliente();
            cliente.ClienteId = clienteViewModel.ClienteId;
            cliente.Nome = clienteViewModel.Nome;
            cliente.Cpf = clienteViewModel.Cpf;
            cliente.Email = clienteViewModel.Email;
            cliente.Telefone = clienteViewModel.Telefone;
            return cliente;
        }
    }

}
