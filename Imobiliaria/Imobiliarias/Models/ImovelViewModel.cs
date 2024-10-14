using Imobiliaria.Dominio.ModuloImovel;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Imobiliarias.Models

{
    public enum TipoDeNegocio
    {
        Venda = 0,
        Aluguel = 1
    }
    public enum TipoImovel
    {
        Casa = 0,
        Terreno = 1,
        Apartamento = 2
    }
    public class ImovelViewModel
    {
        public ImovelViewModel()
        {
            Fotos = new List<string>();
        }
        public int ImovelId { get; set; }

        public string Endereco { get; set; } = null!;

        public TipoImovel Tipo { get; set; }

        public decimal Area { get; set; }

        public decimal Valor { get; set; }

        public string? Descricao { get; set; }

        public TipoDeNegocio Negocio { get; set; }
        [Display(Name = "Corretor da venda")]
        public string NomeCorretorNegociador { get; set; }
		[Display(Name = "Corretor gestor")]
		public string NomeCorretorGestor { get; set; }
		[Display(Name = "Cliente dono")]

		public string NomeClienteDono { get; set; }

        public bool Disponivel { get; set; }

        public List <string>? Fotos { get; set; }
        public string FotoDeCapa { get; set; }
        public string Titulo { get; set; }

    }
    public class CreateImovelViewModel
    {
        public string Endereco { get; set; } = null!;

        public int Tipo { get; set; }

        public decimal Area { get; set; }

        public decimal Valor { get; set; }

        public string? Descricao { get; set; }

        public int Negocio { get; set; }
        public int? CorretorNegocioId { get; set; }
        [Required]
        public int CorretorGestorId { get; set; }
        [Required]

        public int ClienteDonoId { get; set; }

        public bool Disponivel { get; set; }

        public string? Fotos { get; set; }
        public List<IFormFile> ArquivosFotos { get; set; }

    }
    
    public class EditarImovelViewModel
    {
        public int ImovelId { get; set; }

        public string Endereco { get; set; } = null!;

        public int Tipo { get; set; }

        public decimal Area { get; set; }

        public decimal Valor { get; set; }

        public string? Descricao { get; set; }

        public int Negocio { get; set; }
        [Required]
        public int? CorretorNegocioId { get; set; }
        [Required]
        public int CorretorGestorId { get; set; }
        [Required]

        public int ClienteDonoId { get; set; }

        public bool Disponivel { get; set; }

        public List <string>? Fotos { get; set; }
        public string FotoDeCapa { get; set; }

    }

    public static class ImovelViewModelExtends {
        public static ImovelViewModel ToImovelViewModel(this Imovel imovel)
        {
            ImovelViewModel imovelViewModel = new ImovelViewModel();
            string fotoDeCapa = "/imagens/not-found.png";
			if (string.IsNullOrEmpty(imovel.Fotos) is false)
			{
                imovelViewModel.Fotos =  JsonConvert.DeserializeObject<List<string>>(imovel.Fotos);
				fotoDeCapa = imovelViewModel.Fotos.First();
			}
            imovelViewModel.ImovelId = imovel.ImovelId;
            imovelViewModel.Endereco = imovel.Endereco;
            imovelViewModel.Tipo = (TipoImovel)imovel.Tipo;
            imovelViewModel.Area = imovel.Area;
            imovelViewModel.Valor = imovel.Valor;
            imovelViewModel.Descricao = imovel.Descricao;
            imovelViewModel.Negocio = (TipoDeNegocio)imovel.Negocio;
            imovelViewModel.NomeCorretorGestor = imovel.CorretorGestor.Nome;
            imovelViewModel.NomeCorretorNegociador = imovel.CorretorNegocio.Nome;
            imovelViewModel.NomeClienteDono = imovel.ClienteDono.Nome;
            imovelViewModel.Disponivel = imovel.Disponivel;
            imovelViewModel.FotoDeCapa = fotoDeCapa;
            imovelViewModel.Titulo = Enum.GetName(imovelViewModel.Tipo);
        
            return imovelViewModel;
        }
        public static EditarImovelViewModel ToImovelViewModelEditar(this Imovel imovel)
        {
            EditarImovelViewModel imovelViewModel = new EditarImovelViewModel();
            string fotoDeCapa = "/imagens/not-found.png";
            if (string.IsNullOrEmpty(imovel.Fotos) is false)
            {
                imovelViewModel.Fotos = JsonConvert.DeserializeObject<List<string>>(imovel.Fotos);
                fotoDeCapa = imovelViewModel.Fotos.First();
            }
            imovelViewModel.ImovelId = imovel.ImovelId;
            imovelViewModel.Endereco = imovel.Endereco;
            imovelViewModel.Tipo = imovel.Tipo;
            imovelViewModel.Area = imovel.Area;
            imovelViewModel.Valor = imovel.Valor;
            imovelViewModel.Descricao = imovel.Descricao;
            imovelViewModel.Negocio = imovel.Negocio;
            imovelViewModel.CorretorGestorId = imovel.CorretorGestorId;
            imovelViewModel.CorretorNegocioId = imovel.CorretorNegocioId;
            imovelViewModel.ClienteDonoId = imovel.ClienteDonoId;
            imovelViewModel.Disponivel = imovel.Disponivel;
            imovelViewModel.FotoDeCapa = fotoDeCapa;

            return imovelViewModel;
        }
        public static List<ImovelViewModel> ToImoveisViewModelList(this List<Imovel> imovels)
        {
            List<ImovelViewModel> imovelsViewModel = new List<ImovelViewModel>();
            foreach (Imovel imovel in imovels)
            {
                ImovelViewModel imovelViewModel = imovel.ToImovelViewModel();
                imovelsViewModel.Add(imovelViewModel);
            }
            return imovelsViewModel;
        }

        public static Imovel ToimovelModel(this CreateImovelViewModel imovelViewModel)
        {
            Imovel imovel = new Imovel();
            imovel.Endereco = imovelViewModel.Endereco;
            imovel.Tipo = imovelViewModel.Tipo;
            imovel.Area = imovelViewModel.Area;
            imovel.Valor = imovelViewModel.Valor;
            imovel.Descricao = imovelViewModel.Descricao;
            imovel.Negocio = imovelViewModel.Negocio;
            imovel.CorretorNegocioId = imovelViewModel.CorretorNegocioId;
            imovel.CorretorGestorId = imovelViewModel.CorretorGestorId;
            imovel.ClienteDonoId = imovelViewModel.ClienteDonoId;
            imovel.Disponivel = imovelViewModel.Disponivel;
            imovel.Fotos = imovelViewModel.Fotos;
            return imovel;
        }

        public static Imovel ToImovelModel(this EditarImovelViewModel imovelViewModel)
        {
            Imovel imovel = new Imovel();
            string fotoDeCapa = "/imagens/not-found.png";
            if (string.IsNullOrEmpty(imovel.Fotos) is false)
            {
                imovelViewModel.Fotos = JsonConvert.DeserializeObject<List<string>>(imovel.Fotos);
                fotoDeCapa = imovelViewModel.Fotos.First();
            }
            imovel.ImovelId = imovelViewModel.ImovelId;
            imovel.Endereco = imovelViewModel.Endereco;
            imovel.Tipo = imovelViewModel.Tipo;
            imovel.Area = imovelViewModel.Area;
            imovel.Valor = imovelViewModel.Valor;
            imovel.Descricao = imovelViewModel.Descricao;
            imovel.Negocio = imovelViewModel.Negocio;
            imovel.CorretorNegocioId = imovelViewModel.CorretorNegocioId;
            imovel.CorretorGestorId = imovelViewModel.CorretorGestorId;
            imovel.ClienteDonoId = imovelViewModel.ClienteDonoId;
            imovel.Disponivel = imovelViewModel.Disponivel;
            imovelViewModel.FotoDeCapa = fotoDeCapa;
            return imovel;
        }
    }

}
