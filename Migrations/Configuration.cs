namespace Qiliqid.V1.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HtmlAgilityPack;
    using Qiliqid.V1.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<Qiliqid.V1.BDD>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(Qiliqid.V1.BDD context)
        {
            QiBot bot = new QiBot
            {
                DomaineName = "https://www.supermarches.ca",
                Node = "//*[@id='table28']/tr[2]/td/div/table/tr",
            };
            List<Article> articles = new List<Article>();
            int lastPageNumber = 208;
            int k = 1;
            for (int i = 1; i <= lastPageNumber; i++)
            {
                HtmlNodeCollection res = bot.GetDataFromSource("https://www.supermarches.ca/pages/Default.asp?page=" + i.ToString());
                if (k <= 3) // La aille max d'une liste est de 30 elemetns et nous recuperons 10/pages
                {
                    bot.PutIntoList(res, articles);
                    articles.ForEach(a => context.Articles.AddOrUpdate(p => p.DateDebut_DateFin, a));
                    context.SaveChanges();
                    k++;
                }
                else // Quand ca dépasse, on remet k = 1 et on renitialise la liste
                {
                    k = 1;
                    articles.RemoveRange(0, articles.Count - 1);
                    bot.PutIntoList(res, articles);
                    articles.ForEach(a => context.Articles.AddOrUpdate(p => p.DateDebut_DateFin, a));
                    context.SaveChanges();
                    k++;
                }
            }
        }

    }

}
