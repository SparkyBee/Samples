using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

namespace Intranet.Models
{
   
  public class RptGen
    {  
        
        dbEngine _db;

        public string GetMailingLabels()
        {
            _db = new dbEngine();

           var MarinerList =  _db.GetAllMariners();

           string filename = AppDomain.CurrentDomain.GetData("DataDirectory") + "\\Exports\\MailingLabels.csv";

           StreamWriter csv = File.CreateText(filename);


           IList<string> Labels = new List<string>(); 
           StringBuilder sb = new StringBuilder(); 
            //header row.
           sb.Append( "FirstName~" +
                       "LastName~" +
                       "AddressAddress2~" +
                       "CityStateZipCode~");
           Labels.Add(sb.ToString());
           sb.Length = 0; 

           foreach (var mar in MarinerList)
           {
               sb.Append(mar.FirstName + "~" +
                         mar.LastName + "~" +
                         mar.Address + " " +
                         mar.Address2 + "~" +
                         mar.City + ", " + mar.State + "  " + mar.ZipCode + "~");
                     
               Labels.Add(sb.ToString());
               sb.Length = 0;   
           }

           foreach (var mystring in Labels)
           {
               csv.WriteLine(mystring); 
           }
           csv.Flush(); 
           csv.Close();

           return  filename ;
        
        }





        public string GetExpiry(int fonttype)
        {
            _db = new dbEngine();
           IList<RptExpireIssued> RptList =  _db.GetExpiryRpt();
           StringBuilder sb = new StringBuilder();
           sb.Append("<html><head><link href='../../Content/site.css'rel='stylesheet' type='text/css' /></head><body style='font-family:Trebuchet MS; font-size:10pt;'>");
           sb.Append("<h3>For Global Compliance Follow Up - Sort by Expiration date</h3>");
           
            // 8 cols
           if (fonttype == 0)
           {
               sb.Append("<table class='myTable'><thead><tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Issued</b></td><td><b>Issued Date</b></td><td><b>Expiration Date</b></td><td><b>Home Address</b></td><td><b>Rig Name</b></td><td><b>Company Name</b></td></tr></thead>");
           }
           else
           {
               sb.Append("<table class='myTable smallerTableFont'><thead><tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Issued</b></td><td><b>Issued Date</b></td><td><b>Expiration Date</b></td><td><b>Home Address</b></td><td><b>Rig Name</b></td><td><b>Company Name</b></td></tr></thead>");
           }

           int count = 1; 
           foreach (var line in RptList)
           {
               if ((count++ % 2) == 1)
               {
                   sb.Append("<tr class='odd-row'>");
               }
               else
               {
                   sb.Append("<tr>");
               }
               
               sb.Append("<td>"); sb.Append(line.FirstName); sb.Append("</td>");
               sb.Append("<td>"); sb.Append(line.LastName); sb.Append("</td>");
               sb.Append("<td>"); sb.Append(line.BirthDate.ToString("MM/dd/yyyy")); sb.Append("</td>");
               sb.Append("<td>"); sb.Append(line.LicenseIssued); sb.Append("</td>");
               sb.Append("<td>"); sb.Append(line.IssueDate.ToString("MM/dd/yyyy")); sb.Append("</td>");
               sb.Append("<td>"); sb.Append(line.ExpireDate.ToString("MM/dd/yyyy")); sb.Append("</td>");
               sb.Append("<td>"); sb.Append(line.HomeAddr); sb.Append("</td>");
               sb.Append("<td>"); sb.Append(line.RigName); sb.Append("</td>");
               sb.Append("<td>"); sb.Append(line.CompanyName); sb.Append("</td>");
               sb.Append("</tr>");
           }
            sb.Append("</table>");
            return sb.ToString(); 
        }

