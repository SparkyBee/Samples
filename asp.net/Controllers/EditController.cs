using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Intranet.Models;
using Intranet.Utils;

namespace Intranet.Controllers
{
    public class EditController : Controller
    {
        dbEngine _db;

   
        [HttpPost]
        public void AddAct(FormCollection collect)
        {
            DateTime dt = Convert.ToDateTime(collect["setDate"]);
            string note = collect["setNotes"];
            int marlicid = Convert.ToInt16(collect["MarLicId"]); 

            _db = new dbEngine();

            _db.AddActivity(new Activity { MarinerLicenseID = marlicid, ActDate = dt, ActNotes = note }); 

        }
        [HttpPost]
        public void UpdateActivity(FormCollection collect)
        {
            DateTime dt = Convert.ToDateTime(collect["setDate"]);
            string note = collect["setNotes"];
            int actid = Convert.ToInt16(collect["ActID"]);

            _db = new dbEngine();

            _db.UpdateActivity(actid, dt, note); 

        }
        [HttpPost]
        public void DeleteActivity(FormCollection collect)
        {
            int actid = Convert.ToInt16(collect["ActID"]);

            _db = new dbEngine();

            _db.DeleteActivity(actid); 

        }



        [HttpPost]
        public void DelAttachment(FormCollection collect)
        {
            int attid = Convert.ToInt16(collect["attid"]);
            
            _db = new dbEngine();

            _db.MarinerAttachmentDelete(attid); 
            //_db.d(new Activity { MarinerLicenseID = marlicid, ActDate = dt, ActNotes = note });

        }

        [HttpPost]
        public void DelLicenseAttachment(FormCollection collect)
        {
            int attid = Convert.ToInt16(collect["attid"]);

            _db = new dbEngine();

            _db.LicenseAttachmentDelete(attid);
            //_db.d(new Activity { MarinerLicenseID = marlicid, ActDate = dt, ActNotes = note });

        }



        public ActionResult License(int id = 0)
        {
 
            _db = new dbEngine();

           MarinerLicense ml=  _db.GetAMarinerLicense(id);

           // set read only or Lookup in ViewBags & helps with debugging.
           IList<Country> cty = _db.GetCountries();
           IList<License> lic = _db.GetLicenses();
           IList<LicenseAttachment> licAttach = _db.GetLicenseAttachments(id);
           IList<Activity> actList = _db.GetMarActivities(id); 
           ViewBag.LookupCountry = cty;
           ViewBag.LookupLicense = lic;
           ViewBag.LicenseAttachments = licAttach;
           ViewBag.ActivityList = actList;
           Mariner mr = null; 
           if (ml != null)
           {
               mr = _db.GetMariner(ml.MarinerID);
               //find the country the lic is in 
               if ((ml.LicenseID != null) && (ml.LicenseID != 0))
               {
                   ViewBag.CurrentCountryID = lic.Where(p => p.Id == ml.LicenseID).Single().CountryID;
               }
               else
               {
                   ViewBag.CurrentCountryID = 0;
               }
           }

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
            

         
           return View(ml); 
            
        }
        //
        // POST: /Edit/UpdateLicense
        [HttpPost]
        public ActionResult UpdateLicense(FormCollection collect)
        {


            string UserMsg;
            string umType;
            MarinerLicense ml = new MarinerLicense();
            ml.id = Convert.ToInt16(collect["id"]);
            ml.MarinerID = Convert.ToInt16(collect["MarinerID"]);
            if (collect["EndorsementInfo"].Trim() == "")
            {

                umType = Const.UserError;
                UserMsg = "Error: Endorsement is required. Please try again.";
                return Redirect("License/" + ml.id.ToString() + "?type=" + umType + "&msg=" + Url.Encode(UserMsg));
                 
            }

            
            ml.LicenseID = Convert.ToInt16(collect["LicenseID"]);
            ml.EndorsementInfo = collect["EndorsementInfo"].Trim();

            if (ml.EndorsementInfo.Trim() == "DELETE LICENSE")
            {
                _db = new dbEngine();
                _db.MarinerLicenseDelete(ml.id);
                return Redirect("../Info/Details/" + ml.MarinerID.ToString() + "?type=s&msg=" + Url.Encode("License Deleted"));

            }
            else
            {
                ml.IssueDate = Convert.ToDateTime(collect["IssueDate"]);
                ml.ExpirationDate = Convert.ToDateTime(collect["ExpirationDate"]);
                ml.NotesGlobal = collect["NotesGlobal"].Trim();
                ml.NotesPendingGovt = collect["NotesPendingGovt"].Trim();
                ml.PendingGovt = (collect["ckPendingGovt"] == "true") ? true : false;
                ml.PendingGlobal = (collect["ckPendingGC"] == "true") ? true : false;
                _db = new dbEngine();

                if (_db.MarinerLicenseUpdate(ml))
                {
                    umType = "s";
                    UserMsg = "Update was Successful!";
                }
                else
                {
                    umType = "s";
                    UserMsg = "Error:  The update was not completed. Please try again.";
                }

                // return RedirectToAction(new { controller = "Info", action = "Detail", id = ml.id, usermessage =UserMsg });

                return Redirect("License/" + ml.id.ToString() + "?type=" + umType + "&msg=" + Url.Encode(UserMsg));

            }
        }





