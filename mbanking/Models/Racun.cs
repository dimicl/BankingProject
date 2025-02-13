using System.Text.Json.Serialization;

namespace WebTemplate.Models
{
    public class Racun
    {
        [Key]
        public int id { get; set;}
        
        public required string brojRacuna { get; set;}
        public decimal sredstva { get; set;}
        public string? valuta { get; set;}

        [JsonIgnore]
        public Korisnik? Korisnik { get; set; }

        public List<Transakcija>? Transakcije { get; set; }
    }
}