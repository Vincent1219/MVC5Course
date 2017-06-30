namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ValidationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public int 訂單數量 {
            get {
                return OrderLine.Count;
                //return this.OrderLine.Where(p => p.Qty > 400).Count;
                //return this.OrderLine.Where(p => p.Qty > 400).ToList().Count;
                //return this.OrderLine.Count(p => p.Qty > 400); 取得數量 效能較好
            }
        }

        /// <summary> 模型驗證 - Models bind 完成 輸入驗證完成才會執行 </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 回傳多個錯誤訊息
            if (this.Price > 100 && this.Stock > 5)
            {
                yield return new ValidationResult("價格與庫存數量不合理", new string[] { "Price", "stock" });
            }
            if (this.OrderLine.Count() == 18 && this.Stock > 0)
            {
                yield return new ValidationResult("Stock 與 訂單數量不匹配", new string[] { "stock" });
            }
            yield break;
        }
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "請輸入商品名稱")]
        [DisplayName("商品名稱")]
        [商品名稱必須包含Will字串(ErrorMessage = "商品名稱必須包含Will")]
        //[MaxLength(80)]
        [MaxWords(3)]
        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }

        [Required]
        [DisplayName("商品價格")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Range(0, 9999999, ErrorMessage = "請輸入正確的商品金額")]
        public Nullable<decimal> Price { get; set; }

        [Required]
        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }

        [Required]
        [DisplayName("商品庫存量")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Range(0, 99999, ErrorMessage = "請輸入正確的數量")]
        public Nullable<decimal> Stock { get; set; }


        public bool Is刪除 { get; set; }

        // 欄位 建立時間 必須是日期。出現此錯誤需加上DisplayFormat
        // 日期格式部分 DisplayFormat、DataType是一組一定要綁定，否則會出現年/月/日
        // DataFormatString一定要是 ( - ) 才可以正常輸出日期
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)] // 決定View輸出的型態
        [DisplayName("建立時間")]
        public System.DateTime CreatedOn { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
