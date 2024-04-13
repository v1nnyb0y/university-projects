using System.Collections.Generic;

namespace Provider.Interfaces
{
    /// <summary>
    ///     Interface for communication with Room classes
    /// </summary>
    public interface IRoomFunc
    {
        #region Managing user accesses

        /// <summary>
        ///     Get users in current room
        /// </summary>
        /// <param name="roomPos"></param>
        /// <param name="roomId">Room Id</param>
        /// <returns>List of the users and access</returns>
        CurrentProvider GetUsersInRoom
        (
            int roomPos,
            int roomId
        );

        /// <summary>
        ///     Update accesses
        /// </summary>
        /// <param name="accessedObjects">User id, access</param>
        void UpdateAccessesAsync
        (
            List<object[]> accessedObjects
        );

        #endregion

        #region GetRoomData

        CurrentProvider GetRoomSensors
        (
            int posRoom,
            int roomId
        );

        CurrentProvider GetManipulatedData
        (
            int posRoom
        );

        void UpdateStatusOfTheSensor
        (
            int    sensorId,
            string newStatus
        );

        void SetNewSettings
        (
            string       newCfg,
            params int[] ids
        );

        #endregion
    }
}