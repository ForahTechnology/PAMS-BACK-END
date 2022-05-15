using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.Application.Interfaces.Utilities
{
    /// <summary>
    /// This interface accesses the details of the currently signed in user.
    /// </summary>
    public interface IContextAccessor
    {
        /// <summary>
        /// This returns the ID of the currently signed in user.
        /// </summary>
        /// <returns></returns>
        Guid GetCurrentUserId();

        /// <summary>
        /// This returns the email address of the currently signed in user.
        /// </summary>
        /// <returns></returns>
        string GetCurrentUserEmail();
    }
}
