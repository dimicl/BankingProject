using System.Text.Json.Serialization;

namespace WebTemplate.Models
{
    public class Transakcija
    {
        [Key]
        public int id { get; set; }

        public required decimal iznos { get; set; }

        public required DateTime datum { get; set; } = DateTime.UtcNow;

        public required string tip { get; set; } 

        public int RacunId { get; set; }
        [JsonIgnore]
        public Racun? Racun { get; set; }
    }
}
