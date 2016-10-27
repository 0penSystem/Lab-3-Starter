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
    public class Database
    {
        /// <summary>Gets the customers.</summary>
        public Customers Customers { get; private set; } = new Customers();

        /// <summary>Gets the products.</summary>
        public Products Products { get; private set; } = new Products();
    }
}
