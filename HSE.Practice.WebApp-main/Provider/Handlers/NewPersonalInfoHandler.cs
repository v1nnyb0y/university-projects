using System;

namespace Provider.Handlers
{
    /// <inheritdoc />
    /// <summary>
    ///     Handler for the event to update information
    /// </summary>
    public class NewPersonalInfoHandler : EventArgs
    {
        /// <summary>
        ///     enum for the type of update
        /// </summary>
        public enum UpdateType
        {
            Password,
            Avatar,
            AvatarAndTextInfo
        }


        /// <summary>
        ///     Initialize handler
        /// </summary>
        /// <param name="currentUpdateType">What information should be updated</param>
        public NewPersonalInfoHandler
        (
            UpdateType currentUpdateType
        )
        {
            CurrentUpdateType = currentUpdateType;
        }

        /// <summary>
        ///     Type of update
        /// </summary>
        public UpdateType CurrentUpdateType { get; set; }
    }
}