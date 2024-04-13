using System.Web.Http.Cors;
using System.Web.Mvc;
using Provider;

namespace SmartHouse_Control_Web.Controllers
{
    [EnableCors
    (
        "http://localhost:60799",
        "*",
        "*"
    )]
    public class RoomViewController : Controller
    {
        public ActionResult Index
        (
            object roomId
        ) {
            if (Session["CurrUsr"] == null) {
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

            roomId = int.Parse(((string[]) roomId)[0]);

            ((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId]
                                                 .Index = (int)roomId;

            string path = Converter.ConvertToWexbim
                (
                 Server.MapPath
                     (
                      $"~/data/room_{Session.SessionID}_{((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId].RoomId}.ifc"
                     ),
                 ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                                                       .RoomId,
                 createJson: ((CurrentProvider)Session["CurrUsr"]).User.Access == "Администратор"
                );

            if (path != null) {
                ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                                                      .FileData =
                    $"room_{Session.SessionID}_{((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId].RoomId}.wexbim";

                ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                                                      .FileJson =
                    $"room_{Session.SessionID}_{((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId].RoomId}.json";
            }
            else {
                ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                                                      .FileData = null;
                ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                                                      .FileJson = null;
            }

            if (((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId].Access == "Полный доступ")
            {
                Session["CurrUsr"] = ((CurrentProvider)Session["CurrUsr"]).GetRoomSensors
                    (
                     (int)roomId,
                     ((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId]
                                                          .RoomId
                    );
            }

            if (((CurrentProvider) Session["CurrUsr"]).User.Access == "Администратор") {
                Session["CurrUsr"] = ((CurrentProvider) Session["CurrUsr"]).GetUsersInRoom
                    (
                     (int) roomId,
                     ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                                                           .RoomId
                    );
            }

            return View
                (
                 ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                );
        }

        public FilePathResult GetFile
        (
            object roomId
        ) {
            roomId = int.Parse
                (
                 ((string[])roomId)[0]
                );

            string filePath = Server.MapPath
                (
                 $"~/excelData/excel_{Session.SessionID}_export_{(int) roomId}.xlsx"
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
    }
}