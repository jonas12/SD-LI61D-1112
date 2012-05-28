using System.Collections.Generic;

namespace CommonInterface
{
    public interface IPeerRequestContext
    {
        int Jumps { get; set; }
        /// <summary>
        /// </summary>
        /// <param name="sp">The sp.</param>
        /// <returns><code>true</code> if it was successfully added.
        ///          <code>false</code> if it already exists in the list.</returns>
        bool CheckAndAdd(int sp);
    }
}