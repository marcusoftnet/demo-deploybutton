using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
  public SuperHeroController(SuperHeroContext context)
  {
    this.context = context;
  }
  private readonly SuperHeroContext context;

  [HttpGet]
  public async Task<ActionResult<List<SuperHero>>> Get()
  {
    return Ok(await context.SuperHeroes.ToListAsync());
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<SuperHero>> Get(int id)
  {
    var hero = await context.SuperHeroes.FindAsync(id);
    if (hero == null)
      return NotFound($"Hero with id '{id}' not found");
    return Ok(hero);
  }

  [HttpPost]
  public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
  {
    context.SuperHeroes.Add(hero);
    await context.SaveChangesAsync();
    return Ok(await context.SuperHeroes.ToListAsync());
  }

  [HttpPut]
  public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
  {
    var hero = await context.SuperHeroes.FindAsync(request.Id);
    if (hero == null)
      return NotFound($"Hero with id '{request.Id}' not found");

    hero.Name = request.Name;
    hero.FirstName = request.FirstName;
    hero.LastName = request.LastName;
    hero.Place = request.Place;

    await context.SaveChangesAsync();

    return Ok(await context.SuperHeroes.ToListAsync());
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<List<SuperHero>>> Delete(int id)
  {
    var hero = await context.SuperHeroes.FirstOrDefaultAsync(hero => hero.Id == id);
    if (hero == null)
      return NotFound($"Hero with id '{id}' not found");

    context.SuperHeroes.Remove(hero);
    await context.SaveChangesAsync();

    return Ok(await context.SuperHeroes.ToListAsync());
  }

}