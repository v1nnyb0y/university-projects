using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Provider.Handlers;

namespace Provider.CurrentSession
{
    /// <summary>
    ///     Class for the current person
    /// </summary>
    public class Person
    {
        #region Methods

        /// <summary>
        ///     Get FIO of the User
        /// </summary>
        [Display(Name = "Ф.И.О.")]
        public string FIO
        {
            get => $"{SecondName} {Name} {FatherName}";
            set
            {
                var splitterFIO = value.Split
                (
                    ' '
                );
                Name = splitterFIO[1];
                SecondName = splitterFIO[0];
                FatherName = splitterFIO[2];
            }
        }

        #endregion

        #region Fields

        [Display(Name = "Имя")]
        [RegularExpression(@"[\D]{1,}", ErrorMessage = "Имя некорректно")]
        public string Name { get; set; }

        [RegularExpression(@"[\D]{1,}", ErrorMessage = "Фамилия некорректна")]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [RegularExpression(@"[\D]{1,}", ErrorMessage = "Отчество некорректно")]
        [Display(Name = "Отчество")]
        public string FatherName { get; set; }


        [Display(Name = "Контактный номер")]
        [RegularExpression(@"[0-9]{11}", ErrorMessage = "Номер телефона некорректен")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Адрес проживания")]
        public string Address { get; set; }

        public int PersonId { get; set; }
        public int AddressId { get; set; }

        #endregion

        #region Initialize

        /// <summary>
        ///     Initialize class (make full copy)
        /// </summary>
        /// <param name="copiedPerson">Source Person</param>
        public Person
        (
            Person copiedPerson
        )
        {
            Name = copiedPerson.Name;
            SecondName = copiedPerson.SecondName;
            FatherName = copiedPerson.FatherName;
            PhoneNumber = copiedPerson.PhoneNumber;
            EMail = copiedPerson.EMail;
            Address = copiedPerson.Address;
            PersonId = copiedPerson.PersonId;
            AddressId = copiedPerson.AddressId;
        }

        /// <summary>
        ///     Initialize class
        /// </summary>
        public Person()
        {
        }

        /// <summary>
        ///     Initialize class
        /// </summary>
        /// <param name="name">Person's Name</param>
        /// <param name="secondName">Person's Second Name</param>
        /// <param name="fatherName">Person's Father Name</param>
        /// <param name="phoneNumber">Person's Phone Number</param>
        /// <param name="eMail">Person's eMail</param>
        /// <param name="address">Person's Address</param>
        /// <param name="idAddress">Address's ID</param>
        /// <param name="idPerson">Person's ID</param>
        public Person
        (
            string name,
            string secondName,
            string fatherName,
            string phoneNumber,
            string eMail,
            string address,
            int idAddress,
            int idPerson
        )
        {
            Name = name;
            SecondName = secondName;
            FatherName = fatherName;
            PhoneNumber = phoneNumber;
            EMail = eMail;
            Address = address;
            PersonId = idPerson;
            AddressId = idAddress;
        }

        #endregion
    }
}