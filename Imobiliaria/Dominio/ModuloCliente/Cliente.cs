using System;
using System.Collections.Generic;
using Imobiliaria.Dominio.ModuloImovel;
using Imobiliaria.Dominio.ModuloUsuario;

namespace Imobiliarias;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string? Telefone { get; set; }

    public string? Email { get; set; }

    public Usuario? Usuario { get; set; }
    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<Imovel> Imoveis { get; set; } = new List<Imovel>();

    public virtual ICollection<MensagensContato> MensagensContatos { get; set; } = new List<MensagensContato>();
}
