using Models.Models.WorkFlowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.EntityFrameworkJoinEntities.DTOs
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string ChainedTagName { get; set; }
        public string PostMessage { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int PostViews { get; set; }
        public int AverageMark { get; set; }
        public int DayLast { get; set; }
        public DateTime BeginDate { get; set; }
        public Guid ProfileId { get; set; }
        public bool IsFollowed { get; set; }
        public bool IsBlocked { get; set; }
    }
}
