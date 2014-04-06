using System;

namespace $safeprojectname$.Extensions
{
    /// <summary>
    /// Helps with DateTime calls.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Counts the month difference between two <seealso cref="DateTime"/> objects.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The count of months between startDate and endDate.</returns>
        public static int CountMonthDifference(this DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                var endDateModified = endDate.Value.AddDays(1);

                var monthPart = endDateModified.Month - startDate.Value.Month;
                var yearPart = endDateModified.Year - startDate.Value.Year;
                return monthPart + (yearPart * 12);
            }

            return 0;
        }

        /// <summary>
        /// Gets the 1st day of the previous month based on the current date.
        /// </summary>
        /// <param name="currentDate">The current date object.</param>
        /// <returns>The first of the previous month.</returns>
        public static DateTime FetchCloseDate(this DateTime currentDate)
        {
            var previousMonth = currentDate.AddMonths(-1);
            return new DateTime(previousMonth.Year, previousMonth.Month, 1).Date;
        }
    }
}
