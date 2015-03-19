using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intranet.Utils;
using System.Reflection;
using System.IO;
using System.Diagnostics;
 



namespace Intranet.Models
{
    public enum SearchOptions {Last=1, First=2, Emp=3, DOB=4 }; //define search Get options. 
    public enum ReportOpt { All, GOV, GC, Every };

    public class dbEngine
    {
       /// <summary>
       /// Repository
       /// </summary>
        
        public dbEngine()
        {
        }

        

        #region  Search
        public IList<MarinerResult> SearchforMariner(SearchOptions Options,  string SearchText)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    IList<MarinerResult> myList = null;  
                    switch (Options)
                    {
                        case SearchOptions.Last:
                            {
                                myList = (from p in db.Mariners
                                             where
                                                 p.LastName.StartsWith(SearchText)
                                             orderby p.LastName
                                             select new MarinerResult
                                             {
                                                 Fullname = p.LastName.Trim() + ", " + p.FirstName.Trim(),
                                                 CompanyName = p.Employer.Trim(),
                                                 DOB = DtrStr(p.DateOfBirth),
                                                 Location = p.Country + ": " + p.City + ", " + p.State,
                                                 MarinerID = p.id
                                             }).ToList();    
                                
                                
                                
                                break; 
                            }
                        case SearchOptions.First:
                            {
                                myList = (from p in db.Mariners
                                             where
                                                 p.FirstName.StartsWith(SearchText)
                                             orderby p.FirstName
                                             select new MarinerResult
                                             {
                                                 Fullname =  p.FirstName.Trim() + " " + p.LastName.Trim(),
                                                 CompanyName = p.Employer.Trim(),
                                                 DOB = DtrStr(p.DateOfBirth),
                                                 Location = p.Country + ": " + p.City + ", " + p.State,
                                                 MarinerID = p.id
                                             }).ToList();    
                                
                                
                                break; 
                            
                            }
                        case SearchOptions.Emp:
                            {
                                myList = (from p in db.Mariners
                                             where
                                                 p.Employer.StartsWith(SearchText)
                                             orderby p.Employer, p.LastName
                                             select new MarinerResult
                                             {
                                                 Fullname = p.LastName.Trim() + ", " + p.FirstName.Trim(),
                                                 CompanyName = p.Employer.Trim(),
                                                 DOB = DtrStr(p.DateOfBirth),
                                                 Location = p.Country + ": " + p.City + ", " + p.State,
                                                 MarinerID = p.id
                                             }).ToList();
                                
                                break; 
                            }

                        case SearchOptions.DOB:
                            {
                                myList = (from p in db.Mariners
                                          where
                                              p.DateOfBirth == Convert.ToDateTime(SearchText)
                                          orderby p.LastName, p.Employer
                                          select new MarinerResult
                                          {
                                              Fullname = p.LastName.Trim() + ", " + p.FirstName.Trim(),
                                              CompanyName = p.Employer.Trim(),
                                              DOB = DtrStr(p.DateOfBirth),
                                              Location = p.Country + ": " + p.City + ", " + p.State,
                                              MarinerID = p.id
                                          }).ToList();

                                break;
                            }
                        
                    
                    }
                    