        internal string GetPending(int pendingType,string EmployerSelected, int fonttype)
        {
            _db = new dbEngine();
            IList<RptPending> RptList = _db.GetPendingRpt(pendingType, EmployerSelected);  
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><link href='../../Content/site.css'rel='stylesheet' type='text/css' /></head><body style='font-family:Trebuchet MS; font-size:10pt;'>");
            sb.Append("<h3>Sort by Last Name - Pending ");
            if (pendingType == 1)
            {
                sb.Append("Governing Agency -- Company: "+ EmployerSelected +"</h3>");
            }
            else
            {
                sb.Append("Global Compliance -- Company: " + EmployerSelected + "</h3>");
            }
            if (fonttype == 0)
            {
                sb.Append("<table class='myTable'><thead><tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Applying for</b></td><td><b>Rig Name</b></td><td><b>Company Name</b></td><td><b>Status</b></td></tr></thead>");
            }
            else
            {
                sb.Append("<table class='myTable smallerTableFont'><thead><tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Applying for</b></td><td><b>Rig Name</b></td><td><b>Company Name</b></td><td><b>Status</b></td></tr></thead>");
            }
            int count = 1;
            if (RptList != null)
            {
                foreach (var line in RptList.OrderBy(p => p.LastName).ThenBy(p=>p.FirstName))
                {
                    if ((count++ % 2) == 1)
                    {
                        sb.Append("<tr class='odd-row'>");
                    }
                    else
                    {
                        sb.Append("<tr>");
                    }

                    sb.Append("<td>"); sb.Append(line.LastName); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.FirstName); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.BirthDate.ToString("MM/dd/yyyy")); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.LicenseApply); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.RigName); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.CompanyName); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.Activity); sb.Append("</td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");
            return sb.ToString(); 
             
        }

        public string GetIssuedCred(DateTime StartDate, DateTime EndDate,int fonttype)
        {
            _db = new dbEngine();
            IList<RptExpireIssued> RptList = _db.GetIssuedRpt(StartDate, EndDate);
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><link href='../../Content/site.css'rel='stylesheet' type='text/css' /></head><body style='font-family:Trebuchet MS; font-size:10pt;'>");
            sb.Append("<h3>Sort by Last Name & Date Range - Issued Credentials</h3>");
            sb.Append("<h4>Issued Date Range: "); 
            if (StartDate <= DateTime.Now.AddYears(-50))
            {
                 sb.Append("All Dates </h4>"); 
            }
            else 
            {
                sb.Append(StartDate.ToString("MM/dd/yyyy") + "-" + EndDate.ToString("MM/dd/yyyy") + "</h4>"); 
            }
            if (fonttype == 0)
            {
                sb.Append("<table class='myTable'><thead><tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Issued</b></td><td><b>Rig Name</b></td><td><b>Company Name</b></td><td><b>Issued Date</b></td><td><b>Expiration Date</b></td></tr></thead>");
            }
            else
            {
                sb.Append("<table class='myTable smallerTableFont'><thead><tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Issued</b></td><td><b>Rig Name</b></td><td><b>Company Name</b></td><td><b>Issued Date</b></td><td><b>Expiration Date</b></td></tr></thead>");
            }
            int count = 1;
            if (RptList != null)
            {
                foreach (var line in RptList.OrderBy(p => p.LastName).ThenBy(p=>p.FirstName))
                {
                    if ((count++ % 2) == 1)
                    {
                        sb.Append("<tr class='odd-row'>");
                    }
                    else
                    {
                        sb.Append("<tr>");
                    }

                    sb.Append("<td>"); sb.Append(line.LastName); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.FirstName); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.BirthDate.ToString("MM/dd/yyyy")); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.LicenseIssued); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.RigName); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.CompanyName); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.IssueDate.ToString("MM/dd/yyyy")); sb.Append("</td>");
                    sb.Append("<td>"); sb.Append(line.ExpireDate.ToString("MM/dd/yyyy")); sb.Append("</td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");
            return sb.ToString();   
        }

        internal string GetPendingEmpRig(string EmpRig, int fonttype, ReportOpt S)
        {
 
            _db = new dbEngine();
            IList<RptPending> RptList = _db.GetEmpRig(EmpRig, S);
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><link href='../../Content/site.css'rel='stylesheet' type='text/css' /></head><body style='font-family:Trebuchet MS; font-size:10pt;'>");
            sb.Append("<h3>Sort by Last Name  Rig: ");

            string PendingType = ""; 
            switch (S)
            {
                case ReportOpt.All:
                    {
                        PendingType = " -- All Pending";    
                        break;
                    }
                case ReportOpt.GOV:
                    {
                        PendingType = " -- Pending Governing Agency";    
                        break; 
                    }
                case ReportOpt.GC:
                    {
                        PendingType = " -- Pending Global Compliance";    
                        break; 
                    }
                case ReportOpt.Every:
                    {
                        PendingType = " -- All Pending and Non-Pending";
                        break;
                    }
            }
                sb.Append( EmpRig + PendingType + "</h3>");

                if (EmpRig == "All")
                {
                    //filter by Search Type here


                    if (fonttype == 0)
                    {
                        sb.Append("<table class='myTable'><thead><tr><td><b>Company - Rig</b></td><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Applying for</b></td><td><b>Status</b></td></tr></thead>");
                    }
                    else
                    {
                        sb.Append("<table class='myTable smallerTableFont'><thead><tr><td><b>Company - Rig</b></td><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Applying for</b></td><td><b>Status</b></td></tr></thead>");
                    }
                    int count = 1;
                    if (RptList != null)
                    {
                        


                        foreach (var line in RptList.OrderBy(p => p.CompanyName).ThenBy(p => p.RigName).ThenBy(p => p.LastName).ThenBy(p => p.FirstName))
                        {
                            if ((count++ % 2) == 1)
                            {
                                sb.Append("<tr class='odd-row'>");
                            }
                            else
                            {
                                sb.Append("<tr>");
                            }
                            sb.Append("<td>"); sb.Append(line.CompanyName +" - "+line.RigName); sb.Append("</td>");
                            sb.Append("<td>"); sb.Append(line.LastName); sb.Append("</td>");
                            sb.Append("<td>"); sb.Append(line.FirstName); sb.Append("</td>");
                            sb.Append("<td>"); sb.Append(line.BirthDate.ToString("MM/dd/yyyy")); sb.Append("</td>");
                            sb.Append("<td>"); sb.Append(line.LicenseApply); sb.Append("</td>");
                            //sb.Append("<td>"); sb.Append(line.RigName); sb.Append("</td>");
                            //sb.Append("<td>"); sb.Append(line.CompanyName); sb.Append("</td>");
                            sb.Append("<td>"); sb.Append(line.Activity); sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                    }
                    sb.Append("</table>");
                    return sb.ToString();
                
                
                
                
                }
                else
                {
                    //filter by Search Type here


                    if (fonttype == 0)
                    {
                        sb.Append("<table class='myTable'><thead><tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Applying for</b></td><td><b>Status</b></td></tr></thead>");
                    }
                    else
                    {
                        sb.Append("<table class='myTable smallerTableFont'><thead><tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Birth Date</b></td><td><b>License Applying for</b></td><td><b>Status</b></td></tr></thead>");
                    }
                    int count = 1;
                    if (RptList != null)
                    {
                        foreach (var line in RptList.OrderBy(p => p.LastName).ThenBy(p => p.FirstName))
                        {
                            if ((count++ % 2) == 1)
                            {
                                sb.Append("<tr class='odd-row'>");
                            }
                            else
                            {
                                sb.Append("<tr>");
                            }

                            sb.Append("<td>"); sb.Append(line.LastName); sb.Append("</td>");
                            sb.Append("<td>"); sb.Append(line.FirstName); sb.Append("</td>");
                            sb.Append("<td>"); sb.Append(line.BirthDate.ToString("MM/dd/yyyy")); sb.Append("</td>");
                            sb.Append("<td>"); sb.Append(line.LicenseApply); sb.Append("</td>");
                            //sb.Append("<td>"); sb.Append(line.RigName); sb.Append("</td>");
                            //sb.Append("<td>"); sb.Append(line.CompanyName); sb.Append("</td>");
                            sb.Append("<td>"); sb.Append(line.Activity); sb.Append("</td>");
                            sb.Append("</tr>");
                        }
                    }
                    sb.Append("</table>");
                    return sb.ToString();
                }
        }
    }

 
}