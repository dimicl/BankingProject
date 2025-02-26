namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class StednjaController : ControllerBase
{
    public BankaContext Context { get; set; }

    public StednjaController(BankaContext context)
    {
        Context = context;
    }

    [HttpPost]
    [Route("addStednja")]
    public async Task<ActionResult> addStednja([FromBody] StednjaRequest request)
    {
        try
        {
            var user = await Context.Korisnici.Include(k=>k.Stednje).FirstOrDefaultAsync(s=> s.pin == request.Pin);
            if(user == null)
                return BadRequest("Korisnik ne postoji");
            
            if(request == null)
                return BadRequest("Zahtev ne postoji");

            var stednja = new Stednja{
                Naziv = request.Naziv,
                Cilj = request.Cilj,
                Vrednost = 0,
                Korisnik = user
            };
            user.Stednje.Add(stednja);
            await Context.SaveChangesAsync();

            return Ok(new { stednja });
        }
        catch (Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }
    [HttpPost]
    [Route("getStednje")]
    public async Task<ActionResult> getStednje([FromBody] PinRequest request)
    {
        try
        {
            var user = await Context.Korisnici.Include(k=>k.Stednje).FirstOrDefaultAsync(s=>s.pin == request.Pin);
            if(user == null)
                return BadRequest("Korisnik ne postoji");
            
            return Ok(new { user.Stednje });
        }
        catch (Exception e)
        {
            
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("dodajVrednost")]
    public async Task<ActionResult> dodajSumu(DodajVrednostStednja request)
    {
        try
        {
            var user = await Context.Korisnici.Include(r=>r.Racun).Include(s=>s.Stednje).FirstOrDefaultAsync(s=>s.pin == request.Pin);
            if(user == null)
                return BadRequest("Korisnik ne postoji");
            
            var stednja = user.Stednje?.FirstOrDefault(s=>s.Naziv==request.Naziv);
            if(stednja == null)
                return BadRequest("Stednja ne postoji");

            if(user.Racun == null)
                return BadRequest("Racun ne postoji");

            if(user.Racun.sredstva < request.Iznos)
                return BadRequest("Nemate dovoljno sredstva na racunu");

            user.Racun.sredstva -= request.Iznos;
            Context.Racuni.Update(user.Racun);
            await Context.SaveChangesAsync();

            stednja.Vrednost += request.Iznos;
            Context.Stednje.Update(stednja);

            await Context.SaveChangesAsync();

            return Ok(new { stednja.Vrednost });        
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("removeStednja")]
    public async Task<ActionResult> removeStednja([FromBody] BrisanjeStednje request)
    {
        try
        {
            var user = await Context.Korisnici.Include(r=>r.Racun).Include(s=>s.Stednje).FirstOrDefaultAsync(k=>k.pin == request.Pin);
            if(user == null)
                return BadRequest("Korisnik ne postoji");

            var racun = user.Racun;
            if(racun == null)
                return BadRequest("Racun ne postoji");

            var stednja = user.Stednje?.FirstOrDefault(s=>s.Naziv == request.Naziv);
            if(stednja == null)
                return BadRequest("Stednja ne postoji");

            
            racun.sredstva += stednja.Vrednost;
            Context.Racuni.Update(racun);
            await Context.SaveChangesAsync();

            Context.Stednje.Remove(stednja);
            await Context.SaveChangesAsync();

            return Ok("Stednja uspesno obrisana.");
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
            
        }
    }
}
