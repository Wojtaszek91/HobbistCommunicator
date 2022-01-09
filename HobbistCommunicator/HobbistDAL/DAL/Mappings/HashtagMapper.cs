using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.Mappings
{
    public static class HashtagMapper
    {
        public static HashTagDto MapHashTagToDto(HashTag hashtag)
        {
            return new HashTagDto() { HashTagName = hashtag.HashTagName, Popularity = hashtag.Popularity };
        }
    }
}
