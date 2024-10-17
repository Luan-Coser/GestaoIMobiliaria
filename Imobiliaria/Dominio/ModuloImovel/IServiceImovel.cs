using Imobiliaria.Dominio.ModuloImovel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Reflection;

namespace Imobiliaria.Dominio.ModuloImovel
{
    public interface IServiceImovel
    {
        int CriarImovel(Imovel Imovel);
        List<Imovel> TrazerImoveis();
        void SalvarImovel(Imovel Imovel);
        Imovel TragaImovelId(int Id);
        void RemoverImovel(int Id);
	    void UploadImg(int Id, List<IFormFile> arquivoFoto);
		void RemoveImg(int Id, List<string> fotos);
	}


    public class ServiceImovel : IServiceImovel
    {
        private IImovelRepositorio _imovelRepositorio { get; }
        public ServiceImovel(IImovelRepositorio imovel) {
			_imovelRepositorio = imovel;
        }

		public int CriarImovel(Imovel imovel)
		{
			_imovelRepositorio.CriarImovel(imovel);
			return imovel.ImovelId;
		}

		public List<Imovel> TrazerImoveis()
		{
			return _imovelRepositorio.TrazerImoveis();
		}

		public void SalvarImovel(Imovel imovel)
		{
			_imovelRepositorio.SalvarImovel(imovel);
		}

		public Imovel TragaImovelId(int Id)
		{
			return _imovelRepositorio.TragaImovelId(Id);
		}

		public void RemoverImovel(int Id)
		{
			_imovelRepositorio.RemoverImovel(Id);
		}

		public void UploadImg(int Id, List<IFormFile> fotos)
		{
			if (fotos is not null)
			{
				var imovelDB = TragaImovelId(Id);
				var fotosUrls = new List<string>();       
			    if (string.IsNullOrEmpty(imovelDB.Fotos) is false)
				{
					fotosUrls = JsonConvert.DeserializeObject<List<string>>(imovelDB.Fotos);
				}
				foreach (var item in fotos)
				{
					var fileVirtualPath = $"fotos/{Id}/";
					var directoryImovel = Path.Combine("wwwroot", fileVirtualPath);
					if (Directory.Exists(directoryImovel) is false)
					{
						Directory.CreateDirectory(directoryImovel);
					}
					fileVirtualPath += "/" + item.FileName;
					var filePath = Path.Combine(directoryImovel, item.FileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						item.CopyTo(stream);
					}
					fotosUrls.Add("/" + fileVirtualPath);
				}
				imovelDB.Fotos = JsonConvert.SerializeObject(fotosUrls);
				SalvarImovel(imovelDB);
			}
		}

		public void RemoveImg(int Id, List<string> fotos)
		{
			if (fotos is null || fotos.Count == 0) {  return; }
			var imovelDB = TragaImovelId(Id); 
			var fotosUrls = new List<string>();
			if (string.IsNullOrEmpty(imovelDB.Fotos) is false)
			{
				fotosUrls = JsonConvert.DeserializeObject<List<string>>(imovelDB.Fotos);
			}
			foreach (var item in fotos)
			{
				var filePath = "wwwroot" + "\\" + item;
				System.IO.File.Delete(filePath);
				fotosUrls.Remove(item);
			}
			imovelDB.Fotos = JsonConvert.SerializeObject(fotosUrls);
			SalvarImovel(imovelDB);
		}


	}
}
