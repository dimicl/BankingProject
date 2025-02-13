namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{
    public BankaContext Context { get; set; }

    public KorisnikController(BankaContext context)
    {
        Context = context;
    }

    [HttpGet]
    [Route("getKorisnici")]
    public async Task<ActionResult> getKorisnici()
    {
        try
        {
            var korisnici = await Context.Korisnici.Include(k=>k.Racun).ToListAsync();
            return Ok(korisnici);
        }
        catch(Exception e)
        {
            return BadRequest("Greska " +  e.ToString());
        }
    } 

    [HttpPost]
    [Route("addKorisnik")]
    public async Task<ActionResult> addKorisnik([FromBody]Korisnik c)
    {
        try
        {
            if(c==null)
                return BadRequest("Clan ne postoji\n");

            if(c.Racun != null){
                Context.Racuni.Add(c.Racun);
                await Context.SaveChangesAsync();
                c.RacunId = c.Racun.id;
            }

            await Context.Korisnici.AddAsync(c);
            await Context.SaveChangesAsync();
            return Ok("Uspesno dodat clan\n");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);            
        }

    }    

    [HttpPost]
    [Route("getKorisnik")]
    public async Task<ActionResult> getKorisnik([FromBody] GetUserRequest request)
    {
        try
        {
            var user = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(k=>k.pin == request.Pin);
            if(user == null)
                return BadRequest("Ne postoji korisnik.");
            
            return Ok(new 
            {
                Ime = user.ime,
                Prezime = user.prezime,
                BrojRacuna = user.Racun?.brojRacuna,
                Stanje = user.Racun?.sredstva
            });
        }
        catch (Exception e)
        {
            
            return BadRequest("Greska " + e.Message);
        }
    }

    [HttpDelete]
    [Route("deleteUser")]
    public async Task<ActionResult> deleteKorisnik([FromBody] DeleteUserRequest request)
    {
        try
        {
            var user = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(l=>l.pin == request.Pin);
            if(user == null)
                return BadRequest("Korisnik nije pronadjen");

            if(user.Racun != null)
                Context.Racuni.Remove(user.Racun);
            
            Context.Korisnici.Remove(user);
            await Context.SaveChangesAsync();

            return Ok("Korisnik obrisan.");
            
        }
        catch (Exception e)
        {
            
            return BadRequest("Greska " + e.Message);
        }
    }
}
