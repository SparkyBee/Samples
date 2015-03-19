using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Models;

namespace Intranet.Controllers
{
    public class InfoController : Controller
    {
        dbEngine _db; 

        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /Info/5

        
        //
        // GET: /Info/Details/5

        public ActionResult Details(int id=-100, string usermessage="")
        {
             _db = new dbEngine();

            MarInfo myMar = _db.GetMarinerInfo(id); 

            return View(myMar);
        }

        //
        // GET: /Info/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Info/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
              
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Info/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Info/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Info/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Info/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
