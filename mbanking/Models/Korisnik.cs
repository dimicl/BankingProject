namespace WebTemplate.Models
{
    public class Korisnik
    {
        [Key]
        public int id {get; set;}
        [MaxLength(20)]
        public required string ime {get; set;}
        public required string prezime {get; set;}

        [EmailAddress]
        public string? email { get; set; }

        [MaxLength(13)] [MinLength(13)]
        public int? RacunId { get; set; }
        public Racun? Racun {get; set;}

         [MaxLength(4)]
         public required string pin { get; set; } 
    }
}