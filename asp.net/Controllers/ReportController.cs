using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Models;
using System.Text;

namespace Intranet.Controllers
{
    public class ReportController : Controller
    {
 
        
        //
        // GET: /Report/
         

        public ActionResult Index()
        {
            dbEngine _db = new dbEngine();
            ViewBag.EmpList = _db.GetAllEmployers();
            ViewBag.EmpRigs = _db.GetAllEmployersRigs(); 
            return View();
        }

        //
        // GET: /Report/Details/5

        public void Details(int id=0, string StartDate="", string EndDate="", string Employer="All", string EmpRig ="",int fonttype=0)
        {
            RptGen rp = new RptGen();
            
          switch (id) 
          {
              case 1:
                      Response.ContentType = "text/csv";
                      Response.AppendHeader("Content-disposition", "attachment;filename=mailingLabels.csv");
                      Response.WriteFile(rp.GetMailingLabels());
                      Response.End();
                      break;

            case 2:
                      Response.ContentType = "text/csv";
                     // Response.AppendHeader("Content-disposition", "attachment;filename=Expiration.csv");
                      Response.Write(rp.GetExpiry(fonttype)); 
                      Response.End();
                      break;
 
              case 3:
                      Response.ContentType = "text/csv";
                     // Response.AppendHeader("Content-disposition", "attachment;filename=mailingLabels.csv");
                      Response.Write(rp.GetPending(1, Employer,fonttype));
                      Response.End();
                      break;

              case 4:
                      Response.ContentType = "text/csv";
                     // Response.AppendHeader("Content-disposition", "attachment;filename=mailingLabels.csv");
                      Response.Write(rp.GetPending(2, Employer,fonttype));
                      Response.End();
                      break;
 
              case 5:
                      Response.ContentType = "text/csv";
                     // Response.AppendHeader("Content-disposition", "attachment;filename=mailingLabels.csv");
                      if (StartDate != "" && EndDate != "")
                      {
                          Response.Write(rp.GetIssuedCred(DateTime.Parse(StartDate), DateTime.Parse(EndDate),fonttype));
                      }
                      else
                      {
                          // get all
                          Response.Write(rp.GetIssuedCred(DateTime.Now.AddYears(-50), DateTime.Now,fonttype));
                      }
                      Response.End();
                      break;
              case 6:
                      Response.ContentType = "text/csv";
                      // Response.AppendHeader("Content-disposition", "attachment;filename=mailingLabels.csv");
                      Response.Write(rp.GetPendingEmpRig(EmpRig, fonttype, ReportOpt.All)); 
                      Response.End();
                      break;

              case 7:
                      Response.ContentType = "text/csv";
                      // Response.AppendHeader("Content-disposition", "attachment;filename=mailingLabels.csv");
                      Response.Write(rp.GetPendingEmpRig(EmpRig, fonttype, ReportOpt.GOV));
                      Response.End();
                      break;
              case 8:
                      Response.ContentType = "text/csv";
                      // Response.AppendHeader("Content-disposition", "attachment;filename=mailingLabels.csv");
                      Response.Write(rp.GetPendingEmpRig(EmpRig, fonttype, ReportOpt.GC));
                      Response.End();
                      break;
              case 9:
                      Response.ContentType = "text/csv";
                      // Response.AppendHeader("Content-disposition", "attachment;filename=mailingLabels.csv");
                      Response.Write(rp.GetPendingEmpRig(EmpRig, fonttype, ReportOpt.Every));
                      Response.End();
                      break;
 
 


        }
    }

        //
        // GET: /Report/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Report/Create

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
        // GET: /Report/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Report/Edit/5

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
        // GET: /Report/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Report/Delete/5

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
