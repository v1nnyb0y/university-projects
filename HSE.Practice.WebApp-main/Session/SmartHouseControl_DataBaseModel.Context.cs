using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Requests.Interfaces;

namespace Requests
{
    public class QQContext : DbContext,
        ISelect,
        IUpdate,
        IInsert,
        IRemove
    {
        #region Initialize

        public QQContext()
            : base
            (
                "name=QQContext"
            )
        {
        }

        #endregion

        #region IInsert functions

        #region Add new user

        /// <summary>
        ///     Add new user
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <param name="roomId">Room Id</param>
        /// <param name="fio">FIO of the added user</param>
        public virtual List<object> AddNewUser
        (
            string login,
            string password,
            int roomId,
            params string[] fio
        )
        {
            var newAccess = new Access
            {
                Access_LVL = "Отсутствует доступ",
                UserRoom = new UserRoom
                {
                    Room = (from room in Rooms
                            where room.ID == roomId
                            select room).First(),
                    User = new User
                    {
                        Login = login,
                        Person = new Person
                        {
                            Name = fio[1],
                            Second_Name = fio[0],
                            Address = new Address
                            {
                                Name = ""
                            }
                        }
                    }
                }
            };


            Accesses.Add
            (
                newAccess
            );

            SaveChanges();
            var newUser = (from user in Users
                           orderby user.ID descending
                           select user).First();
            newUser.Password_Encrypted = EncryptByKey
            (
                password,
                newUser.ID
            );
            SaveChanges();

            return new List<object>
            {
                newAccess.ID,
                newUser.Person.ID_Address
            };
        }

        public void InsertNewValue
        (
            int optionId,
            string newValue
        )
        {
            Time_Series.Add
                (
                 new Time_Series()
                 {
                     Date = DateTime.Now.Date,
                     Time = DateTime.Now.TimeOfDay,
                     Value = newValue,
                     ID_Option = optionId
                 }
                );
            SaveChanges();
        }

        #endregion

        #endregion

        #region IRemove functions

        #region Remove user

        /// <summary>
        ///     remove user by cascade method
        /// </summary>
        /// <param name="addressId">Address Id</param>
        public virtual void RemoveUser
        (
            int addressId
        )
        {
            var addressRemoved = (from address in Addresses
                                  where address.ID == addressId
                                  select address).First();
            Addresses.Remove
            (
                addressRemoved
            );
            SaveChanges();
        }

        #endregion

        #endregion

        #region Fields

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Quantity> Quantities { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomModel> RoomModels { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }
        public virtual DbSet<Sensor_Type> Sensor_Type { get; set; }
        public virtual DbSet<Thing> Things { get; set; }
        public virtual DbSet<Thing_Type> Thing_Type { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRoom> UserRooms { get; set; }
        public virtual DbSet<Access> Accesses { get; set; }
        public virtual DbSet<Time_Series> Time_Series { get; set; }

        #endregion

        #region DataBase functions

        [DbFunction
        (
            "SM_Control_DataBaseModel.Store",
            "DecryptByKey"
        )]
        private protected virtual string DecryptByKey
        (
            byte[] password,
            int userId
        )
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            var parameters = new List<ObjectParameter>
            {
                new ObjectParameter
                (
                    "password",
                    password
                ),
                new ObjectParameter
                (
                    "userId",
                    userId
                )
            };

            return objectContext.CreateQuery<string>
                (
                    @"SM_Control_DataBaseModel.Store.DecryptByKey(@password, @userId)",
                    parameters.ToArray()
                )
                .ExecuteAsync
                (
                    MergeOption.NoTracking
                )
                .Result.FirstOrDefault();
        }


        private protected virtual byte[] EncryptByKey
        (
            string password,
            int userId
        )
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            var parameters = new List<ObjectParameter>
            {
                new ObjectParameter
                (
                    "password",
                    password
                ),
                new ObjectParameter
                (
                    "userId",
                    userId
                )
            };

            return objectContext.ExecuteFunction<byte[]>
                (
                    "EncryptByKey",
                    parameters.ToArray()
                )
                .FirstOrDefault();
        }

        #endregion

        #region ISelect functions

        #region Authentication

        /// <summary>
        ///     Check access level
        /// </summary>
        /// <param name="login">Checking login</param>
        /// <param name="password">Checking password</param>
        /// <returns>List of the objects (fields of the Person; User; Room)</returns>
        public virtual List<object>[] IsAccess
        (
            string login,
            string password
        )
        {
            var userId = CheckLoginAndPasswordAsync
                (
                    login,
                    password
                )
                .Result;

            if (userId == 0) return null;

            var personData = GetDataAsync
                (
                    userId,
                    password
                )
                .Result;
            var roomData = GetDataAsync
                (
                    userId
                )
                .Result;

            return new[]
            {
                personData,
                roomData
            };
        }

        #region Async Func

        /// <summary>
        ///     Is there some access to the application
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Bool; UserId</returns>
        private protected virtual async Task<int> CheckLoginAndPasswordAsync
        (
            string login,
            string password
        )
        {
            return await Task.Run
                (
                    () =>
                    {
                        var userIndex = (from user
                                in Users
                                         where user.Login == login &&
                                               DecryptByKey
                                               (
                                                   user.Password_Encrypted,
                                                   user.ID
                                               ) ==
                                               password
                                         select user.ID).FirstOrDefault();
                        return userIndex;
                    }
                )
                .ConfigureAwait
                (
                    false
                );
        }

        /// <summary>
        ///     Get Person's fields
        /// </summary>
        /// <param name="userId">User's Id</param>
        /// <param name="password">Password of user</param>
        /// <returns>List of the fields of the person data</returns>
        private protected virtual async Task<List<object>> GetDataAsync
        (
            int userId,
            string password
        )
        {
            return await Task.Run
                (
                    () =>
                    {
                        var result = new List<object>();
                        (from user in Users
                         join person in People
                             on user.ID_Person equals person.ID
                         join address in Addresses on person.ID_Address equals address.ID
                         where user.ID == userId
                         select new
                         {
                             UserId = user.ID,
                             user.Organisation,
                             user.C_Function,
                             password,
                             user.Avatar,
                             PersonId = person.ID,
                             person.Name,
                             person.Second_Name,
                             person.Last_Name,
                             person.Contact_Info,
                             person.Mail,
                             Address = address.Name,
                             address.ID
                         }
                            ).ToList().ForEach
                            (
                                info => result.AddRange
                                (
                                    new List<object>
                                    {
                                        info.UserId,
                                        info.Organisation,
                                        info.C_Function,
                                        info.password,
                                        info.Avatar,
                                        info.PersonId,
                                        info.Name,
                                        info.Second_Name,
                                        info.Last_Name,
                                        info.Contact_Info,
                                        info.Mail,
                                        info.Address,
                                        info.ID
                                    }
                                )
                            );
                        return result;
                    }
                )
                .ConfigureAwait
                (
                    false
                );
        }

        /// <summary>
        ///     Get Room's info
        /// </summary>
        /// <param name="userId">User's id</param>
        /// <returns>List of the Fields of the Room Access</returns>
        private async Task<List<dynamic>> GetDataAsync
        (
            int userId
        )
        {
            return await Task.Run
                (
                    () =>
                    {
                        var result = new List<object>();
                        (from user in Users
                         join connection in UserRooms on user.ID equals connection
                             .ID_User
                         join access in Accesses on connection.ID equals access
                             .ID_UserRoom
                         join room in Rooms on connection.ID_Room equals room.ID
                         join model in RoomModels on room.ID equals model.ID_Room
                         join file in Files on model.ID_Model equals file.ID
                         where user.ID == userId && file.is_current == 1
                         select new
                         {
                             room.Name,
                             access.Access_LVL,
                             room.ID,
                             file.cfg
                         }).ToList().ForEach
                        (
                            info => result.Add
                            (
                                new List<object>
                                {
                                    info.Name,
                                    info.Access_LVL,
                                    info.ID,
                                    info.cfg
                                }
                            )
                        );

                        return result;
                    }
                )
                .ConfigureAwait
                (
                    false
                );
        }

        #endregion

        #endregion

        #region Get Rooms Info

        #region Rooms and Users

        /// <summary>
        ///     Get users in one room with accesses
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns>List of the users and access</returns>
        public virtual List<object[]> GetUsersInRoom
        (
            int roomId
        )
        {
            var collection = new List<object[]>();
            (from room in Rooms
             join connection in UserRooms on room.ID equals connection.ID_Room
             join user in Users on connection.ID_User equals user.ID
             join person in People on user.ID_Person equals person.ID
             join access in Accesses on connection.ID equals access.ID_UserRoom
             join address in Addresses on person.ID_Address equals address.ID
             where room.ID == roomId && access.Access_LVL != "Администратор"
             select new
             {
                 addressId = address.ID,
                 access.ID,
                 person.Second_Name,
                 person.Name,
                 person.Last_Name,
                 access.Access_LVL
             }).ForEachAsync
                (
                    userAccess => collection.Add
                    (
                        new object[]
                        {
                            userAccess.addressId,
                            userAccess.ID,
                            $"{userAccess.Second_Name} {userAccess.Name} {userAccess.Last_Name}",
                            userAccess.Access_LVL
                        }
                    )
                )
                .Wait();

            return collection;
        }

        #endregion

        #region Get room Model

        /// <summary>
        /// Get room model
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns></returns>
        public byte[] GetRoomModel
        (
            int roomId
        )
        {
            var result = (from model in RoomModels
                          join file in Files on model.ID_Model equals file.ID
                          where file.is_current == 1 && model.ID_Room == roomId
                          select file.fileData).First();

            return result;
        }

        /// <summary>
        /// Get controllers in the room
        /// </summary>
        /// <param name="roomId">Room Id</param>
        /// <returns></returns>
        public List<object[]> GetRoomSensors
        (
            int roomId
        )
        {
            List<object[]> collection = new List<object[]>();
            (from roomModel in RoomModels
             join thing in Things on roomModel.ID equals thing.ID_RoomModel
             join sensor in Sensors on thing.ID equals sensor.ID_Thing
             join sensorType in Sensor_Type on sensor.ID_TYPE equals sensorType.ID
             join option in Options on sensor.ID equals option.ID_Sensor
             let realValue = Time_Series.Where
                                         (
                                          timeSeries => timeSeries.ID_Option == option.ID
                                         )
                                        .OrderByDescending
                                             (
                                              timeSeries => timeSeries.Date
                                             )
                                        .ThenByDescending
                                             (
                                              timeSeries => timeSeries.Time
                                             )
                                        .FirstOrDefault()
             join postfix in Quantities on option.ID_Quantity equals postfix.ID
             where roomModel.ID_Room == roomId
             select new
             {
                 sensor.Name,
                 Type = sensorType.Name,
                 sensor.Status,
                 Value = realValue.Value + " " + postfix.Postfix
             }).ForEachAsync
                (
                 s => collection.Add
                     (
                      new object[]
                          {
                              s.Name,
                              s.Type,
                              s.Status,
                              s.Value
                          }
                     )
                ).Wait();

            return collection;
        }

        public List<object[]> GetManipulatedRooms
        (
            int roomId
        )
        {
            List<object[]> collection = new List<object[]>();
            (from roomModel in RoomModels
             join thing in Things on roomModel.ID equals thing.ID_RoomModel
             join sensor in Sensors on thing.ID equals sensor.ID_Thing
             join sensorType in Sensor_Type on sensor.ID_TYPE equals sensorType.ID
             join option in Options on sensor.ID equals option.ID_Sensor
             let realValue = Time_Series.Where
                                         (
                                          timeSeries => timeSeries.ID_Option == option.ID
                                         )
                                        .OrderByDescending
                                             (
                                              timeSeries => timeSeries.Date
                                             )
                                        .ThenByDescending
                                             (
                                              timeSeries => timeSeries.Time
                                             )
                                        .FirstOrDefault()
             join postfix in Quantities on option.ID_Quantity equals postfix.ID
             where roomModel.ID_Room == roomId && option.Type_Option == "Состояние"
             select new
             {
                 sensor.Name,
                 Type = sensorType.Name,
                 sensor.Status,
                 Value = realValue.Value + " " + postfix.Postfix,
                 sensor.ID,
                 OptionId = option.ID
             }).ForEachAsync
                (
                 s => collection.Add
                     (
                      new object[]
                          {
                              s.Name,
                              s.Type,
                              s.Status,
                              s.Value,
                              s.ID,
                              s.OptionId
                          }
                     )
                ).Wait();

            return collection;
        }

        #endregion

        #endregion

        #region Add new user

        /// <summary>
        ///     Check user exist
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Exist user?</returns>
        public virtual bool IsUserExist
        (
            string login,
            string password
        )
        {
            var userId = CheckLoginAndPasswordAsync
            (
                login,
                password
            ).Result;
            return userId != 0;
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
            var currentUser = (from user in Users
                               where user.ID == userId
                               select user).First();
            return CheckLoginAndPasswordAsync
                       (
                           currentUser.Login,
                           password
                       )
                       .Result !=
                   0;
        }

        #endregion

        #endregion

        #region IUpdate functions

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
        public virtual async void UpdateExtraInformationAndAvatar
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
        )
        {
            await Task.Run
            (
                () =>
                {
                    var updatingUser = (from user in Users
                                        where user.ID == userId
                                        select user).First();
                    updatingUser.Person.Address.Name = address;
                    updatingUser.Person.Name = name;
                    updatingUser.Person.Second_Name = secondName;
                    updatingUser.Person.Last_Name = fatherName;
                    updatingUser.Person.Mail = eMail;
                    updatingUser.Person.Contact_Info = phoneNumber;
                    updatingUser.Organisation = organization;
                    updatingUser.Avatar = (byte[])avatar;
                    SaveChanges();
                    Dispose();
                }
            );
        }

