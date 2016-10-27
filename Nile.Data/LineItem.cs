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
    /// <summary>Represents a line item in an order.</summary>
    public class LineItem
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="LineItem"/> class.</summary>
        public LineItem ()
        { }

        /// <summary>Initializes an instance of the <see cref="LineItem"/> class.</summary>
        /// <param name="id">The ID.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 0.</exception>
        public LineItem ( int id )
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 0);

            Id = id;
        }
        #endregion

        /// <summary>Gets the ID of the line item.</summary>
        public int Id { get; private set; }

        /// <summary>Gets or sets the product associated with the order.</summary>
        public Product Product { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        public int Quantity { get; set; }

        /// <summary>Gets the total price for the line item.</summary>
        public decimal TotalPrice
        {
            get { return (Product?.UnitPrice ?? 0) * Quantity; }
        }
    }
}