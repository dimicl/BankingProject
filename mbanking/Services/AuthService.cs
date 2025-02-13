public class AuthService
{
    private readonly BankaContext Context;
    private readonly Random random = new Random();
    public AuthService(BankaContext context)
    {
        Context = context;

    }

    public string GeneratePin()
    {
        return random.Next(1000, 9999).ToString();
    }

    public string GenerateBrojRacuna()
    {
        string start = random.Next(100,999).ToString();
        string zeros = "000000";
        string ending = random.Next(222222222,999999999).ToString();
        return start + zeros + ending;
    }

    public bool Verify(string unetaLozinka, string skladistenaLozinka)
    {
        return unetaLozinka == skladistenaLozinka;
    }
}