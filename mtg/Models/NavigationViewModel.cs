using mtg_lib.Data;
using X.PagedList;

namespace mtg.Models;

public class NavigationViewModel
{
    public IPagedList<CardsCounted> CardsModel { get; set; }
    public ActionAndControllerModel ActionAndControllerModel { get; set; }
}