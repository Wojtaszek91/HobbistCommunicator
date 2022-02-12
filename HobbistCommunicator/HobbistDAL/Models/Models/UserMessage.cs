using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserMessage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SenderProfileId { get; set; }
        public Guid TargetProfileId { get; set; }
        public string TargetUserName { get; set; }
        public string Content { get; set; }
        public DateTime SendTime { get; set; }
        public bool HasBeenOpen { get; set; }
        public bool HasBeenSend { get; set; }

        public UserMessage(Guid senderProfileId, Guid targetProfileId, string content, string targetUserName)
        {
            SenderProfileId = senderProfileId;
            TargetProfileId = targetProfileId;
            TargetUserName = targetUserName;
            Content = content;
            SendTime = DateTime.Now;
            HasBeenOpen = false;
            HasBeenSend = false;
        }
    }
}
