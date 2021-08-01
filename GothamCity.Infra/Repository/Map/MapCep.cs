using GothamCity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GothamCity.Infra.Repository.Map
{
    public class MapCep : IEntityTypeConfiguration<Cep>
    {
        public void Configure(EntityTypeBuilder<Cep> builder)
        {
            builder.ToTable("Cep");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.CodigoCep).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Cidade).HasMaxLength(150).IsRequired();
        }
    }
}
