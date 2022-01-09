using Models.Models.EntityFrameworkJoinEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserAccount
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MinLength(8)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public bool isBlocked { get; set; }

        public ICollection<Post> Posts { get; set; }

        public Guid? UserProfileId { get; set; }
        [ForeignKey("UserProfileId")]
        public UserProfile UserProfile { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
