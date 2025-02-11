using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a min of 3 Characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a max of 3 Characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name has to be a max of 50 Characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
