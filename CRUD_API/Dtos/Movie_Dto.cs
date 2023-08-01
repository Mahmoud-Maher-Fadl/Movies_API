namespace CRUD_API.Dtos
{
    public class Movie_Dto
    {
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }
        public IFormFile Poster { get; set; }
        public byte CategoryId { get; set; }
    }
}
