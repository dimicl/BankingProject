namespace WebTemplate.Controllers;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    public BankaContext Context { get; set; }
    public AuthService authService { get; set; }
    public AuthController(BankaContext context, AuthService service)
    {
        Context = context;
        authService = service;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> register([FromBody] RegisterRequest request)
    {
        try
        {
            if(Context.Korisnici.Any(k=> k.email == request.Email))
                return BadRequest("Email vec postoji.");
            
            string pinKod = authService.GeneratePin();
            string brojR = authService.GenerateBrojRacuna();
            var racun = new Racun
            {
                brojRacuna = brojR,
                sredstva = 0,
                valuta = "RSD"
            };

            var korisnik = new Korisnik
            {
                ime = request.Ime,
                prezime = request.Prezime,
                email = request.Email,
                pin = pinKod,
                Racun = racun
            };

            Context.Korisnici.Add(korisnik);
            await Context.SaveChangesAsync();
            return Ok(new {Message = "Registracija uspesna.", PinKod=pinKod, racun = new { racun.brojRacuna, racun.sredstva, racun.valuta }});
            
        }
        catch (Exception e)
        {
            return BadRequest("Greska" + e.Message);
        }
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> login([FromBody] LoginRequest request)
    {
        try
        {
            var korisnik = await Context.Korisnici.Include(k=>k.Racun).FirstOrDefaultAsync(k=> k.pin == request.Pin);
            if(korisnik == null)
                return Unauthorized(new { message = "Pogresan pin" });
            
            return Ok(new { message  = "Korisnik " + korisnik.ime + " sa pinom " + korisnik.pin + " se uspesno ulogovao", korisnik = new {korisnik.ime, korisnik.prezime, korisnik.pin, korisnik.email} , racun = new { korisnik.Racun?.brojRacuna, korisnik.Racun?.sredstva, korisnik.Racun?.valuta} });
        }
        catch (Exception e)
        {
            
            return BadRequest("Greska " + e.Message);
        }
    }

    
}
