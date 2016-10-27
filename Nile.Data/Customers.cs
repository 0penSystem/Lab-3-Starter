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
    /// <summary>Provides a repository for customers.</summary>
    public class Customers
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="Customers"/> class.</summary>
        public Customers ()
        {
            _customers = new List<Customer>()
            {
                new Customer(_sequence.Next()) { FirstName = "Bob", LastName = "Miller" },
                new Customer(_sequence.Next()) { FirstName = "Sue", LastName = "Storm" },
                new Customer(_sequence.Next()) { FirstName = "Reed", LastName = "Richard" },
                new Customer(_sequence.Next()) { FirstName = "Peter", LastName = "Parker" },
                new Customer(_sequence.Next()) { FirstName = "Tony", LastName = "Stark" }
            };    
        }
        #endregion

        /// <summary>Adds a customer.</summary>
        /// <param name="customer">The customer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="customer"/> is «null».</exception>
        /// <exception cref="ValidationException"><paramref name="customer"/> is invalid.</exception>
        /// <exception cref="ArgumentException">The customer already exists.</exception>
        public void Add ( Customer customer )
        {
            Verify.ArgumentIsNotNull(nameof(customer), customer);
            Verify.ArgumentIsValid(nameof(customer), customer);

            if (FindByName(customer.FirstName, customer.LastName, 0) != null)
                throw new ArgumentException("Customer already exists.", nameof(customer));

            customer.Id = _sequence.Next();

            _customers.Add(customer);
        }

        /// <summary>Gets a customer.</summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>The customer, if any.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 1.</exception>
        public Customer Get ( int id )
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 1);

            return _customers.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>Gets all the customers.</summary>
        /// <returns>The list of customers.</returns>
        public IEnumerable<Customer> GetAll ()
        {
            return _customers;
        }

        /// <summary>Removes a customer.</summary>
        /// <param name="id">The customer ID.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 1.</exception>
        public void Remove ( int id )
        {
            var customer = Get(id);

            if (customer != null)
                _customers.Remove(customer);
        }

        /// <summary>Updates an existing customer.</summary>
        /// <param name="customer">The customer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="customer"/> is «null».</exception>
        /// <exception cref="ValidationException"><paramref name="customer"/> is invalid.</exception>
        /// <exception cref="ArgumentException">The customer does not exist or a customer with the same name already exists.</exception>
        public void Update ( Customer customer )
        {
            Verify.ArgumentIsNotNull(nameof(customer), customer);
            Verify.ArgumentIsValid(nameof(customer), customer);

            var existing = _customers.FirstOrDefault(c => c.Id == customer.Id);
            if (existing == null)
                throw new ArgumentException("Customer not found.", nameof(customer));

            if (FindByName(customer.FirstName, customer.LastName, customer.Id) != null)            
                throw new ArgumentException("Customer already exists.", nameof(customer));

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            
        }

        #region Private Members

        private Customer FindByName ( string firstName, string lastName, int ignoreId )
        {
            return _customers.FirstOrDefault(c => c.Id != ignoreId &&
                                                  String.Compare(c.FirstName, firstName, true) == 0 &&
                                                  String.Compare(c.LastName, lastName, true) == 0);
        }

        private readonly List<Customer> _customers;
        private readonly Sequence _sequence = new Sequence();
        #endregion

    }
}

