using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Models;
using Intranet.Utils;

namespace Intranet.Controllers
{
    public class MaintController : Controller
    {
        //
        // GET: /Maint/
        dbEngine _db;

        public ActionResult Index()
        {
            _db = new dbEngine();
            // set read only or Lookup in ViewBags
            IList<Country> cty = _db.GetCountries();
            IList<License> lic = _db.GetLicenses();
            ViewBag.LookupCountry = cty;
            ViewBag.LookupLicense = lic;



            return View();
        }

        
        [HttpPost]
        public ActionResult NewCountry(FormCollection collect)
        {
            _db = new dbEngine();
             string UserMsg;
            string umType;

            string aNewOne = collect["NewCountry"].Trim();
            if ((aNewOne != "") && (aNewOne != null))
            {

                if (_db.AddCountry(aNewOne))
                {
                    umType = Const.UserSuccess;
                    UserMsg = "New Country was Successful!";
                    return Redirect("?type=" + umType + "&msg=" + Url.Encode(UserMsg));
                }
                else
                {
                    umType = Const.UserError;
                    UserMsg = "Error:  Country was not added. ";
                    return Redirect("?type=" + umType + "&msg=" + Url.Encode(UserMsg));
                }

            }
            else
            {
                umType = Const.UserError;
                UserMsg = "Error:  Country input had no value and is required. ";
                return Redirect("?type=" + umType + "&msg=" + Url.Encode(UserMsg));
            
            
            }
            
           // return View();
        }

        
        
        [HttpPost]
        public ActionResult NewLicense(FormCollection collect)
        {

             _db = new dbEngine();
             string UserMsg;
             string umType;

            string aNewOne = collect["NewLic"].Trim();
             int Countryid = Convert.ToInt16(collect["CountryType"]);
             if ((aNewOne != "") && (aNewOne != null) && (Countryid !=0))
             {

                 if (_db.AddCountryLicense(Countryid, aNewOne))
                 {
                     umType = Const.UserSuccess;
                     UserMsg = "New License was Successfully added.";
                     return Redirect("?type=" + umType + "&msg=" + Url.Encode(UserMsg));
                 }
                 else
                 {
                     umType = Const.UserError;
                     UserMsg = "Error:  License was not added. ";
                     return Redirect("?type=" + umType + "&msg=" + Url.Encode(UserMsg));
                 }

             }
             else
             {
                 umType = Const.UserError;
                 UserMsg = "Error:  License input had no value or Country was not selected. ";
                 return Redirect("?type=" + umType + "&msg=" + Url.Encode(UserMsg));


             }
 

        }


        [HttpPost]
        public ActionResult NewEmployer(FormCollection collect)
        {

            _db = new dbEngine();
            string UserMsg;
            string umType;
            if (collect["Pass"].ToString().Trim().ToUpper() == Const.ADMIN_PASS.Trim().ToUpper())
            {
                string newEmployer   =  collect["Emp"].ToString().Trim();
                if (_db.EmployerAddNew(newEmployer))
                {

                    umType = Const.UserSuccess;
                    UserMsg = "New Employer was Successfully added.";
                    return Redirect("?type=" + umType + "&msg=" + Url.Encode(UserMsg));
                }
                else
                {
                    umType = Const.UserError;
                    UserMsg = "Error:  Password is Incorrect. Employer not Saved.";
                    return Redirect("?type=" + umType + "&msg=" + Url.Encode(UserMsg));                
                }

            }
            else
            { 
                umType = Const.UserError;
                UserMsg = "Error:  Password is Incorrect. Employer not Saved.";
                return Redirect("?type=" + umType + "&msg=" + Url.Encode(UserMsg));
            }


        } 



        //
        // POST: /Maint/Create

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
        // GET: /Maint/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Maint/Edit/5

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
        // GET: /Maint/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Maint/Delete/5

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