                    if (myList.Any())
                    {
                        return myList.ToList(); 
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null; 
                }
            }

        }

        public string DtrStr(DateTime? currentDT)
        {
            try
            {
                return Convert.ToDateTime(currentDT).ToString("MM/dd/yyyy"); 
            
            }
            catch (Exception ex)
            {
                Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                return ""; 
            }
        
        
        }


        public IList<MarinerResult> SearchforCompany(string CompanyName)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var myList = from p in db.Mariners
                                 where
                                     p.Employer.StartsWith(CompanyName)
                                 orderby p.LastName
                                 select new MarinerResult
                                 {
                                     Fullname = p.LastName + ", " + p.FirstName,
                                     CompanyName = p.Employer,
                                     Location = p.Country + ": " + p.City + ", " + p.State,
                                     MarinerID = p.id
                                 };
                    if (myList.Any())
                    {

                        return myList.ToList();

                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }

        }
        #endregion

        #region Mariner

        public bool MarinerAddNew(Mariner NewMariner)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    db.Mariners.InsertOnSubmit(NewMariner);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }    

        public bool MarinerUpdate(Mariner UpdateMariner)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var newMar = (from p in db.Mariners where p.id == UpdateMariner.id
                                 select p).Single();
                    newMar.LastName = UpdateMariner.LastName;
                    newMar.FirstName = UpdateMariner.FirstName;
                    newMar.MiddleName = UpdateMariner.MiddleName;
                    newMar.Address = UpdateMariner.Address;
                    newMar.Address2 = UpdateMariner.Address2; 
                    newMar.City = UpdateMariner.City; 
                    newMar.State = UpdateMariner.State;
                    newMar.ZipCode = UpdateMariner.ZipCode;
                    newMar.Country = UpdateMariner.Country;
                    newMar.HomePhone = UpdateMariner.HomePhone;
                    newMar.CellPhone = UpdateMariner.CellPhone;
                    newMar.Email = UpdateMariner.Email;
                    newMar.Employer = UpdateMariner.Employer;
                    newMar.RigName = UpdateMariner.RigName;
                    newMar.Citizenship = UpdateMariner.Citizenship;
                    newMar.SSN = UpdateMariner.SSN;
                    newMar.Passport = UpdateMariner.Passport;
                    newMar.BirthCert = UpdateMariner.BirthCert;
                    newMar.DateOfBirth = UpdateMariner.DateOfBirth;
                    newMar.CityOfBirth = UpdateMariner.CityOfBirth;
                    newMar.StateOfBirth = UpdateMariner.StateOfBirth;
                    newMar.HairColor = UpdateMariner.HairColor;
                    newMar.EyeColor = UpdateMariner.EyeColor;
                    newMar.Height = UpdateMariner.Height;
                    newMar.Weight = UpdateMariner.Weight;
                    newMar.Notes = UpdateMariner.Notes; 



                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }    

        public bool MarinerDelete(int MarinerID)
        {
        
         using (var db = new dbGCompDataContext())
            {
                try
                {
                    string FirstName ="";
                    string LastName =""; 

                    var DeleteThis = (from p in db.Mariners
                                      where p.id == MarinerID
                                      select p).Single();
                    FirstName = DeleteThis.FirstName; 
                    LastName = DeleteThis.LastName;
                    //delete the licenses
                    db.MarinerLicenses.DeleteAllOnSubmit(db.MarinerLicenses.Where(p => p.MarinerID == DeleteThis.id)); 
                    //delete the mariner
                    db.Mariners.DeleteOnSubmit(DeleteThis);
                    
                    db.SubmitChanges();
                    
                    string oldPath = Path.Combine( System.Web.HttpContext.Current.Server.MapPath("~/Documents"), LastName.Trim() + "_"  + FirstName.Trim());
                    string newPath = Path.Combine(  System.Web.HttpContext.Current.Server.MapPath("~/Documents"),"__deleted___" + LastName.Trim() + "_"  + FirstName.Trim());  
                    Utils.Misc.MoveDirectory(oldPath, newPath);

                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        
        
        }

        public Mariner GetMariner(int MarinerID) 
        {
        
        using (var db = new dbGCompDataContext())
            {
                try
                {
                   var Mar = (from p in db.Mariners where p.id == MarinerID
                             select p).Single();
 
                   return Mar; 
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }
        
        
        }

        #endregion 

        #region License
        public bool MarinerLicenseAddNew(int MarinerID, int LicenseID, string endorsement, DateTime IssueDate, DateTime ExpDate,  string notesPending, string NotesGlobal)
        {        

          
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    MarinerLicense ml = new MarinerLicense();
                    ml.MarinerID = MarinerID;
                    ml.LicenseID = LicenseID;
                    ml.EndorsementInfo = endorsement;
                    ml.IssueDate = IssueDate;
                    ml.ExpirationDate = ExpDate;
                    ml.NotesGlobal = NotesGlobal;
                    ml.NotesPendingGovt = notesPending; 

                    db.MarinerLicenses.InsertOnSubmit(ml);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }

        public bool MarinerLicenseAddNew(MarinerLicense marinerLisc)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    db.MarinerLicenses.InsertOnSubmit(marinerLisc);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }    

        public bool MarinerLicenseUpdate(MarinerLicense marinerLisc)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var ml = (from p in db.MarinerLicenses
                                          where p.id == marinerLisc.id
                                          select p).Single();
                 
                    ml.NotesPendingGovt = marinerLisc.NotesPendingGovt;
                    ml.NotesGlobal = marinerLisc.NotesGlobal;
                    ml.IssueDate = marinerLisc.IssueDate;
                    ml.LicenseID = marinerLisc.LicenseID;
                    ml.MarinerID = marinerLisc.MarinerID;
                    ml.ExpirationDate = marinerLisc.ExpirationDate;
                    ml.EndorsementInfo = marinerLisc.EndorsementInfo;
                    ml.PendingGlobal = marinerLisc.PendingGlobal;
                    ml.PendingGovt = marinerLisc.PendingGovt;

                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }

        public bool MarinerLicenseDelete(int marinerLiscenseID)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var DeleteThis = (from p in db.MarinerLicenses
                                      where p.id == marinerLiscenseID
                                      select p).Single();
                    var RemoveAttach = from p in db.LicenseAttachments where p.MarinerLicenseID == DeleteThis.id select p; 
                    var RemoveActivities = from p in db.Activities where p.MarinerLicenseID == DeleteThis.id select p;

                    db.Activities.DeleteAllOnSubmit(RemoveActivities); 

                    db.LicenseAttachments.DeleteAllOnSubmit(RemoveAttach); 

                    db.MarinerLicenses.DeleteOnSubmit(DeleteThis);

                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }
              
        public IList<MarinerLicense> GetMarinerLicense(int MarinerID)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                   var Lic = from p in db.MarinerLicenses where p.MarinerID == MarinerID
                             select p; 
                    return Lic.ToList(); 
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }
        }

        public MarinerLicense GetAMarinerLicense(int licenseID)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    MarinerLicense Lic = (from p in db.MarinerLicenses
                              where p.id == licenseID
                              select p).Single();
                    return Lic;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }
        
        
        
        
        }
        
        #endregion 

        #region Attachments
        public bool MarinerAttachmentAdd(int MarinerID,  string filename, string location, string notes=null)
        { 

            // if mariner license ID is zero, then it's mariner's attachement, not license. 
             using (var db = new dbGCompDataContext())
            {
                try
                {
                    MarinerAttachment ma = new MarinerAttachment();
                    ma.MarinerID = MarinerID;
                   
                    ma.Filename = filename;
                    ma.Location = location;
                    ma.Notes = notes;

                    db.MarinerAttachments.InsertOnSubmit(ma);
                    db.SubmitChanges();
                    return true; 
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }
        public bool MarinerAttachmentDelete(int attID)
        {
 
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var p = db.MarinerAttachments.Where(q => q.id == attID).SingleOrDefault();
                    Utils.Misc.RenameFile(p.Location.Replace("../..", "") + "/" + p.Filename, p.Location.Replace("../..", "") + "/deleted___" + p.Filename);

                    db.MarinerAttachments.DeleteOnSubmit(p);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }


        public bool LicenseAttachmentDelete(int attID)
        {

            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var p = db.LicenseAttachments.Where(q => q.id == attID).SingleOrDefault();
                    Utils.Misc.RenameFile(p.Location.Replace("../..", "") + "/" + p.FileName, p.Location.Replace("../..", "") + "/deleted___" + p.FileName);

                    db.LicenseAttachments.DeleteOnSubmit(p);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }









        public bool LicenseAttachmentAdd(int MarinerID, int MarinerLicenseID, string filename, string location, string notes = null)
        {

            // always add Mariner ID - helps in get. 
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    LicenseAttachment ma = new LicenseAttachment();
                    ma.MarinerID = MarinerID; 
                    ma.MarinerLicenseID = MarinerLicenseID; 
                    ma.FileName = filename;
                    ma.Location = location;
                    ma.Notes = notes;

                    db.LicenseAttachments.InsertOnSubmit(ma);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }

        public IList<MarinerAttachment> GetMarinerAttachments(int MarinerID)
        { 
            using (var db = new dbGCompDataContext())
            {
                try
                {
                   var Attach = from p in db.MarinerAttachments where p.MarinerID == MarinerID 
                                select p; 
                     return Attach.ToList(); 
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null; 
                }
            }
        }

        public IList<LicenseAttachment> GetLicenseAttachments(int LicenseID)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var Attach = from p in db.LicenseAttachments
                                 where p.MarinerLicenseID == LicenseID                                
                                 select p;
                    return Attach.ToList();
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }
        }       
        #endregion

        #region Get Marinfo Main routines
        public MarInfo GetMarinerInfo(int id)
        {
            using (var db = new dbGCompDataContext())
            {
                MarInfo MyMar = new MarInfo();
                try
                {
                    

                    MyMar.Mar = (from p in db.Mariners where p.id == id select p).Single();
                    MyMar.MAttach = (from q in db.MarinerAttachments where q.MarinerID == id select q).ToList<MarinerAttachment>();
                   
                    
                   var mylic = (
                                from r in db.MarinerLicenses where r.MarinerID == id
                                orderby r.LicenseID ascending
                                select r).ToList<MarinerLicense>();

                   IList<MarLicView> mlv = new List<MarLicView>(); 
                   foreach (var ml in mylic)
                   {
                       MarLicView newML = new MarLicView();
                       string lInfo = "", lCountry = "", imgCountry = ""; 
                       int lCountryId = 0,iMarLicID=0; 

                       if (ml.LicenseID != null)
                       {
                           try
                           {
                               var clsInfo = (from p in db.Licenses where p.Id == ml.LicenseID select p).Single();
                               lInfo = clsInfo.Title;
                               lCountryId = Convert.ToInt16(clsInfo.CountryID);
                               var clsCountry = (from p in db.Countries where p.id == lCountryId select p).Single();
                               lCountry = clsCountry.Country1;
                               imgCountry = clsCountry.Image; 
                           }
                           catch (Exception ex)
                           {
                               lInfo = "";
                               lCountry = "";
                               imgCountry = "";
                               iMarLicID = 0; 
                               
                           }
                       }

                       newML.Title = Convert.ToString(lInfo);
                       newML.ImgCountry = Convert.ToString(imgCountry);
                       newML.Country = Convert.ToString(lCountry);
                       newML.ExpDate = Convert.ToDateTime(ml.ExpirationDate).ToShortDateString();
                       newML.IssueDate = Convert.ToDateTime(ml.IssueDate).ToShortDateString();
                       newML.EndoInfo = ml.EndorsementInfo;
                       newML.PendingFlag = ((ml.PendingGlobal == true) || (ml.PendingGovt == true)) ? true : false; 
                       newML.MarinerLicenseID = ml.id;
                       mlv.Add(newML); 
                        
                   }

                   MyMar.MLisc = mlv; 
                   return MyMar;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return MyMar;
                }
            }
        }
        #endregion

        #region Defined Types
        public IList<Country> GetCountries()
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var Countries = from p in db.Countries  orderby p.SortOrder                               
                                 select p ;
                   
                    return Countries.ToList();
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }
        
        }

        public IList<string> GetAllEmployers()
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    
                    var Employers = (from p in db.vw_Employers
                                     orderby p.Employer                                                                  
                                     select p.Employer);
   
                    return Employers.ToList();
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }

        }
        public IList<vw_Employer_Rig> GetAllEmployersRigs()
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {

                    var EmpRigs = (from p in db.vw_Employer_Rigs  
                                   orderby p.Employer, p.RigName
                                     select p);
                    return EmpRigs.ToList();
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex);
                    return null;
                }
            }

        }


        public IList<Employer> GetAllEmployers_Dropdown()
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    return db.Employers.ToList();
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex);
                    return null;
                }
            }

        }

        public int GetEmployerID(string ename)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    return db.Employers.Where(p => p.Employer1 == ename).FirstOrDefault().id;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex);
                    return 0;
                }
            }
        }

        public string GetEmployerName(int empId)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    return db.Employers.Where(p => p.id == empId).FirstOrDefault().Employer1;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex);
                    return "";
                }
            }
        }


        public IList<License> GetLicenses()
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var Licenses = from p in db.Licenses orderby p.CountryID, p.Title
                                    select p;
                    return Licenses.ToList(); 
                   
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }

        }

        public IList<Mariner> GetAllMariners()
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var Mar = from p in db.Mariners
                                   orderby p.LastName, p.FirstName
                                   select p;
                    return Mar.ToList();

                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }
             
        }


        public bool LicenseTypeAddNew(License newLicense)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    License lic = new License();
                    db.Licenses.InsertOnSubmit(lic);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }

        public bool LicenseTypeUpdate(License updateLicense)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var lic = (from p in db.Licenses
                              where p.Id == updateLicense.Id
                              select p).Single();
                    
                    lic = updateLicense; 

                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }

        public bool AddCountry(string NewCountry)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    Country cty = new Country();
                    cty.Country1 = NewCountry;
                    cty.Notes = "";
                    cty.Image = "";
                    cty.SortOrder = 10;
                    db.Countries.InsertOnSubmit(cty); 
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }

        internal bool AddCountryLicense(int Countryid, string LicName)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    License lic = new License();
                    lic.CountryID = Countryid;
                    lic.Title = LicName;
                    lic.Notes = "";
                    lic.SortOrder = 10;
                    db.Licenses.InsertOnSubmit(lic); 
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }



     #endregion

        #region Activites
        public IList<Activity> GetMarActivities(int MarinerLicenseId)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    var Mar = from p in db.Activities 
                              where p.MarinerLicenseID == MarinerLicenseId
                              orderby p.ActDate descending 
                              select p;
                    return Mar.ToList();

                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                }
            }
        }

        public bool AddActivity(Activity NewAct)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {

                    db.Activities.InsertOnSubmit(NewAct);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }

        public bool UpdateActivity(int actID, DateTime theDate, string theNotes)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {

                    var getAct = db.Activities.Where(p => p.id == actID).SingleOrDefault();
                    
                    getAct.ActDate = theDate;
                    getAct.ActNotes = theNotes;

                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }

        public bool DeleteActivity(int actID)
        {
            using (var db = new dbGCompDataContext())
            {

                try
                {

                    db.Activities.DeleteOnSubmit(db.Activities.Where(p => p.id == actID).SingleOrDefault());
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return false;
                }
            }
        }






        #endregion

        #region Report functions
        public IList<RptExpireIssued> GetExpiryRpt()
        {
            using (var db = new dbGCompDataContext())
            {
                IList<RptExpireIssued> rExp = new List<RptExpireIssued>();
 
                try
                {
                    var expirationList = from p in db.MarinerLicenses   
                                         where   p.ExpirationDate != null && 
                                         p.ExpirationDate != DateTime.Parse("01/01/0001")
                                         orderby p.ExpirationDate ascending
                                         select p ;

                    RptExpireIssued re;

                    foreach (var mLis in expirationList)
                    {
                        re = new RptExpireIssued(); 
                        var getMar = (from p in db.Mariners
                                     where p.id == mLis.MarinerID
                                     select p);
                        if(getMar.Any())
                        {
                            var current = getMar.First();

                            re.FirstName = current.FirstName;
                            re.LastName = current.LastName;
                            re.BirthDate = Convert.ToDateTime(current.DateOfBirth); // because it could be null
                            string tempAddr = Misc.DeNull(current.Address2); // avoid null 
                            if ( tempAddr.Length > 0) 
                                {
                                    tempAddr += ", "; 
                                }
                            re.HomeAddr = current.Address +", "+tempAddr +", "+current.City+", "+current.State + " " + current.ZipCode;
                            if (re.HomeAddr.Trim() == ", , ,")
                            {
                                re.HomeAddr = "";
                            }
                            re.CompanyName = current.Employer;
                            re.RigName = current.RigName;
                            re.IssueDate = Convert.ToDateTime(mLis.IssueDate); 
                            re.ExpireDate = Convert.ToDateTime(mLis.ExpirationDate);
                            re.LicenseIssued = mLis.EndorsementInfo; 
                            rExp.Add(re); 
                        }

                    }

                    return rExp; 
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;
                    
                }
            }
        }
        
      
        internal IList<RptPending> GetPendingRpt(int typePending, string Empr)
        {
            
            using (var db = new dbGCompDataContext())
            {
                IList<RptPending> rPend = new List<RptPending>();
 
                try
                {

                   
                    var pendingList = from p in db.MarinerLicenses
                                      where p.PendingGovt == true || p.PendingGlobal == true
                                      select p;
                    
                    
                    if (typePending == 1)
                    {
                        foreach (var lic in pendingList.Where(p => p.PendingGovt == true))
                        {
                            RptPending rp = new RptPending();
                            
                            var Activity = from p in db.Activities
                                            where p.MarinerLicenseID == lic.id
                                            orderby p.id descending
                                            select p;

                            if (Activity.Any())
                            {
                                foreach (var a in Activity)
                                {
                                    rp.Activity += Convert.ToDateTime(a.ActDate).ToString("MM/dd/yyyy") + "-" + a.ActNotes + "<br/>";


                                
                                }
                              
                            }
                            else
                            {
                                rp.Activity = "";
                            }
                            rp.LicenseApply = lic.EndorsementInfo;
                            rp.LastName = GetMariner(lic.MarinerID).LastName;
                            rp.FirstName = GetMariner(lic.MarinerID).FirstName;
                            rp.BirthDate = Convert.ToDateTime(GetMariner(lic.MarinerID).DateOfBirth);
                            rp.CompanyName = GetMariner(lic.MarinerID).Employer;
                            rp.RigName = GetMariner(lic.MarinerID).RigName; 
                            rPend.Add(rp); 
                        }

                      

                    }
                    else
                    {
                        foreach (var lic in pendingList.Where(p => p.PendingGlobal == true))
                        {

                            RptPending rp = new RptPending();
                            var Activity =  from p in db.Activities
                                            where p.MarinerLicenseID == lic.id
                                            orderby p.id descending
                                            select p ;
                            if (Activity.Any() )
                            {
                                foreach (var a in Activity)
                                {
                                    rp.Activity += Convert.ToDateTime(a.ActDate).ToString("MM/dd/yyyy") + "-" + a.ActNotes + "<br/>";



                                }

                               // rp.Activity = Convert.ToDateTime(Activity.First().ActDate).ToShortDateString() + "-" + Activity.First().ActNotes;
                            }
                            else
                            {
                                rp.Activity = "";
                            }
                            rp.LicenseApply = lic.EndorsementInfo;
                            rp.LastName = GetMariner(lic.MarinerID).LastName;
                            rp.FirstName = GetMariner(lic.MarinerID).FirstName;
                            rp.BirthDate = Convert.ToDateTime(GetMariner(lic.MarinerID).DateOfBirth);
                            rp.CompanyName = GetMariner(lic.MarinerID).Employer;
                            rp.RigName = GetMariner(lic.MarinerID).RigName; 
                            rPend.Add(rp); 

                        }
 
                    }
                   
                    if (Empr == "All")
                        {
                            return rPend.ToList(); 
                        }
                        else
                        {
                            return rPend.Where(e => e.CompanyName == Empr).ToList();
                        }
                      
                }
                catch (Exception ex)
                {

                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 

                    //_methodInfo = MethodBase.GetCurrentMethod().DeclaringType.FullName + 
                    //    "." + MethodBase.GetCurrentMethod().Name + " :  ";
                    //Misc.UpdateErrorLog(_methodInfo + ex.Message);
                    return null;

                }
            }
        }

        internal IList<RptExpireIssued> GetIssuedRpt(DateTime StartDate, DateTime EndDate)
        {
            using (var db = new dbGCompDataContext())
            {
                IList<RptExpireIssued> rExp = new List<RptExpireIssued>();

                try
                {
                    var expirationList = from p in db.MarinerLicenses
                                         where p.PendingGlobal == false && p.PendingGovt == false &&
                                         p.IssueDate != DateTime.Parse("01/01/0001") && p.IssueDate != null
                                         && p.IssueDate >= StartDate && p.IssueDate <= EndDate 
                                         select p;

                    RptExpireIssued re;

                    foreach (var mLis in expirationList)
                    {
                        re = new RptExpireIssued();
                        var getMar = (from p in db.Mariners
                                      where p.id == mLis.MarinerID
                                      select p);
                        if (getMar.Any())
                        {
                            var current = getMar.First();

                            re.FirstName = current.FirstName;
                            re.LastName = current.LastName;
                            re.BirthDate = Convert.ToDateTime(current.DateOfBirth); // because it could be null
                          
                            
                            re.CompanyName = current.Employer;
                            re.RigName = GetMariner(mLis.MarinerID).RigName; 
                            re.IssueDate = Convert.ToDateTime(mLis.IssueDate);
                            re.ExpireDate = Convert.ToDateTime(mLis.ExpirationDate);
                            re.LicenseIssued = mLis.EndorsementInfo;
                            rExp.Add(re);
                        }

                    }

                    return rExp;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex); 
                    return null;

                }
            }
        }

 

        #endregion
 

        internal bool EmployerAddNew(string newEmployer)
        {
            using (var db = new dbGCompDataContext())
            {
                try
                {
                    Employer emp = new Employer();
                    emp.Employer1 = newEmployer; 
                    db.Employers.InsertOnSubmit(emp);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex);
                    return false;
                }
            }
        }

        internal IList<RptPending> GetEmpRig(string EmpRig, ReportOpt S)
        {
            using (var db = new dbGCompDataContext())
            {
                IList<RptPending> rPend = new List<RptPending>();
                bool GOV=false, GC=false;
                string FilterString = ""; 

                try
                {
                    if (EmpRig == "All")
                    {
                        
                        var MarList = from q in db.Mariners orderby q.Employer,q.RigName,q.LastName,q.FirstName select q;

                        foreach (var mar in MarList)
                        {      
                            var pendingList = from p in db.MarinerLicenses
                                              where mar.id == p.MarinerID
                                              select p;
                            if (S == ReportOpt.Every)
                            {
                                // don't filter -- get all. 
                            }
                            else
                            {
                                if (S == ReportOpt.GC)
                                {
                                    pendingList = pendingList.Where(p => p.PendingGlobal == true);
                                }
                                else if (S == ReportOpt.GOV)
                                {
                                    pendingList = pendingList.Where(p => p.PendingGovt == true);
                                }
                                else if (S == ReportOpt.All)
                                {
                                    pendingList = pendingList.Where(p => p.PendingGovt == true || p.PendingGlobal == true);
                                }
                            }

                            foreach (var lic in pendingList)
                            {
                                RptPending rp = new RptPending();

                                var Activity = from p in db.Activities
                                               where p.MarinerLicenseID == lic.id
                                               orderby p.id descending
                                               select p;

                                if (Activity.Any())
                                {
                                    foreach (var a in Activity)
                                    {
                                        rp.Activity += Convert.ToDateTime(a.ActDate).ToString("MM/dd/yyyy") + "-" + a.ActNotes + "<br/>";

                                    }

                                }
                                else
                                {
                                    rp.Activity = "";
                                }
                                rp.LicenseApply = lic.EndorsementInfo;
                                rp.LastName = GetMariner(lic.MarinerID).LastName;
                                rp.FirstName = GetMariner(lic.MarinerID).FirstName;
                                rp.BirthDate = Convert.ToDateTime(GetMariner(lic.MarinerID).DateOfBirth);
                                rp.CompanyName = GetMariner(lic.MarinerID).Employer;
                                rp.RigName = GetMariner(lic.MarinerID).RigName;
                                rPend.Add(rp);
                            }
                        }
                    
                    
                    }
                    else
                    {
                        var vwER = (from r in db.vw_Employer_Rigs where r.EmpRig == EmpRig select r).FirstOrDefault();

                        var MarList = from q in db.Mariners where q.Employer == vwER.Employer && q.RigName == vwER.RigName select q;

                        foreach (var mar in MarList)
                        {
                            var pendingList = from p in db.MarinerLicenses
                                              where  mar.id == p.MarinerID
                                              select p;

                            if (S == ReportOpt.Every)
                            {
                                // don't filter -- get all. 
                            }
                            else
                            {
                                if (S == ReportOpt.GC)
                                {
                                    pendingList = pendingList.Where(p => p.PendingGlobal == true);
                                }
                                else if (S == ReportOpt.GOV)
                                {
                                    pendingList = pendingList.Where(p => p.PendingGovt == true);
                                }
                                else if (S == ReportOpt.All)
                                {
                                    pendingList = pendingList.Where(p => p.PendingGovt == true ||  p.PendingGlobal == true);
                                }
                            }

                            foreach (var lic in pendingList)
                            {
                                RptPending rp = new RptPending();

                                var Activity = from p in db.Activities
                                               where p.MarinerLicenseID == lic.id
                                               orderby p.id descending
                                               select p;

                                if (Activity.Any())
                                {
                                    foreach (var a in Activity)
                                    {
                                        rp.Activity += Convert.ToDateTime(a.ActDate).ToString("MM/dd/yyyy") + "-" + a.ActNotes + "<br/>";

                                    }

                                }
                                else
                                {
                                    rp.Activity = "";
                                }
                                rp.LicenseApply = lic.EndorsementInfo;
                                rp.LastName = GetMariner(lic.MarinerID).LastName;
                                rp.FirstName = GetMariner(lic.MarinerID).FirstName;
                                rp.BirthDate = Convert.ToDateTime(GetMariner(lic.MarinerID).DateOfBirth);
                                rp.CompanyName = GetMariner(lic.MarinerID).Employer;
                                rp.RigName = GetMariner(lic.MarinerID).RigName;
                                rPend.Add(rp);
                            }
                        }

                    }
                    return rPend.ToList(); 
            
                    }
                  
                
                catch (Exception ex)
                {

                    Misc.WriteMethodError(MethodBase.GetCurrentMethod(), ex);
 
                    return null;

                }
            }
        }
    }
    
}