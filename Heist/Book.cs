using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heist
{
    class Book
    {
        public string title { get; set; }

        public string authName { get; set; }

        public bool purchased { get; set; }

        public bool download { get; set; }

        public long price { get; set; }

        public List<chapters> lessons { get; set; }

        public string imgUrl { get; set; }

    }

    class chapters
    {
        public string title { get; set; }

        public string name { get; set; }

        public long priceL { get; set; }

    }
}
