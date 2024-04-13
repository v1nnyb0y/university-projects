using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using Microsoft.Win32.SafeHandles;

namespace SmartHouse_Control.Requests
{
    /// <summary>
    ///     Class fot the qq.
    /// </summary>
    internal class qq : ISelect, IInsert, IRemove, IUpdate, IDisposable
    {
        //Builder for the connection to the dataBase
        private readonly SqlConnectionStringBuilder builder;

        //Initialize class
        public qq() {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "26.23.216.237";
            //builder.DataSource = "192.168.253.175";
            builder.UserID = "root";
            builder.Password = "icJ/Atp3q";
            builder.InitialCatalog = "SM_Control";
        }

        #region Get QBE

        /// <summary>
        ///     Do new select
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public DataTable do_select(string select) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    using (var command = new SqlCommand(select, connection)) {
                        using (var adapter = new SqlDataAdapter(command)) {
                            var dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(
                    "Ошибка в запросе",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return null;
        }

        #endregion

        #region Update sensor

        /// <summary>
        ///     Update status of the sensor
        /// </summary>
        /// <param name="id_sensor">Sensor id</param>
        /// <param name="new_status">New status</param>
        public void update_status(int id_sensor, string new_status) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection

                    var sb = new StringBuilder();
                    sb.Append("UPDATE Sensor ");
                    sb.Append("SET Status = @a1 ");
                    sb.Append($"WHERE ((ID = '{id_sensor}'))");

                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_status);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        #endregion

        #region Set new settings

        /// <summary>
        ///     Set new cfg
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="new_cfg">New cfg</param>
        public void update_cfg(int id, string new_cfg) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection

                    var sb = new StringBuilder();
                    sb.Append("UPDATE Files "); //Where update
                    sb.Append("SET is_current = @a1 "); //What update
                    sb.Append($"WHERE ((ID = '{id}'))");

                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", 0);
                    command.ExecuteNonQuery();

                    sb = new StringBuilder();
                    sb.Append("UPDATE Files ");
                    sb.Append("SET is_current = @a2 ");
                    sb.Append($"WHERE ((cfg = '{new_cfg}') AND (ID = '{id}'))");
                    sql_command = sb.ToString(); //Make a command
                    command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a2", 1);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Set new value to the time series
        /// </summary>
        /// <param name="id"></param>
        /// <param name="new_value"></param>
        public void set_new_value(int id, string new_value) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection

                    var sb = new StringBuilder();
                    sb.Append("INSERT Time_Series "); //Where update
                    sb.Append("VALUES (@a1, @a2, @a3, @a4) "); //What update

                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_value);
                    command.Parameters.AddWithValue("a2", DateTime.Now.TimeOfDay);
                    command.Parameters.AddWithValue("a3", DateTime.Now.Date);
                    command.Parameters.AddWithValue("a4", id);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        #endregion

        #region Get info for room menu

        /// <summary>
        ///     get room sensors
        /// </summary>
        /// <param name="id_room">Id room</param>
        /// <returns></returns>
        public List<object[]> get_room_sensors(int id_room) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("SELECT ID, ID_TYPE, Name, Status ");
                    sb.Append("FROM Sensor ");
                    sb.Append("WHERE ((ID_RoomModel = " +
                              "(SELECT ID " +
                              "FROM RoomModel " +
                              $"WHERE (ID_Room = {id_room}))))");

                    var grid = new List<object[]>(); //For grid
                    var ids = new List<int>(); //List for the id
                    var types_id = new List<int>(); //List for types 

                    var sql_command = sb.ToString(); //Make a command
                    using (var command = new SqlCommand(sql_command, connection)) {
                        //Perform command and read the result
                        using (var reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                var obj = new object[4];
                                obj[0] = reader.GetValue(2); //get name
                                obj[2] = reader.GetValue(3); //get status
                                ids.Add(reader.GetInt32(0)); //get id
                                types_id.Add(reader.GetInt32(1)); //get type id
                                grid.Add(obj);
                            }
                        }
                    }

                    var i = 0;
                    foreach (var id in types_id) grid[i++][1] = get_types(id, connection);

                    i = 0;
                    foreach (var id in ids) grid[i++][3] = get_value(id, connection);

