using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data
{
    internal class NileDbContext : DbContext
    {

        public NileDbContext(string connection) : base(connection) { }

        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Product> Products { get; set; }

        static NileDbContext()
        {
            System.Data.Entity.Database.SetInitializer<NileDbContext>(null);
        }

    }
}
