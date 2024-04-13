using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Provider;
using Provider.CurrentSession;
using Provider.Handlers;
using SmartHouse_Control_Web.Models;

namespace SmartHouse_Control_Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index() {
            if (Session["CurrUsr"] == null) {
                Redirect
                    (
                     "/Home"
                    );
                return RedirectToAction("MasterPagePartial", "Home");
            }

            return View
                (
                 (Session["CurrUsr"] as CurrentProvider)?.User
                );
        }

        public ActionResult Edit() {
            if (Session["CurrUsr"] != null)
                return View
                    (
                     (Session["CurrUsr"] as CurrentProvider)?.User
                    );

            Redirect
                (
                 "/Home"
                );
            return RedirectToAction("MasterPagePartial", "Home");

        }

        [HttpPost]
        public async Task<ActionResult> Edit
        (
            HttpPostedFileBase uploadImage,
            User newUser,
            string bttn
        ) {
            if (!ModelState.IsValid) {
                newUser.Avatar = (Session["CurrUsr"] as CurrentProvider)?.User.Avatar;
                return View("Edit", newUser);
            }

            switch (bttn) {
                case "Назад":
                    {
                        return RedirectToAction
                            (
                             "Index"
                            );
                    }

                case "Добавить":
                    {
                        return await UpdateAvatar(uploadImage);
                    }

                default:
                    {
                        return await UpdateUserInfo(newUser);
                    }
            }
        }

        public async Task<ActionResult> UpdateAvatar(HttpPostedFileBase uploadImage)
        {
            if (uploadImage == null)
                return View
                    (
                     (Session["CurrUsr"] as CurrentProvider)?.User
                    );

            var expansion = uploadImage.FileName.Split
                (
                 '.'
                );
            if (expansion[expansion.Length - 1] != "png" &&
                expansion[expansion.Length - 1] != "jpg")
            {
                return View
                    (
                     (Session["CurrUsr"] as CurrentProvider)?.User
                    );
            }

            byte[] imageData;
            using (var binaryReader = new BinaryReader
                (
                 uploadImage.InputStream
                ))
            {
                imageData = binaryReader.ReadBytes
                    (
                     uploadImage.ContentLength
                    );
            }

                        ((CurrentProvider)Session["CurrUsr"]).User.Avatar = imageData;

            await Task.Run
                (
                 () =>
                 {
                     (Session["CurrUsr"] as CurrentProvider)?.UpdateAuthorizedHuman
                         (
                          new NewPersonalInfoHandler
                              (
                               NewPersonalInfoHandler.UpdateType.Avatar
                              )
                         );
                 }
                );
            return View
                (
                 (Session["CurrUsr"] as CurrentProvider)?.User
                );
        }

        public async Task<ActionResult> UpdateUserInfo(User newUser)
        {
            if (newUser.PersonId == 0)
            {
                return View
                    (
                     (Session["CurrUsr"] as CurrentProvider)?.User
                    );
            }

            if (newUser.Password != (Session["CurrUsr"] as CurrentProvider)?.User.Password)
            {
                ((CurrentProvider)Session["CurrUsr"]).User.Password = newUser.Password;
                await Task.Run
                    (
                     () =>
                     {
                         ((CurrentProvider)Session["CurrUsr"]).UpdateAuthorizedHuman
                             (
                              new NewPersonalInfoHandler
                                  (
                                   NewPersonalInfoHandler.UpdateType.Password
                                  )
                             );
                     }
                    );
            }

            if (newUser.Avatar != null)
            {
                ((CurrentProvider)Session["CurrUsr"]).User.Avatar = newUser.Avatar;
            }

                        ((CurrentProvider)Session["CurrUsr"]).User.Name = newUser.Name;
            ((CurrentProvider)Session["CurrUsr"]).User.SecondName = newUser.SecondName;
            ((CurrentProvider)Session["CurrUsr"]).User.FatherName = newUser.FatherName;
            ((CurrentProvider)Session["CurrUsr"]).User.Organization = newUser.Organization;
            ((CurrentProvider)Session["CurrUsr"]).User.Address = newUser.Address;
            ((CurrentProvider)Session["CurrUsr"]).User.EMail = newUser.EMail;
            ((CurrentProvider)Session["CurrUsr"]).User.PhoneNumber = newUser.PhoneNumber;

            await Task.Run
                (
                 () =>
                 {
                     ((CurrentProvider)Session["CurrUsr"]).UpdateAuthorizedHuman
                         (
                          new NewPersonalInfoHandler
                              (
                               NewPersonalInfoHandler.UpdateType.AvatarAndTextInfo
                              )
                         );
                 }
                );

            return RedirectToAction
                (
                 "Index"
                );
        }

        public ActionResult EditAccess
        (
            object userId,
            object roomId, 
            string Bttn,
            Room.SmallUser user
        ) {
            if (Session["CurrUsr"] == null)
            {
                Redirect
                    (
                     "/Home"
                    );
                return RedirectToAction("MasterPagePartial", "Home");
            }

            userId = int.Parse
                (
                 ((string[])userId)[0]
                );
            roomId = int.Parse
                (
                 ((string[])roomId)[0]
                );

            if (Bttn != null) {
                ((CurrentProvider) Session["CurrUsr"]).User.Rooms[(int) roomId]
                                                      .ListUsersInRoom[(int) userId]
                                                      .Access = user.Access;
                List<object[]> coll = new List<object[]>();
                ((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId].ListUsersInRoom.ForEach(userInRoom => coll.Add(new object[]
                                                                                                                           {
                                                                                                                               userInRoom.UserId,
                                                                                                                               userInRoom.Access
                                                                                                                           }));
                ((CurrentProvider)Session["CurrUsr"]).UpdateAccessesAsync(coll);

                return RedirectToAction
                    (
                     "Index",
                     "RoomView",
                     new
                         {
                             roomId
                         }
                    );
            }

            

            return View(((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId].ListUsersInRoom[(int)userId]);
        }

        public ActionResult DeleteUser
        (
            object userId,
            object roomId
        ) {
            if (Session["CurrUsr"] == null)
            {
                Redirect
                    (
                     "/Home"
                    );
                return RedirectToAction("MasterPagePartial", "Home");
            }

            userId = int.Parse
                (
                 ((string[]) userId)[0]
                );
            roomId = int.Parse
                (
                 ((string[])roomId)[0]
                );


            ((CurrentProvider)Session["CurrUsr"]).DeleteUserAsync(((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId].ListUsersInRoom[(int)userId].AddressId);
            ((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int)roomId].ListUsersInRoom.RemoveAt((int)userId);
            return RedirectToAction
                (
                 "Index",
                 "RoomView",
                 new
                     {
                         roomId
                     }
                );
        }

        public ActionResult Register
        (
            object roomId,
            string Bttn,
            NewUser newUser
        ) {
            if (Session["CurrUsr"] == null)
            {
                Redirect
                    (
                     "/Home"
                    );
                return RedirectToAction("MasterPagePartial", "Home");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Неудачная попытка создания пользователя.");
                return View(newUser);
            }

            roomId = int.Parse
                (
                 ((string[])roomId)[0]
                );

            if (Bttn != null) {
                bool isUserExist = ((CurrentProvider) Session["CurrUsr"]).IsUserExist
                    (
                     newUser.Login,
                     newUser.Password
                    );

                if (isUserExist) {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует.");
                    return View(newUser);
                }

                _ = ((CurrentProvider)Session["CurrUsr"]).AddNewUserAsync
                    (
                     login: newUser.Login,
                     password: newUser.Password,
                     roomId: ((CurrentProvider)Session["CurrUsr"]).User.Rooms[(int) roomId].RoomId,
                     newUser.SecondName,
                     newUser.Name
                    );

                return RedirectToAction
                    (
                     "Index",
                     "RoomView",
                     new
                         {
                             roomId
                         }
                    );
            }

            newUser.RoomId = (int) roomId;

            return View(newUser);
        }
    }
}