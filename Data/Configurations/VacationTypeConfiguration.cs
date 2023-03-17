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
    public class VacationTypeConfiguration : IEntityTypeConfiguration<VacationType>
    {
        public void Configure(EntityTypeBuilder<VacationType> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(50);

            List<VacationType> vacationTypes = new List<VacationType>();
            vacationTypes.Add(new VacationType
            {
                VacationTypeId = 1,
                Name = "Casual",
                IntialValue = 7
            });
            vacationTypes.Add(new VacationType
            {
                VacationTypeId = 2,
                Name = "Schedule",
                IntialValue = 14
            });
            builder.HasData(vacationTypes);
        }
    }
}
