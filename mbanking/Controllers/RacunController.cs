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
            
            return Ok(new { k.Racun?.sredstva });

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
                Racun = user.Racun
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
[Route("transfer")]
public async Task<ActionResult> transferNovca([FromBody] TransferRequest request)
{
    try
    {
        var sender = await Context.Korisnici.Include(k => k.Racun).ThenInclude(r => r.Transakcije)
            .FirstOrDefaultAsync(r => r.Racun.brojRacuna == request.SenderAccount);
        
        var receiver = await Context.Korisnici.Include(k => k.Racun).ThenInclude(r => r.Transakcije)
            .FirstOrDefaultAsync(r => r.Racun.brojRacuna == request.ReceiverAccount);

        if (sender == null || receiver == null)
            return BadRequest("Greska, ne postoji.");

        if (sender.Racun?.sredstva <= 0 || sender.Racun?.sredstva < request.Iznos)
            return BadRequest("Nemate dovoljno sredstava za transfer");

        if (sender.Racun?.Transakcije == null)
            sender.Racun.Transakcije = new List<Transakcija>();

        if (receiver.Racun?.Transakcije == null)
            receiver.Racun.Transakcije = new List<Transakcija>();

        sender.Racun.sredstva -= request.Iznos;
        receiver.Racun.sredstva += request.Iznos;

        var transakcijaSender = new Transakcija
        {
            iznos = request.Iznos,
            tip = "Poslato",
            datum = DateTime.Now,
            TekuciSender = sender.Racun?.brojRacuna,
            TekuciReceiver = receiver.Racun?.brojRacuna,
            Svrha = request.Svrha,
            Racun = sender.Racun 
        };

        sender.Racun.Transakcije.Add(transakcijaSender);

        var transakcijaReceiver = new Transakcija
        {
            iznos = request.Iznos,
            tip = "Primljeno",
            datum = DateTime.Now,
            TekuciSender = sender.Racun?.brojRacuna,
            TekuciReceiver = receiver.Racun?.brojRacuna,
            Svrha = request.Svrha,
            Racun = receiver.Racun 
        };

        receiver.Racun.Transakcije.Add(transakcijaReceiver);

        Context.Transakcije.Add(transakcijaSender);
        Context.Transakcije.Add(transakcijaReceiver);

        Context.Korisnici.Update(sender);  
        Context.Korisnici.Update(receiver); 

        await Context.SaveChangesAsync();

        return Ok(new { transakcijaSender, transakcijaReceiver });
    }
    catch (Exception e)
    {
        return BadRequest("Greska: " + e.Message);
    }
}




    [HttpPost]
    [Route("getReceiver")]
    public async Task<ActionResult> getRecv([FromBody] AccountRequest request)
    {
        try
        {
            var receiver = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(l=> l.Racun.brojRacuna == request.tekuciReceiver);

            if(receiver == null)
                return BadRequest("Korisnik ne postoji.");
            
            return Ok(new { receiver });

        }
        catch (Exception e)
        {
            
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("getSender")]
    public async Task<ActionResult> getSender([FromBody] AccountRequest request)
    {
        try
        {
            var sender = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(l=> l.Racun.brojRacuna == request.tekuciReceiver);

            if(sender == null)
                return BadRequest("Korisnik ne postoji.");
            
            return Ok(new { sender });

        }
        catch (Exception e)
        {
            
            return BadRequest(e.Message);
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
            
            return Ok(new { message = "Transakacije od " + user.ime,  user.Racun?.Transakcije  });
        }
        catch (Exception e)
        {
            return BadRequest("Greska " + e.Message);
        }
    }

    [HttpPut]
    [Route("changeValuta")]
    public async Task<ActionResult> promeniValutu([FromBody] string PinProvera, string valuta)
    {
        try
        {
            var user = await Context.Korisnici.Include(r=>r.Racun).FirstOrDefaultAsync(r=> r.pin == PinProvera);
            if(user == null)
                return BadRequest("Racun ne postoji");

            if(user.Racun?.valuta == valuta)
                return BadRequest("Racun je vec u toj valuti");
            decimal kurs;
            if(user.Racun?.valuta == "RSD")
                kurs = 0.0085M;
            else
                kurs = 117.5M;
            
            if(user.Racun == null)
                return BadRequest("Racun ne postoji.");
            user.Racun.valuta = valuta;
            user.Racun.sredstva *= kurs;

            await Context.SaveChangesAsync();
            return Ok($"Nova valuta {user.Racun.valuta} , stanje: {user.Racun.sredstva}");

        }
        catch (Exception e)
        {
            return BadRequest("Greska " + e);
            
        }
    }
}

