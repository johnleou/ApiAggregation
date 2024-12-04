using ApiAggregationProject.Api.DTO;
using ApiAggregationProject.Api.Models;

namespace ApiAggregationProject.Api.Services
{
    public class AggregationService : IAggregationService
    {
        private readonly IDataService<User> _UserClient;
        private readonly IDataService<Comment> _CommentClient;
        private readonly IDataService<Post> _PostClient;

        public AggregationService(IDataService<User> UserClient, IDataService<Comment> CommentClient, IDataService<Post> PostClient)
        {
            _UserClient = UserClient;
            _CommentClient = CommentClient;
            _PostClient = PostClient;
        }

        public async Task<AggregatedResponse> GetAggregatedData()
        {
            var users = await _UserClient.GetDataAsync();
            var comments = await _CommentClient.GetDataAsync();
            var posts = await _PostClient.GetDataAsync();

            var usersDto = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Username = u.Username
            }).ToList();

            var commentsDto = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Body = c.Body,
            }).ToList();

            var postsDto = posts.Select(p => new PostDTO
            {
                Id = p.Id,
                Body = p.Body,
                Title = p.Body
            }).ToList();

            return new AggregatedResponse
            {
                User = usersDto,
                Comment = commentsDto,
                Post = postsDto
            };
        }
    }

}
