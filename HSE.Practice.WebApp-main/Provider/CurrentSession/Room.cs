using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Provider.CurrentSession
{
    /// <summary>
    ///     Class with info about Room
    /// </summary>
    public class Room
    {
        public class Filter
        {
            [Display(Name = "Состояние датчика")]
            public string Turning { get; set; }

            [Display(Name = "Термодатчик")]
            public bool Temperature { get; set; }

            [Display(Name = "Датчик давления")]
            public bool Presser { get; set; }

            [Display(Name = "Освещение")]
            public bool Lightning { get; set; }

            [Display(Name = "Визуализации")]
            public bool Visual { get; set; }

            [Display(Name = "Датчик движения")]
            public bool Movement { get; set; }

            [Display(Name = "Датчик газа")]
            public bool Gas { get; set; }

            [Display(Name = "Датчик дождя")]
            public bool Rain { get; set; }

            [Display(Name = "Датчик времени")]
            public bool Time { get; set; }

            [Display(Name = "Датчик связи")]
            public bool Connection { get; set; }

            [Display(Name = "Датчик состояния")]
            public bool Open { get; set; }
        }

        public class Sensor
        {
            public int SensorId { get; set; }
            public int OptionId { get; set; }
            public string Name  { get; set; }
            public string Fam   { get; set; }
            public string State { get; set; }
            public string Data  { get; set; }
        }

        public class SmallUser
        {

            public int RoomId { get; set; }
            public int AddressId { get; set; }
            public int UserId { get; set; }
            [Display(Name = "Ф.И.О.")]
            [ReadOnly(true)]
            public string FIO { get; set; }
            [Display(Name = "Уровень доступа")]
            public string Access { get; set; }
        }

        #region Fields

        public Filter Filters { get; set; }
        public string Name { get; set; }
        public string Access { get; set; }
        public int RoomId { get; set; }
        public string Settings { get; set; }
        public string FileData { get; set; }
        public string FileJson { get; set; }
        public int Index { get; set; }
        public List<Sensor> ListSensors { get; set; }
        public List<SmallUser> ListUsersInRoom { get; set; }

        #endregion

        #region Initialize

        /// <summary>
        ///     Initialize class
        /// </summary>
        public Room()
        {
        }

        /// <summary>
        ///     Initialize class
        /// </summary>
        /// <param name="name">Name of the Room</param>
        /// <param name="access">Access level to the Room</param>
        /// <param name="roomId">Room ID</param>
        /// <param name="settings">Settings of the model of the Room</param>
        public Room
        (
            string name,
            string access,
            int roomId,
            string settings
        )
        {
            Name = name;
            Access = access;
            RoomId = roomId;
            Settings = settings;
        }

        /// <summary>
        ///     Initialize class (make full copy)
        /// </summary>
        /// <param name="copiedRoom">Source Room</param>
        public Room
        (
            Room copiedRoom
        )
        {
            Name = copiedRoom.Name;
            Access = copiedRoom.Access;
            RoomId = copiedRoom.RoomId;
            Settings = copiedRoom.Settings;
        }

        #endregion
    }
}