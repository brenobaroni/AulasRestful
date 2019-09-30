using RestWithAspNetUdemy.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RestWithAspNetUdemy.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Column("Title")]
        public string Title { get; set; }

        [Column("Author")]
        public string Author { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("LaunchDate")]
        public DateTime LaunchDate { get; set; }
    }
}
