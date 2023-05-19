using Microsoft.EntityFrameworkCore;
using SimApi.Data.Domain;

namespace SimApi.Data.Context;

public class SimDbContext : DbContext
{
    public SimDbContext(DbContextOptions<SimDbContext> options) : base(options)
    {

    }

    // dbset

    public DbSet<Staff> Staff { get; set; }



}
