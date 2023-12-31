﻿namespace CRUD_API.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public byte [] Poster { get; set; }
        public byte CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
