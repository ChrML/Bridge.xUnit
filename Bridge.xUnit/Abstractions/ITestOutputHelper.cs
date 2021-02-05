namespace Xunit.Abstractions
{
    /// <summary>
    /// Provides access to writing console output from unit-tests.
    /// </summary>
    public interface ITestOutputHelper
    {
        /// <summary>
        /// Writes a single line to the console output.
        /// </summary>
        /// <param name="line"></param>
        void WriteLine(string line);
    }
}
