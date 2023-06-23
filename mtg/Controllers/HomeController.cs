using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mtg.Models;
using mtg_lib.Services;
using mtg_lib.Data;
using X.PagedList;
using System.Text.Json;

namespace mtg.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private CardsService service;
    private int lastSelectedCardId;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        service = new CardsService();
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("cards/{name}")]
    public List<Card> Card(string name)
    {
        List<Card> cards = service.GetCardByName(name);
        return cards;
    }

    [Route("cards/manaCosts")]
    public string GetHighestManaCost() 
    {
        return service.GetHighestManaCost();

    }

    [Route("cards/rarities")]
    public List<string> GetCardRarities()
    {
        return service.GetRarities();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login","Authentication");
    }
    
    [HttpGet]
    public IActionResult Index(string filterNameSearchString, string manaCost, string rarity, int? page, int? pageSize)
    {
        var filteredCards = FilterCardsBy(filterNameSearchString, manaCost, rarity, page, pageSize);
        return View(filteredCards);
    }

    private IPagedList<CardsCounted> FilterCardsBy(string name, string manaCost, string rarity, int? page, int? pageSize)
    {
        var cards = service.GetCardsByFilter(name, manaCost, rarity);

        int pgSize = pageSize ?? 5;
        int pageNumber = page ?? 1;
        
        ViewBag.FilterNameSearchString = name;
        ViewBag.PageSize = pageSize;
        ViewBag.ManaCost = manaCost;
        ViewBag.Rarity = rarity;
        
        var cardsCounted = ConvertToListOfCardsCounted(cards);
        
        return cardsCounted.ToPagedList(pageNumber, pgSize);
    }

    private List<CardsCounted> ConvertToListOfCardsCounted(List<Card> cards)
    {
        var converted = new List<CardsCounted>();
        foreach (var card in cards)
        {
            converted.Add(new CardsCounted { Card = card, Count = null});
        }

        return converted;
    }
    
}
