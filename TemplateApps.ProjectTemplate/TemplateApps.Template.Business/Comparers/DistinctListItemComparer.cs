using $safeprojectname$.Models;
using $saferootprojectname$.Common;
using System.Collections.Generic;

namespace $safeprojectname$.Comparers
{
    /// <summary>
    /// A comparer for distinct list items.
    /// </summary>
    public class DistinctListItemComparer
        : IEqualityComparer<ListItem>
    {
        /// <summary>
        /// Compares two list item objects.
        /// </summary>
        /// <param name="x">The left side.</param>
        /// <param name="y">The right side.</param>
        /// <returns></returns>
        public bool Equals(ListItem x, ListItem y)
        {
            Arg.IsNotNull(() => x);
            Arg.IsNotNull(() => y);

            return x.Code == y.Code && x.Text == y.Text;
        }

        /// <summary>
        /// Gets the hash code for this given object.
        /// </summary>
        /// <param name="obj">The object that needs a hash code.</param>
        /// <returns>The hash code representation of the object.</returns>
        public int GetHashCode(ListItem obj)
        {
            Arg.IsNotNull(() => obj);

            return obj.Code.GetHashCode();
        }
    }
}
