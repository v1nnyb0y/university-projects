using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Provider.CurrentSession
{
    /// <summary>
    ///     Class for the current User
    /// </summary>
    public class User : Person
    {
        #region Fields

        [DataType(DataType.Text)]
        [Display(Name = "Организация")]
        public string Organization { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Аватар")]
        public byte[] Avatar { get; set; }

        [Display(Name = "Доступные помещения")]
        public List<Room> Rooms { get; set; }

        [Display(Name = "Глобальный доступ")]
        public string Access { get; }

        [Display(Name = "Идентификатор")]
        public int UserId { get; }

        #endregion

        #region Initialize 

        /// <summary>
        ///     Initialize class (make full copy)
        /// </summary>
        /// <param name="copiedUser">Source code</param>
        public User
        (
            User copiedUser
        )
            : base
            (
                copiedUser.Name,
                copiedUser.SecondName,
                copiedUser.FatherName,
                copiedUser.PhoneNumber,
                copiedUser.EMail,
                copiedUser.Address,
                copiedUser.AddressId,
                copiedUser.PersonId
            )
        {
            UserId = copiedUser.UserId;
            Organization = copiedUser.Organization;
            Password = copiedUser.Password;
            Avatar = copiedUser.Avatar;
            Access = copiedUser.Access;
            Rooms = copiedUser.Rooms;
        }

        /// <summary>
        ///     Initialize class
        /// </summary>
        public User()
        {
        }

        /// <summary>
        ///     Initialize class
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="organization">Name of the current organization</param>
        /// <param name="access">Level of Access</param>
        /// <param name="password">Current password</param>
        /// <param name="avatar">Current Avatar</param>
        /// <param name="personId">Person's ID</param>
        /// <param name="name">Person's Name</param>
        /// <param name="secondName">Person's Second Name</param>
        /// <param name="fatherName">Person's Father Name</param>
        /// <param name="phoneNumber">Person's phone number</param>
        /// <param name="eMail">Person's e-Mail</param>
        /// <param name="address">Person's address</param>
        /// <param name="addressId">Address ID</param>
        public User
        (
            int id,
            string organization,
            string access,
            string password,
            byte[] avatar,
            int personId,
            string name,
            string secondName,
            string fatherName,
            string phoneNumber,
            string eMail,
            string address,
            int addressId
        )
            : base
            (
                name,
                secondName,
                fatherName,
                phoneNumber,
                eMail,
                address,
                addressId,
                personId
            )
        {
            UserId = id;
            Organization = organization;
            Password = password;
            Avatar = avatar;
            Access = access;
        }

        #endregion
    }
}