using ITI.Enterprise.InterviewTask.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.Enterprise.InterviewTask.DataModel.EntityConfigurations
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.Price).IsRequired();

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Company).WithMany(c => c.Products).HasForeignKey(p => p.CompanyId).IsRequired();

           

        }
    }
}
