using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OCPSolution.Data;
using OCPSolution.Models;
using PagedList;

namespace OCPSolution.Controllers
{
    public class FoodsController : Controller
    {
        private readonly SaleContext _context;
        public FoodsController(SaleContext context)
        {
            _context = context;
        }
        public IActionResult Index(string search, string currentFilter, int? page)
        {

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;
            IEnumerable<Food> products = _context.Foods;
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(x => x.Name.Contains(search));
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Id,Name,Provider,DueDate,Price")] Food food)
        {
            try
            {
                _context.Add(food);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View();
            }
        }
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id != null)
                {
                    IEnumerable<Food> lf = _context.Foods;
                    Food food = lf.Where(x => x.Id == id).FirstOrDefault();
                    return View(food);
                }
                else
                {
                    ViewBag.Error = "Medicine not found";
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public IActionResult Edit(int? id, [Bind("Id,Name,Provider,DueDate,Price")] Food food)
        {
            try
            {
                if (id == food.Id)
                {
                    _context.Update(food);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Product not found";
                    return View();
                }

            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View();
            }
        }
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id != null)
                {
                    IEnumerable<Food> lf = _context.Foods;
                    Food product = lf.Where(x => x.Id == id).FirstOrDefault();
                    return View(product);
                }
                else
                {
                    ViewBag.Error = "product not found";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            try
            {
                var product = _context.Foods.Find(id);
                _context.Remove(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            return View();
        }
    }
}