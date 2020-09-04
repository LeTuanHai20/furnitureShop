using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Shop.Dto.ShopSetting;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [BaseAuthorization]
    public class ShopSettingController : BaseController
    {
        private readonly IShopSettingRepository _shopSettingRepository;
        public ShopSettingController(IShopSettingRepository shopSettingRepository)
        {
            _shopSettingRepository = shopSettingRepository;
        }
        public ActionResult Index()
        {
            ViewBag.Message = TempData["WarningCreateShopSetting"];
            return View(_shopSettingRepository.GetShopSettingViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ShopSettingViewModel shopSetting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_shopSettingRepository.All.Count() == 0)
                    {
                        _shopSettingRepository.Add(new ShopSetting()
                        {
                            Id = Guid.NewGuid().ToString(),
                            PageName = shopSetting.PageName,
                            Pagetitle = shopSetting.Pagetitle,
                            PageDescription = shopSetting.PageDescription,
                            Keyword = shopSetting.Keyword,
                            TaxPercent = shopSetting.TaxPercent
                        });
                        _shopSettingRepository.Save(requestContext);
                    }
                    else 
                    {
                        ShopSetting s = _shopSettingRepository.All.Where(s => s.Id == shopSetting.Id).First();
                        s.PageName = shopSetting.PageName;
                        s.Pagetitle = shopSetting.Pagetitle;
                        s.PageDescription = shopSetting.PageDescription;
                        s.Keyword = shopSetting.Keyword;
                        s.TaxPercent = shopSetting.TaxPercent;
                        _shopSettingRepository.Save(requestContext);
                    }
                    
                }
                catch (Exception )
                {
                    return View(_shopSettingRepository.GetShopSettingViewModel());
                }
                return View(_shopSettingRepository.GetShopSettingViewModel());
            }
            return View(_shopSettingRepository.GetShopSettingViewModel());
        }
    }
}
