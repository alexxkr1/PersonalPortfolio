﻿using Microsoft.AspNetCore.Mvc;
using Volta.Models;
using System.Diagnostics;
using Volta.Data;
using Portfolio.Core.Domain;
using Portfolio.Data.Repository;
using Portfolio.Data.FileManager;

namespace Volta.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repo;
        private readonly IFilemanager _filemanager;

        public HomeController(IRepository repo, IFilemanager filemanager)
        {
            _repo = repo;
            _filemanager = filemanager;
        }

        public IActionResult Index()
        {
          var posts = _repo.GetAllPosts();
            return View(posts);
        }
        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new PostViewModel());
            else
            {
                var post = _repo.GetPost((int) id);
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
    }
}