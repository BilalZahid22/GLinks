using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CRUD
{
    public static class UserHandling
    {
        static GLinksDBEntities db = new GLinksDBEntities();
        static SqlConnection getcon()
        {
            return new SqlConnection(db.Database.Connection.ConnectionString);
        }
        static public bool addUser(user user)
        {
            try
            {
                db.users.Add(user);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        static public user CheckUser(String nam,String pas)
        {
            using (SqlConnection con = getcon())
            {
                con.Open();
                SqlCommand ps = new SqlCommand("select * from users where uname= @nam and pass = @pas", con);
                ps.Parameters.AddWithValue("@nam", nam);
                ps.Parameters.AddWithValue("@pas", pas);
                SqlDataReader dr = ps.ExecuteReader();
                user rtn = new user();
                if (dr.Read()) // found
                {
                    rtn.ID = Convert.ToInt32(dr[0].ToString());
                    rtn.uname = nam;
                    rtn.pass = pas;
                    rtn.email = dr[3].ToString();
                    return rtn;
                }
                return null;
            }
        }
    }
}
