using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Provider;
using Provider.CurrentSession;

namespace SmartHouse_Control_Web.Controllers
{
    public class RoomDataController : Controller
    {
        public ActionResult Index(object roomId, Room newRoomSensors, string load)
        {
            if (Session["CurrUsr"] == null)
            {
                Redirect
                    (
                     "/Home"
                    );
                return RedirectToAction
                    (
                     "MasterPagePartial",
                     "Home"
                    );
            }

            roomId = int.Parse
                (
                 ((string[]) roomId)[0]
                );

            if (newRoomSensors != null) {
                roomId = newRoomSensors.RoomId;
            }

            ((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId]
                                                 .Index = (int)roomId;

            if (newRoomSensors != null) {
                if (newRoomSensors.Filters != null &&
                    load                   == null) {
                    ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                                                          .Filters = newRoomSensors.Filters;
                }
                else {
                    ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                                                          .Filters = new Room.Filter()
                                                                         {
                                                                             Connection  = true,
                                                                             Gas         = true,
                                                                             Lightning   = true,
                                                                             Movement    = true,
                                                                             Open        = true,
                                                                             Presser     = true,
                                                                             Rain        = true,
                                                                             Temperature = true,
                                                                             Time        = true,
                                                                             Turning     = "Все",
                                                                             Visual      = true

                                                                         };
                }
            }
            else {
                ((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId]
                                                     .Filters = new Room.Filter()
                                                                    {
                                                                        Connection  = true,
                                                                        Gas         = true,
                                                                        Lightning   = true,
                                                                        Movement    = true,
                                                                        Open        = true,
                                                                        Presser     = true,
                                                                        Rain        = true,
                                                                        Temperature = true,
                                                                        Time        = true,
                                                                        Turning     = "Все",
                                                                        Visual      = true

                                                                    };
            }

            Session["CurrUsr"] = ((CurrentProvider) Session["CurrUsr"]).GetManipulatedData
                    (
                     (int) roomId
                    );

                return View
                (
                 ((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId]
                );
        }

        public ActionResult ChangeValue
        (
            string name,
            string sensorId,
            string oldValue,
            string roomId
        ) {
            if (Session["CurrUsr"] == null)
            {
                Redirect
                    (
                     "/Home"
                    );
                return RedirectToAction
                    (
                     "MasterPagePartial",
                     "Home"
                    );
            }

            oldValue = oldValue.Replace
                (
                 " ",
                 ""
                );
            int type = -1;
            string newSettings = null;
            switch (name) {
                case "Дверь входная":
                    {
                        newSettings = String.Concat
                            (
                             oldValue == "Открыто"
                                 ? "0"
                                 : "1",
                             ((CurrentProvider) Session["CurrUsr"]).User.Rooms[int.Parse
                                                                                   (
                                                                                    roomId
                                                                                   )]
                                                                   .Settings[1],
                             ((CurrentProvider) Session["CurrUsr"]).User.Rooms[int.Parse
                                                                                   (
                                                                                    roomId
                                                                                   )]
                                                                   .Settings[2]
                            );

                        ((CurrentProvider) Session["CurrUsr"]).User.Rooms[int.Parse
                                                                              (
                                                                               roomId
                                                                              )]
                                                              .Settings = newSettings;
                        type = 0;
                        break;
                    }

                case "Дверь прмоежуточная":
                    {
                        newSettings = String.Concat
                            (
                             ((CurrentProvider)Session["CurrUsr"]).User.Rooms[int.Parse
                                                                                  (
                                                                                   roomId
                                                                                  )]
                                                                  .Settings[0],
                             oldValue == "Открыто"
                                 ? "0"
                                 : "1"
                            ,
                             ((CurrentProvider)Session["CurrUsr"]).User.Rooms[int.Parse
                                                                                  (
                                                                                   roomId
                                                                                  )]
                                                                  .Settings[2]
                            );

                        ((CurrentProvider)Session["CurrUsr"]).User.Rooms[int.Parse
                                                                             (
                                                                              roomId
                                                                             )]
                                                             .Settings = newSettings;
                        type = 1;
                        break;
                    }

                case "Окно":
                    {
                        newSettings = String.Concat
                            (
                             ((CurrentProvider)Session["CurrUsr"]).User.Rooms[int.Parse
                                                                                  (
                                                                                   roomId
                                                                                  )]
                                                                  .Settings[0],
                             ((CurrentProvider)Session["CurrUsr"]).User.Rooms[int.Parse
                                                                                  (
                                                                                   roomId
                                                                                  )]
                                                                  .Settings[1],
                             oldValue == "Открыто"
                                 ? "0"
                                 : "1"
                            );

                        ((CurrentProvider)Session["CurrUsr"]).User.Rooms[int.Parse
                                                                             (
                                                                              roomId
                                                                             )]
                                                             .Settings = newSettings;
                        type = 2;
                        break;
                    }
            }

            ((CurrentProvider) Session["CurrUsr"]).SetNewSettings
                (
                 newSettings,
                 ((CurrentProvider) Session["CurrUsr"]).User.Rooms[int.Parse
                                                                       (
                                                                        roomId
                                                                       )]
                                                       .RoomId,
                 type
                );

            return RedirectToAction
                (
                 "Index",
                 new
                     {
                         roomId
                     }
                );
        }

        public ActionResult TurnOn
        (
            string sensorId,
            bool turnOn,
            string roomId
        ) {
            if (Session["CurrUsr"] == null)
            {
                Redirect
                    (
                     "/Home"
                    );
                return RedirectToAction
                    (
                     "MasterPagePartial",
                     "Home"
                    );
            }

            int sensorIndex = ((CurrentProvider) Session["CurrUsr"]).User.Rooms[int.Parse
                                                                                    (
                                                                                     roomId
                                                                                    )]
                                                                    .ListSensors[int.Parse
                                                                                     (
                                                                                      sensorId
                                                                                     )]
                                                                    .SensorId;

            ((CurrentProvider) Session["CurrUsr"]).UpdateStatusOfTheSensor
                (
                 sensorIndex,
                 turnOn
                     ? "Включен"
                     : "Выключен"
                );

            return RedirectToAction
                (
                 "Index",
                 new
                     {
                         roomId
                     }
                );
        }

        public FilePathResult GetFile
        (
            object roomId
        )
        {
            roomId = int.Parse
                (
                 ((string[])roomId)[0]
                );

            string filePath = Server.MapPath
                (
                 $"~/excelData/excel_{Session.SessionID}_export_{(int)roomId}.xlsx"
                );
            string fileType = "application/xlsx";
            string fileName = $"ExportSensors_{roomId}.xlsx";

            ((CurrentProvider)Session["CurrUsr"]).ExportSensors((int)roomId, filePath);

            return File
                (
                 filePath,
                 fileType,
                 fileName
                );
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload, object roomId)
        {
            if (Session["CurrUsr"] == null)
            {
                Redirect
                    (
                     "/Home"
                    );
                return RedirectToAction
                    (
                     "MasterPagePartial",
                     "Home"
                    );
            }

            roomId = int.Parse
                (
                 ((string[])roomId)[0]
                );
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/excelData/" + fileName));

                if (Path.GetExtension(fileName) == ".xlsx") {
                    Session["CurrUsr"] = ((CurrentProvider) Session["CurrUsr"]).LoadSensors
                        (
                         (int) roomId,
                         Server.MapPath("~/excelData/" + fileName)
                        );
                }
            }

            
            return RedirectToAction("Index", new {roomId, load = "1" });
        }
    }
}