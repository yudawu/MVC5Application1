using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC5Application1.Models.ViewModels
{
    public class 客戶聯絡人List
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        //[StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        //[Required]
        //public string 姓名 { get; set; }

        //[StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [RegularExpression("[0-9]{4}-[0-9]{6}", ErrorMessage = "範例格式: 0911-111111")]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    }
}