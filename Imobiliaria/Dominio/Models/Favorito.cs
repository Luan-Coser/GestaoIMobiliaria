using System;
using System.Collections.Generic;
using Imobiliaria.Dominio.ModuloImovel;

namespace Imobiliarias;

public partial class Favorito
{
    public int FavoritoId { get; set; }

    public int ClienteId { get; set; }

    public int ImovelId { get; set; }

    public DateOnly DataAdicionado { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Imovel Imovel { get; set; } = null!;
}
