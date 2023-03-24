using nimap.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace nimap.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        MvcEntities dbObj = new MvcEntities();

        public int ProductId { get; private set; }

        public ActionResult Product(Product obj)
        {
            if (obj != null)
                return View(obj);
            else
                return View();

        }
        [HttpPost]
        public ActionResult AddProduct(Product model)


        {


            if (ModelState.IsValid)
            {


                Product obj = new Product();

                obj.ProductId = model.ProductId;
                obj.ProductName = model.ProductName;
                obj.CategoryId = model.CategoryId;
                obj.CategoryName = model.CategoryName;



                if (model.ProductId == ProductId)
                {
                    dbObj.Product.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {

                    dbObj.SaveChanges();
                }

            }
            ModelState.Clear();

            return View("Product");

        }




        public ActionResult Delete(int ProductId)
        {
            var res = dbObj.Product.Where(x => x.ProductId == ProductId).First();
            dbObj.Product.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.Product.ToList();

            return View("ProductList", list);



        }



        public ActionResult ProductList(int pg = 1)
        {

            List<Product> products = dbObj.Product.ToList();

            const int pageSize = 10;
            if (pg < 1)
                pg = 1;

            int recsCount = products.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }







    }
}