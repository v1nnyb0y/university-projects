using System;
using System.Collections.Generic;

namespace Requests.Interfaces
{
    /// <summary>
    ///     Interface for the update LINQs
    /// </summary>
    public interface IUpdate : IDisposable
    {
        #region Room update functions

        #region Accesses

        /// <summary>
        ///     Create new access
        /// </summary>
        void UpdateAccesses
        (
            List<object[]> accessesList
        );

        #endregion

        #endregion

        #region Human data update

        /// <summary>
        ///     Update information like Name, E-Mail etc.
        /// </summary>
        /// <param name="avatar">New avatar</param>
        /// <param name="address">New address</param>
        /// <param name="name">New name</param>
        /// <param name="secondName">New second name</param>
        /// <param name="fatherName">New father name</param>
        /// <param name="eMail">New e-Mail</param>
        /// <param name="phoneNumber">New phone Number</param>
        /// <param name="organization">New organization</param>
        /// <param name="userId">User ID</param>
        void UpdateExtraInformationAndAvatar
        (
            object avatar,
            string address,
            string name,
            string secondName,
            string fatherName,
            string eMail,
            string phoneNumber,
            string organization,
            int userId
        );

        /// <summary>
        ///     Update password
        /// </summary>
        /// <param name="newPassword">New password</param>
        /// <param name="userId">User's ID</param>
        void UpdatePassword
        (
            string newPassword,
            int userId
        );

        /// <summary>
        ///     Update avatar
        /// </summary>
        /// <param name="newAvatar">Byte[] newAvatar</param>
        /// <param name="userId">User's ID</param>
        void UpdateAvatar
        (
            object newAvatar,
            int userId
        );

        #endregion

        #region Manipulated sensors

        /// <summary>
        /// Update status of the sensor
        /// </summary>
        /// <param name="sensorId"></param>
        /// <param name="newStatus"></param>
        void UpdateStatusSensor
        (
            int    sensorId,
            string newStatus
        );

        void UpdateConfig
        (
            int    roomId,
            string newCfg
        );

        #endregion
    }
}