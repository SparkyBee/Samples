using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Utils
{
    public class Const
    {
        //form return status
        public const string UserSuccess = "s";
        public const string UserError = "e";

        public static string APP_PATH
        {
            get
            {
                AppDomain domain = AppDomain.CurrentDomain;

                return domain.BaseDirectory;
            }
        }

        public static string LOG_PATH
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LOG_PATH"].ToString();
            }
        }

        public static string EXPORT_PATH
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["EXPORT_PATH"].ToString(); ;
            }
        }

        public static string ENCRYPTION_KEY
        {
            get
            {
                return "!J4k3bf@#$234!";
            }
        }

        public static string CURRENT_MODE
        {
            get
            {
                return (System.Configuration.ConfigurationManager.ConnectionStrings["W_CAREConnectionString"]).ToString().Substring(18, 6);
            }
        }

        public static string APP_VERSION
        {
            get
            {
                return (System.Configuration.ConfigurationManager.AppSettings["APP_VERSION"]).ToString();
            }
        }

        public static string ADMIN_PASS
        {
            get
            {
                return (System.Configuration.ConfigurationManager.AppSettings["ADMIN_PASS"]).ToString();
            }
        }



    }
}