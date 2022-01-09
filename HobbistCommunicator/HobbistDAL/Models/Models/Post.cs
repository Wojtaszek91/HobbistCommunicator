using Models.Models.WorkFlowModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public HashTag ChainedTag { get; set; }
        [Required]
        [MinLength(20)]
        public string PostMessage { get; set; }
        public bool IsBlocked { get; set; }
        [Column(TypeName = "decimal(27,25)")]
        public decimal Lat { get; set; }
        [Column(TypeName = "decimal(27,25)")]
        public decimal Lng { get; set; }
        public int PostViews { get; set; }
        public int AverageMark { get; set; }
        public int DayLast { get; set; }
        [Required]
        public DateTime BeginDate { get; set; }
        [Required]
        public Guid UserProfileId { get; set; }
      
        [ForeignKey("UserProfileId")]
        public virtual UserProfile UserProfile { get; set; }

        [NotMapped]
        public List<Guid> FollowersList { get; set; } = new List<Guid>();
        [Obsolete("Only for Persistence by EntityFramework")]
        public string Followers
        {
            get { return FollowersList == null || !FollowersList.Any() ? null : JsonConvert.SerializeObject(FollowersList); }

            set { if (string.IsNullOrWhiteSpace(value)) FollowersList.Clear(); else FollowersList = JsonConvert.DeserializeObject<List<Guid>>(value);}
        }
    }
}
