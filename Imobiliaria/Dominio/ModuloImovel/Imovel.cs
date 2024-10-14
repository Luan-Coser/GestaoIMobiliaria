using System;
using System.Collections.Generic;
using Imobiliaria.Dominio.ModuloCorretor;
using Imobiliarias;

namespace Imobiliaria.Dominio.ModuloImovel;

public partial class Imovel
{
    public int ImovelId { get; set; }

    public string Endereco { get; set; } = null!;

    public int Tipo { get; set; }

    public decimal Area { get; set; }

    public decimal Valor { get; set; }

    public string? Descricao { get; set; }

    public int Negocio { get; set; }

    public int? CorretorNegocioId { get; set; }

    public int CorretorGestorId { get; set; }

    public int ClienteDonoId { get; set; }

    public bool Disponivel { get; set; }

    public string? Fotos { get; set; }

    public virtual Cliente ClienteDono { get; set; } = null!;

    public virtual Corretor CorretorGestor { get; set; } = null!;

    public virtual Corretor? CorretorNegocio { get; set; }

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<MensagensContato> MensagensContatos { get; set; } = new List<MensagensContato>();
}
