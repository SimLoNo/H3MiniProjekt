using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3MiniProjekt.DAL.Database.Models
{
    public class Author
    {
        public int AuthorId { get; set; } // PK
        public string Name { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public bool IsAlive { get; set; }
        public int BookId { get; set; }
        public List<Book> Book { get; set; }
    }
}
