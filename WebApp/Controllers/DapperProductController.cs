using ADODB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Dapper;

using System.Data;

namespace WebApp.Controllers
{
    public class DapperProductController : Controller
    {
      

        private readonly IConfiguration Configuration;
       
        public DapperProductController(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        // GET: DapperProductController
        public ActionResult Index()
        {
            List<Products> products = new List<Products>();

            using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString("DbConnection")))
            {

                products = db.Query<Products>("Select * From Products").ToList();
            }
            return View(products);
        }

        // GET: DapperProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DapperProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DapperProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products)
        {
            try
            {
                
                using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString("DbConnection")))
                {
                    string sqlQuery = "Insert Into Products (Name, Description, UnitPrice,CategoryId) Values(@Name, @Description, @UnitPrice,@CategoryId)";

                    int rowsAffected = db.Execute(sqlQuery, products);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DapperProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Products product = new Products();
            using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString("DbConnection")))
            {
                product = db.Query<Products>("Select * From Products WHERE ProductID =" + id, new { id }).SingleOrDefault();

           
            }
            return View(product);
        }

        // POST: DapperProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Products  products)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString("DbConnection")))
                {
                    string sqlQuery = "UPDATE Products set Name='" + products.Name +
                        "',Description='" + products.Description +
                        "',CategoryId=" + products.CategoryId +
                         ",UnitPrice=" + products.UnitPrice +
                        " WHERE ProductID=" + products.ProductId;

                    int rowsAffected = db.Execute(sqlQuery);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DapperProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Products product = new Products();
            using (IDbConnection db = new SqlConnection(Configuration.GetConnectionString("DbConnection")))
            {
                string sqlQuery = "Delete From Products WHERE ProductID = " + id;

                int rowsAffected = db.Execute(sqlQuery);
            }
            return RedirectToAction("Index");
        }

        //// POST: DapperProductController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
      
        //}
    }
}
