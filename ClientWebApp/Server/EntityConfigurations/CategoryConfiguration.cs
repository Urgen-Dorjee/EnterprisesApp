namespace ClientWebApp.Server.EntityConfigurations;


public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {

        builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
        builder.Property(e => e.CategoryName).IsRequired().HasMaxLength(15);
        builder.Property(e => e.Description).HasColumnType("varchar");
        builder.Property(e => e.Picture).HasColumnType("image");
    }
}


