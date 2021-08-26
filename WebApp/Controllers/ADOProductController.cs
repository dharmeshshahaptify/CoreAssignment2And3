using ADODB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Controllers
{
    public class ADOProductController : Controller
    {
        private readonly IConfiguration Configuration;
        ProcessData ProcessData;
        public ADOProductController(IConfiguration configuration)
        {
            Configuration = configuration;
            ProcessData = new ProcessData(Configuration.GetConnectionString("DbConnection"));
        }


        public IActionResult Index()
        {
            SqlConnection sqlconnection = ProcessData.GetConnection();
            List<Products> p = ProcessData.getListProduct(sqlconnection);

            return View(p);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Products p)
        {
            try
            {
                SqlConnection sqlconnection = ProcessData.GetConnection();
                ProcessData.saveProduct(sqlconnection, p);

            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            SqlConnection sqlconnection = ProcessData.GetConnection();
            //Product p =ProcessData.getproduct(sqlconnection, id);
            Products p = ProcessData.usp_getproduct(sqlconnection, id);

            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Products p)
        {
            try
            {
                SqlConnection sqlconnection = ProcessData.GetConnection();
                ProcessData.UpdateProduct(sqlconnection, p);

            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            SqlConnection sqlconnection = ProcessData.GetConnection();
            ProcessData.deleteproduct(sqlconnection, id);

            return RedirectToAction("Index");
        }

    }
}
