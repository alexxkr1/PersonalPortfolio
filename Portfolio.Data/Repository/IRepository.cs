using Portfolio.Core.Domain;
using Portfolio.Core.Domain.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        List<Post> GetAllPosts(string category);
        void RemovePost(int id);
        void AddPost(Post post);
        void UpdatePost(Post post);

        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsync();

    }
}
