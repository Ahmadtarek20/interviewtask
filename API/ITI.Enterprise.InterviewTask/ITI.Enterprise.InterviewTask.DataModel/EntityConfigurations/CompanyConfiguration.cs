using System;
using System.Collections.Generic;
using System.Text;
using ITI.Enterprise.InterviewTask.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.Enterprise.InterviewTask.DataModel.EntityConfigurations
{
    public class CompanyConfiguration: IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");
            builder.Property(c => c.Name).IsRequired();
            builder.HasKey(c => c.Id);
        }
    }
}
