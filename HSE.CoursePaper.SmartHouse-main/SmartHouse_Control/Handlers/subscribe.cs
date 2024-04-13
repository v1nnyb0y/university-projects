using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using SmartHouse_Control.Output;
using SmartHouse_Control.Requests;
using SmartHouse_Control.Session;

namespace SmartHouse_Control.Handlers
{
    /// <summary>
    ///     Class with subscription functions
    /// </summary>
    internal abstract class subscribe
    {
        private static readonly ISelect select = new qq(); //Remember server connection (select)
        private static readonly IUpdate update = new qq(); //Remember server connection (update)
        private static readonly IInsert insert = new qq(); //Remember server connection (insert)
        private static readonly IExcel excel_parser = new excel_using(); //Output

        #region Output

        /// <summary>
        ///     Output excel file
        /// </summary>
        /// <param name="room_name"></param>
        /// <param name="dt"></param>
        public static void output_excel_format_one(string room_name, DataTable dt) {
            excel_parser.output_type_one(room_name, dt);
        }

        #endregion

        #region Input

        /// <summary>
        ///     Input settings of the room
        /// </summary>
        /// <returns></returns>
        public static DataTable load_room_settings(DataTable dt) {
            return excel_parser.input_file(dt);
        }

        #endregion

        #region Insert

        /// <summary>
        ///     New user
        /// </summary>
        /// <param name="rooms_id">Rooms id</param>
        /// <param name="second_name">Second name</param>
        /// <param name="name">Name</param>
        /// <param name="last_name">Last name</param>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        public static bool add_new_user(List<int> rooms_id, string second_name, string name, string last_name,
            string login, string password) {
            var ok = select.is_existed(login, password);
            if (ok) return false;

            var id_address = insert.new_address();
            var new_user = insert.new_person_user(name, second_name, last_name, id_address);
            update.insert_user(login, password, new_user);
            foreach (var room_id in rooms_id) {
                var room_model_id = insert.insert_new_user_to_room(new_user, room_id);
                insert.insert_new_access(room_model_id);
            }

            return true;
        }

        #endregion

        #region Update

        #region Update person info

        /// <summary>
        ///     Update info about person
        /// </summary>
        /// <param name="args">Person info</param>
        public static void update_info(object source, personal_handler args) {
            switch (args.info_type) {
                case personal_handler.Type.Address: {
                    update.update_address(args.address, (source as user).get_address_id);
                    break;
                }
                case personal_handler.Type.Function: {
                    update.update_function(args.function, (source as user).get_user_id);
                    break;
                }
                case personal_handler.Type.Last_Name: {
                    update.update_last_name(args.last_name, (source as user).get_person_id);
                    break;
                }
                case personal_handler.Type.Mail: {
                    update.update_mail(args.mail, (source as user).get_person_id);
                    break;
                }
                case personal_handler.Type.Name: {
                    update.update_name(args.name, (source as user).get_person_id);
                    break;
                }
                case personal_handler.Type.Password: {
                    update.update_password(args.password, (source as user).get_user_id);
                    break;
                }
                case personal_handler.Type.Phone_number: {
                    update.update_phone(args.phone_number, (source as user).get_person_id);
                    break;
                }
                case personal_handler.Type.Work: {
                    update.update_organisation(args.work, (source as user).get_user_id);
                    break;
                }
                case personal_handler.Type.Second_Name: {
                    update.update_second_name(args.second_name, (source as user).get_person_id);
                    break;
                }
                default: {
                    update.update_avatar(args.avatar, (source as user).get_user_id);
                    break;
                }
            }
        }

        #endregion

        #region Update access info

        /// <summary>
        ///     Update accesses
        /// </summary>
        /// <param name="accesses">New accesses</param>
        public static void update_accesses(string[] accesses, int id_room) {
            update.update_accesses(accesses, id_room);
        }

        #endregion

        #region Update cfg

        /// <summary>
        ///     Set new params
        /// </summary>
        /// <param name="id"></param>
        /// <param name="new_cfg"></param>
        /// <param name="ids"></param>
        public static void new_settings(int id, string new_cfg, params int[] ids) {
            update.update_cfg(ids[0], new_cfg);
            switch (ids[1]) {
                case 0:
                    insert.set_new_value(19, new_cfg[0] == '1' ? "Открыто" : "Закрыто");
                    break;
                case 1:
                    insert.set_new_value(20, new_cfg[1] == '1' ? "Открыто" : "Закрыто");
                    break;
                case 2:
                    insert.set_new_value(21, new_cfg[2] == '1' ? "Открыто" : "Закрыто");
                    break;
            }
        }

        #endregion

        #region Update Status

