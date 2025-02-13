namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class RacunController : ControllerBase
{
    public BankaContext Context { get; set; }

    public RacunController(BankaContext context)
    {
        Context = context;
    }

    [HttpPost]
    [Route("getStanje")]
    public async Task<ActionResult> getStanje([FromBody] PinRequest request)
    {
        try
        {
            var k = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(r=>r.pin == request.Pin);
            if(k == null)
                return BadRequest("Racun ne postoji.");
            
            return Ok(k.Racun?.sredstva);

        }
        catch (Exception e)
        {
            return BadRequest("Greska " + e.Message);            

        }
    }

    [HttpPut]
    [Route("uplata")]
    public async Task<ActionResult> uplataNovca([FromBody] TransakcijaRequest request)
    {
        try
        {
            var user = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(r=>r.pin == request.Pin);
            if(user ==  null)
                return BadRequest("User ne postoji.");
            
            if(request.Iznos <= 0)
                return BadRequest("Iznos mora biti validan");

            if(user.Racun != null)
                user.Racun.sredstva = (user.Racun.sredstva) + request.Iznos;

            var transakcija = new Transakcija
            {
                iznos = request.Iznos,
                tip = "Uplata",
                datum = DateTime.Now,
                Racun = user.Racun,
                RacunId = user.Racun.id
            };
            
            Context.Transakcije.Add(transakcija);
            await Context.SaveChangesAsync();
            return Ok(transakcija);
        }
        catch (Exception e)
        {
            return BadRequest("Greska " + e.Message);
        }
    }

    [HttpPut]
    [Route("isplata")]
    public async Task<ActionResult> isplataNovca([FromBody] TransakcijaRequest request)
    {
        try
        {
            var user = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(r=>r.pin == request.Pin);
            if(user ==  null)
                return BadRequest("User ne postoji.");
            
            if(request.Iznos <= 0)
                return BadRequest("Iznos mora biti validan");

            if(user.Racun != null)
                user.Racun.sredstva = (user.Racun.sredstva) - request.Iznos;

            var transakcija = new Transakcija
            {
                iznos = request.Iznos,
                tip = "Isplata",
                datum = DateTime.Now,
                Racun = user.Racun,
                RacunId = user.Racun.id
            };
            Context.Transakcije.Add(transakcija);
            await Context.SaveChangesAsync();

            return Ok(user.Racun?.sredstva);
        }
        catch (Exception e)
        {
            
            return BadRequest("Greska " + e.Message);
        }
    }
    
    [HttpPut]
    [Route("transfer")]
    public async Task<ActionResult> transferNovca([FromBody] TransferRequest request)
    {
        try
        {
           var sender = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(r=>r.pin == request.SenderPin);
           var receiver = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(r=>r.pin == request.ReceiverPin);

           if(sender == null || receiver == null)
                return BadRequest("Greska, ne postoji.");

            if(sender.Racun?.sredstva <= 0)
                return BadRequest("Nemate dovoljno sredstava za transfer");

            if(sender.Racun != null && receiver.Racun != null && sender.Racun.sredstva > 0)
            {   
                sender.Racun.sredstva -= request.Iznos;
                receiver.Racun.sredstva += request.Iznos;
            }
            var transakcija = new Transakcija
            {
                iznos = request.Iznos,
                tip = "Transfer",
                datum = DateTime.Now,
                Racun = sender.Racun,
                RacunId = sender.Racun.id
            };
            Context.Transakcije.Add(transakcija);
            await Context.SaveChangesAsync();

            return Ok("Transfer novca obavljen.");

        }
        catch (Exception e)
        {
            return BadRequest("Greska" + e);
        }
    }
    
    [HttpPost]
    [Route("getTransakcije")]
    public async Task<ActionResult> getTransakcije([FromBody] PinRequest request)
    {
        try
        {
            var user = await Context.Korisnici.Include(k=>k.Racun)
                                                    .ThenInclude(r=>r.Transakcije)
                                                    .FirstOrDefaultAsync(t=> t.pin == request.Pin);
            if(user == null)
                return BadRequest("Racun ne postoji.");
            
            return Ok(user.Racun?.Transakcije);
        }
        catch (Exception e)
        {
            return BadRequest("Greska " + e.Message);
        }
    }
}

