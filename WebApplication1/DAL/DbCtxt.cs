using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
//this class is used to connect to the database written in PostgresSQL
namespace WebApplication1.DAL;
public class DbCtxt : DbContext
{
    public virtual DbSet<Models.Teacher> Teacher { get; set; }
    public DbCtxt(DbContextOptions<DbCtxt> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.Teacher>(entity =>
        {
            entity.HasKey(e => e.tid);
            entity.Property(e => e.name).IsRequired();
            entity.Property(e => e.email).IsRequired();
            entity.Property(e => e.password).IsRequired();
        });
    }


}