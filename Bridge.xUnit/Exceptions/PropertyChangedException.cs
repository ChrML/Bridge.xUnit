using System.Globalization;
using Xunit.Sdk;

namespace Xunit
{
    /// <summary>
    /// Exception thrown when code unexpectedly fails change a property.
    /// </summary>
    public class PropertyChangedException : XunitException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="PropertyChangedException"/> class.
        /// </summary>
        /// <param name="actual"></param>
        public PropertyChangedException(string propertyName)
             : base(string.Format(CultureInfo.CurrentCulture, "Assert.PropertyChanged failure: Property {0} was not set", propertyName))
        { }
    }
}
