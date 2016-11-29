/*
 * Sample Implementation
 * ITSE 1430
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data
{
    /// <summary>Provides a grouping for the various data objects.</summary>
    public class Database : IDisposable
    {

        NileDbContext _context;

        public Database(string connection)
        {
            _context = new NileDbContext(connection);
            Products = new Products(_context);
            Customers = new Customers(_context);
        }

        /// <summary>Gets the customers.</summary>
        public Customers Customers { get; private set; }

        /// <summary>Gets the products.</summary>
        public Products Products { get; private set; }


        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }



}