        public ActionResult Mariner(int id=0)
        {
            _db = new dbEngine();
            Mariner mr = _db.GetMariner(id);
            ViewBag.MarAttachments = _db.GetMarinerAttachments(id);
            int empId = _db.GetEmployerID(mr.Employer); 
            SelectList employerSelectList = new SelectList(_db.GetAllEmployers_Dropdown(),"Employer1", "Employer1",mr.Employer);
            ViewBag.EmployerSelectList = employerSelectList;
            return View(mr);
        }

        // This action handles the form POST and the upload

        [HttpPost]
        public ActionResult UpdateMariner(FormCollection collect)
        {
            string UserMsg;

            Mariner mr = new Mariner();
            mr.id = Convert.ToInt16(collect["id"]);
            if ((collect["LastName"].Trim() == "") || (collect["FirstName"].Trim() == ""))
            {

                UserMsg = "Error:  First Name and Last Name are required.";
                return Redirect("Mariner/" + mr.id.ToString() + "?type=" + Const.UserError + "&msg=" + Url.Encode(UserMsg));
            }

            
            mr.id = Convert.ToInt16(collect["id"]);
            mr.LastName =  collect["LastName"].Trim();
            mr.FirstName = collect["FirstName"].Trim();
            mr.MiddleName = collect["MiddleName"].Trim();

            if ((mr.LastName.Trim() == "DELETE") &&
               (mr.FirstName.Trim() == "DELETE") &&
               (mr.MiddleName.Trim() == "DELETE")
                )
            {
                // user request delete 
                _db = new dbEngine();
                _db.MarinerDelete(mr.id);
                return Redirect("Mariner/" + mr.id.ToString() + "?type=s" + "&msg=" + Url.Encode("Successful Deletion of Mariner"));
            }
            else
            {

                mr.Address = collect["Address"].Trim();
                mr.Address2 = collect["Address2"].Trim();
                mr.City = collect["City"].Trim();
                mr.State = collect["State"].Trim();
                mr.ZipCode = collect["ZipCode"].Trim();
                mr.HomePhone = collect["HomePhone"].Trim();
                mr.CellPhone = collect["CellPhone"].Trim();
                mr.Email = collect["Email"].Trim();
                mr.Country = collect["Country"].Trim();
               // var Empname = _db.GetEmployerName(Convert.ToInt16());
                mr.Employer = collect["Employer"].Trim();
                mr.RigName = collect["RigName"].Trim();
                mr.SSN = collect["SSN"].Trim();
                mr.Passport = collect["Passport"].Trim();
                mr.BirthCert = collect["BirthCert"].Trim();
                mr.DateOfBirth = Convert.ToDateTime(collect["DateOfBirth"]);
                mr.CityOfBirth = collect["CityOfBirth"].Trim();
                mr.StateOfBirth = collect["StateOfBirth"].Trim();
                mr.CountryOfBirth = collect["CountryOfBirth"].Trim();
                mr.Citizenship = collect["Citizenship"].Trim();
                /*   mr.HairColor = collect["HairColor"].Trim();
                   mr.EyeColor = collect["EyeColor"].Trim();
                   mr.Height = collect["Height"].Trim();
                   mr.Weight = collect["Weight"].Trim(); */
                mr.Notes = collect["Notes"].Trim();
                _db = new dbEngine();

                string umType;
                if (_db.MarinerUpdate(mr))
                {
                    umType = "s";
                    UserMsg = "Update was Successful!";
                }
                else
                {
                    umType = "s";
                    UserMsg = "Error:  The update was not completed. Please try again.";
                }

                // return RedirectToAction(new { controller = "Info", action = "Detail", id = ml.id, usermessage =UserMsg });



                return Redirect("Mariner/" + mr.id.ToString() + "?type=" + umType + "&msg=" + Url.Encode(UserMsg));
            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/documents"), "aa_" + fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Upload");
        }


        [HttpPost]
        public ActionResult UploadLicAttach(HttpPostedFileBase file, int licenseid, int marinerid)
        {

            _db = new dbEngine(); 
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {

                var mCurrent = _db.GetMariner(marinerid);
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/Documents folder
                
                var path =Path.Combine( Server.MapPath("~/Documents"), mCurrent.LastName.Trim() + "_"
                    + mCurrent.FirstName.Trim());

                Utils.Misc.DoesPathExistAndCreate(path); 
                var pathfile = Path.Combine(path,   fileName);
                file.SaveAs(pathfile);
                _db.LicenseAttachmentAdd(marinerid, licenseid, fileName, ("../../Documents/"+ mCurrent.LastName + "_"+ mCurrent.FirstName).Trim()); 
           }

                  // redirect back to the index action to show the form once again
            return RedirectToAction("License/"+licenseid);
        }

        [HttpPost]
        public ActionResult UploadMarAttach(HttpPostedFileBase file,  int marinerid)
        {

            _db = new dbEngine();
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {

                var mCurrent = _db.GetMariner(marinerid);
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/Documents folder

                var path = Path.Combine(Server.MapPath("~/Documents"), mCurrent.LastName.Trim() + "_"
                    + mCurrent.FirstName.Trim());

                Utils.Misc.DoesPathExistAndCreate(path);
                var pathfile = Path.Combine(path, fileName);
                file.SaveAs(pathfile);
                _db.MarinerAttachmentAdd(marinerid,  fileName, ("../../Documents/" + mCurrent.LastName + "_" + mCurrent.FirstName).Trim());
            }

            // redirect back to the index action to show the form once again
            return RedirectToAction("Mariner/" + marinerid);
        }







    }
}
