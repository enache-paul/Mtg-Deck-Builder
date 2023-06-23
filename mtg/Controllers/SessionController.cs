using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using mtg_lib.Data;
using mtg_lib.Services;
using mtg.Models;

namespace mtg.Controllers;

public class SessionController : Controller
{
    private readonly ILogger<SessionController> _logger;
    private DecksService service;

    public SessionController(ILogger<SessionController> logger)
    {
        _logger = logger;
        service = new DecksService();
    }

    [HttpPost]
    public IActionResult SaveDeckOnDb(string deckName)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);

        service.AddListOfCardsToSavedDeck(GetListOfCardSession(), userId, deckName);
        
        return RedirectToAction("Collection", "Deck");
    }

    public List<CardSession> GetListOfCardSession()
    {
        var cardsJson = HttpContext.Session.GetString("CardList") ?? "[]";
        return JsonSerializer.Deserialize<List<CardSession>>(cardsJson);    
    }
    
    [HttpPost]
    public IActionResult AddCardToSession(int cardId)
    {
        var cardsJson = HttpContext.Session.GetString("CardList") ?? "[]";
        Console.WriteLine(HttpContext.Session.GetString("CardList"));
        var cards = JsonSerializer.Deserialize<List<CardSession>>(cardsJson);
        var cardsWithDuplicated = IncrementCounterIfDuplicate(cards, cardId);
        
        if (cardsWithDuplicated != null)
        {
            cards = cardsWithDuplicated;
        }
        else
        {
            var cardSession = new CardSession { Id = cardId, Count = 1};
            cards.Add(cardSession);
        }
        

        var serializedCards = JsonSerializer.Serialize(cards);
        
        HttpContext.Session.SetString("CardList", serializedCards);
        Console.WriteLine(serializedCards);
        
        return Json(new { success = true });
    }

    private static List<CardSession>? IncrementCounterIfDuplicate(List<CardSession> cards, int targetId)
    {
        foreach (var card in cards)
        {
            if (card.Id == targetId)
            {
                card.Count++;
                return cards;
            }            
        }

        return null;
    }
    
    [HttpPost]
    public IActionResult RemoveCardFromSession(int cardId)
    {
        var cardsJson = HttpContext.Session.GetString("CardList") ?? "[]";
        var cards = JsonSerializer.Deserialize<List<CardSession>>(cardsJson);
        
        var cardsDecremented = DecrementCounterIfDuplicate(cards, cardId);

        if (cardsDecremented == null)
        {
            return Json(new { error = "Failed to remove" });
        }

        var serializedCards = JsonSerializer.Serialize(cardsDecremented);
        
        HttpContext.Session.SetString("CardList", serializedCards);
        Console.WriteLine(serializedCards);
        Console.WriteLine("Search string here", ViewBag.FilterNameSearchString);
        
        var redirect = new
        {
            url = Url.Action("Collection", "Deck")
        };
        
        return Json(new { success = redirect });
    }
    
    private static List<CardSession>? DecrementCounterIfDuplicate(List<CardSession> cards, int targetId)
    {
        foreach (var card in cards)
        {
            if (card.Id == targetId)
            {
                if (card.Count > 1)
                {
                    card.Count--;
                    
                }
                else
                {
                    cards.Remove(card);
                }
                
                return cards;
            }            
        }

        return null;
    }
    
    [HttpPost]
    public IActionResult DeleteSessionCards()
    {
        HttpContext.Session.Remove("CardList");

        return RedirectToAction("Collection", "Deck");
    }
}