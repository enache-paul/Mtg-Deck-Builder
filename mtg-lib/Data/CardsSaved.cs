using System;
using System.Collections.Generic;

namespace mtg_lib.Data
{
    public partial class CardsSaved
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int DeckId { get; set; }
        public long CardId { get; set; }
        public int? Count { get; set; }

        public virtual Card Card { get; set; } = null!;
        public virtual DecksSaved Deck { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
