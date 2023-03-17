using employee_task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(50);
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender
            {
                GenderId = 1,
                Name = "Male"
            });
            genders.Add(new Gender
            {
                GenderId = 2,
                Name = "Female"
            });
            builder.HasData(genders);
        }
    }
}
