using Cards.Api.Data;
using Cards.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbContext;

        public CardsController(CardsDbContext cardsDbContext)
        {
            this.cardsDbContext = cardsDbContext;
        }

        // Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await cardsDbContext.Cards.ToListAsync();
            return Ok(cards);
        }

        //Get Single Card
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute]Guid id)
        {
            var card = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
                if (card != null)
            {
            return Ok(card);
            }

            return NotFound("card not Found");
        }


        //Add Card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
           card.Id = Guid.NewGuid();

           await cardsDbContext.Cards.AddAsync(card);
            await cardsDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }


        //update A card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingcard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingcard != null)
            {
                existingcard.CardholderName = card.CardholderName;
                await cardsDbContext.SaveChangesAsync();

                return Ok(existingcard);
            }

            return NotFound("card not Found");

        }


        //Delete A card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingcard = await cardsDbContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingcard != null)
            {
                cardsDbContext.Remove(existingcard);
                await cardsDbContext.SaveChangesAsync();

                return Ok(existingcard);
            }

            return NotFound("card not Found");

        }


    }
}
