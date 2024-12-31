namespace votrungduong_API.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? AuthorName { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public int? Likes { get; set; }
    }
}
