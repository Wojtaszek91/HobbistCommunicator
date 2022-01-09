using DAL.DataContext;
using DAL.Repositories.IRepositories;
using HobbistApi.Mappings;
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class HashTagRepository : IHashTagRepository
    {
        private readonly ApplicationDbContext _context;
        public HashTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetAllHashTagNames() => _context.HashTags.Select(x => x.HashTagName).ToList();

        public List<HashTagDto> GetAllHashtagsDto()
        {
            List<HashTagDto> hashtagDtoList = new List<HashTagDto>();
            foreach (var hashTag in _context.HashTags.ToList()) { hashtagDtoList.Add(HashtagMapper.MapHashTagToDto(hashTag)); }

            return hashtagDtoList;
        }

        public List<HashTag> GetAllHashtags()
        {
            List<HashTag> hashTagList = new List<HashTag>();
            foreach (var hashTag in _context.HashTags.ToList()) { hashTagList.Add(hashTag); }

            return hashTagList;
        }

        public bool AddHashTag(string h)
        {
            if (string.IsNullOrEmpty(h)) return false;

            var newTag = new HashTag()
            {
                HashTagName = h,
                Popularity = 0
            };
            _context.HashTags.Add(newTag);

            return Save();
        }

        public bool AddPopularity(Guid id)
        {
            var dbHash = _context.HashTags.FirstOrDefault(h => h.Id == id);
            dbHash.Popularity++;
            return Save();
        }

        public bool DecreasePopuplarity(Guid id)
        {
            var dbHash = _context.HashTags.FirstOrDefault(h => h.Id == id);
            dbHash.Popularity--;
            return Save();
        }

        public bool EditHashTag(HashTagDto hashtagDTO)
        {
            if (hashtagDTO != null)
            {
                var tagFromDb = _context.HashTags.FirstOrDefault(dbH => dbH.HashTagName == hashtagDTO.HashTagName);
                tagFromDb.HashTagName = hashtagDTO.HashTagName;
                _context.HashTags.Update(tagFromDb);
            }
            return Save();
        }

        public bool EditHashTagNoReturnType(HashTagDto hashtagDTO)
        {
            if (EditHashTag(hashtagDTO)) return false;

            return Save();
        }

        public bool DoesHashTagExists(Guid id)
        {
            if (_context.HashTags.FirstOrDefault(h => h.Id == id) == null) return false;

            return true;
        }

        public bool DeleteHashTag(Guid id)
        {
            var hashtag = _context.HashTags.FirstOrDefault(h => h.Id == id);
            if (hashtag == null) return false;

            _context.HashTags.Remove(hashtag);
            return Save();
        }

        public HashTagDto GetHashTagById(Guid id)
        {
            var tagFromDb = _context.HashTags.FirstOrDefault(h => h.Id == id);
            if (tagFromDb == null) return null;

            HashTagDto tagDto = new HashTagDto()
            {
                HashTagName = tagFromDb.HashTagName,
                Popularity = tagFromDb.Popularity
            };
            return tagDto;
        }

        public HashTag GetHashTagByName(string name)
        {
            return _context.HashTags.FirstOrDefault(h => h.HashTagName == name);
        }

        public int GetHashTagPopularity(Guid id)
        {
            var dbHashTag = _context.HashTags.FirstOrDefault(h => h.Id == id);
            return dbHashTag.Popularity;
        }

        public int GetHashTagPopularityByName(string name)
        {
            var dbHashTag = _context.HashTags.FirstOrDefault(h => h.HashTagName == name);
            return dbHashTag.Popularity;
        }

        public bool RemoveHashTag(Guid id)
        {
            var dbHashTag = _context.HashTags.FirstOrDefault(h => h.Id == id);
            _context.HashTags.Remove(dbHashTag);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
