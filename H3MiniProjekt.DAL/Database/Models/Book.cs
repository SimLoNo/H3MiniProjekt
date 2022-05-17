using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3MiniProjekt.DAL.Database.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public double WordCound { get; set; }
        public bool Binding { get; set; }
        public int ReleaseYear { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