                    return grid;
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return null;
        }

        /// <summary>
        ///     Get types
        /// </summary>
        /// <param name="id">Type id</param>
        /// <returns></returns>
        private string get_types(int id, SqlConnection connection) {
            var sb = new StringBuilder();
            sb.Append("SELECT Name ");
            sb.Append("FROM Sensor_Type ");
            sb.Append($"WHERE ((ID = '{id}'))");

            var sql_command = sb.ToString();
            using (var command = new SqlCommand(sql_command, connection)) {
                using (var reader = command.ExecuteReader()) {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
        }

        /// <summary>
        ///     Get last value
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private string get_value(int id, SqlConnection connection) {
            var sb = new StringBuilder();
            sb.Append("SELECT ID, ID_Quantity ");
            sb.Append("FROM [Option] ");
            sb.Append($"WHERE ((ID_Sensor = '{id}'))");


            var sql_command = sb.ToString();
            using (var command = new SqlCommand(sql_command, connection)) {
                var id_opt = 0;
                var id_quan = 0;
                using (var reader = command.ExecuteReader()) {
                    reader.Read();
                    id_opt = reader.GetInt32(0);
                    id_quan = reader.GetInt32(1);
                }

                return string.Concat(
                    get_real_value(id_opt, connection),
                    " ",
                    get_postfix(id_quan, connection)
                );
            }
        }

        /// <summary>
        ///     Get postfix
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private string get_postfix(int id, SqlConnection connection) {
            var sb = new StringBuilder();
            sb.Append("SELECT Postfix ");
            sb.Append("FROM Quantity ");
            sb.Append($"WHERE ((ID = '{id}'))");

            var sql_command = sb.ToString();
            using (var command = new SqlCommand(sql_command, connection)) {
                using (var reader = command.ExecuteReader()) {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
        }

        /// <summary>
        ///     Get real value
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private string get_real_value(int id, SqlConnection connection) {
            var sb = new StringBuilder();
            sb.Append("SELECT TOP 1 Value, Date, Time ");
            sb.Append("FROM Time_Series ");
            sb.Append($"WHERE ((ID_Option = '{id}')) ");
            sb.Append("ORDER BY Date, Time DESC");

            var sql_command = sb.ToString();
            using (var command = new SqlCommand(sql_command, connection)) {
                using (var reader = command.ExecuteReader()) {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
        }

        /// <summary>
        ///     Get room model
        /// </summary>
        /// <param name="id_room">Id room</param>
        /// <param name="settings">settings</param>
        /// <returns></returns>
        public string get_model(int id_room) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("SELECT fileData ");
                    sb.Append("FROM Files ");
                    sb.Append("WHERE ((ID = " +
                              "(SELECT ID_Model " +
                              "FROM RoomModel " +
                              $"WHERE ((ID_Room = '{id_room}')))) AND ");
                    sb.Append($"(is_current = '{1}'))");

                    var file_path_obj = "room_507.ifc";
                    byte[] buffer = null;
                    var sql_command = sb.ToString();

                    using (var command = new SqlCommand(sql_command, connection)) {
                        using (var reader = command.ExecuteReader()) {
                            if (reader.HasRows) {
                                reader.Read();
                                buffer = (byte[]) reader.GetValue(0);
                            }
                        }
                    }

                    if (buffer == null) return "";

                    try {
                        File.WriteAllBytes("room_507.ifc", buffer);
                    }
                    catch { }

                    return file_path_obj;
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return null;
        }

        /// <summary>
        ///     Get room model
        /// </summary>
        /// <param name="id_room">Id room</param>
        /// <param name="settings">settings</param>
        /// <returns></returns>
        public Tuple<string, string> get_curr_model(int id_room) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("SELECT fileData, cfg ");
                    sb.Append("FROM Files ");
                    sb.Append("WHERE ((ID = " +
                              "(SELECT ID_Model " +
                              "FROM RoomModel " +
                              $"WHERE ((ID_Room = '{id_room}')))) AND ");
                    sb.Append($"(is_current = '{1}'))");

                    var file_path_obj = "room_507_1.ifc";
                    byte[] buffer = null;
                    string settings = null;
                    var sql_command = sb.ToString();

                    using (var command = new SqlCommand(sql_command, connection)) {
                        using (var reader = command.ExecuteReader()) {
                            if (reader.HasRows) {
                                reader.Read();
                                buffer = (byte[]) reader.GetValue(0);
                                settings = reader.GetString(1);
                            }
                        }
                    }

                    if (buffer == null) return null;

                    try {
                        File.WriteAllBytes("room_507_1.ifc", buffer);
                    }
                    catch { }

                    return new Tuple<string, string>(settings, file_path_obj);
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return null;
        }

        #endregion

        #region Getting Start Information

        /// <summary>
        ///     Check access to the application
        /// </summary>
        /// <param name="login">Entered login</param>
        /// <param name="password">Entered password</param>
        /// <returns></returns>
        public object[] is_access(string login, string password) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb_user = new StringBuilder();
                    var sb_person = new StringBuilder();
                    sb_user.Append("OPEN SYMMETRIC KEY password_key DECRYPTION BY CERTIFICATE password_e;");
                    sb_user.Append("SELECT [User].[ID]," +
                                   " [User].[Organisation]," +
                                   " [User].[_Function]," +
                                   " CONVERT(varchar, DecryptByKey(Password_Encrypted, 1, HashBytes('SHA1', CONVERT(varbinary, [User].[ID]))))," +
                                   " [User].[Avatar]," +
                                   " [User].[ID_Person] ");
                    sb_user.Append("FROM [User] ");
                    sb_user.Append($"WHERE (([User].[Login] = '{login}') AND ");
                    sb_user.Append(
                        $"(CONVERT(varchar, DecryptByKey(Password_Encrypted, 1, HashBytes('SHA1', CONVERT(varbinary, [User].[ID])))) = '{password}'))");

                    var user_arr = new object[6];
                    var sql_command = sb_user.ToString(); //Make a command
                    using (var command = new SqlCommand(sql_command, connection)) {
                        //Perform command and read the result
                        using (var reader = command.ExecuteReader()) {
                            if (reader.Read() && reader.HasRows) reader.GetValues(user_arr);
                        }
                    }

                    if (user_arr[0] == null)
                        return null;

                    sb_person.Append("SELECT [Person].[ID]," +
                                     " [Person].[Name]," +
                                     " [Person].[Second_Name]," +
                                     " [Person].[Last_Name]," +
                                     " [Person].[Contact_Info]," +
                                     " [Person].[Mail]," +
                                     " [Person].[ID_Address] ");
                    sb_person.Append("FROM Person ");
                    sb_person.Append($"WHERE ((ID = '{user_arr[user_arr.Length - 1]}'))");
                    sql_command = sb_person.ToString();
                    var person_arr = new object[7];

                    using (var command = new SqlCommand(sql_command, connection)) {
                        //Perform command and read the result
                        using (var reader = command.ExecuteReader()) {
                            if (reader.Read() && reader.HasRows) reader.GetValues(person_arr);
                        }
                    }

                    var arr = new object[12];
                    Array.Resize(ref user_arr, user_arr.Length - 1);
                    for (var i = 0; i < user_arr.Length; ++i) arr[i] = user_arr[i];

                    for (var i = 0; i < person_arr.Length; ++i) arr[i + 5] = person_arr[i];

                    return arr;
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return null;
        }

        /// <summary>
        ///     Get the address
        /// </summary>
        /// <param name="id_address">Address ID</param>
        /// <returns></returns>
        public object[] get_address(int id_address) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("SELECT [Address].[Name] "); //What SELECT
                    sb.Append("FROM Address "); //From that table    
                    sb.Append($"WHERE (([Address].[ID] = '{id_address}'));"); //The if clause
                    var sql_command = sb.ToString(); //Make a command
                    using (var command = new SqlCommand(sql_command, connection)) {
                        //Perform command and read the result
                        using (var reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                var r_arr = new object[1];
                                reader.GetValues(r_arr);
                                return r_arr;
                            }
                        }
                    }
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return null;
        }

        /// <summary>
        ///     Get access
        /// </summary>
        /// <param name="id_user">User ID</param>
        /// <returns></returns>
        public List<object[]> get_access(int id_user) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb_names = new StringBuilder();
                    var sb_access = new StringBuilder();

                    sb_names.Append("SELECT [Room].[ID], [Room].[Name] "); //What SELECT
                    sb_access.Append("SELECT [Access].[Access_LVL] ");

                    sb_names.Append("FROM Room "); //From that table  
                    sb_access.Append("FROM Access ");

                    //if clause
                    sb_names.Append("WHERE (([Room].[ID] IN " +
                                    "(SELECT ID_Room " +
                                    "FROM UserRoom " +
                                    $"WHERE (ID_User = '{id_user}'))))");

                    sb_access.Append("WHERE (([Access].[ID] IN " +
                                     "(SELECT ID " +
                                     "FROM UserRoom " +
                                     $"WHERE (ID_User = '{id_user}'))))");

                    var sql_command_ac = sb_access.ToString(); //Make a command
                    var sql_command_nm = sb_names.ToString();

                    using (var command_nm = new SqlCommand(sql_command_nm, connection)) {
                        using (var command_ac = new SqlCommand(sql_command_ac, connection)) {
                            List<object> accesses = new List<object>(),
                                names = new List<object>(),
                                ids = new List<object>();

                            //Perform command and read the result
                            using (var reader_ac = command_ac.ExecuteReader()) {
                                while (reader_ac.Read()) accesses.Add(reader_ac.GetValue(0));
                            }

                            //Perform command and read the result
                            using (var reader_nm = command_nm.ExecuteReader()) {
                                while (reader_nm.Read())
                                    if (reader_nm.HasRows) {
                                        ids.Add(reader_nm.GetValue(0));
                                        names.Add(reader_nm.GetValue(1));
                                    }
                            }

                            var outp = new List<object[]>();

                            for (var i = 0; i < names.Count; ++i) {
                                object[] concat =
                                    {
                                        ids[i],
                                        names[i],
                                        accesses[i],
                                        get_cfg(get_id(ids[i], connection), connection)
                                    };

                                outp.Add(concat);
                            }

                            return outp;
                        }
                    }
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return null;
        }

        private object get_id(object obj, SqlConnection connection) {
            var sb = new StringBuilder();
            sb.Append("SELECT ID_Model ");
            sb.Append("FROM RoomModel ");
            sb.Append($"WHERE ((ID = '{obj}'))");

            var sql_command = sb.ToString();

            using (var command = new SqlCommand(sql_command, connection)) {
                using (var reader = command.ExecuteReader()) {
                    if (reader.Read() && reader.HasRows)
                        return reader.GetValue(0);
                    return null;
                }
            }
        }

        /// <summary>
        ///     Get settings of the model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private object get_cfg(object id, SqlConnection connection) {
            if (id == null)
                return "";
            var sb = new StringBuilder();
            sb.Append("SELECT cfg ");
            sb.Append("FROM Files ");
            sb.Append($"WHERE ((ID = '{id}') AND (is_current = '1'))");

            var sql_command = sb.ToString();

            using (var command = new SqlCommand(sql_command, connection)) {
                using (var reader = command.ExecuteReader()) {
                    reader.Read();
                    return reader.GetValue(0);
                }
            }
        }

        #endregion

        #region Update Information for Personal page

        /// <summary>
        ///     Update name
        /// </summary>
        /// <param name="new_name">New name</param>
        /// <param name="id_person">Person ID</param>
        public void update_name(string new_name, int id_person) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("UPDATE Person "); //Where update
                    sb.Append("SET Name = @a1 "); //What update
                    sb.Append($"WHERE [ID] = '{id_person}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_name);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Update second name
        /// </summary>
        /// <param name="new_second_name">New second name</param>
        /// <param name="id_person">Person ID</param>
        public void update_second_name(string new_second_name, int id_person) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("UPDATE Person "); //Where update
                    sb.Append("SET Second_Name = @a1 "); //What update
                    sb.Append($"WHERE [ID] = '{id_person}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_second_name);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Update last name
        /// </summary>
        /// <param name="new_last_name">New last name</param>
        /// <param name="id_person">Person ID</param>
        public void update_last_name(string new_last_name, int id_person) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("UPDATE Person "); //Where update
                    sb.Append("SET Last_Name = @a1 "); //What update
                    sb.Append($"WHERE [ID] = '{id_person}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_last_name);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Update address
        /// </summary>
        /// <param name="new_address">New address</param>
        /// <param name="id_address">Address ID</param>
        public void update_address(string new_address, int id_address) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("UPDATE Address "); //Where update
                    sb.Append("SET Name = @a1 "); //What update
                    sb.Append($"WHERE [ID] = '{id_address}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_address);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Update phone number
        /// </summary>
        /// <param name="new_phone">New phone number</param>
        /// <param name="id_person">Person ID</param>
        public void update_phone(string new_phone, int id_person) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("UPDATE Person "); //Where update
                    sb.Append("SET Contact_Info = @a1 "); //What update
                    sb.Append($"WHERE [ID] = '{id_person}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_phone);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Update mail
        /// </summary>
        /// <param name="new_mail">New e-mail</param>
        /// <param name="id_person">Person ID</param>
        public void update_mail(string new_mail, int id_person) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("UPDATE Person "); //Where update
                    sb.Append("SET Mail = @a1 "); //What update
                    sb.Append($"WHERE [ID] = '{id_person}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_mail);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Update organisation
        /// </summary>
        /// <param name="new_organisation">New organisation</param>
        /// <param name="id_user">User ID</param>
        public void update_organisation(string new_organisation, int id_user) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("UPDATE [User] "); //Where update
                    sb.Append("SET Organisation = @a1 "); //What update
                    sb.Append($"WHERE [ID] = '{id_user}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_organisation);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Update function
        /// </summary>
        /// <param name="new_function">New function</param>
        /// <param name="id_user">User ID</param>
        public void update_function(string new_function, int id_user) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("UPDATE [User] "); //Where update
                    sb.Append("SET _Fuction = @a1 "); //What update
                    sb.Append($"WHERE [ID] = '{id_user}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_function);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Update avatar
        /// </summary>
        /// <param name="new_avatar">New avatar</param>
        /// <param name="id_user">User ID</param>
        public void update_avatar(object new_avatar, int id_user) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("UPDATE [User] "); //Where update
                    sb.Append("SET Avatar = @a1 "); //What update
                    sb.Append($"WHERE [ID] = '{id_user}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    command.Parameters.AddWithValue("a1", new_avatar);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Update password
        /// </summary>
        /// <param name="new_password">New password</param>
        /// <param name="id_user">User ID</param>
        public void update_password(string new_password, int id_user) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("OPEN SYMMETRIC KEY password_key DECRYPTION BY CERTIFICATE password_e;");
                    sb.Append("UPDATE [User] "); //Where update
                    sb.Append(
                        $"SET Password_Encrypted = EncryptByKey(Key_GUID('password_key'), '{new_password}' , 1, HashBytes('SHA1', CONVERT(varbinary, [User].[ID])))"); //What update
                    sb.Append($"WHERE [ID] = '{id_user}'");
                    var sql_command = sb.ToString(); //Make a command
                    var command = new SqlCommand(sql_command, connection);
                    //command.Parameters.AddWithValue("a1", $"");

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        #endregion

        #region Getting info for change

        /// <summary>
        ///     Get all users in the room
        /// </summary>
        /// <param name="id_room">ID room</param>
        /// <returns></returns>
        public DataTable get_all_users(int id_room) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection

                    var outp = new DataTable();
                    var indexes = get_ids_users_access(id_room, connection);
                    outp.Columns.Add("ФИО");
                    outp.Columns.Add("Уровень доступа");
                    foreach (var index in indexes) {
                        var arr = fio_access(
                            index[0],
                            person_id(index[1], connection),
                            connection);

                        if (arr[1] == "Администратор") continue;

                        outp.Rows.Add(arr);
                    }

                    return outp;
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return null;
        }

        #region Extra selects for the get_all_users

        /// <summary>
        ///     Get users of the
        /// </summary>
        /// <param name="id_room"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private List<int[]> get_ids_users_access(int id_room, SqlConnection connection) {
            var sb = new StringBuilder();
            var outp = new List<int[]>();

            sb.Append("SELECT ID, ID_User ");
            sb.Append("FROM UserRoom ");
            sb.Append($"WHERE ((ID_Room = '{id_room}'))");

            var sql_command = sb.ToString(); //Make a command
            using (var command = new SqlCommand(sql_command, connection)) {
                //Perform command and read the result
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        int[] temp =
                            {
                                reader.GetInt32(0),
                                reader.GetInt32(1)
                            };
                        outp.Add(temp);
                    }
                }
            }

            return outp;
        }

        /// <summary>
        ///     Get person id
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        private int person_id(int id_user, SqlConnection connection) {
            var sb = new StringBuilder();

            sb.Append("SELECT ID_Person ");
            sb.Append("FROM [User] ");
            sb.Append($"WHERE ((ID = '{id_user}'))");

            var sql_command = sb.ToString(); //Make a command
            using (var command = new SqlCommand(sql_command, connection)) {
                //Perform command and read the result
                using (var reader = command.ExecuteReader()) {
                    reader.Read();
                    return reader.GetInt32(0);
                }
            }
        }

        /// <summary>
        ///     Get dataTable line
        /// </summary>
        /// <param name="id_access">Access id</param>
        /// <param name="id_person">Person id</param>
        /// <returns></returns>
        private string[] fio_access(int id_access, int id_person, SqlConnection connection) {
            var sb = new StringBuilder();

            sb.Append("SELECT [Person].[Second_Name] + ' ' + [Person].[Name] + ' ' + [Person].[Last_Name] ");
            sb.Append("FROM Person ");
            sb.Append($"WHERE ((ID = '{id_person}'))");

            var outp = new string[2];

            var sql_command = sb.ToString(); //Make a command
            using (var command = new SqlCommand(sql_command, connection)) {
                //Perform command and read the result
                using (var reader = command.ExecuteReader()) {
                    reader.Read();
                    outp[0] = reader.GetString(0);
                }
            }

            sb = new StringBuilder();

            sb.Append("SELECT [Access].[Access_LVL] ");
            sb.Append("FROM Access ");
            sb.Append($"WHERE ((ID = '{id_access}'))");

            sql_command = sb.ToString(); //Make a command
            using (var command = new SqlCommand(sql_command, connection)) {
                //Perform command and read the result
                using (var reader = command.ExecuteReader()) {
                    reader.Read();
                    outp[1] = reader.GetString(0);
                }
            }

            return outp;
        }

        #endregion

        #endregion

        #region Update access info

        /// <summary>
        ///     Update accesses
        /// </summary>
        /// <param name="accesses">New accesses</param>
        public void update_accesses(string[] accesses, int id_room) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var id_access = get_accesses(id_room, connection);

                    var sb = new StringBuilder();
                    var i = 0;
                    foreach (var id in id_access) {
                        sb.Append("UPDATE Access "); //Where update
                        sb.Append("SET Access_LVL = @a1 "); //What update
                        sb.Append($"WHERE (ID = '{id}')");
                        var sql_command = sb.ToString(); //Make a command
                        var command = new SqlCommand(sql_command, connection);
                        command.Parameters.AddWithValue("a1", accesses[i]);
                        i++;

                        command.ExecuteNonQuery();
                        command.Cancel();
                        sb.Clear();
                    }
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Get users
        /// </summary>
        /// <param name="id_room"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private int[] get_accesses(int id_room, SqlConnection connection) {
            var sb = new StringBuilder();
            var outp = new List<int>();

            sb.Append("SELECT ID ");
            sb.Append("FROM UserRoom ");
            sb.Append($"WHERE ((ID_Room = '{id_room}'))");


            var sql_command = sb.ToString(); //Make a command
            using (var command = new SqlCommand(sql_command, connection)) {
                //Perform command and read the result
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) outp.Add(reader.GetInt32(0));
                }
            }


            var real_outp = new List<int>();
            foreach (var item in outp) {
                sb.Clear();
                sb.Append("SELECT ID ");
                sb.Append("FROM Access ");
                sb.Append($"WHERE NOT (Access_LVL = 'Администратор') AND (ID_UserRoom = {item})");
                using (var command = new SqlCommand(sb.ToString(), connection)) {
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) real_outp.Add(reader.GetInt32(0));
                    }
                }
            }


            return real_outp.ToArray();
        }

        #endregion

        #region Get Conrol and data menu info

        /// <summary>
        ///     Get groups
        /// </summary>
        public string[] get_sensor_group() {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection

                    var sb = new StringBuilder();
                    sb.Append("SELECT Name ");
                    sb.Append("FROM Sensor_Type");

                    var sql_command = sb.ToString(); //Make a command
                    using (var command = new SqlCommand(sql_command, connection)) {
                        using (var reader = command.ExecuteReader()) {
                            var str_arr = new List<string>();
                            while (reader.Read()) str_arr.Add(reader.GetString(0));

                            return str_arr.ToArray();
                        }
                    }
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return null;
        }

        /// <summary>
        ///     Get controllers
        /// </summary>
        /// <param name="id_room">Room id</param>
        /// <returns></returns>
        public List<object[]> get_room_sensors_controller(int id_room) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection

                    var grid = new List<object[]>(); //For grid
                    var ids = new List<int>(); //List for the id
                    var types_id = new List<int>(); //List for types 
                    var things = get_things(id_room, connection);

                    foreach (var thing in things) {
                        var sb = new StringBuilder();
                        sb.Append("SELECT ID, ID_TYPE, Name, Status ");
                        sb.Append("FROM Sensor ");
                        sb.Append($"WHERE ((ID_Thing = '{thing}'))");

                        var sql_command = sb.ToString(); //Make a command
                        using (var command = new SqlCommand(sql_command, connection)) {
                            //Perform command and read the result
                            using (var reader = command.ExecuteReader()) {
                                while (reader.Read()) {
                                    var obj = new object[5];
                                    obj[0] = reader.GetValue(0);
                                    obj[1] = reader.GetValue(2); //get name
                                    obj[3] = reader.GetValue(3); //get status
                                    ids.Add(reader.GetInt32(0)); //get id
                                    types_id.Add(reader.GetInt32(1)); //get type id
                                    grid.Add(obj);
                                }
                            }
                        }
                    }

                    var i = 0;
                    foreach (var id in types_id) grid[i++][2] = get_types(id, connection);

                    i = 0;
                    foreach (var id in ids) {
                        var str = get_value_control(id, connection);
                        if (str != null) grid[i][4] = str;

                        i++;
                    }

                    return grid;
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return null;
        }

        /// <summary>
        ///     Get last value
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private string get_value_control(int id, SqlConnection connection) {
            var sb = new StringBuilder();
            sb.Append("SELECT ID, ID_Quantity ");
            sb.Append("FROM [Option] ");
            sb.Append($"WHERE ((ID_Sensor = '{id}') AND ([Option].[Type_Option] = 'Состояние'))");

            var sql_command = sb.ToString();
            using (var command = new SqlCommand(sql_command, connection)) {
                var id_opt = 0;
                var id_quan = 0;
                using (var reader = command.ExecuteReader()) {
                    if (reader.HasRows && reader.Read()) {
                        id_opt = reader.GetInt32(0);
                        id_quan = reader.GetInt32(1);
                    }
                    else {
                        return null;
                    }
                }

                return string.Concat(
                    get_real_value(id_opt, connection),
                    get_postfix(id_quan, connection)
                );
            }
        }

        /// <summary>
        ///     Get things from the room
        /// </summary>
        /// <param name="id_room">Room id</param>
        /// <returns></returns>
        private List<object> get_things(int id_room, SqlConnection connection) {
            var sb = new StringBuilder();
            sb.Append("SELECT ID ");
            sb.Append("FROM Thing ");
            sb.Append("WHERE ((ID_RoomModel = " +
                      "(SELECT ID " +
                      "FROM RoomModel " +
                      $"WHERE (ID_Room = {id_room}))))");

            var sql_command = sb.ToString();
            using (var command = new SqlCommand(sql_command, connection)) {
                using (var reader = command.ExecuteReader()) {
                    var list = new List<object>();
                    while (reader.Read()) list.Add(reader.GetValue(0));

                    return list;
                }
            }
        }

        #endregion

        #region Insert new user

        /// <summary>
        ///     Insert password and login
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        public void insert_user(string login, string password, int id_user) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("OPEN SYMMETRIC KEY password_key DECRYPTION BY CERTIFICATE password_e;");
                    sb.Append("UPDATE [User] "); //Where update
                    sb.Append(
                        $"SET Password_Encrypted = EncryptByKey(Key_GUID('password_key'), '{password}' , 1, HashBytes('SHA1', CONVERT(varbinary, [User].[ID]))), Login = '{login}', _Function = 'Пользователь' "); //What update
                    sb.Append($"WHERE [ID] = '{id_user}'");
                    var sql_command = new SqlCommand(sb.ToString(), connection);
                    sql_command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Create address
        /// </summary>
        /// <returns>id of the address</returns>
        public int new_address() {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("INSERT INTO [Address] "); //Insert into
                    sb.Append("VALUES ('')"); //what insert

                    var sql_command = new SqlCommand(sb.ToString(), connection);
                    sql_command.ExecuteNonQuery();
                    sql_command.Cancel();

                    sb.Clear(); //new qq
                    sb.Append("SELECT TOP 1 ID "); //what select
                    sb.Append("FROM [Address] "); //From where
                    sb.Append("ORDER BY ID DESC"); //order 

                    using (sql_command = new SqlCommand(sb.ToString(), connection)) {
                        using (var reader = sql_command.ExecuteReader()) {
                            reader.Read();
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return -1;
        }

        /// <summary>
        ///     Create new person and user (user because i need id to encrypt password)
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="second_name">Second name</param>
        /// <param name="last_name">Last name</param>
        /// <param name="id_address">Address id</param>
        /// <returns>id_user</returns>
        public int new_person_user(string name, string second_name, string last_name, int id_address) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection

                    #region Insert person

                    var sb = new StringBuilder();
                    sb.Append("INSERT INTO Person ([Name], Second_Name, Last_Name, ID_Address)"); //Insert into
                    sb.Append("VALUES (@a1, @a2, @a3, @a4)"); //What insert

                    var sql_command = new SqlCommand(sb.ToString(), connection);
                    sql_command.Parameters.AddWithValue("a1", name);
                    sql_command.Parameters.AddWithValue("a2", second_name);
                    sql_command.Parameters.AddWithValue("a3", last_name);
                    sql_command.Parameters.AddWithValue("a4", id_address);

                    sql_command.ExecuteNonQuery();
                    sql_command.Cancel();
                    sb.Clear();

                    #endregion

                    #region IDentify person id

                    var id_person = -1;

                    sb.Append("SELECT TOP 1 ID "); //what select
                    sb.Append("FROM Person "); //from where
                    sb.Append("ORDER BY ID DESC"); //order 
                    using (sql_command = new SqlCommand(sb.ToString(), connection)) {
                        using (var reader = sql_command.ExecuteReader()) {
                            reader.Read();
                            id_person = reader.GetInt32(0);
                        }
                    }

                    #endregion

                    #region Insert user

                    sb.Clear();
                    sb.Append("INSERT INTO [User] (ID_Person) "); //What insert
                    sb.Append("VALUES (@a1)");

                    sql_command = new SqlCommand(sb.ToString(), connection);
                    sql_command.Parameters.AddWithValue("a1", id_person);
                    sql_command.ExecuteNonQuery();
                    sql_command.Cancel();
                    sb.Clear();

                    #endregion

                    #region Select user id

                    sb.Append("SELECT TOP 1 ID "); //what select
                    sb.Append("FROM [User] "); //from where
                    sb.Append("ORDER BY ID DESC"); //order

                    using (sql_command = new SqlCommand(sb.ToString(), connection)) {
                        using (var reader = sql_command.ExecuteReader()) {
                            reader.Read();
                            return reader.GetInt32(0);
                        }
                    }

                    #endregion
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return -1;
        }

        /// <summary>
        ///     Insert new string to roommodel
        /// </summary>
        /// <param name="user_id">user id</param>
        /// <param name="room_id">room id</param>
        /// <returns></returns>
        public int insert_new_user_to_room(int user_id, int room_id) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("INSERT INTO UserRoom (ID_User, ID_Room) "); //Insert into
                    sb.Append("VALUES (@a1, @a2)"); //what insert

                    var sql_command = new SqlCommand(sb.ToString(), connection);
                    sql_command.Parameters.AddWithValue("a1", user_id);
                    sql_command.Parameters.AddWithValue("a2", room_id);
                    sql_command.ExecuteNonQuery();
                    sql_command.Cancel();

                    sb.Clear(); //new qq
                    sb.Append("SELECT TOP 1 ID "); //what select
                    sb.Append("FROM UserRoom "); //From where
                    sb.Append("ORDER BY ID DESC"); //order 

                    using (sql_command = new SqlCommand(sb.ToString(), connection)) {
                        using (var reader = sql_command.ExecuteReader()) {
                            reader.Read();
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return -1;
        }

        /// <summary>
        ///     Insert new access
        /// </summary>
        /// <param name="room_model_id"></param>
        /// <returns></returns>
        public void insert_new_access(int room_model_id) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb = new StringBuilder();
                    sb.Append("INSERT INTO Access (Access_LVL, ID_UserRoom) "); //Insert into
                    sb.Append("VALUES (@a1, @a2)"); //what insert

                    var sql_command = new SqlCommand(sb.ToString(), connection);
                    sql_command.Parameters.AddWithValue("a1", "Отсутствует доступ");
                    sql_command.Parameters.AddWithValue("a2", room_model_id);
                    sql_command.ExecuteNonQuery();
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Is user exist
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public bool is_existed(string login, string password) {
            try {
                using (var connection = new SqlConnection(builder.ConnectionString)) {
                    connection.Open(); //Open connection
                    var sb_user = new StringBuilder();
                    sb_user.Append("OPEN SYMMETRIC KEY password_key DECRYPTION BY CERTIFICATE password_e;");
                    sb_user.Append("SELECT [User].[ID] ");
                    sb_user.Append("FROM [User] ");
                    sb_user.Append($"WHERE (([User].[Login] = '{login}') AND ");
                    sb_user.Append(
                        $"(CONVERT(varchar, DecryptByKey(Password_Encrypted, 1, HashBytes('SHA1', CONVERT(varbinary, [User].[ID])))) = '{password}'))");

                    var sql_command = sb_user.ToString(); //Make a command
                    using (var command = new SqlCommand(sql_command, connection)) {
                        //Perform command and read the result
                        using (var reader = command.ExecuteReader()) {
                            return reader.HasRows;
                        }
                    }
                }
            }
            catch (SqlException exception) {
                MessageBox.Show(exception.ToString());
            }

            return false;
        }

        #endregion

        #region Dispose Region

        // Flag: Has Dispose already been called?
        private bool disposed;

        // Instantiate a SafeHandle instance.
        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing) {
            if (disposed)
                return;

            if (disposing) handle.Dispose();

            disposed = true;
        }

        #endregion
    }
}