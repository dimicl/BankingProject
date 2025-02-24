using System.Text.Json.Serialization;

namespace WebTemplate.Models
{
    public class Stednja
    {
        [Key]
        public int id { get; set; }

        public required string Naziv { get; set; }
        public decimal Cilj { get; set; }

        [JsonIgnore]
        public Korisnik? Korisnik { get; set; }
    }
}
