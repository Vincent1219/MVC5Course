using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVC5Course.Models.ViewModel
{
    public class SearchProducts : IValidatableObject
    {
        public string productName { set; get; }

        public int? StockStart { set; get; }
        public int? stockEnd { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 回傳多個錯誤訊息
            if (this.stockEnd < this.StockStart)
            {
                yield return new ValidationResult("庫存資料篩選條件錯誤", new string[] { "StockStart", "stockEnd" });
            }
        }
    }
}