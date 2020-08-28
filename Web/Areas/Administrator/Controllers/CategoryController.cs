using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Shop.Dto.Category;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Infrastructure.Common;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [BaseAuthorization]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
       
        }
        public ViewResult Index()
        {
            IEnumerable<CategoryViewModel> categories = categoryRepository.GetCategoryViewModels();
            categories = CategoryViewModel.GetTreeMenuViewModels(categories);
            return View(categories);
        }
        private void SetComboData(string HierarchyCode = null)
        {
            var categoryQuery = categoryRepository.All;
            if (!string.IsNullOrEmpty(HierarchyCode))
            {
                categoryQuery = categoryQuery.Where(p => !p.HierarchyCode.StartsWith(HierarchyCode));
            }
            ViewBag.categories = categoryQuery.OrderBy(p => p.HierarchyCode).Select(p => new SelectListItem
            {
                Text = p.CategoryName,
                Value = p.HierarchyCode
            }).ToList(); 
        }
        public ViewResult Create()
        {
            SetComboData();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!categoryRepository.CheckSlugExist(model.Slug)) {
                        model.Id = Guid.NewGuid().ToString();
                        Category category = new Category();
                        PropertyCopy.Copy(model, category);
                        category.HierarchyCode = categoryRepository.GenerateHierarchyCode(model.ParentHierarchyCode);
                        categoryRepository.Add(category);
                        categoryRepository.Save(requestContext);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception )
                { 
                    SetComboData();
                    return View();
                }
            }
            SetComboData();
            return View();
        }
        public ActionResult Update(string id)
        {
            var model = categoryRepository.GetMenuViewModel(id);
            SetComboData(model.HierarchyCode);
            model.ParentHierarchyCode = model.HierarchyCode.Substring(0, model.HierarchyCode.Length - Domain.Common.Consts.Infrastructure.HierarchyCodeLength);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!categoryRepository.CheckSlugExist(model.Slug))
                    {
                        Category category = categoryRepository.Single(p => p.Id == model.Id);
                        PropertyCopy.Copy(model, category);
                        string CurrentHierarchyParent = category.HierarchyCode.Substring(0, category.HierarchyCode.Length - Domain.Common.Consts.Infrastructure.HierarchyCodeLength);
                        if (string.IsNullOrEmpty(model.ParentHierarchyCode) ? !string.IsNullOrEmpty(CurrentHierarchyParent) : model.ParentHierarchyCode != CurrentHierarchyParent)
                        {
                            var HierarchyCode = categoryRepository.GenerateHierarchyCode(model.ParentHierarchyCode);
                            var ChildMenus = categoryRepository.GetChildMenus(category.HierarchyCode);
                            foreach (Category item in ChildMenus)
                            {
                                item.HierarchyCode = model.ParentHierarchyCode + item.HierarchyCode.Substring(HierarchyCode.Length);
                            }
                            category.HierarchyCode = HierarchyCode;
                        }
                        categoryRepository.Update(category);
                        categoryRepository.Save(requestContext);
                    }       
                }
                catch (Exception )
                {
                    SetComboData(model.HierarchyCode);
                    return View();
                }
                return RedirectToAction("Index");
            }
            SetComboData(model.HierarchyCode);
            return View();
        }
        [HttpPost]
        public bool Delete(string id)
        {
            try
            {
                Category menu = categoryRepository.Single(p => p.Id == id);
                if (categoryRepository.CanDeleteMenu(menu.HierarchyCode))
                {
                    categoryRepository.Delete(menu);
                    categoryRepository.Save(requestContext);  
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception )
            {
                return false;
            }

        }
    }
   
    
}
