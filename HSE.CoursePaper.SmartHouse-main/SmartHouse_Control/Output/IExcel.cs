using System.Data;

namespace SmartHouse_Control.Output
{
    /// <summary>
    ///     Interface for the output to excel
    /// </summary>
    internal interface IExcel
    {
        /// <summary>
        ///     Output to the excel file
        /// </summary>
        /// <param name="dt">Data table</param>
        /// <param name="room_name">Room name</param>
        void output_type_one(string room_name, DataTable dt);

        /// <summary>
        ///     Input excel file
        /// </summary>
        /// <returns></returns>
        DataTable input_file(DataTable dt);
    }
}