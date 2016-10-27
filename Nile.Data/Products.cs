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
    /// <summary>Provides a repository for products.</summary>
    public class Products
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="Products"/> class.</summary>
        public Products ()
        {
            _products = new List<Product>()
            {
                new Product(_sequence.Next()) { Name = "Baseball Glove", UnitPrice = 10.50M },
                new Product(_sequence.Next()) { Name = "Baseball Bat", UnitPrice = 12.75M },
                new Product(_sequence.Next()) { Name = "Referee Uniform", UnitPrice = 80M },
                new Product(_sequence.Next()) { Name = "Baseball Uniform (Small)", UnitPrice = 15M },
                new Product(_sequence.Next()) { Name = "Baseball Uniform (Medium)", UnitPrice = 25M },
                new Product(_sequence.Next()) { Name = "Baseball Uniform (Large)", UnitPrice = 40M },
                new Product(_sequence.Next()) { Name = "Baseball Tee", UnitPrice = 25.75M },
                new Product(_sequence.Next()) { Name = "Baseball Bat (Metal)", UnitPrice = 10.50M },
                new Product(_sequence.Next()) { Name = "Baseball Glove (Left)", UnitPrice = 10.50M, Discontinued = true },
                new Product(_sequence.Next()) { Name = "Referee Uniform (Classic)", UnitPrice = 115M, Discontinued = true }
            };
        }
        #endregion

        /// <summary>Adds a product to the list.</summary>
        /// <param name="product">The product to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="product"/> is «null».</exception>
        /// <exception cref="ValidationException"><paramref name="product"/> is invalid.</exception>
        /// <exception cref="ArgumentException">A product with the same name already exists.</exception>
        public void Add ( Product product )
        {
            //Validate arguments
            Verify.ArgumentIsNotNull(nameof(product), product);
            Verify.ArgumentIsValid(nameof(product), product);

            //Must have a unique name
            if (_products.Any(p => String.Compare(product.Name, p.Name, true) == 0))
                throw new ArgumentException("Name must be unique.", nameof(product));

            //Generate a new ID
            product.Id = _sequence.Next();

            _products.Add(product);
        }

        /// <summary>Gets a product.</summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product, if any.</returns>        
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 1.</exception>
        public Product Get ( int id )
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 1);
                            
            return _products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>Gets all the products.</summary>
        /// <returns>The list of products.</returns>
        public IEnumerable<Product> GetAll ()
        {
            return _products;
        }

        /// <summary>Removes a product.</summary>
        /// <param name="id">The ID.</param>        
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 1..</exception>
        public void Remove ( int id )
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 1);

            //Remove it
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                product.Discontinued = true;
        }

        /// <summary>Updates a product.</summary>
        /// <param name="product">The product to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="product"/> is «null».</exception>
        /// <exception cref="ArgumentException"><paramref name="product"/> does not exist or a product with that name already exists.</exception>
        /// <exception cref="ValidationException"><paramref name="product"/> is invalid.</exception>
        public void Update ( Product product )
        {
            //Validate arguments
            Verify.ArgumentIsNotNull(nameof(product), product);
            Verify.ArgumentIsValid(nameof(product), product);

            //Find the existing product
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing == null)
                throw new ArgumentException("Product does not exist.", nameof(product));

            //Must have a unique name
            if (_products.Any(p => p.Id != product.Id && String.Compare(product.Name, p.Name, true) == 0))
                throw new ArgumentException("Name must be unique.", nameof(product));

            existing.UnitPrice = product.UnitPrice;
            existing.Name = product.Name;
            existing.Discontinued = product.Discontinued;

        }

        #region Private Members

        private readonly List<Product> _products;
        private readonly Sequence _sequence = new Sequence();
        #endregion
    }
}

