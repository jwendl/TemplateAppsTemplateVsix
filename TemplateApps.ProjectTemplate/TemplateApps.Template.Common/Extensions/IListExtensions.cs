using System.Collections.Generic;
using System.Linq;

namespace $safeprojectname$.Extensions
{
    /// <summary>
    /// Extension methods for IList interface.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Resets items in a list and adds new items to the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList">The source list.</param>
        /// <param name="itemsToAdd">The items to add.</param>
        public static void ResetAndAddItems<T>(this IList<T> sourceList, IList<T> itemsToAdd)
        {
            Arg.IsNotNull(() => sourceList);
            Arg.IsNotNull(() => itemsToAdd);

            sourceList.Clear();
            itemsToAdd.ToList().ForEach(t => sourceList.Add(t));
        }

        /// <summary>
        /// Resets items in a list and adds new items to the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceList">The source list.</param>
        /// <param name="itemsToAdd">The items to add.</param>
        public static void ResetAndAddItems<T>(this IList<T> sourceList, IEnumerable<T> itemsToAdd)
        {
            Arg.IsNotNull(() => sourceList);
            Arg.IsNotNull(() => itemsToAdd);

            sourceList.Clear();
            itemsToAdd.ToList().ForEach(t => sourceList.Add(t));
        }
    }
}