        /// <summary>
        ///     Update status
        /// </summary>
        /// <param name="id_sensor">Sensor id</param>
        /// <param name="new_status">New status</param>
        public static void update_status(int id_sensor, string new_status) {
            update.update_status(id_sensor, new_status);
        }

        #endregion

        #endregion

        #region Select

        /// <summary>
        ///     Do new select
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public static DataTable generated_select(string select) {
            return subscribe.select.do_select(select);
        }

        #region Get control and data info

        /// <summary>
        ///     Get data for the groups of sensors
        /// </summary>
        /// <returns></returns>
        public static string[] get_groups() {
            return select.get_sensor_group();
        }

        /// <summary>
        ///     Get grid for the control and data menu
        /// </summary>
        /// <param name="id_room">Room id</param>
        /// <returns></returns>
        public static DataTable get_grid_room(int id_room, params string[] arr) {
            var grid = select.get_room_sensors_controller(id_room);

            var dt = new DataTable();
            dt.Columns.Add("IDS");
            dt.Columns.Add("Наименование");
            dt.Columns.Add("Семейство");
            dt.Columns.Add("Статус");
            dt.Columns.Add("Последнее значение");

            if (arr[1] != "Открытие") return dt;


            if ("Все" == arr[0]) {
                foreach (var row in grid)
                    if (row[0] != null &&
                        row[1] != null &&
                        row[2] != null &&
                        row[3] != null &&
                        row[4] != null)
                        dt.Rows.Add(row);

                return dt;
            }

            if ("Выключенные" == arr[0]) {
                foreach (var row in grid)
                    if (row[0] != null &&
                        row[1] != null &&
                        row[2] != null &&
                        row[3] != null &&
                        row[4] != null &&
                        row[3] as string == "Выключен")
                        dt.Rows.Add(row);

                return dt;
            }

            if ("Включенные" == arr[0]) {
                foreach (var row in grid)
                    if (row[0] != null &&
                        row[1] != null &&
                        row[2] != null &&
                        row[3] != null &&
                        row[4] != null &&
                        row[3] as string == "Включен")
                        dt.Rows.Add(row);
                return dt;
            }

            return null;
        }

        #endregion

        #region Getting start info

        /// <summary>
        ///     Function to give access to the application
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Tuple<Visibility, bool, user> check_login_password(access_handler args) {
            #region Get start info about user

            var info_user = select.is_access(args.login, args.password); //Check login and password
            if (info_user is null)
                return new Tuple<Visibility, bool, user>(Visibility.Visible, false, null); //Wrong password or login

            user user_obj;

            var info_address = select.get_address(int.Parse(info_user[11].ToString())); //Find address

            user_obj = new user(int.Parse(info_user[0].ToString()),
                info_user[1].ToString(),
                info_user[2].ToString(),
                info_user[3].ToString(),
                info_user[4] as byte[],
                int.Parse(info_user[5].ToString()),
                info_user[6].ToString(),
                info_user[7].ToString(),
                info_user[8].ToString(),
                info_user[9].ToString(),
                info_user[10].ToString(),
                info_address[0].ToString(),
                int.Parse(info_user[11].ToString()));

            #endregion

            user_obj.set_rooms(select.get_access(user_obj.get_user_id));

            return new Tuple<Visibility, bool, user>(Visibility.Hidden, true, user_obj);
        }

        #endregion

        #region Load model info

        /// <summary>
        ///     Load all users
        /// </summary>
        public static DataTable load_all_users(int id_room) {
            return select.get_all_users(id_room);
        }

        #endregion

        #region Get model

        /// <summary>
        ///     get model from db
        /// </summary>
        /// <param name="id_room">Id room</param>
        /// <returns></returns>
        public static string find_model(int id_room, params string[] settings) {
            return select.get_model(id_room);
        }

        /// <summary>
        ///     Get sensors info
        /// </summary>
        /// <param name="id_room">Room id</param>
        /// <returns></returns>
        public static DataTable load_room_sensors(int id_room, params string[] settings) {
            var grid = select.get_room_sensors(id_room);

            if (grid.Count == 0) return null;

            var dt = new DataTable();
            dt.Columns.Add("Наименование");
            dt.Columns.Add("Семейство");
            dt.Columns.Add("Статус");
            dt.Columns.Add("Последнее значение");

            foreach (var row in grid) dt.Rows.Add(row);

            return dt;
        }

        #endregion

        #region Thread info

        /// <summary>
        ///     Room id
        /// </summary>
        /// <param name="id_room">Room id</param>
        /// <returns></returns>
        public static Tuple<string, string> get_setts(int id_room) {
            return select.get_curr_model(id_room);
        }

        #endregion

        #endregion
    }
}