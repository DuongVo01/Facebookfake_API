using Microsoft.EntityFrameworkCore;
using votrungduong_API.Models;

namespace votrungduong_API.Repositories
{
    public class PostRepository: IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }
        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }
        public async Task AddPostAsync(Post Post)
        {
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePostAsync(Post Post)
        {
            _context.Entry(Post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeletePostAsync(int id)
        {
            var Post = await _context.Posts.FindAsync(id);
            if (Post != null)
            {
                _context.Posts.Remove(Post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
