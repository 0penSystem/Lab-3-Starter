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
    /// <summary>Represents a product.</summary>
    public class Product : IValidatableObject
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="Product"/> class.</summary>
        public Product ()
        { }

        /// <summary>Initializes an instance of the <see cref="Product"/> class.</summary>
        /// <param name="id">The ID.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 0.</exception>
        public Product ( int id )
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 0);

            Id = id;
        }
        #endregion

        /// <summary>Gets the ID of the product.</summary>
        public int Id { get; internal set; }
        
        /// <summary>Gets or sets whether the product is discontinued.</summary>
        public bool Discontinued { get; set; }

        /// <summary>Gets or sets the name of the product.</summary>        
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value; }
        }

        /// <summary>Gets or sets the price of the product..</summary>
        public decimal UnitPrice { get; set; }

        /// <summary>Validates the object.</summary>                       
        /// <param name="validationContext">The context.</param>
        /// <returns>The list of errors.</returns>
        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext )
        {
            if (String.IsNullOrEmpty(Name))
                yield return new ValidationResult("Name is required.", new[] { nameof(Name) });

            if (UnitPrice <= 0)
                yield return new ValidationResult("Unit Price must be > 0.", new[] { nameof(UnitPrice) });
        }

        #region Private Members

        private string _name;
        #endregion
    }
}