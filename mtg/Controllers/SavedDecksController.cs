using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mtg_lib.Data;
using mtg_lib.Services;
using mtg.Models;
using X.PagedList;

namespace mtg.Controllers;

[Authorize]
public class SavedDecksController : Controller
{
    private readonly ILogger<SavedDecksController> _logger;
    private DecksService decksService;
    private CardsService cardsService;

    public SavedDecksController(ILogger<SavedDecksController> logger)
    {
        _logger = logger;
        decksService = new DecksService();
        cardsService = new CardsService();
    }
    
    public IActionResult LoadDecks()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult LoadDecks(string filterNameSearchString, int? page, int? pageSize, int selectedDeck)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
        var decksSavedList = decksService.GetUserSavedDecks(userId);

        var deckId = GetFirstDeckId(decksSavedList, selectedDeck);
        var cardsCountedList = decksService.GetCardsByDeck(userId, deckId);

        var pagedList = FilterCardsBy(filterNameSearchString, page, pageSize, cardsCountedList, deckId);
        
        return View(new SavedDecksVm { CardsInDecks = pagedList, DecksSaved = decksSavedList});
    }

    public IPagedList<CardsCounted> FilterCardsBy(string name, int? page, int? pageSize, List<CardsCounted> cardsCounted, int deckId)
    {
        var cardsFiltered = cardsService.FilterListOfCards(cardsCounted, name);

        int pgSize = pageSize ?? 5;
        int pageNumber = page ?? 1;
        
        ViewBag.FilterNameSearchString = name;
        ViewBag.PageSize = pageSize;
        ViewBag.SelectedDeck = deckId;
        Console.WriteLine("in filter deck is : " + deckId);

        return cardsFiltered.ToPagedList(pageNumber, pgSize);
    }

    private int GetFirstDeckId(List<DecksSaved> decks, int selectedDeck)
    {
        try
        {
            return selectedDeck != 0 ? selectedDeck : decks.First().Id;
        }
        catch (InvalidOperationException e)
        {
            return 0;
        }
    }
}