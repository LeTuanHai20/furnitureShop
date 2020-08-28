using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Shop.Dto.Blogs;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Infrastructure.Common;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [BaseAuthorization]
    public class BlogController : BaseController
    {
        private readonly IBlogRepository blogRepository;
        private readonly IBlogCategoryRepository blogCategoryRepository;

        public BlogController(IBlogRepository blogRepository, IBlogCategoryRepository blogCategoryRepository)
        {
            this.blogRepository = blogRepository;
            this.blogCategoryRepository = blogCategoryRepository;
        }
        public ViewResult Index()
        {
            if (CheckBlogCategory())
            {
                ViewBag.CheckBlogCategory = true;
                return View(blogRepository.GetBlogsViewModel());
            }
            return View();
        }
        private bool CheckBlogCategory()
        {
            if (blogCategoryRepository.GetBlogCategoryViewModels().Any())
            {
                return true;
            }
            return false;
        }
        public ViewResult Create()
        {
            ViewBag.slugs = blogCategoryRepository.All.Select(b => new SelectListItem
            {
                Text = b.Slug,
                Value = b.Slug
            }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Id = Guid.NewGuid().ToString();
                    Blog blog = new Blog();
                    PropertyCopy.Copy(model, blog);
                    blogRepository.Add(blog);
                    blogRepository.Save(requestContext);
                    return RedirectToAction("Index");
                }
                catch (Exception )
                {
                    return View();
                }
            }
            return View();
        }
        public ViewResult Update(string id)
        {
            ViewBag.slugs = blogCategoryRepository.All.Select(b => new SelectListItem
            {
                Text = b.Slug,
                Value = b.Slug
            }).ToList();
            return View(blogRepository.GetBlogViewModel(id));
        }
        [HttpPost]
        public ActionResult Update(BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Blog blog = new Blog();
                    PropertyCopy.Copy(model, blog);
                    blogRepository.Update(blog);
                    blogRepository.Save(requestContext);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.slugs = blogCategoryRepository.All.Select(b => new SelectListItem
                    {
                        Text = b.Slug,
                        Value = b.Slug
                    }).ToList();
                    return View();
                }
            }
            ViewBag.slugs = blogCategoryRepository.All.Select(b => new SelectListItem
            {
                Text = b.Slug,
                Value = b.Slug
            }).ToList();
            return View();
        }
        public bool Delete(string id)
        {
            try
            {
                Blog blog = blogRepository.All.Where(b => b.Id == id).FirstOrDefault();
                blogRepository.Delete(blog);
                blogRepository.Save(requestContext);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
