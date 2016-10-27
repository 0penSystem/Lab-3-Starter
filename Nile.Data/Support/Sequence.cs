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
    /// <summary>Provides a simple class for generating sequential numbers.</summary>
    public class Sequence
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="Sequence"/> class.</summary>
        public Sequence () : this(1)
        { }

        /// <summary>Initializes an instance of the <see cref="Sequence"/> class.</summary>
        /// <param name="startValue">The starting value.</param>
        public Sequence ( int startValue )
        {
            _value = startValue;
        }
        #endregion

        /// <summary>Gets the next value.</summary>
        /// <returns>The next value.</returns>
        public int Next ()
        {
            return _value++;
        }

        #region Private Members
                
        private int _value;

        #endregion
    }
}
