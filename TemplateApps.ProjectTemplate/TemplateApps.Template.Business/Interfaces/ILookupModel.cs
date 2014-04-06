using System.Collections.Generic;

namespace $safeprojectname$.Interfaces
{
    /// <summary>
    /// An interface for cache lookups (heavily cached by DI).
    /// </summary>
    /// <typeparam name="TBusinessObject">The business object held by this container.</typeparam>
    public interface ILookupModel<TBusinessObject>
    {
        /// <summary>
        /// Fetches all items from cache.
        /// </summary>
        IList<TBusinessObject> FetchAllItemsFromCache();
    }
}
