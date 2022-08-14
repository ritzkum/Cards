using System.ComponentModel.DataAnnotations;

namespace Cards.Api.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }

        public string CardholderName {get; set;}

    }
}
