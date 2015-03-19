using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Models;
using Intranet.Utils;

namespace Intranet.Controllers
{
    public class NewController : Controller
    {
        dbEngine _db;

        public ActionResult License(int id = 0)
        {
            _db = new dbEngine();

            ViewBag.Error = null; 
            Mariner mr = _db.GetMariner(id);

            // set read only or Lookup in ViewBags
            IList<Country> cty = _db.GetCountries();
            IList<License> lic = _db.GetLicenses();
           
            ViewBag.LookupCountry = cty;
            ViewBag.LookupLicense = lic;
    
            if (mr != null)
            {
                ViewBag.Mariner = mr.LastName + ", " + mr.FirstName + " " + mr.MiddleName;
                ViewBag.MarinerAddress = mr.City + ", " + mr.State + " " + mr.Country;

            }
            else
            {
                ViewBag.Mariner = "";
                ViewBag.MarinerAddress = "";

            }
            return View(); 
        }

        [HttpPost]
        public ActionResult License(FormCollection collect)
        {
            ViewBag.Error = null; 
            MarinerLicense ml = new MarinerLicense();
            ml.MarinerID = Convert.ToInt16(collect["MarinerID"]);
            string UserMsg;
            string umType;
            if (collect["EndorsementInfo"].Trim() == "")
            { 
            
                    umType = Const.UserError;
                    UserMsg = "Error: Endorsement is required. Please try again.";
                    return Redirect( ml.MarinerID.ToString() + "?type=" + umType + "&msg=" + Url.Encode(UserMsg));
            }

            
            
            
            
            
            if (collect["LicenseID"] == "")
            {
                ml.LicenseID = 0; 
            }
            else
            {
                ml.LicenseID = Convert.ToInt16(collect["LicenseID"]);
            }
            if  (ml.LicenseID == 0)
            {
                /// repeat standard page hit if error. 
                /// make sure to pass id
                        _db = new dbEngine();
                        Mariner mr = _db.GetMariner(ml.MarinerID);

                        // set read only or Lookup in ViewBags
                        IList<Country> cty = _db.GetCountries();
                        IList<License> lic = _db.GetLicenses();

                        ViewBag.LookupCountry = cty;
                        ViewBag.LookupLicense = lic;

                        if (mr != null)
                        {
                            ViewBag.Mariner = mr.LastName + ", " + mr.FirstName + " " + mr.MiddleName;
                            ViewBag.MarinerAddress = mr.City + ", " + mr.State + " " + mr.Country;

                        }
                        else
                        {
                            ViewBag.Mariner = "";
                            ViewBag.MarinerAddress = "";

                        }

                        ViewBag.Error = "Error";

                return View(); 
            }
            else
            {
                ml.EndorsementInfo = collect["EndorsementInfo"];
                try
                {
                    ml.IssueDate = Convert.ToDateTime(collect["IssueDate"]);
                }
                catch (Exception ex)
                {
                    ml.IssueDate = null;
                }
                try
                {
                    ml.ExpirationDate = Convert.ToDateTime(collect["ExpirationDate"]);
                }
                catch (Exception ex)
                {
                    ml.ExpirationDate = null;
                }
                ml.NotesGlobal = collect["NotesGlobal"];
                ml.NotesPendingGovt = collect["NotesPendingGovt"];
                ml.PendingGovt =  (collect["ckPendingGovt"] == "true") ? true: false;
                ml.PendingGlobal = (collect["ckPendingGC"] == "true") ? true: false; 
                _db = new dbEngine();
                
                if (_db.MarinerLicenseAddNew(ml))
                {
                    umType = Const.UserSuccess;
                    UserMsg = "Update was Successful!";
                    return Redirect("../../Info/Details/" + ml.MarinerID.ToString() + "?type=" + umType + "&msg=" + Url.Encode(UserMsg));
                }
                else
                {
                    umType = Const.UserError;
                    UserMsg = "Error:  The update was not completed. Please try again.";
                    return Redirect( ml.MarinerID.ToString() + "?type=" + umType + "&msg=" + Url.Encode(UserMsg));
                }

                
            }
            
          
        }

