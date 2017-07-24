using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace CRUD
{
    public static class AddGame
    {
        static GLinksDBEntities db = new GLinksDBEntities();
        static SqlConnection getcon()
        {
            return new SqlConnection(db.Database.Connection.ConnectionString);
        }

        static public void checkgame(game user)
        {
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("select * from games where G_name=@nam", con);
            //    ps.Parameters.AddWithValue("@nam", user.G_nameProp);
            //    SqlDataReader dr = ps.ExecuteReader();
            //    if (dr.Read()) // found
            //    {
            //        //updategame(user);
            //    }
            //    else
            //    {
            //        addgame(user);
            //        //updateUser(user);
            //    }
            //}
            game g = db.games.FirstOrDefault(x => x.ID == user.ID);
            if (g == null || g != user)
            {
                addgame(user);
            }

        }

        static public bool addgame(game user)
        {
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("insert into games(G_name, Description, SysReq, Image,gamply,instlV,link) values (@nam, @des, @sys, @img,@gmpl,@instl,@lnk)", con);
            //    ps.Parameters.AddWithValue("@nam", user.G_name);
            //    ps.Parameters.AddWithValue("@des", user.Description);
            //    ps.Parameters.AddWithValue("@sys", user.SysReq);
            //    ps.Parameters.AddWithValue("@img", user.Image);
            //    ps.Parameters.AddWithValue("@gmpl", user.gamply);
            //    ps.Parameters.AddWithValue("@instl", user.instlV);
            //    ps.Parameters.AddWithValue("@lnk", user.link);
            //    int i = ps.ExecuteNonQuery();
            //    if (i > 0)
            //    {
            //        return true;
            //    }
            //    return false;
            //}
            //game g = new game();
            //g.Description = user.DescriptionProp;
            //g.gamply = user.GamplyProp;
            //g.Image = user.ImageProp;
            //g.instlV = user.InstlVProp;
            //g.link = user.LinkProp;
            //g.SysReq = user.SysReqProp;
            //if (db.games.ToList().Count() == 0)
            //    user.ID = 1;
            //else
            //    user.ID = db.games.Max(x => x.ID) + 1;
            
            db.games.Add(user);
            db.SaveChanges();
            return true;
        }

        static public bool deletegame(String userId)
        {
            using (SqlConnection con = getcon())
            {
                con.Open();
                DeleteImages(getgameByname(userId));
                SqlCommand ps = new SqlCommand("delete from games where G_name=@nam", con);
                // Parameters start with 1
                ps.Parameters.AddWithValue("@nam", userId);
                return ps.ExecuteNonQuery() > 0;
            }
            //db.games.SqlQuery()
            //var gm = db.games.Remove(db.games.First(x => x.G_name == userId));
            //db.SaveChanges();

        }
        static public bool updategamedescr(game user, String desc)
        {
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("update games set Description=@desc where G_name=@nam", con);
            //    ps.Parameters.AddWithValue("@nam", user.G_nameProp);
            //    ps.Parameters.AddWithValue("@desc", desc);
            //    return ps.ExecuteNonQuery() > 0;

            //}
            game t = db.games.First(x => x.ID == user.ID);
            if (t == null)
                return false;
            t.Description = desc;
            db.games.Attach(t);

            db.Entry(t).Property(x => x.Description).IsModified = true;
            db.SaveChanges();
            return true;
        }
        static public bool updategameSySReq(game user, String desc)
        {
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("update games set SysReq=@desc where G_name=@nam");
            //    ps.Parameters.AddWithValue("@nam", user.G_nameProp);
            //    ps.Parameters.AddWithValue("@desc", desc);
            //    return ps.ExecuteNonQuery() > 0;
            //}
            game t = db.games.First(x => x.ID == user.ID);
            if (t == null)
                return false;
            t.SysReq = desc;
            db.games.Attach(t);

            db.Entry(t).Property(x => x.SysReq).IsModified = true;
            db.SaveChanges();
            return true;
        }
        static public bool updateScreenShots(game user, List<Image> desc)
        {
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("update games set SysReq=@desc where G_name=@nam");
            //    ps.Parameters.AddWithValue("@nam", user.G_nameProp);
            //    ps.Parameters.AddWithValue("@desc", desc);
            //    return ps.ExecuteNonQuery() > 0;
            //}
            game t = db.games.First(x => x.ID == user.ID);
            if (t == null)
                return false;

            //foreach (Image x in t.Images)
            //{
            //    x.GID = t.ID;
            //    x.game = t;
            //    db.Images.Remove(x);
            //}
            //List<Image> il = t.Images.ToList();
            //for(int i = 0; i < t.Images.Count();i++)
            //{
            //    db.Images.Remove(il[0]);
            //}
            //t.Images = desc;

            //foreach (Image x in t.Images)
            //{
            //    x.GID = t.ID;
            //    x.game = t;
            //}
            //db.games.Attach(t);

            ////db.Entry(t).Property(x => x.Images).IsModified = true;
            //db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            //foreach (var item in t.Images)
            //{
            //    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            //}
            //db.SaveChanges();
            DeleteImages(user);
            InsertImages(user, desc);
            return true;
        }


        static public bool InsertImages(game user, List<Image> desc)
        {
            using (SqlConnection con = getcon())
            {
                con.Open();
                foreach (Image x in desc)
                {
                    SqlCommand ps = new SqlCommand("insert into Images(GID, images) values (@nam, @des)", con);
                    ps.Parameters.AddWithValue("@nam", user.ID);
                    ps.Parameters.AddWithValue("@des", x.images);
                    ps.ExecuteNonQuery();
                }
                return true;
            }
            //game t = db.games.First(x => x.ID == user.ID);
            //if (t == null)
            //    return false;

            ////foreach (Image x in t.Images)
            ////{
            ////    x.GID = t.ID;
            ////    x.game = t;
            ////    db.Images.Remove(x);
            ////}
            ////List<Image> il = t.Images.ToList();
            ////for(int i = 0; i < t.Images.Count();i++)
            ////{
            ////    db.Images.Remove(il[0]);
            ////}
            //t.Images = desc;

            //foreach (Image x in t.Images)
            //{
            //    x.GID = t.ID;
            //    x.game = t;
            //}
            //db.games.Attach(t);

            ////db.Entry(t).Property(x => x.Images).IsModified = true;
            //db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            //foreach (var item in t.Images)
            //{
            //    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            //}
            //db.SaveChanges();

            
        }

        static public bool DeleteImages(game user)
        {
            using (SqlConnection con = getcon())
            {
                con.Open();
                SqlCommand ps = new SqlCommand("Delete from Images where GID=@desc",con);
                
                ps.Parameters.AddWithValue("@desc", user.ID);
                return ps.ExecuteNonQuery() > 0;
            }
            //game t = db.games.First(x => x.ID == user.ID);
            //if (t == null)
            //    return false;

            ////foreach (Image x in t.Images)
            ////{
            ////    x.GID = t.ID;
            ////    x.game = t;
            ////    db.Images.Remove(x);
            ////}
            ////List<Image> il = t.Images.ToList();
            ////for(int i = 0; i < t.Images.Count();i++)
            ////{
            ////    db.Images.Remove(il[0]);
            ////}
            //t.Images = desc;

            //foreach (Image x in t.Images)
            //{
            //    x.GID = t.ID;
            //    x.game = t;
            //}
            //db.games.Attach(t);

            ////db.Entry(t).Property(x => x.Images).IsModified = true;
            //db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            //foreach (var item in t.Images)
            //{
            //    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            //}
            //db.SaveChanges();

            return true;
        }
        static public bool updategameImage(game user, String desc)
        {
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("update games set Image=@desc where G_name=@nam");
            //    ps.Parameters.AddWithValue("@nam", user.G_nameProp);
            //    ps.Parameters.AddWithValue("@desc", desc);
            //    return ps.ExecuteNonQuery() > 0;
            //}
            game t = db.games.First(x => x.ID == user.ID);
            if (t == null)
                return false;
            t.Image = desc;
            db.games.Attach(t);
            db.Entry(t).Property(x => x.Image).IsModified = true;
            db.SaveChanges();
            return true;
        }
        static public bool updategamegamply(game user, String desc)
        {
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("update games set gamply=@desc where G_name=@nam");
            //    ps.Parameters.AddWithValue("@nam", user.G_nameProp);
            //    ps.Parameters.AddWithValue("@desc", desc);
            //    return ps.ExecuteNonQuery() > 0;
            //}
            game t = db.games.First(x => x.ID == user.ID);
            if (t == null)
                return false;
            t.gamply = desc;
            db.games.Attach(t);

            db.Entry(t).Property(x => x.gamply).IsModified = true;
            db.SaveChanges();
            return true;
        }
        static public bool updategameinstlV(game user, String desc)
        {
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("update games set instlV=@desc where G_name=@nam");
            //    ps.Parameters.AddWithValue("@nam", user.G_nameProp);
            //    ps.Parameters.AddWithValue("@desc", desc);
            //    return ps.ExecuteNonQuery() > 0;
            //}
            game t = db.games.First(x => x.ID == user.ID);
            if (t == null)
                return false;
            t.instlV = desc;
            db.games.Attach(t);

            db.Entry(t).Property(x => x.instlV).IsModified = true;
            db.SaveChanges();
            return true;
        }
        static public bool updategamelink(game user, String desc)
        {
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("update games set link=@desc where G_name=@nam");
            //    ps.Parameters.AddWithValue("@nam", user.G_nameProp);
            //    ps.Parameters.AddWithValue("@desc", desc);
            //    return ps.ExecuteNonQuery() > 0;
            //}
            game t = db.games.First(x => x.ID == user.ID);
            if (t == null)
                return false;
            t.link = desc;
            db.games.Attach(t);

            db.Entry(t).Property(x => x.link).IsModified = true;
            db.SaveChanges();
            return true;
        }

        static public List<Game> getAllgames()
        {
            List<Game> users = new List<Game>();
            using (SqlConnection con = getcon())
            {
                con.Open();

                SqlCommand ps = new SqlCommand("select * from games");
                SqlDataReader rs = ps.ExecuteReader();
                while (rs.Read())
                {
                    Game user = new Game();
                    user.G_nameProp = (String)rs["G_name"];
                    user.DescriptionProp = (String)rs["Description"];
                    user.ImageProp = (String)rs["Image"];
                    user.SysReqProp = (String)rs["SysReq"];
                    user.GamplyProp = (String)rs["gamply"];
                    user.InstlVProp = (String)rs["instlV"];
                    user.LinkProp = (String)rs["link"];
                    users.Add(user);
                }
            }


            return users;
        }
        static public game getgameByname(String userId)
        {
            //Game user = new Game();
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("select * from games where G_name=@nam", con);
            //    ps.Parameters.AddWithValue("@nam", userId);
            //    SqlDataReader rs = ps.ExecuteReader();
            //    if (rs.Read())
            //    {
            //        user.G_nameProp = (String)rs["G_name"];
            //        user.DescriptionProp = (String)rs["Description"];
            //        user.ImageProp = (String)rs["Image"];
            //        user.SysReqProp = (String)rs["SysReq"];
            //        user.GamplyProp = (String)rs["gamply"];
            //        user.InstlVProp = (String)rs["instlV"];
            //        user.LinkProp = (String)rs["link"];
            //    }
            //}
            //return user;
            return db.games.SingleOrDefault(user => user.G_name == userId);
        }

        public static List<game> getAllGames()
        {
            //Game user = new Game();
            //using (SqlConnection con = getcon())
            //{
            //    con.Open();
            //    SqlCommand ps = new SqlCommand("select * from games where G_name=@nam", con);
            //    ps.Parameters.AddWithValue("@nam", userId);
            //    SqlDataReader rs = ps.ExecuteReader();
            //    if (rs.Read())
            //    {
            //        user.G_nameProp = (String)rs["G_name"];
            //        user.DescriptionProp = (String)rs["Description"];
            //        user.ImageProp = (String)rs["Image"];
            //        user.SysReqProp = (String)rs["SysReq"];
            //        user.GamplyProp = (String)rs["gamply"];
            //        user.InstlVProp = (String)rs["instlV"];
            //        user.LinkProp = (String)rs["link"];
            //    }
            //}
            //return user;
            return db.games.ToList();
        }
    }
}

