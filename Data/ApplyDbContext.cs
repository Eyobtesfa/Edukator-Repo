using Edukator_2.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Edukator_2.Data
{
    public class ApplyDbContext : DbContext
    {
        public ApplyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}
