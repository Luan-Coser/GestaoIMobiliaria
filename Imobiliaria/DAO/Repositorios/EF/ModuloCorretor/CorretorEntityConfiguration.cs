using Imobiliaria.Dominio.ModuloCorretor;
using Imobiliaria.Dominio.ModuloCliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imobiliaria.Dominio.ModuloUsuario;

namespace DAO.Repositorios.EF.ModuloCorretor
{
    public class CorretorEntityConfiguration : IEntityTypeConfiguration<Corretor>
	{
		public void Configure(EntityTypeBuilder<Corretor> builder)
		{
			builder.ToTable("Corretores");
			builder.HasKey(e => e.CorretorId).HasName("PK__Corretor__4878C58FFBA660A3");

			builder.HasIndex(e => e.Cpf, "UQ__Corretor__C1F89731960F9C31").IsUnique();

			builder.HasIndex(e => e.Creci, "UQ__Corretor__C46674094787D8EB").IsUnique();

			builder.Property(e => e.Cpf)
				.HasMaxLength(11)
				.HasColumnName("CPF");
			builder.Property(e => e.Creci)
				.HasMaxLength(20)
				.HasColumnName("CRECI");
			builder.Property(e => e.Email).HasMaxLength(100);
			builder.Property(e => e.Nome).HasMaxLength(100);
			builder.Property(e => e.Telefone).HasMaxLength(20);

			builder
				.HasOne(e => e.Usuario)
				.WithOne(e => e.Corretor)
				.HasForeignKey<Usuario>(e => e.CorretorId)
				.HasConstraintName("FK_Usuario_Corretor");
		}
	}
}
