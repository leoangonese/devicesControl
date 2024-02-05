using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace phoneController.Controllers
{
    public class ItemController : Controller
    {
        private const string CacheKey = "ItemCache";

        private List<Item> GetOrInitializeCache()
        {
            var items = HttpContext.Cache[CacheKey] as List<Item>;

            if (items == null)
            {
                items = new List<Item>();
                HttpContext.Cache[CacheKey] = items;
            }

            return items;
        }

        public ActionResult Index()
        {
            var items = GetOrInitializeCache();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Item item)
        {
            var items = GetOrInitializeCache();
            items.Add(item);

            HttpContext.Cache[CacheKey] = items;

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var items = GetOrInitializeCache();
            var item = items.FirstOrDefault(e => e.id == id);
            return View(item);
        }

        public ActionResult Edit(int id)
        {
            var items = GetOrInitializeCache();
            var item = items.FirstOrDefault(e => e.id == id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            var items = GetOrInitializeCache();
            var existingItem = items.FirstOrDefault(e => e.id == item.id);
            if (existingItem != null)
            {
                existingItem.date = item.date;
                existingItem.type = item.type;
                existingItem.status = item.status;
                existingItem.hardware = item.hardware;
                existingItem.card = item.card;
                existingItem.online = item.online;
                existingItem.profile = item.profile;
            }

            HttpContext.Cache[CacheKey] = items;

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var items = GetOrInitializeCache();
            var item = items.FirstOrDefault(e => e.id == id);
            return View(item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var items = GetOrInitializeCache();
            var item = items.FirstOrDefault(e => e.id == id);

            if (item != null)
            {
                items.Remove(item);
            }

            HttpContext.Cache[CacheKey] = items;

            return RedirectToAction("Index");
        }
    }
}
