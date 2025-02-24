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
}
