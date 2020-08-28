using Domain.Application.Dto.Menus;
using Domain.Common;
using Infrastructure.Common;
using Infrastructure.Web.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Web
{
    public class UserInfoCache
    {
        ICacheBase cache;
        private readonly string MenuRoleCacheKey = "MenuRolePermission";
        public UserInfoCache(ICacheBase _cache)
        {
            cache = _cache;
        }

        private string GetUserKey(string UserId)
        {
            return string.Format("UserInfo:{0}", UserId);
        }

        public UserInfo GetUser(string UserId)
        {
            try
            {
                return cache.Get<UserInfo>(GetUserKey(UserId));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetUser(UserInfo user)
        {
            if (user == null || string.IsNullOrEmpty(user.Id))
            {
                throw new ArgumentException("Invalid Argument User");
            }
            cache.Set<UserInfo>(GetUserKey(user.Id), user);
        }

        public void RemoveUser(string UserId)
        {
            try
            {
                cache.Remove(GetUserKey(UserId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveMenuCaches()
        {
            try
            {
                cache.Remove(MenuRoleCacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

		public void UpdateMenuCaches(IEnumerable<CacheMenu> menus)
		{
            cache.Set<List<CacheMenu>>(MenuRoleCacheKey, menus.ToList());
		}

		public List<CacheMenu> GetMenuCaches()
		{
			try
			{
				return cache.Get<List<CacheMenu>>(MenuRoleCacheKey);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
