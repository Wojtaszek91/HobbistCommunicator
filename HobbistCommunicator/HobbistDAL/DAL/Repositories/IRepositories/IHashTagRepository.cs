
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IHashTagRepository
    {
        List<HashTag> GetAllHashtags();
        List<HashTagDto> GetAllHashtagsDto();
        IEnumerable<string> GetAllHashTagNames();
        HashTagDto GetHashTagById(Guid id);
        int GetHashTagPopularity(Guid id);
        HashTag GetHashTagByName(string name);
        int GetHashTagPopularityByName(string name);
        bool AddPopularity(Guid id);
        bool DecreasePopuplarity(Guid id);
        bool DoesHashTagExists(Guid id);
        bool DeleteHashTag(Guid id);
        bool AddHashTag(string h);
        bool RemoveHashTag(Guid id);
        bool EditHashTag(HashTagDto h);
        bool EditHashTagNoReturnType(HashTagDto h);
        bool Save();
    }
}
