using ApiAggregationProject.Api.DTO;

namespace ApiAggregationProject.Api.Models
{
    public class AggregatedResponse
    {
        public List<UserDTO> User { get; set; }
        public List<CommentDTO> Comment { get; set; }
        public List<PostDTO> Post { get; set; }
    }
}
