using Microsoft.EntityFrameworkCore;

namespace mtg_lib.Services;

using mtg_lib.Data;

public class CardsService
{
    private mtgContext _context;

    public CardsService()
    {
        this._context = new mtgContext();
    }

    public List<Card> GetCards()
    {
        List<Card> cards = this._context.Cards.ToList();
        return cards;
    }

    public List<Card> GetCardByName(string name)
    {
        List<Card> cards = this._context.Cards.Where(card => card.Name.Contains(name)).ToList();
        return cards;
    }

    public Card GetCardById(int? id)
    {
        Card card = _context.Cards.First(card => card.Id == id);
        return card;
    }

    public List<Card> GetCardsByFilter(string name, string manaCost, string rarity) {
        
        List<Card> cards = this._context.Cards.ToList();

        if (name != null) { cards = cards.Where(card => card.Name.Contains(name)).ToList(); }

        if (manaCost != "Any") { cards = cards.Where(card => card.ConvertedManaCost == manaCost ).ToList(); }

        if (rarity != "Any") { cards = cards.Where(card => card.RarityCode == rarity).ToList(); }
 
        return cards;
    }

    public List<CardsCounted> FilterListOfCards(List<CardsCounted> cardsCounted, string? name)
    {
        if (name == null) return cardsCounted;
        
        List<CardsCounted> cards = cardsCounted.Where(card => card.Card.Name.Contains(name)).ToList();
        return cards;
    }

    public string GetHighestManaCost()
    {
        return _context.Cards.OrderByDescending(card => card.ConvertedManaCost).First().ConvertedManaCost;
    }

    public List<string> GetRarities()
    {
        return _context.Rarities.Select(rarity => rarity.Name).ToList();
    }
}