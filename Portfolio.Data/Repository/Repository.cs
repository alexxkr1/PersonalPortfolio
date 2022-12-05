using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;
using Portfolio.Core.Domain;
using Portfolio.Core.Domain.Comments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volta.Data;

namespace Portfolio.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            _context.posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {

            return _context.posts.ToList();
        }


        public List<Post> GetAllPosts(string category)
        {
            Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };

            //return _context.posts
            //    .Where(post => InCategory(post))
            //    .ToList();
            return _context.posts.Where(post => post.Category == category).ToList();
            //return _context.posts.ToList();
        }

       
        public Post GetPost(int id)
        {
            return _context.posts
                .Include(p => p.MainComments)
                .Include(p => p.MainComments)
                .FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
           _context.posts.Remove(GetPost(id));
            
        }

        public void UpdatePost(Post post)
        {
            _context.posts.Update(post);        
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0 )
            {
                return true;
            }
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }
    }
}
