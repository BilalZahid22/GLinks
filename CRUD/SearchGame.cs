using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace CRUD
{
    public static class SearchGame
    {
        static GLinksDBEntities db = new GLinksDBEntities();
        public static SqlConnection getCon()
        {
            return new SqlConnection(db.Database.Connection.ConnectionString);
        }
        public static List<game> Search(List<string> qury)
        {
            List<game> rtn = null;
            if (qury.Count > 0)
            {
                
                string where = "";
                for (int i = 0; i < qury.Count; i++)
                {
                    where = whereclause("G_name", "Contains", qury[i], where);
                }
                string BaseQ = "Select * from games";
                string q = "";

                if (BaseQ != "")
                {
                    if (where == "")
                    {
                        q = BaseQ;
                    }
                    else
                        q = BaseQ + " Where " + where;
                    using (SqlConnection con = getCon())
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand(q, con);
                        SqlDataReader rs = com.ExecuteReader();
                        rtn = new List<game>();
                        while (rs.Read())
                        {
                            game user = new game();
                            user.ID = rs.GetInt32(rs.GetOrdinal("ID"));
                            user.G_name = (String)rs["G_name"];
                            user.Description = (String)rs["Description"];
                            user.Image = (String)rs["Image"];
                            user.SysReq = (String)rs["SysReq"];
                            user.gamply = (String)rs["gamply"];
                            user.instlV = (String)rs["instlV"];
                            user.link = (String)rs["link"];
                            rtn.Add(user);
                        }
                    }
                    return rtn;
                }
            }
            return null;
        }

        static string whereclause(string col, string op, string val, string where)
        {
            if (val.Equals(null) || val.Equals(""))
                return "";
            if (!where.Equals(""))
                where += " AND";
            string cond = "";
            if (op.Equals("Starts With"))
                cond = string.Format(" {0} like '{1}%' ", col, val);
            else if (op.Equals("End With"))
                cond = string.Format(" {0} like '%{1}' ", col, val);
            else if (op.Equals("Contains"))
                cond = string.Format(" {0} like '%{1}%' ", col, val);
            else if (op.Equals("Equal"))
                cond = string.Format(" {0} = '{1}' ", col, val);
            else if (op.Equals("Greater Than"))
                cond = string.Format(" {0} > {1} ", col, val);
            else if (op.Equals("Less Than"))
                cond = string.Format(" {0} < {1} ", col, val);
            where += cond;
            return where;
        }

    }
}
