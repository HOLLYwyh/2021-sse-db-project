using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Configuration;
using System.ComponentModel.DataAnnotations;


namespace InternetMall.Models
{
    public class Seller
    {
        [Required]

        [StringLength(6)]
        public string seller_id { get; set; }
        [Required]

        [StringLength(18)]
        public string passwd { get; set; }
        [Required]

        [StringLength(30)]
        public string nickname  { get; set; }
        [Required]

        [StringLength(30)]
        public string name { get; set; }
        [Required]

        [StringLength(18)]
        public string id_number { get; set; }
        [Required]

        [StringLength(11)]
        public string phone { get; set; }
    }
    public class SellerDbContext : DbContext
    {
        public DbSet<Seller> Seller { get; set; }
    }
}