        /// <summary>
        ///     Update password
        /// </summary>
        /// <param name="newPassword">New password</param>
        /// <param name="userId">User's ID</param>
        public virtual async void UpdatePassword
        (
            string newPassword,
            int userId
        )
        {
            await Task.Run
            (
                () =>
                {
                    var updatingUser = (from user in Users
                                        where user.ID == userId
                                        select user).First();

                    updatingUser.Password_Encrypted = EncryptByKey
                    (
                        newPassword,
                        userId
                    );
                    SaveChanges();
                    Dispose();
                }
            );
        }

        /// <summary>
        ///     Update avatar
        /// </summary>
        /// <param name="newAvatar">Byte[] newAvatar</param>
        /// <param name="userId">User's ID</param>
        public virtual async void UpdateAvatar
        (
            object newAvatar,
            int userId
        )
        {
            await Task.Run
            (
                () =>
                {
                    var updatingUser = (from user in Users
                                        where user.ID == userId
                                        select user).First();
                    updatingUser.Avatar = (byte[])newAvatar;
                    SaveChanges();
                    Dispose();
                }
            );
        }

        public void UpdateStatusSensor
        (
            int sensorId,
            string newStatus
        )
        {
            var sensorUpd = (from sensor in Sensors
                             where sensor.ID == sensorId
                             select sensor).First();
            sensorUpd.Status = newStatus;
            SaveChanges();
        }

        public void UpdateConfig
        (
            int roomId,
            string newCfg
        )
        {
            var files = (from file in Files
                         where file.ID == roomId
                         select file);

            foreach (var file in files)
            {
                file.is_current = file.cfg == newCfg
                                      ? 1
                                      : 0;
            }

            SaveChanges();
        }

        #endregion

        #region Room update functions

        #region Accesses

        /// <summary>
        ///     Create new access
        /// </summary>
        public virtual void UpdateAccesses
        (
            List<object[]> accessesList
        )
        {
            accessesList.ForEach(
                accessListElement =>
                {
                    var indexOfAccess = (int)accessListElement[0];
                    var accessForChanging = (from access in Accesses
                                             where access.ID == indexOfAccess
                                             select access).First();
                    accessForChanging.Access_LVL = (string)accessListElement[1];
                });
            SaveChanges();
        }

        #endregion

        #endregion

        #endregion
    }
}