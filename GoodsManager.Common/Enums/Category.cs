using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsManager.Common.Enums
{
    public enum Category
    {
        [Display(Name = "Електроніка")]
        Electronics,
        [Display(Name = "Одяг")]
        Clothing,
        [Display(Name = "Продукти")]
        Food,
        [Display(Name = "Інструменти")]
        Tools
    }
}
