using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataManipulation;
using NPOI.SS.Formula.Functions;
using Provider.CurrentSession;
using Provider.Handlers;
using Provider.Interfaces;
using Requests;
using Requests.Interfaces;
using Xbim.COBieLite;
using Xbim.Ifc;
using Xbim.ModelGeometry.Scene;
using Person = Provider.CurrentSession.Person;
using Room = Provider.CurrentSession.Room;
using User = Provider.CurrentSession.User;
using File = System.IO.File;

namespace Provider
{
    /// <summary>
    ///     Provider new information for the application
    /// </summary>
    public class CurrentProvider : IHumanFunc,
        IRoomFunc,
        IQBuilder,
        IDataFunc
    {
        #region Initialize

        /// <summary>
        ///     Initialize current properties for the site
        /// </summary>
        public CurrentProvider()
        {
            Person = new Person();
            User = new User();
            RequestBuilder = new QBuilder();
            DataManipulation = new DataManipulator();
        }

        /// <summary>
        ///     Initialize current provider for the site (make a full copy)
        /// </summary>
        /// <param name="copiedProvider">Source for the copy</param>
        public CurrentProvider
        (
            CurrentProvider copiedProvider
        )
        {
            Person = new Person
            (
                copiedProvider.Person
            );
            User = new User
            (
                copiedProvider.User
            );
            RequestBuilder = new QBuilder();
            DataManipulation = new DataManipulator();
        }

        #endregion

        #region Fields

        /// <summary>
        ///     Person
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        ///     User
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     Create request
        /// </summary>
        public QBuilder RequestBuilder { get; set; }

        /// <summary>
        ///     Data manipulation
        /// </summary>
        public DataManipulator DataManipulation { get; set; }

        #endregion

        #region IRoomFunc

        #region Managing user accesses

        /// <summary>
        ///     Get users in current room
        /// </summary>
        /// <param name="roomPos"></param>
        /// <param name="roomId">Room Id</param>
        /// <returns>List of the users and access</returns>
        public virtual CurrentProvider GetUsersInRoom
        (
            int roomPos, 
            int roomId
        )
        {
            List<object[]> usersInRoom;
            using (ISelect db = new QQContext())
            {
                usersInRoom = db.GetUsersInRoom
                (
                    roomId
                );
            }
            User.Rooms[roomPos].ListUsersInRoom = new List<Room.SmallUser>();
            usersInRoom.ForEach(user => User.Rooms[roomPos].ListUsersInRoom.Add(new Room.SmallUser()
                                                                                    {
                                                                                        AddressId = (int) user[0],
                                                                                        UserId = (int)user[1],
                                                                                        FIO = (string)user[2],
                                                                                        Access = (string)user[3]
                                                                                    }));

            return this;
        }

        /// <summary>
        ///     Update accesses
        /// </summary>
        /// <param name="accessedObjects">User id, access</param>
        public virtual void UpdateAccessesAsync
        (
            List<object[]> accessedObjects
        )
        {
            using (IUpdate db = new QQContext()) {
                db.UpdateAccesses
                    (
                     accessedObjects
                    );
            }
        }

        #endregion

        #region GetRoomData

        public CurrentProvider GetRoomSensors
        (
            int posRoom,
            int roomId
        ) {
            List<object[]> result;
            using (ISelect select = new QQContext()) {
                result = select.GetRoomSensors
                    (
                     roomId
                    );
            }
            
            User.Rooms[posRoom].ListSensors = new List<Room.Sensor>();
            result.ForEach(line=> User.Rooms[posRoom].ListSensors.Add(new Room.Sensor()
                                                                          {
                                                                              Name = (string)line[0],
                                                                              Fam = (string)line[1],
                                                                              State = (string)line[2],
                                                                              Data = (string)line[3]
                                                                          }));

            return this;
        }

        public CurrentProvider GetManipulatedData
        (
            int posRoom
        ) {
            List<object[]> result;
            using (ISelect select = new QQContext()) {
                result = select.GetManipulatedRooms
                    (
                     User.Rooms[posRoom]
                         .RoomId
                    );
            }

            User.Rooms[posRoom].ListSensors = new List<Room.Sensor>();
            result.ForEach(line => User.Rooms[posRoom].ListSensors.Add(new Room.Sensor()
                                                                           {
                                                                               Name  = (string)line[0],
                                                                               Fam   = (string)line[1],
                                                                               State = (string)line[2],
                                                                               Data  = (string)line[3],
                                                                               SensorId = (int)line[4],
                                                                               OptionId = (int)line[5]
                                                                           }));

            return this;
        }

        public void UpdateStatusOfTheSensor
        (
            int sensorId,
            string newStatus
        ) {
            using (IUpdate update = new QQContext()) {
                update.UpdateStatusSensor(sensorId, newStatus);
            }
        }

        public void SetNewSettings
        (
            string       newCfg,
            params int[] ids
        ) {
            using (IUpdate update = new QQContext()) {
                update.UpdateConfig
                    (
                     ids[0],
                     newCfg
                    );
            }

            using (IInsert insert = new QQContext()) {
                switch (ids[1]) {
                    case 0:
                        insert.InsertNewValue(19, newCfg[0] == '1' ? "Открыто" : "Закрыто");
                        break;
                    case 1:
                        insert.InsertNewValue(20, newCfg[1] == '1' ? "Открыто" : "Закрыто");
                        break;
                    case 2:
                        insert.InsertNewValue(21, newCfg[2] == '1' ? "Открыто" : "Закрыто");
                        break;
                }
            }
        }

        #endregion

        #endregion

        #region IHumanFunc

        #region Authentication

        /// <summary>
        ///     Check level of the access to the application
        /// </summary>
        /// <param name="args">Login and Password</param>
        /// <returns></returns>
        public virtual Tuple<object, bool, CurrentProvider> CheckAccess
        (
            AccessHandler args
        )
        {
            List<object>[] data;
            using (ISelect db = new QQContext())
            {
                data = db.IsAccess
                (
                    args.Login,
                    args.Password
                );
            }

            if (data == null)
                return new Tuple<object, bool, CurrentProvider>
                (
                    "On",
                    false,
                    null
                );

            CreatePersonalData
            (
                data[0]
            );
            CreateRoomData
            (
                data[1]
            );

            return new Tuple<object, bool, CurrentProvider>
            (
                "Off",
                true,
                this
            );
        }

        /// <summary>
        ///     Create data from the List to Room/User/Person
        /// </summary>
        /// <param name="userData">Data array</param>
        private protected virtual void CreatePersonalData
        (
            List<object> userData
        )
        {

            User = new User
            (
                (int)userData[0],
                (string)userData[1],
                (string)userData[2],
                (string)userData[3],
                (byte[])userData[4],
                (int)userData[5],
                (string)userData[6],
                (string)userData[7],
                (string)userData[8],
                (string)userData[9],
                (string)userData[10],
                (string)userData[11],
                (int)userData[12]
            );
        }

        /// <summary>
        ///     Create data from the List to Room
        /// </summary>
        /// <param name="roomDataList">Data List array</param>
        private protected virtual void CreateRoomData
        (
            List<object> roomDataList
        )
        {

            User.Rooms = new List<Room>();
            foreach (List<object> roomData in roomDataList)
            {
                var newRoom = new Room
                (
                    (string)roomData[0],
                    (string)roomData[1],
                    (int)roomData[2],
                    (string)roomData[3]
                );
                User.Rooms.Add
                (
                    newRoom
                );
            }

        }

        #endregion

        #region Updating information

        /// <summary>
        ///     Update information
        /// </summary>
        /// <param name="args">Arguments</param>
        public virtual void UpdateAuthorizedHuman
        (
            NewPersonalInfoHandler args
        )
        {
            IUpdate db = new QQContext();
            switch (args.CurrentUpdateType)
            {
                case NewPersonalInfoHandler.UpdateType.Avatar:
                    {
                        db.UpdateAvatar
                        (
                            User.Avatar,
                            User.UserId
                        );
                        break;
                    }
                case NewPersonalInfoHandler.UpdateType.AvatarAndTextInfo:
                    {
                        db.UpdateExtraInformationAndAvatar
                        (
                            User.Avatar,
                            User.Address,
                            User.Name,
                            User.SecondName,
                            User.FatherName,
                            User.EMail,
                            User.PhoneNumber,
                            User.Organization,
                            User.UserId
                        );
                        break;
                    }
                case NewPersonalInfoHandler.UpdateType.Password:
                    {
                        db.UpdatePassword
                        (
                            User.Password,
                            User.UserId
                        );
                        break;
                    }
                default: return;
            }
        }

        #endregion

        #region Add new user

        /// <summary>
        ///     Add new user to db
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <param name="roomId">Room Id</param>
        /// <param name="fio">FIO of the user</param>
        public virtual List<object> AddNewUserAsync
        (
            string login,
            string password,
            int roomId,
            params string[] fio
        )
        {
            using (IInsert db = new QQContext())
            {
                return db.AddNewUser
                    (
                     login,
                     password,
                     roomId,
                     fio
                    );
            }
        }

        /// <summary>
        ///     Check user on exist
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Exist user</returns>
        public virtual bool IsUserExist
        (
            string login,
            string password
        )
        {
            using (ISelect db = new QQContext())
            {
                return db.IsUserExist
                (
                    login,
                    password
                );
            }
        }

        /// <summary>
        ///     Check user on exist
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">Password</param>
        /// <returns>Exist user</returns>
        public virtual bool IsUserExist
        (
            int userId,
            string password
        )
        {
            using (ISelect db = new QQContext())
            {
                return db.IsUserExist
                (
                    userId,
                    password
                );
            }
        }

        #endregion

        #region Delete user from db

        /// <summary>
        ///     Delete user from the db
        /// </summary>
        /// <param name="addressId">Address Id</param>
        public virtual void DeleteUserAsync
        (
            int addressId
        )
        {
            using (IRemove db = new QQContext())
            {
                db.RemoveUser
                    (
                     addressId
                    );
            }
        }

        #endregion

        #endregion

        #region IQBuilder

        #endregion

        #region IDataFunc

        public void ExportSensors
        (
            int roomIndex,
            string filePath
        ) {
            IExcelExport export = new DataManipulator();
            DataTable var = new DataTable();
            var.Columns.Add("1");
            var.Columns.Add("2");
            var.Columns.Add("3");
            var.Columns.Add("4");
            foreach(var sensor in User.Rooms[roomIndex].ListSensors) {
                var.Rows.Add
                    (
                     sensor.Name,
                     sensor.Fam,
                     sensor.State,
                     sensor.Data
                    );
            }
            export.ExportFile(filePath, User.Rooms[roomIndex].Name, var);
        }


        public CurrentProvider LoadSensors
        (
            int    roomIndex,
            string filePath
        ) {
            IExcelImport import = new DataManipulator();
            DataTable    var    = new DataTable();
            var.Columns.Add("1");
            var.Columns.Add("2");
            var.Columns.Add("3");
            var.Columns.Add("4");
            foreach (var sensor in User.Rooms[roomIndex].ListSensors)
            {
                var.Rows.Add
                    (
                     sensor.Name,
                     sensor.Fam,
                     sensor.State,
                     sensor.Data
                    );
            }

            var = import.InputFile
                (
                 filePath,
                 var
                );

            if (var != null) {
                for (var i = 0; i < var.Rows.Count; ++i)
                {
                    var currSett = User.Rooms[roomIndex].Settings;
                    var newSett  = "";
                    if ("Дверь входная" == var.Rows[i][0].ToString())
                    {
                        newSett = var.Rows[i][3].ToString();
                        newSett = string.Concat(newSett == "Закрыто" ?"0" :"1", currSett[1], currSett[2]);
                        SetNewSettings(newSett, User.Rooms[roomIndex].RoomId, 0);
                        UpdateStatusOfTheSensor(27, var.Rows[i][2].ToString());
                    }

                    if ("Дверь прмоежуточная" == var.Rows[i][0].ToString())
                    {
                        newSett = var.Rows[i][3].ToString();
                        newSett = string.Concat(currSett[0], newSett == "Закрыто" ? "0" : "1", currSett[2]);
                        SetNewSettings(newSett, User.Rooms[roomIndex].RoomId, 1);
                        UpdateStatusOfTheSensor(28, var.Rows[i][2].ToString());
                    }

                    if ("Окно" == var.Rows[i][0].ToString())
                    {
                        newSett = var.Rows[i][3].ToString();
                        newSett = string.Concat(currSett[0], currSett[1], newSett == "Закрыто" ? "0" : "1");
                        SetNewSettings(newSett, User.Rooms[roomIndex].RoomId, 2);
                        UpdateStatusOfTheSensor(29, var.Rows[i][2].ToString());
                    }
                }
            }

            return this;
        }

        #endregion
    }

