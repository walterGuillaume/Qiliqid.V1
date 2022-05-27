using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.Data.Entity;
using System.Net;

namespace Qiliqid.V1.Models
{
    public class QiBot
    {
        private BDD db = new BDD();
        public string DomaineName { get; set; }
        public string Node { get; set; }

        public HtmlNodeCollection GetDataFromSource(string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            HtmlNodeCollection hnc = doc.DocumentNode.SelectNodes(this.Node);
            return hnc;
        }
        public void DropTableArticles()
        {
            List<Article> articles = db.Articles.ToList();
            db.Articles.RemoveRange(articles);
            db.SaveChanges();
        }

        public void PutIntoBDD(HtmlNodeCollection res)
        {
            if (res != null)
            {
                Random i = new Random();
                foreach (HtmlNode item in res)
                {
                    string img = item.ChildNodes[1].ChildNodes[1].ChildNodes[1].GetAttributeValue("src", null).Trim();
                    string description = item.ChildNodes[3].InnerText.Trim().Replace("   ", "<br>");
                    string format = item.ChildNodes[5].InnerText.Trim();
                    string marque = item.ChildNodes[7].InnerText.Trim();
                    string prix = item.ChildNodes[9].InnerText.Trim();
                    string dateDebut_dateFin = item.ChildNodes[10].InnerHtml;
                    string magasin = item.ChildNodes[12].InnerHtml;
                    db.Articles.Add(new Article
                    {
                        Id = i.Next(1, 999999999),
                        Image = "<img src='" + this.DomaineName + img + "' />",
                        Description = description,
                        Format = format,
                        Marque = marque,
                        Prix = prix,
                        DateDebut_DateFin = dateDebut_dateFin,
                        Magasin = magasin
                    });
                    db.SaveChanges();
                }
            }
        }
        public void PutIntoList(HtmlNodeCollection res, List<Article> listArticles)
        {
            if (res != null)
            {
                Random i = new Random();
                foreach (HtmlNode item in res)
                {
                    string img = item.ChildNodes[1].ChildNodes[1].ChildNodes[1].GetAttributeValue("src", null).Trim();
                    string description = item.ChildNodes[3].InnerText.Trim();
                    string format = item.ChildNodes[5].InnerText.Trim();
                    string marque = item.ChildNodes[7].InnerText.Trim();
                    string prix = item.ChildNodes[9].InnerText.Trim();
                    string dateDebut_dateFin = item.ChildNodes[10].InnerHtml;
                    string magasin = item.ChildNodes[12].InnerHtml;
                    listArticles.Add(new Article
                    {
                        Id = i.Next(1, 111111111),
                        Image = this.DomaineName + img,
                        Description = description,
                        Format = format,
                        Marque = marque,
                        Prix = prix,
                        DateDebut_DateFin = dateDebut_dateFin,
                        Magasin = magasin
                    });
                }
            }
        }
    }
}