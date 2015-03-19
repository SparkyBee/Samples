using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Models;



namespace Intranet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.SearchError = "help-block hide";
            return View();
        }

        [HttpGet]
        public ActionResult Index(string rdSearchOptions, string txtSearch )
        {

            IList<MarinerResult> rList = null;
            ViewBag.SearchError = "help-block hide";

            if (txtSearch == "")
            {
                ViewBag.SearchError = "help-block ";
            }
            else
            {
                dbEngine _db = new dbEngine();

                switch (rdSearchOptions)
                {
                    case "Last":
                        {
                            rList = _db.SearchforMariner(SearchOptions.Last,txtSearch);
                            break; 
                        }
                    case "First":
                        {
                            rList = _db.SearchforMariner(SearchOptions.First, txtSearch);
                            break;
                        }
                    case "Emp":
                        {
                            rList = _db.SearchforMariner(SearchOptions.Emp, txtSearch);
                            break;
                        }
                    case "DOB":
                        {
                            rList = _db.SearchforMariner(SearchOptions.DOB, txtSearch);
                            break;
                        }
                    
                
                }

                   
                
            }
            ViewBag.ResultData = rList; 

            return View();
        }

        public ActionResult test1()
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }
    }
}
