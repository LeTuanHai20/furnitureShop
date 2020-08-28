using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application.Entities;
using Domain.Shop.Dto.Tags;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [BaseAuthorization]
    public class TagController : BaseController
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public ActionResult Index()
        {
            return View(_tagRepository.GetTagViewModels());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tagRepository.Add(new Tag()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = model.Name
                    });
                    _tagRepository.Save(requestContext);

                    return RedirectToAction("Index");
                }
                catch (Exception )
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public bool Delete(string id)
        {
            try
            {
                Tag tag = _tagRepository.Get(id);
                _tagRepository.Delete(tag);
                _tagRepository.Save(requestContext);
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public ActionResult Update(string id)
        {
            var model = _tagRepository.GetTagViewModel(id);
            if (model == null)
            {
                return View();
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Tag t = _tagRepository.All.Where(s => s.Id == tag.Id).First();
                    t.Name = tag.Name;
                    _tagRepository.Save(requestContext);
                }
                catch (Exception)
                {
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
