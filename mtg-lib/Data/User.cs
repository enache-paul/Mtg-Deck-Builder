using System;
using System.Collections.Generic;

namespace mtg_lib.Data
{
    public partial class User
    {
        public User()
        {
            CardsSaveds = new HashSet<CardsSaved>();
            DecksSaveds = new HashSet<DecksSaved>();
        }

        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public virtual ICollection<CardsSaved> CardsSaveds { get; set; }
        public virtual ICollection<DecksSaved> DecksSaveds { get; set; }
    }
}
