/*
 * Sample Implementation
 * ITSE 1430
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data
{
    /// <summary>Provides a repository for products.</summary>
    public class Products
    {
        NileDbContext _context;

        #region Construction

        /// <summary>Initializes an instance of the <see cref="Products"/> class.</summary>
        internal Products(NileDbContext context)
        {
            _context = context;

        }
        #endregion


        private IDbSet<Product> Query()
        {
            return _context.Products;
        }

        /// <summary>Adds a product to the list.</summary>
        /// <param name="product">The product to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="product"/> is «null».</exception>
        /// <exception cref="ValidationException"><paramref name="product"/> is invalid.</exception>
        /// <exception cref="ArgumentException">A product with the same name already exists.</exception>
        public Product Add(Product product)
        {
            //Validate arguments
            Verify.ArgumentIsNotNull(nameof(product), product);
            Verify.ArgumentIsValid(nameof(product), product);

            var temp = Query().Create();
            temp.Name = product.Name;
            temp.UnitPrice = product.UnitPrice;
            temp.Discontinued = product.Discontinued;


            var ret = Query().Add(temp);
            _context.SaveChanges();
            return ret;
        }

        /// <summary>Gets a product.</summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product, if any.</returns>        
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 1.</exception>
        public Product Get(int id)
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 1);

            return Query().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>Gets all the products.</summary>
        /// <returns>The list of products.</returns>
        public IEnumerable<Product> GetAll()
        {
            return Query().ToList();
        }

        /// <summary>Removes a product.</summary>
        /// <param name="id">The ID.</param>        
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 1..</exception>
        public void Remove(int id)
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 1);

            //Remove it
            var product = Query().FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Discontinued = true;
                _context.SaveChanges();
            }
        }

        /// <summary>Updates a product.</summary>
        /// <param name="product">The product to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="product"/> is «null».</exception>
        /// <exception cref="ArgumentException"><paramref name="product"/> does not exist or a product with that name already exists.</exception>
        /// <exception cref="ValidationException"><paramref name="product"/> is invalid.</exception>
        public void Update(Product product)
        {
            //Validate arguments
            Verify.ArgumentIsNotNull(nameof(product), product);
            Verify.ArgumentIsValid(nameof(product), product);



            //Find the existing product
            var existing = Query().FirstOrDefault(p => p.Id == product.Id);
            if (existing == null)
                throw new ArgumentException("Product does not exist.", nameof(product));

            //Must have a unique name
            if (Query().Any(p => p.Id != product.Id && String.Compare(product.Name, p.Name, true) == 0))
                throw new ArgumentException("Name must be unique.", nameof(product));

            existing.UnitPrice = product.UnitPrice;
            existing.Name = product.Name;
            existing.Discontinued = product.Discontinued;

            _context.SaveChanges();

        }
    }
}

