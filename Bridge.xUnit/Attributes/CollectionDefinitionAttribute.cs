using System;

namespace Xunit
{
    /// <summary>
    /// Used to declare a test collection container class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CollectionDefinitionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionDefinitionAttribute"/> class.
        /// </summary>
        /// <param name="name"></param>
#pragma warning disable IDE0060 // Remove unused parameter
        public CollectionDefinitionAttribute(string name)
        {
        }
#pragma warning restore IDE0060 // Remove unused parameter

        /// <summary>
        /// Gets or sets whether tests in this collection runs in parallel with any other collections.
        /// </summary>
        public bool DisableParallelization { get; set; }
    }
}
