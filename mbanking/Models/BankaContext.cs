namespace WebTemplate.Models;

public class BankaContext : DbContext
{
    public DbSet<Korisnik> Korisnici { get; set;}
    public DbSet<Racun> Racuni { get; set; }
    public DbSet<Transakcija> Transakcije { get; set; }
    public BankaContext(DbContextOptions options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Korisnik>()
        .HasOne(k => k.Racun)
        .WithOne(r => r.Korisnik)
        .HasForeignKey<Korisnik>(k => k.RacunId)
        .OnDelete(DeleteBehavior.SetNull);
}
}
