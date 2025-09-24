﻿namespace PokemonReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int Rating { get; set; }

        // 1 to many
        public Reviewer Reviewer { get; set; }
        // 1 to many
        public Pokemon Pokemon { get; set; }
    }
}
