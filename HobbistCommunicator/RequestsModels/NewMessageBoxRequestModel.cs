using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistCommunicator.RequestsModels
{
    public class NewMessageBoxRequestModel
    {
        public Guid ProfileOneId { get; set; }
        public Guid ProfileTwoId { get; set; }
    }
}
