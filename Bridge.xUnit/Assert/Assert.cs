using System;
using System.Collections;
using System.Collections.Generic;

namespace Xunit
{
    /// <summary>
    /// Contains various static methods that are used to verify that conditions are met during the process of running tests.
    /// </summary>
    public partial class Assert
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Assert"/> class.
        /// </summary>
        protected Assert() 
        { 
        }

        static IComparer<T> GetComparer<T>() where T : IComparable
        {
            

            return new AssertComparer<T>();
        }

        static IEqualityComparer<T> GetEqualityComparer<T>(IEqualityComparer innerComparer = null)
        {
            return new AssertEqualityComparer<T>(innerComparer);
        }
    }
}
