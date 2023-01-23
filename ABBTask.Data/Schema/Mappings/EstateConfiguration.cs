using ABBTask.Data.Schema.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABBTask.Data.Schema.Mappings
{
    public class EstateConfiguration : IEntityTypeConfiguration<Estate>
    {
        public void Configure(EntityTypeBuilder<Estate> builder)
        {
            builder.ToTable("Estate")
                .HasKey(e => e.Id);

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(400)")
                .IsRequired();

            builder.Property(e => e.Price)
                .HasColumnName("Price")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ExpirationDate)
                .HasColumnName("ExpirationDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.ModificationDate)
                .HasColumnName("ModificationDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.IsSold)
                .HasColumnName("IsSold")
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}