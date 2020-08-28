using Domain.Shop.Dto.BlogCategories;
using Domain.Shop.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Component
{
    public class BlogCategoryViewComponent : ViewComponent
    {
        private readonly IBlogCategoryRepository blogCategoryRepository;

        public BlogCategoryViewComponent(IBlogCategoryRepository blogCategoryRepository)
        {
            this.blogCategoryRepository = blogCategoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            IEnumerable<BlogCategoryViewModel> blogCategories = blogCategoryRepository.GetBlogCategoryViewModels();
            blogCategories = BlogCategoryViewModel.GetTreeBlogCategoryViewModels(blogCategories);
            return View(blogCategories);
        }
    }
}
