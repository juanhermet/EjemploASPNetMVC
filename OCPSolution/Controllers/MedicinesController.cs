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
    public class MedicinesController : Controller
    {
        private readonly SaleContext _context;
        public MedicinesController(SaleContext context)
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
            IEnumerable<Medicine> products = _context.Medicines;
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
        public IActionResult Create([Bind("Id,Name,Provider,DueDate,Price")] Medicine medicine)
        {
            try
            {
                _context.Add(medicine);
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
                    IEnumerable<Medicine> lm = _context.Medicines;
                    Medicine medicine = lm.Where(x => x.Id == id).FirstOrDefault();
                    return View(medicine);
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
        public IActionResult Edit(int? id, [Bind("Id,Name,Provider,DueDate,Price")] Medicine medicine)
        {
            try
            {
                if (id == medicine.Id)
                {
                    _context.Update(medicine);
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
                    IEnumerable<Medicine> lm = _context.Medicines;
                    Medicine product = lm.Where(x => x.Id == id).FirstOrDefault();
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
                var product = _context.Medicines.Find(id);
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