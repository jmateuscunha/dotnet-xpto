using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Xpto.Domain.Entities;

namespace Xpto.Infra.Database.Relational.Mappings;

public class AssetMapping : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedNever().HasColumnType("uuid").IsRequired();
        builder.Property(x => x.Address).HasColumnName("name").IsRequired();
        builder.Property(x => x.WalletId).HasColumnName("wallet_id").IsRequired();
        builder.Property(x => x.BlockchainId).HasColumnName("blockchain_id").IsRequired();

        builder.HasOne(x => x.Wallet).WithMany(a => a.Assets).HasForeignKey(w => w.WalletId);

        builder.ToTable("asset");
    }
}
