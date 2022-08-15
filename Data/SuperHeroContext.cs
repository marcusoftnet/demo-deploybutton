using Microsoft.EntityFrameworkCore;

public class SuperHeroContext : DbContext
{
  public SuperHeroContext(DbContextOptions<SuperHeroContext> options) : base(options) { }

  public DbSet<SuperHero> SuperHeroes { get; set; }
}