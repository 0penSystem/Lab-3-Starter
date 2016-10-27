/*
 * Sample Implementation
 * ITSE 1430
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data
{
    /// <summary>Represents a customer's order.</summary>
    public class Order : IValidatableObject
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="Order"/> class.</summary>
        public Order ()
        { }

        /// <summary>Initializes an instance of the <see cref="Order"/> class.</summary>
        /// <param name="id">The ID.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 0.</exception>
        public Order ( int id )
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 0);

            Id = id;
        }
        #endregion

        /// <summary>Gets the ID of the order.</summary>
        public int Id { get; internal set; }

        /// <summary>Gets whether the order is complete.</summary>
        public bool IsComplete { get; private set; }

        /// <summary>Gets the list of line items.</summary>
        public IEnumerable<LineItem> LineItems
        {
            get { return _items; }
        }
            
        /// <summary>Gets the date of the order.</summary>
        public DateTime OrderDate { get; } = DateTime.Now;

        /// <summary>Gets the total price for the order.</summary>
        public decimal TotalPrice
        {
            get 
            {
                return LineItems.Sum(i => i.TotalPrice);
            }
        }

        /// <summary>Adds a product to the order.</summary>
        /// <param name="product">The product to add.</param>
        /// <param name="quantity">The quantity.</param>
        /// <exception cref="ArgumentNullException"><paramref name="product"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name="product"/> is invalid.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="quantity"/> is less than 1..</exception>
        /// <exception cref="InvalidOperationException">The order is complete.</exception>
        public void AddToOrder ( Product product, int quantity )
        {
            Verify.ArgumentIsNotNull(nameof(product), product);
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(quantity), quantity, 1);
            Verify.ArgumentIs(nameof(product), product, p => !p.Discontinued, "Product is discontinued.");

            if (IsComplete)
                throw new InvalidOperationException("Order is complete and cannot be modified.");

            //If the product already exists, add to the quantity otherwise create a new one
            var existing = _items.FirstOrDefault(i => i.Product == product);
            if (existing != null)
                existing.Quantity += quantity;
            else
            {
                existing = new LineItem(_sequence.Next()) { Product = product, Quantity = quantity };
                _items.Add(existing);
            };
        }

        /// <summary>Completes the order.</summary>
        /// <exception cref="ValidationException">The order is invalid.</exception>
        public void Complete ()
        {
            if (IsComplete)
                return;

            Verify.ArgumentIsValid("Order", this);

            IsComplete = true;
        }

        /// <summary>Removes a product from the order.</summary>
        /// <param name="productId">The product ID.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="productId"/> is less than 1.</exception>
        /// <exception cref="InvalidOperationException">The order is complete.</exception>
        public void RemoveFromOrder ( int productId )
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(productId), productId, 1);

            if (IsComplete)
                throw new InvalidOperationException("Order is complete and cannot be modified.");

            //Find it
            var item = _items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
                _items.Remove(item);
        }

        /// <summary>Validates the object.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The list of errors.</returns>
        public IEnumerable<ValidationResult> Validate ( ValidationContext context )
        {
            if (!_items.Any())
                yield return new ValidationResult("No items are in order.", new[] { nameof(LineItems) });
        }

        #region Private Members

        private readonly List<LineItem> _items = new List<LineItem>();
        private readonly Sequence _sequence = new Sequence();
        #endregion
    }
}
