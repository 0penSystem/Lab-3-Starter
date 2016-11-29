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
    /// <summary>Provides a repository for customers.</summary>
    public class Customers
    {
        NileDbContext _context;

        #region Construction

        /// <summary>Initializes an instance of the <see cref="Customers"/> class.</summary>
        internal Customers(NileDbContext context)
        {
            _context = context;
        }
        #endregion

        private IDbSet<Customer> Query()
        {
            return _context.Customers;
        }


        /// <summary>Adds a customer.</summary>
        /// <param name="customer">The customer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="customer"/> is «null».</exception>
        /// <exception cref="ValidationException"><paramref name="customer"/> is invalid.</exception>
        /// <exception cref="ArgumentException">The customer already exists.</exception>
        public Customer Add(Customer customer)
        {
            Verify.ArgumentIsNotNull(nameof(customer), customer);
            Verify.ArgumentIsValid(nameof(customer), customer);

            if (FindByName(customer.FirstName, customer.LastName, 0) != null)
                throw new ArgumentException("Customer already exists.", nameof(customer));

            var cust = Query().Create();

            cust.FirstName = customer.FirstName;
            cust.LastName = customer.LastName;

            var returnCustomer = Query().Add(cust);

            _context.SaveChanges();

            return returnCustomer;
        }

        /// <summary>Gets a customer.</summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>The customer, if any.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 1.</exception>
        public Customer Get(int id)
        {
            Verify.ArgumentIsGreaterThanOrEqualTo(nameof(id), id, 1);

            return Query().FirstOrDefault(c => c.Id == id);
        }

        /// <summary>Gets all the customers.</summary>
        /// <returns>The list of customers.</returns>
        public IEnumerable<Customer> GetAll()
        {
            return Query().ToList();
        }

        /// <summary>Removes a customer.</summary>
        /// <param name="id">The customer ID.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than 1.</exception>
        public void Remove(int id)
        {
            var customer = Get(id);

            if (customer != null)
            {
                Query().Remove(customer);
                _context.SaveChanges();
            }
        }

        /// <summary>Updates an existing customer.</summary>
        /// <param name="customer">The customer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="customer"/> is «null».</exception>
        /// <exception cref="ValidationException"><paramref name="customer"/> is invalid.</exception>
        /// <exception cref="ArgumentException">The customer does not exist or a customer with the same name already exists.</exception>
        public void Update(Customer customer)
        {
            Verify.ArgumentIsNotNull(nameof(customer), customer);
            Verify.ArgumentIsValid(nameof(customer), customer);

            var existing = Query().FirstOrDefault(c => c.Id == customer.Id);
            if (existing == null)
                throw new ArgumentException("Customer not found.", nameof(customer));

            if (FindByName(customer.FirstName, customer.LastName, customer.Id) != null)
                throw new ArgumentException("Customer already exists.", nameof(customer));

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;

            _context.SaveChanges();
        }

        #region Private Members

        private Customer FindByName(string firstName, string lastName, int ignoreId)
        {
            return Query().FirstOrDefault(c => c.Id != ignoreId &&
                                                  String.Compare(c.FirstName, firstName, true) == 0 &&
                                                  String.Compare(c.LastName, lastName, true) == 0);
        }

        #endregion

    }
}

