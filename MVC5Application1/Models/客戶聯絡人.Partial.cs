namespace MVC5Application1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();

            if (this.Id == 0)
            {
                //Create
                if (repo.All().Where(客 => 客.客戶Id == this.客戶Id && 客.Email == this.Email).Any())
                {
                    yield return new ValidationResult("Email 已存在", new string[] { "Email" });
                }
            }
            else
            {
                //Update
                if (repo.All().Where(客 => 客.客戶Id == this.客戶Id && 客.Email == this.Email && 客.Id != this.Id).Any())
                {
                    yield return new ValidationResult("Email 已存在", new string[] { "Email" });
                }
            }

            yield return new ValidationResult("");
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [RegularExpression("[0-9]{4}-[0-9]{6}", ErrorMessage = "範例格式: 0911-111111")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        public Nullable<bool> 是否已刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
