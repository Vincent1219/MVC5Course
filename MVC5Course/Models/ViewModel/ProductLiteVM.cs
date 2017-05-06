using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModel
{
    public class ProductLiteVM
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "請輸入商品名稱")]
        [DisplayName("商品名稱")]
        public string ProductName { get; set; }

        [DisplayName("商品價格")]
        [DisplayFormat(DataFormatString = "{0:0}")]
        public Nullable<decimal> Price { get; set; }

        [DisplayName("商品庫存量")]
        [DisplayFormat(DataFormatString = "{0:0}")]
        public Nullable<decimal> Stock { get; set; }
    }
}