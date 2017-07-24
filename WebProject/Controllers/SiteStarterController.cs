using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD;
using System.IO;

namespace WebProject.Controllers
{
    public class SiteStarterController : Controller
    {
        // GET: SiteStarter
        public ActionResult Index()
        {

            ViewBag.UserName = Session["UserName"];
            return View(AddGame.getAllGames());
        }

        public ActionResult Search()
        {
            ViewBag.UserName = Session["UserName"];
            String qur = Request.Form.Get("Search");
            ViewBag.qur = qur;
            List<string> qr = new List<string>();
            qr = qur.Split(' ').ToList();
            return View(SearchGame.Search(qr));
        }

        [HttpPost]
        public ActionResult LogSign2()
        {
            String rt = "";
            String Lognam = Request.Form.Get("uname");
            String Logpass = Request.Form.Get("pass");
            if (Lognam != "" && Logpass != "" && Lognam != null && Logpass != null)
            {
                if (UserHandling.CheckUser(Lognam, Logpass) != null)
                {
                    Session["UserName"] = Lognam;
                    if (Lognam == "admin" && Logpass == "1122")
                    {
                        //Response.Redirect("~/SiteStarter/AdminHome", false);
                        rt = "~/SiteStarter/AdminHome";
                    }

                    else
                    {
                        //Response.Redirect("~/SiteStarter/Index", false);
                        rt = "~/SiteStarter/Index";
                    }
                }
                else
                {
                    //Response.Write("<script>alert('Invalid UserName Or Password');</script>");
                    //Response.Redirect("~/SiteStarter/LogSign", false);
                    rt = "~/SiteStarter/LogSign";
                }
            }
            else
            {
                String Signnam = Request.Form.Get("usernamesignup");
                String Signpass = Request.Form.Get("passwordsignup");
                String Signpasscnfrm = Request.Form.Get("passwordsignup_confirm");
                String Emailpass = Request.Form.Get("emailsignup");
                if (Signnam != "" && Signpass != "" && Signpasscnfrm != "" && Emailpass != "")
                {
                    if (Signpass == Signpasscnfrm)
                    {
                        user tmp = new user();
                        tmp.email = Emailpass;
                        tmp.pass = Signpass;
                        tmp.uname = Signnam;
                        if (UserHandling.addUser(tmp))
                        {
                            Response.Write("<script>alert('Added');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Not Added');</script>");
                        }
                        //Response.Redirect("~/SiteStarter/LogSign", false);
                        rt = "~/SiteStarter/LogSign";
                    }
                    else
                    {
                        //Response.Write("<script>alert('Passwords does not match');</script>");
                        //Response.Redirect("~/SiteStarter/LogSign", false);
                        rt = "~/SiteStarter/LogSign";
                    }
                }
                else
                {
                    //Response.Write("<script>alert('One or more fields are empty');</script>");
                    //Response.Redirect("~/SiteStarter/LogSign", false);
                    rt = "~/SiteStarter/LogSign";
                }
            }
            return Redirect(rt);
        }
        public ActionResult LogSign()
        {

            return View();
        }
        [HttpGet]
        public ActionResult GameDetail(String nam)
        {
            ViewBag.UserName = Session["UserName"];
            return View(AddGame.getgameByname(nam));
        }
        [HttpGet]
        public ActionResult AdminHome()
        {
            if (Session["UserName"] != null)
                if (Session["UserName"].ToString().Equals("admin"))
                {
                    List<game> ab = AddGame.getAllGames();
                    return View(ab);
                }
            return Redirect("~/SiteStarter/Index");
        }
        [HttpPost]
        public ActionResult AdminHome2()
        {
            String nam = Request.Form.Get("title");
            nam = nam.Trim();
            String des = Request.Form.Get("desc");
            String sys = Request.Form.Get("sysreq");
            String lnk = Request.Form.Get("link");
            var img = Request.Files.Get("image");
            var gm = Request.Files.Get("gmply");
            var insl = Request.Files.Get("instlV");
            var iis = Request.Files.GetMultiple("Mimages");
            if (nam != "" && des != "" && sys != "" && lnk != "" && img.FileName != "" && insl.FileName != "" && gm.FileName != "" && iis.Count > 0)
            {
                var path = Path.Combine(Server.MapPath("~/Uploaded_Content/"), nam + "\\TitleImage\\" + img.FileName);
                new FileInfo(path).Directory.Create();

                img.SaveAs(path);
                path = ("~/Uploaded_Content/" + nam + "/TitleImage/" + img.FileName);
                String im = path;



                path = Path.Combine(Server.MapPath("~/Uploaded_Content/"), nam + "\\GamePlayVideo\\" + gm.FileName);
                new FileInfo(path).Directory.Create();

                gm.SaveAs(path);
                path = ("~/Uploaded_Content/" + nam + "/GamePlayVideo/" + gm.FileName);
                String gmpl = path;


                path = Path.Combine(Server.MapPath("~/Uploaded_Content/"), nam + "\\InstallationVideo\\" + insl.FileName);
                new FileInfo(path).Directory.Create();

                insl.SaveAs(path);
                path = ("~/Uploaded_Content/" + nam + "/InstallationVideo/" + insl.FileName);
                String ins = path;


                List<Image> imglst = new List<Image>();

                foreach (var i in iis)
                {
                    path = Path.Combine(Server.MapPath("~/Uploaded_Content/"), nam + "\\Images\\" + i.FileName);
                    new FileInfo(path).Directory.Create();
                    i.SaveAs(path);
                    Image tmp = new Image();
                    path = ("~/Uploaded_Content/" + nam + "/Images/" + i.FileName);
                    tmp.images = path;
                    imglst.Add(tmp);
                }
                game ad = new game();
                ad.Description = des;
                ad.gamply = gmpl;
                ad.link = lnk;
                ad.G_name = nam;
                ad.instlV = ins;
                ad.SysReq = sys;
                ad.Image = im;
                ad.Images = imglst;
                bool rt = AddGame.addgame(ad);
                ViewBag.MyScript = "<script language='javascript' type='text/javascript'>alert('Added Successfully');</script>";
                //Response.Redirect("/SiteStarter/AdminHome", false);
            }
            else
            {
                ViewBag.MyScript = "<script language='javascript' type='text/javascript'>alert('Some Fields are empty');</script>";
            }
            //String nam2 = "'";

            return Redirect("~/SiteStarter/AdminHome");
        }
        [HttpPost]
        public ActionResult AdminUpdateDescription()
        {
            String des = Request.Form.Get("descG");
            String gname = Request.Form.Get("gameget");
            AddGame.updategamedescr(AddGame.getgameByname(gname), des);
            //Response.Redirect("/SiteStarter/AdminHome", false);
            return Redirect("~/SiteStarter/AdminHome");
        }
        [HttpPost]
        public ActionResult AdminUpdateSysReq()
        {
            String des = Request.Form.Get("sysreqG");
            String gname = Request.Form.Get("gameget");
            AddGame.updategameSySReq(AddGame.getgameByname(gname), des);
            //Response.Redirect("/SiteStarter/AdminHome", false);
            return Redirect("~/SiteStarter/AdminHome");
        }
        [HttpPost]
        public ActionResult AdminUpdateLink()
        {
            String des = Request.Form.Get("linkG");
            String gname = Request.Form.Get("gameget");
            AddGame.updategamelink(AddGame.getgameByname(gname), des);
            //Response.Redirect("/SiteStarter/AdminHome", false);
            return Redirect("~/SiteStarter/AdminHome");
        }
        [HttpPost]
        public ActionResult AdminUpdateTitleImage()
        {
            String gname = Request.Form.Get("gameget");
            game g = AddGame.getgameByname(gname);
            new FileInfo(Server.MapPath(g.Image)).Delete();
            var img = Request.Files.Get("imageG");
            var path = Path.Combine(Server.MapPath("~/Uploaded_Content/"), g.G_name + "\\TitleImage\\" + img.FileName);
            new FileInfo(path).Directory.Create();
            img.SaveAs(path);
            path = ("~/Uploaded_Content/" + g.G_name + "/TitleImage/" + img.FileName);
            AddGame.updategameImage(g, path);
            //Response.Redirect("/SiteStarter/AdminHome", false);
            return Redirect("~/SiteStarter/AdminHome");
        }
        [HttpPost]
        public ActionResult AdminUpdateInstallVid()
        {
            String gname = Request.Form.Get("gameget");
            game g = AddGame.getgameByname(gname);
            new FileInfo(Server.MapPath(g.instlV)).Delete();
            var img = Request.Files.Get("instlVG");
            var path = Path.Combine(Server.MapPath("~/Uploaded_Content/"), g.G_name + "\\InstallationVideo\\" + img.FileName);
            new FileInfo(path).Directory.Create();
            img.SaveAs(path);
            path = ("~/Uploaded_Content/" + g.G_name + "/InstallationVideo/" + img.FileName);
            AddGame.updategameinstlV(g, path);
            //Response.Redirect("/SiteStarter/AdminHome", false);
            return Redirect("~/SiteStarter/AdminHome");
        }
        [HttpPost]
        public ActionResult AdminUpdateGamplay()
        {
            String gname = Request.Form.Get("gameget");
            game g = AddGame.getgameByname(gname);
            new FileInfo(Server.MapPath(g.gamply)).Delete();
            var img = Request.Files.Get("gmplyG");
            var path = Path.Combine(Server.MapPath("~/Uploaded_Content/"), g.G_name + "\\GamePlayVideo\\" + img.FileName);
            new FileInfo(path).Directory.Create();
            img.SaveAs(path);
            path = ("~/Uploaded_Content/" + g.G_name + "/GamePlayVideo/" + img.FileName);
            AddGame.updategamegamply(g, path);
            //Response.Redirect("/SiteStarter/AdminHome", false);
            return Redirect("~/SiteStarter/AdminHome");
        }
        [HttpPost]
        public ActionResult AdminUpdateScreenShots()
        {
            String gname = Request.Form.Get("gameget");
            game g = AddGame.getgameByname(gname);
            foreach (Image x in g.Images)
                new FileInfo(Server.MapPath(x.images)).Delete();
            List<Image> imglst = new List<Image>();
            var iis = Request.Files.GetMultiple("SS1");

            foreach (var i in iis)
            {
                var path = Path.Combine(Server.MapPath("~/Uploaded_Content/"), g.G_name + "\\Images\\" + i.FileName);
                new FileInfo(path).Directory.Create();
                i.SaveAs(path);
                Image tmp = new Image();
                path = ("~/Uploaded_Content/" + g.G_name + "/Images/" + i.FileName);
                tmp.images = path;
                imglst.Add(tmp);
            }
            AddGame.updateScreenShots(g, imglst);
            //Response.Redirect("/SiteStarter/AdminHome", false);
            return Redirect("~/SiteStarter/AdminHome");
        }
        [HttpPost]
        public ActionResult AdminDeleteGame()
        {
            String gname = Request.Form.Get("gameget");
            game g = AddGame.getgameByname(gname);
            var path = Path.Combine(Server.MapPath("~/Uploaded_Content/"), g.G_name + "\\");
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            di.Delete();
            AddGame.deletegame(gname);
            //Response.Redirect("/SiteStarter/AdminHome", false);
            return Redirect("~/SiteStarter/AdminHome");
        }
        public ActionResult SignOut()
        {
            Session["UserName"] = null;
            return Redirect("~/SiteStarter/Index");
        }
    }
}