        //
        // GET: /New/Create

        public ActionResult Mariner()
        {
            _db = new dbEngine();
            SelectList employerSelectList = new SelectList(_db.GetAllEmployers_Dropdown(), "id", "Employer1");
            ViewBag.EmployerSelectList = employerSelectList;
            return View();
        } 

        //
        // POST: /New/Create

        [HttpPost]
        public ActionResult Mariner(FormCollection collect)
        {

            string UserMsg;
            
            Mariner mr = new Mariner();

            if ((collect["LastName"].Trim() == "") || (collect["FirstName"].Trim() == "") | (collect["Employer"].Trim() == "") || (collect["Employer"].Trim() == "-- Select Employer --"))
            {
                      
                  UserMsg = "Error:  First Name, Last Name and Employer are required to add a new Mariner";
                  return Redirect("Mariner" + "?type=" + Const.UserError + "&msg=" + Url.Encode(UserMsg));
            }

            mr.LastName = collect["LastName"].Trim();
            mr.FirstName = collect["FirstName"].Trim();
            mr.MiddleName = collect["MiddleName"].Trim();
            mr.Address = collect["Address"].Trim();
            mr.Address2 = collect["Address2"].Trim();
            mr.City = collect["City"].Trim();
            mr.State = collect["State"].Trim();
            mr.ZipCode = collect["ZipCode"].Trim();
            mr.HomePhone = collect["HomePhone"].Trim();
            mr.CellPhone = collect["CellPhone"].Trim();
            mr.Email = collect["Email"].Trim();
            mr.Country = collect["Country"].Trim();
            _db = new dbEngine();
            var Empname = _db.GetEmployerName(Convert.ToInt16(collect["Employer"]));
            mr.Employer = Empname;
            collect["Employer"].Trim();
            mr.RigName = collect["RigName"].Trim();
            mr.SSN = collect["SSN"].Trim();
            mr.Passport = collect["Passport"].Trim();
            mr.BirthCert = collect["BirthCert"].Trim();
            mr.Citizenship = collect["Citizenship"].Trim(); 
            try
            {
                mr.DateOfBirth = Convert.ToDateTime(collect["DateOfBirth"]);
            }
            catch (Exception ex)
            {
                mr.DateOfBirth = null ; 
            }
            mr.CityOfBirth = collect["CityOfBirth"].Trim();
            mr.StateOfBirth = collect["StateOfBirth"].Trim(); 

            mr.CountryOfBirth = collect["CountryOfBirth"].Trim();
           /* mr.HairColor = collect["HairColor"].Trim();
            mr.EyeColor = collect["EyeColor"].Trim();
            mr.Height = collect["Height"].Trim();
            mr.Weight = collect["Weight"].Trim(); */ 
            mr.Notes = collect["Notes"].Trim();
            _db = new dbEngine();
            string umType = "";
            if (_db.MarinerAddNew(mr))
            {
                umType = Const.UserSuccess;
                UserMsg = "Update was Successful!";
                return Redirect("../Info/Details/" + mr.id.ToString() + "?type=" + umType + "&msg=" + Url.Encode(UserMsg));
            }
            else
            {
                umType = Const.UserError;
                UserMsg = "Error:  The update was not completed. Please try again.";
                return Redirect("New/Mariner" + "?type=" + umType + "&msg=" + Url.Encode(UserMsg));
        
            }

            // return RedirectToAction(new { controller = "Info", action = "Detail", id = ml.id, usermessage =UserMsg });



           // return Redirect("Mariner/" + mr.id.ToString() + "?type=" + umType + "&msg=" + Url.Encode(UserMsg));
        }
        
        //
        // GET: /New/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /New/Edit/5

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
        // GET: /New/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /New/Delete/5

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
