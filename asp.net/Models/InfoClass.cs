using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class InfoClass
    {
    }

    public class MarinerResult
    {
        public int MarinerID;
        public string Fullname; 
        public string CompanyName; 
        public string Location;
        public string DOB; 

    }

    public class MarInfo
    {
        public Mariner Mar;
        public IList<MarLicView> MLisc;
        public IList<MarinerAttachment> MAttach;
       

    }

    public class LicInfo
    {
        public MarinerResult Mar; 
        public IList<MarinerLicense> MLisc;
        public IList<LicenseAttachment> LAttach;
    
    }
   
    public class MarLicView
    {
        public int MarinerLicenseID; 
        public string Country;
        public string ImgCountry;
        public string Title;
        public string EndoInfo;
        public string IssueDate;
        public string ExpDate;
        public bool PendingFlag; 
        public IList<Activity> ListAct;
    }
    public class UsrMsg
    {
        public bool MsgOn = false;
        public bool Success = true;
        public string UserMessage =""; 
        

    }

    public class RptExpireIssued
    {
        public string LastName;
        public string FirstName;
        public DateTime BirthDate;
        public string LicenseIssued; 
        public DateTime IssueDate;
        public DateTime ExpireDate;
        public string HomeAddr;
        public string CompanyName;
        public string RigName;
    
    }
    public class RptPending
    {
        public string LastName;
        public string FirstName;
        public DateTime BirthDate;
        public string LicenseApply;
        public string Activity; //Status
        public string CompanyName;
        public string RigName; 
    }
}