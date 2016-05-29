using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace refactor_me.DataAccess
{
    public class Helpers
    {
        public static SqlConnection NewConnection()
        {
            var connStrFromConfig = ConfigurationManager.ConnectionStrings["Products"].ConnectionString;
            if (connStrFromConfig.Contains("{DataDirectory"))
            {
                var connstr = connStrFromConfig.Replace("{DataDirectory}", HttpContext.Current.Server.MapPath("~/App_Data"));
                return new SqlConnection(connstr);
            }
            return new SqlConnection(connStrFromConfig);
        }      
    }
}