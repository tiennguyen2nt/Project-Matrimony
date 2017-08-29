using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTT.WIILOVE.Data.Model.Param
{
    public class User
    {
        [Required(ErrorMessage = "Tên không được bỏ trống!")]
        public string FirstName { get; set; }
        [Required (ErrorMessage = "Họ không được bỏ trống!")]
        public string LastName { get; set; }
        [Required (ErrorMessage = "Email hoặc số điện thoại không được bỏ trống!")]
        public string EmailOrPhone { get; set; }
        [Required (ErrorMessage = "Mật khẩu không được bỏ trống!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Nhậm lại mật khẩu không được bỏ trống!")]
        public string RetypePassword { get; set; }
        [Required]
        public int Day { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required (ErrorMessage = "Bạn chưa đồng ý với điều khoản và dịch vụ của chúng tôi.")]
        public bool Terms { get; set; }
    }
}
