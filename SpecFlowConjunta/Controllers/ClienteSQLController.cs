using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpecFlowConjunta.Models;
using SpecFlowConjunta.Data;
using SpecFlowConjunta.Data;
using SpecFlowConjunta.Models;
using System.Diagnostics;

namespace SpecFlowConjunta.Controllers
{
    public class ClienteSQLController : Controller
    {
        private readonly ClienteSQLDataAccessLayer _dataAccessLayer;

        public ClienteSQLController()
        {
            _dataAccessLayer = new ClienteSQLDataAccessLayer();
        }

        // GET: ClienteSQL
        public IActionResult Index()
        {
            List<Product> products = _dataAccessLayer.GetAllProducts();
            return View(products);
        }

        // GET: ClienteSQL/Details/5
        public IActionResult Details(int id)
        {
            Product product = _dataAccessLayer.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ClienteSQL/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteSQL/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductName,Category,Price,StockQuantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                _dataAccessLayer.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ClienteSQL/Edit/5
        public IActionResult Edit(int id)
        {
            Product product = _dataAccessLayer.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ClienteSQL/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,ProductName,Category,Price,StockQuantity")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dataAccessLayer.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ClienteSQL/Delete/5
        public IActionResult Delete(int id)
        {
            Product product = _dataAccessLayer.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ClienteSQL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _dataAccessLayer.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}