    public static class Converter
    {
        #region ConvertorToWebim

        public static string ConvertToWexbim(string fileName, int roomId, bool createJson = false)
        {
            var wexBimFilename = Path.ChangeExtension(fileName, "wexbim");

            byte[] temp;
            using (ISelect select = new QQContext()) {
               temp = select.GetRoomModel
                    (
                     roomId
                    );
            }
            if (temp == null)
            {
                return null;
            }

            File.WriteAllBytes
                (
                 fileName,
                 temp
                );

            using (var model = IfcStore.Open(fileName))
            {
                var context = new Xbim3DModelContext(model);
                context.CreateContext();

                using (var wexBiMfile = File.Create(wexBimFilename))
                {
                    using (var wexBimBinaryWriter = new BinaryWriter(wexBiMfile))
                    {
                        model.SaveAsWexBim(wexBimBinaryWriter);
                        wexBimBinaryWriter.Close();
                    }
                    wexBiMfile.Close();
                }

                if (createJson) {
                    var cobieFileName = Path.ChangeExtension
                        (
                         fileName,
                         "json"
                        );
                    using (var cobieFile = new FileStream
                        (
                         cobieFileName,
                         FileMode.Create
                        )) {
                        var helper = new CoBieLiteHelper
                            (
                             model,
                             "UniClass"
                            );
                        var facility = helper.GetFacilities()
                                             .FirstOrDefault();
                        if (facility != null) {
                            using (var writer = new StreamWriter
                                (
                                 cobieFile
                                )) {
                                CoBieLiteHelper.WriteJson
                                    (
                                     writer,
                                     facility
                                    );
                                writer.Close();
                            }
                        }
                    }
                }
            }

            File.Delete(fileName);
            return wexBimFilename;
        }


        #endregion
    }
}