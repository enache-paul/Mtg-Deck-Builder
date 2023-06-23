using Microsoft.EntityFrameworkCore;

namespace mtg_lib.Services;

using mtg_lib.Data;

public class DecksService
{
    private mtgContext _context;

    public DecksService()
    {
        this._context = new mtgContext();
    }

     public void AddListOfCardsToSavedDeck(List<CardSession> cardsSession, int userId, string deckName)
    {
        if (cardsSession.Count <= 0) return;

        int newDeckId = CreateNewDeckSaved(userId, deckName);

        cardsSession.ForEach(card =>
        {
            AddCardToSavedDeck(userId, newDeckId, (int)card.Id, (int)card.Count);
        });
    }
    private int CreateNewDeckSaved(int userId, string deckName)
    {
        try
        {
            DecksSaved deck = new DecksSaved
            {
                UserId = userId,
                Name = deckName
            };
            
            _context.DecksSaveds.Add(deck);
            _context.SaveChanges();

            return deck.Id;
        }
        catch (DbUpdateException ex)
        {
            return 0;
        }
    }

    private void AddCardToSavedDeck(int userId, int deckId, int cardId, int count)
    {
        try
        {
            CardsSaved card = new CardsSaved
            {
                UserId = userId,
                DeckId = deckId,
                CardId = cardId,
                Count = count
            };
            
            _context.CardsSaveds.Add(card);
            _context.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<CardsCounted> GetCardsByDeck(int userId, int deckId)
    {
        List<CardsSaved> listCardsSaved = _context.CardsSaveds.Where(savedCard => 
            savedCard.UserId == userId &&
            savedCard.DeckId == deckId
        ).ToList();

        List<CardsCounted> listCards = new List<CardsCounted>();
        
        foreach (var cardSaved in listCardsSaved)           
        {  
            //this could have looked more readable if i used LINQ Select method, however that causes a memory leak if
            // it iterates referencing the _context
            CardsService cardsService = new CardsService();

            Card card = cardsService.GetCardById((int)cardSaved.CardId); 
            int? count = cardSaved.Count;
            
            listCards.Add(new CardsCounted
            {
                Card = card,
                Count = count
            });
        }

        return listCards;
    }

    public List<DecksSaved> GetUserSavedDecks(int userId)
    {
        return _context.DecksSaveds.Where(deck => deck.UserId == userId).ToList();
    }

}