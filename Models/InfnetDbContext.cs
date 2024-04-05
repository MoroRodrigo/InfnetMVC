namespace InfnetMVC.Models
{
    // InfnetDbContext.cs
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    namespace InfnetMVC.Models
    {
        public class InfnetDbContext : DbContext
        {
            public InfnetDbContext(DbContextOptions<InfnetDbContext> options) : base(options)
            {
            }

            public DbSet<Funcionario> Funcionarios { get; set; }
            public DbSet<Departamento> Departamentos { get; set; }
        }
    }

}
