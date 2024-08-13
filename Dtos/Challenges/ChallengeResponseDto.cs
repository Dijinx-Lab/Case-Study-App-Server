using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Dtos.Challenges
{
    public class ChallengeResponseDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Upload? Upload { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}