using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OCPSolution.Data;
using OCPSolution.Models;
using PagedList;

namespace OCPSolution.Controllers
{
    public class SalesController : Controller
    {
        private readonly SaleContext _context;
        public SalesController(SaleContext context)
        {
            _context = context;
        }
        public IActionResult Index(DateTime begin, DateTime end, string currentFilter, int? page)
        {


            IEnumerable<Sale> ls = _context.Sales;
            if (begin != null & end != null)
            {
                ls.Where(x => x.Date.CompareTo(begin) >= 0 & x.Date.CompareTo(end) <= 0);
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(ls.ToPagedList(pageNumber, pageSize));

        }

        public IActionResult Create()
        {
            Sale sale = new Sale();
            sale.Date = DateTime.Now;

            _context.Add(sale);
            _context.SaveChanges();
            ViewBag.ListProducts = listProducts();

            return View(sale);
        }
        [HttpPost]
        public IActionResult Create([Bind("Id,Client,Date")] Sale sale)
        {
            try
            {
                Sale upsale = _context.Sales.Where(x => x.Id == sale.Id).FirstOrDefault();
                upsale.Client = sale.Client;
                _context.Update(upsale);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));


            }
            catch (Exception e)
            {

                ViewBag.Error = e.ToString();
                return View();
            }
        }
        public IActionResult AddRow()
        {
            ViewBag.ListProducts = listProducts();
            return View();
        }
        [HttpPost]
        public IActionResult AddRow(int id, int quantity)
        {
            try
            {
                IEnumerable<Medicine> lm = _context.Medicines;
                IEnumerable<Food> lf = _context.Foods;
                IEnumerable<Sale> ls = _context.Sales;
                Row row = new Row();
                Sale sale = new Sale();
                int Last = ls.Max(x => x.Id);
                sale = _context.Sales.Where(x => x.Id == Last).FirstOrDefault();
                if (lm.Where(x => x.Id == id).Sum(x => x.Price) > 0)
                {
                    Medicine product = lm.Where(x => x.Id == id).First();
                    row.Product = product;
                    row.Total = product.Desc * quantity;
                    sale.Total = sale.Total + row.Total;
                }
                else
                {
                    if (lf.Where(x => x.Id == id).Sum(x => x.Price) > 0)
                    {
                        Food product = lf.Where(x => x.Id == id).First();
                        row.Product = product;
                        row.Total = product.Price * quantity;
                        sale.Total = sale.Total + row.Total;
                    }
                }

                row.Quantity = quantity;
                row.Sale = sale;

                ViewData["Total"] = sale.Total;
                _context.Add(row);
                _context.Update(sale);
                _context.SaveChanges();

                ViewBag.ListProducts = listProducts();

                return View();
            }
            catch (Exception e)
            {
                ViewBag.ListProducts = listProducts();
                ViewBag.AddError = e.ToString();
                return View();
            }
        }
        public IActionResult Details(int? id)
        {
            try
            {
                IEnumerable<Row> rows = _context.Rows.Include(x => x.Product).Where(x => x.Sale.Id == id);
                return View(rows);
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
                Sale sales = _context.Sales.Where(x => x.Id == id).FirstOrDefault();
                return View(sales);
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
                Sale sale = _context.Sales.Find(id);
                IEnumerable<Row> rows = _context.Rows.Include(x => x.Sale).Where(x => x.Sale.Id == sale.Id);
                deleteRow(rows);
                _context.Remove(sale);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            return View();
        }
        private SelectList listProducts()
        {

            IEnumerable<Product> lp = _context.Products;
            return new SelectList(lp, "Id", "Name");
        }
        private void deleteRow(IEnumerable<Row> rows)
        {
            foreach (var r in rows)
            {
                _context.Remove(r);
            }
        }
    }
}