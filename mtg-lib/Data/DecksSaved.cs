using System;
using System.Collections.Generic;

namespace mtg_lib.Data
{
    public partial class DecksSaved
    {
        public DecksSaved()
        {
            CardsSaveds = new HashSet<CardsSaved>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<CardsSaved> CardsSaveds { get; set; }
    }
}
