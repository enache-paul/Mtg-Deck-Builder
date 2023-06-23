using mtg_lib.Data;
using X.PagedList;

namespace mtg.Models;

public class SavedDecksVm
{
    public IPagedList<CardsCounted> CardsInDecks;

    public List<DecksSaved> DecksSaved;
}