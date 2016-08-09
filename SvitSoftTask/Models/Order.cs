using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SvitSoftTask.Models
{
    public class Order
    {

        private DateTime? orderDate;

        public int ID { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Введите имя")]
        public string ClientName { get; set; }

        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Некорректный E-mail")]
        [Required(ErrorMessage = "Требуется E-mail")]
        public string Email { get; set; }

        [DisplayName("Телефон")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{2})[-. ]?([0-9]{2})$", ErrorMessage = "Введите 10-ти значный номер телефона")]
        public string Phone { get; set; }

        [DisplayName("Дата")]
        public DateTime OrderDate
        {
            get
            {
                return orderDate ?? DateTime.Now;
            }

            set { orderDate = value; }
        }
    }
}