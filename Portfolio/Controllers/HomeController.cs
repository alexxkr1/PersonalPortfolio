using Microsoft.AspNetCore.Mvc;
using Volta.Models;
using System.Diagnostics;
using Volta.Data;
using Portfolio.Core.Domain;
using Portfolio.Data.Repository;
using Portfolio.Data.FileManager;
using Portfolio.Core.Domain.Comments;
using System.Net;
using HtmlAgilityPack;

namespace Volta.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repo;
        private readonly IFilemanager _filemanager;

        public HomeController(IRepository repo, IFilemanager filemanager)
        {
            _repo = repo;
            _filemanager = filemanager;
            
        }

        public IActionResult Index(string category)
        {
            var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category);
            return View(posts);
        }

        public IActionResult LeagueTable()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Klubid> klubid = new List<Klubid>();

            var web = new HtmlWeb();
            var doc = web.Load("https://jalgpall.ee/voistlused/madalamad-liigad/5/III.E");
            var doc2 = web.Load("https://jalgpall.ee/voistlused/5/team/4223?season=2022");

            foreach (var item in doc.DocumentNode.SelectNodes("//*[@id=\"page\"]/div/div[1]/div[1]/div[1]/table/tbody/tr").Take(11))
            {

                string kohtliigas = item.SelectSingleNode(".//td[1]").InnerText.Trim();
                string img = item.SelectSingleNode($".//td[2]/img").GetAttributeValue("src", null).Trim();
                string title = item.SelectSingleNode(".//td[3]").InnerText.Trim();
                string mänge = item.SelectSingleNode(".//td[4]").InnerText.Trim();
                string võite = item.SelectSingleNode(".//td[5]").InnerText.Trim();
                string viike = item.SelectSingleNode(".//td[6]").InnerText.Trim();
                string kaotusi = item.SelectSingleNode(".//td[7]").InnerText.Trim();
                string väravaid = item.SelectSingleNode(".//td[8]").InnerText.Trim();
                string punkte = item.SelectSingleNode(".//td[9]").InnerText.Trim();

                klubid.Add(new Klubid()
                {
                    title = title,
                    kohtliigas = kohtliigas,
                    logo = img,
                    võite = võite,
                    viike = viike,
                    kaotusi = kaotusi,
                    väravaid = väravaid,
                    punkte = punkte,
                    mänge = mänge

                });


            }



            foreach (var item in doc2.DocumentNode.SelectNodes("//*[@id=\"page\"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr").Take(4))
            {

                string date = item.SelectSingleNode(".//td[1]").InnerText.Trim();
                string teamleft = item.SelectSingleNode($".//td[2]/div/img").GetAttributeValue("src", null).Trim();
                string teamright = item.SelectSingleNode($".//td[4]/div/img").GetAttributeValue("src", null).Trim();
                string score = item.SelectSingleNode(".//td[3]").InnerText.Trim();
                string score6 = item.SelectSingleNode(".//td").InnerText.Trim();
                klubid.Add(new Klubid()
                {
                    date = date,
                    teamleft = teamleft,
                    teamright = teamright,
                    score = score


                });


            }

            return View(klubid);
        }


        public IActionResult LastGames()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<Klubid> klubid = new List<Klubid>();

            var web = new HtmlWeb();
            var doc2 = web.Load("https://jalgpall.ee/voistlused/5/team/4223?season=2022");


            foreach (var item in doc2.DocumentNode.SelectNodes("//*[@id=\"page\"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr").Take(20))
            {

                string date = item.SelectSingleNode(".//td[1]").InnerText.Trim();
                string teamleft = item.SelectSingleNode($".//td[2]/div/img").GetAttributeValue("src", null).Trim();
                string teamright = item.SelectSingleNode($".//td[4]/div/img").GetAttributeValue("src", null).Trim();
                string score = item.SelectSingleNode(".//td[3]").InnerText.Trim();
                string score6 = item.SelectSingleNode(".//td").InnerText.Trim();

                //*[@id="page"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr[44]/td
                //*[@id="page"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr[3]/td[4]/div/img
                //*[@id="page"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr[1]/td[4]/div/img
                //*[@id="page"]/div[2]/div[1]/div/div[2]/ul/li[1]/table/tbody/tr[1]/td[3]/a
                klubid.Add(new Klubid()
                {
                    date = date,
                    teamleft = teamleft,
                    teamright = teamright,
                    score = score


                });


            }

            return View(klubid);
        }
        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }
        public IActionResult Contact()
        {
         
            return View();
        }

        public IActionResult Blog(string category)
        {

            var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category);
            return View(posts);
        }
    }
}