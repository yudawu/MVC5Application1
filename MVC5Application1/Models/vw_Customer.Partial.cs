namespace MVC5Application1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(vw_CustomerMetaData))]
    public partial class vw_Customer
    {
    }
    
    public partial class vw_CustomerMetaData
    {
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> 聯絡人數量 { get; set; }
        public Nullable<int> 銀行帳戶數量 { get; set; }
        [Required]
        public int Id { get; set; }
        public Nullable<bool> 是否已刪除 { get; set; }
    }
}
