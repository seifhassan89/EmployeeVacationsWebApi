using employee_task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(50);

            List<Role> Roles = new List<Role>();
            Roles.Add(new Role
            {
                RoleId = (int)RoleEnum.Employee,
                Name = "Employee"
            });
            Roles.Add(new Role
            {
                RoleId = (int)RoleEnum.HR_Manger,
                Name = "HR Manger"
            });
            builder.HasData(Roles);
        }
    }
}
