using System;

namespace Requests.Interfaces
{
    /// <summary>
    ///     Interface for the remove user LINQs
    /// </summary>
    public interface IRemove : IDisposable
    {
        #region Remove user

        /// <summary>
        ///     remove user by cascade method
        /// </summary>
        /// <param name="addressId">Address Id</param>
        void RemoveUser
        (
            int addressId
        );

        #endregion
    }
}