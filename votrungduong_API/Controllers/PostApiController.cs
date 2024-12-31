using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using votrungduong_API.Models;
using votrungduong_API.Repositories;

namespace votrungduong_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = Admin)]
    public class PostApiController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostApiController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // Lấy danh sách tất cả các bài viết
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await _postRepository.GetPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Lấy bài viết theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var post = await _postRepository.GetPostByIdAsync(id);
                if (post == null)
                    return NotFound("Post not found");

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Thêm bài viết mới
        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            try
            {
                if (post == null)
                    return BadRequest("Post is null");

                await _postRepository.AddPostAsync(post);
                return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Cập nhật bài viết
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            try
            {
                if (id != post.Id)
                    return BadRequest();
                await _postRepository.UpdatePostAsync(post);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "Internal server error");
            }
        }

        // Xóa bài viết
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var post = await _postRepository.GetPostByIdAsync(id);
                if (post == null)
                    return NotFound("Post not found");

                await _postRepository.DeletePostAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
