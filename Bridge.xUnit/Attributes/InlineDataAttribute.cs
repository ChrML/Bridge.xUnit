using System;
using System.Collections.Generic;
using System.Reflection;

namespace Xunit
{
    /// <summary>
    /// Provides a data source for a data theory, with the data coming from inline values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class InlineDataAttribute : DataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineDataAttribute"/> class.
        /// </summary>
        /// <param name="data"></param>
        public InlineDataAttribute(params object[] data)
        {
            this.data = data;
        }

        /// <summary>
        /// Gets the inline data provided by this attribute.
        /// </summary>
        /// <param name="testMethod"></param>
        /// <returns></returns>
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return new[] { this.data };
        }

        #region Privates
        readonly object[] data;
        #endregion
    }
}
