using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qiliqid.V1.Models
{
    public class Article
    {
        public int? Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public string Marque { get; set; }
        public string Prix { get; set; }
        public string DateDebut_DateFin { get; set; }
        public string Periode { get => DateDebut_DateFin; }
        public string Magasin { get; set; }
    }
}