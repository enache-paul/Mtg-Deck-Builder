using Microsoft.AspNetCore.Mvc;
using mtg_lib.Data;
using mtg_lib.Services;
using mtg.Models;
using X.PagedList;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace mtg.Controllers;

[Authorize]
public class DeckController : Controller
{
    private readonly ILogger<DeckController> _logger;
    private CardsService service;

    public DeckController(ILogger<DeckController> logger)
    {
        _logger = logger;
        service = new CardsService();
    }
    
    public IActionResult Collection()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Collection(string filterNameSearchString, int? page, int? pageSize)
    {
        var filteredCards = FilterCardsBy(filterNameSearchString, page, pageSize);
        return View(filteredCards);
    }
    
    public List<CardSession> GetListOfCardSession() // TODO: this is a duplicate from Session controller. 
    {
        var cardsJson = HttpContext.Session.GetString("CardList") ?? "[]";
        return JsonSerializer.Deserialize<List<CardSession>>(cardsJson);    
    }
    
    private IPagedList<CardsCounted> FilterCardsBy(string name, int? page, int? pageSize)
    {
        
        var cards = service.FilterListOfCards(GetCardsSessionDb(ConvertToCardSession()), name);

        int pgSize = pageSize ?? 5;
        int pageNumber = page ?? 1;
        
        ViewBag.FilterNameSearchString = name;
        ViewBag.PageSize = pageSize;

        return cards.ToPagedList(pageNumber, pgSize);
    }

    public List<CardSession> ConvertToCardSession()
    {
        var cardsJson = HttpContext.Session.GetString("CardList") ?? "[]";
        return JsonSerializer.Deserialize<List<CardSession>>(cardsJson);
    }

    public List<CardsCounted> GetCardsSessionDb(List<CardSession> sessionCards)
    {
        var cards = new List<CardsCounted>();

        foreach (var sessionCard in sessionCards)
        {
            var card = service.GetCardById(sessionCard.Id);
            cards.Add(new CardsCounted { Card = card, Count = sessionCard.Count });
        }

        Console.WriteLine(cards.Count);
        return cards;
    }
    
    public List<CardsCounted> GetSessionCardByName(string? name, List<CardsCounted> sessionList)
    {
        if (name == null)
        {
            return sessionList;
        }
        List<CardsCounted> cards = sessionList.Where(card => card.Card.Name.Contains(name)).ToList();
        return cards;
    }
}