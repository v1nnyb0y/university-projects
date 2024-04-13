using System.Collections.Generic;
using SmartHouse_Control.Session;

namespace SmartHouse_Control.InfoS
{
    /// <summary>
    ///     Interface for room
    /// </summary>
    internal interface IRoom
    {
        List<room> rooms { get; set; }

        int active_index { get; set; }
    }
}