using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volta.Data;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data.FileManager;
using Portfolio.Data.Repository;
using Portfolio.Core.Domain;
using Portfolio.Core.Domain.Comments;
using Microsoft.AspNetCore.Identity;

namespace Portfolio.Controllers
{

   
    public class AdminController : Controller

    {
        private readonly IRepository _repo;

        private readonly IFilemanager _filemanager;


        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context, IRepository repo, IFilemanager filemanager)
        {
            _repo = repo;
            _filemanager = filemanager;
            _context = context;
        }

    
        public async Task<IActionResult> Index()
        {
            return View(await _context.posts.ToListAsync());
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new PostViewModel());
            else
            {
                var post = _repo.GetPost((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    body = post.body,
                    CurrentImage = post.Image,
                    Description = post.Description,
                    Category = post.Category,
                    Tags = post.Tags
                }); ;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {


            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                body = vm.body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags,

            };

            if (vm.Image == null)
                post.Image = vm.CurrentImage;
            else
                post.Image = await _filemanager.SaveImage(vm.Image);

            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);



            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_filemanager.ImageStream(image), $"image/{mime}");
        }

        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = vm.PostId });
            }
            var post = _repo.GetPost(vm.PostId);
            if (vm.MainCommentId > 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now,
                });
                _repo.UpdatePost(post);
            }
            //else
            //{
            //    var comment = new SubComment
            //    {
            //        MainCommentId = vm.MainCommentId,
            //        Message = vm.Message,
            //        Created = DateTime.Now,
            //    };
            //    _repo.AddSubComment(comment);
            //}

            await _repo.SaveChangesAsync();

            return View();
        }
    }
}
