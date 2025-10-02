using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UPXV.Models;

namespace UPXV.Data.Mappings;

public class ConsumableMapping : IEntityTypeConfiguration<Consumable>
{
   public void Configure (EntityTypeBuilder<Consumable> builder)
   {
      builder.HasKey(c => c.Nid);
      builder.HasIndex(c => c.Tid).IsUnique();

      builder.HasMany(c => c.Tags)
         .WithMany();

      builder.HasOne(c => c.Unit)
         .WithMany()
         .HasForeignKey(c => c.UnitNid);
   }
}
