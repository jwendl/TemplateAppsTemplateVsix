using System;
using System.Linq.Expressions;
using System.Reflection;

namespace $safeprojectname$
{
    /// <summary>
    /// For use with parameter checking.
    /// </summary>
    public static class Arg
    {
        /// <summary>
        /// Checks whether or not an argument is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <exception cref="System.ArgumentNullException">Argument param.Member.Name cannot be null.</exception>
        public static void IsNotNull<T>(Expression<Func<T>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            var memberSelector = (MemberExpression)selector.Body;
            var constantSelector = (ConstantExpression)memberSelector.Expression;

            var value = ((FieldInfo)memberSelector.Member)
                .GetValue(constantSelector.Value);

            if (value == null)
            {
                string name = ((MemberExpression)selector.Body).Member.Name;
                throw new ArgumentNullException(name);
            }
        }
    }
}
