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
    /// <summary>Represents a customer.</summary>
    public class Customer : IValidatableObject
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="Customer"/> class.</summary>
        public Customer ()
        { }

        /// <summary>Initializes an instance of the <see cref="Customer"/> class.</summary>
        /// <param name="id">The ID.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 0.</exception>
        public Customer ( int id )
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 0);

            Id = id;
        }
        #endregion

        /// <summary>Gets the ID of the customer.</summary>
        public int Id { get; internal set; }

        /// <summary>Gets or sets the current order.</summary>
        public Order CurrentOrder { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        public string FirstName
        {
            get { return _firstName ?? ""; }
            set { _firstName = value; }
        }

        /// <summary>Gets or sets the last name.</summary>
        public string LastName
        {
            get { return _lastName ?? ""; }
            set { _lastName = value; }
        }

        /// <summary>Validates the object.</summary>
        /// <param name="validationContext">The context.</param>
        /// <returns>The errors.</returns>
        public IEnumerable<ValidationResult> Validate ( ValidationContext validationContext )
        {
            if (String.IsNullOrEmpty(FirstName))
                yield return new ValidationResult("First Name is required.", new[] { nameof(FirstName) });

            if (String.IsNullOrEmpty(LastName))
                yield return new ValidationResult("Last Name is required.", new[] { nameof(LastName) });
        }

        #region Private Members

        private string _firstName, _lastName;

        #endregion        
    }
}
