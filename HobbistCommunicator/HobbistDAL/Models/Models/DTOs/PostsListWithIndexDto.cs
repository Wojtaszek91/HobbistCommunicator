using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.DTOs
{
    public class PostsListWithIndexDto
    {
        public List<PostDto> PostList { get; set; }
        public int Index { get; set; }
    }
}
