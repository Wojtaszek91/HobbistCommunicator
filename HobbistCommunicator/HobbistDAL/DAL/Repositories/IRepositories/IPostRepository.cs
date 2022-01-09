using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IPostRepository
    {
        public Post GetPostById(Guid id);
        public IEnumerable<Post> GetPostsByProfileId(Guid profileId, int index);
        public IEnumerable<Post> GetUserPostsFromDateToDate(DateTime beginDate, DateTime endDate, Guid userId, int index);
        public IEnumerable<Post> GetGroupPostsFromDateToDate(DateTime beginDate, DateTime endDate, Guid groupId, int index);
        public IEnumerable<Post> GetHashTagPostsFromDateToDate(DateTime beginDate, DateTime endDate, Guid hashTagId, int index);
        public IEnumerable<Post> GetPostsByProfileIdAndHashTag(Guid profileId, string hashTagName, int index);
        public IEnumerable<Post> GetPostsByHashTag(string hashTag, int index);
        public int GetPostAverageMark(Guid id);
        public int GetPostViews(Guid id);
        public bool AddPostView(Guid id);
        public bool EditPostMessage(Guid id, string message);
        public bool EditPostHashTag(Guid id, HashTagDto hashTag);
        public bool AddPost(Post post);
        public bool BlockPost(Guid postId);
        public bool UnblockPost(Guid postId);
        public bool DeletePost(Guid id);
        public bool EditPost(Post post);
        public Post EditBeginDate(DateTime beginDate, Guid id);
        public Post EditDayLast(int dayLast, Guid id);
        public bool AddFollower(Guid postId, Guid followerId);
        public bool RemoveFollower(Guid postId, Guid followerId);
        public bool DoesPostExists(Guid postId);
        public bool Save();
    }
}
