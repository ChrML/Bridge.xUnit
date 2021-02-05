using System;
using System.Collections;
using System.Globalization;
using Xunit.Sdk;

namespace Xunit
{
    /// <summary>
    /// Base class for exceptions that have actual and expected values.
    /// </summary>
    public class AssertActualExpectedException : XunitException
    {
        /// <summary>
        /// Creates a new instance of the <see href="AssertActualExpectedException"/> class.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="userMessage">The user message to be shown</param>
        /// <param name="expectedTitle">The title to use for the expected value (defaults to "Expected")</param>
        /// <param name="actualTitle">The title to use for the actual value (defaults to "Actual")</param>
        public AssertActualExpectedException(object expected, object actual, string userMessage, string expectedTitle = null, string actualTitle = null)
            : this(expected, actual, userMessage, expectedTitle, actualTitle, null)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see href="AssertActualExpectedException"/> class.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="userMessage">The user message to be shown</param>
        /// <param name="expectedTitle">The title to use for the expected value (defaults to "Expected")</param>
        /// <param name="actualTitle">The title to use for the actual value (defaults to "Actual")</param>
        /// <param name="innerException">The inner exception.</param>
        public AssertActualExpectedException(object expected, object actual, string userMessage, string expectedTitle, string actualTitle, Exception innerException)
            : base(userMessage, innerException)
        {
            this.Actual = actual == null ? null : ConvertToString(actual);
            this.ActualTitle = actualTitle ?? "Actual";
            this.Expected = expected == null ? null : ConvertToString(expected);
            this.ExpectedTitle = expectedTitle ?? "Expected";

            if (actual != null &&
                expected != null &&
                this.Actual == this.Expected &&
                actual.GetType() != expected.GetType())
            {
                this.Actual += string.Format(CultureInfo.CurrentCulture, " ({0})", actual.GetType().FullName);
                this.Expected += string.Format(CultureInfo.CurrentCulture, " ({0})", expected.GetType().FullName);
            }
        }

        /// <summary>
        /// Gets the actual value.
        /// </summary>
        public string Actual { get; private set; }

        /// <summary>
        /// Gets the title used for the actual value.
        /// </summary>
        public string ActualTitle { get; private set; }

        /// <summary>
        /// Gets the expected value.
        /// </summary>
        public string Expected { get; private set; }

        /// <summary>
        /// Gets the title used for the expected value.
        /// </summary>
        public string ExpectedTitle { get; private set; }

        /// <summary>
        /// Gets a message that describes the current exception. Includes the expected and actual values.
        /// </summary>
        /// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
        /// <filterpriority>1</filterpriority>
        public override string Message
        {
            get
            {
                var titleLength = Math.Max(this.ExpectedTitle.Length, this.ActualTitle.Length) + 2;  // + the colon and space
                var formattedExpectedTitle = (this.ExpectedTitle + ":").PadRight(titleLength);
                var formattedActualTitle = (this.ActualTitle + ":").PadRight(titleLength);

                return string.Format(CultureInfo.CurrentCulture,
                                     "{0}{5}{1}{2}{5}{3}{4}",
                                     base.Message,
                                     formattedExpectedTitle,
                                     this.Expected ?? "(null)",
                                     formattedActualTitle,
                                     this.Actual ?? "(null)",
                                     Environment.NewLine);
            }
        }

        static string ConvertToSimpleTypeName(Type typeInfo)
        {
            return typeInfo.Name;

            //if (!typeInfo.IsGenericType)
            //    return typeInfo.Name;

            //var simpleNames = typeInfo.GenericTypeArguments.Select(type => ConvertToSimpleTypeName(type.GetTypeInfo()));
            //var backTickIdx = typeInfo.Name.IndexOf('`');
            //if (backTickIdx < 0)
            //    backTickIdx = typeInfo.Name.Length;  // F# doesn't use backticks for generic type names

            //return string.Format("{0}<{1}>", typeInfo.Name.Substring(0, backTickIdx), string.Join(", ", simpleNames));
        }

        static string ConvertToString(object value)
        {
            var stringValue = value as string;
            if (stringValue != null)
                return stringValue;

            var formattedValue = ArgumentFormatter.Format(value);
            if (value is IEnumerable)
                formattedValue = string.Format("{0} {1}", ConvertToSimpleTypeName(value.GetType()), formattedValue);

            return formattedValue;
        }
    }
}
