using System.ComponentModel.DataAnnotations;

namespace GoodsManager.Common.Enums
{
    public enum City {
        [Display(Name = "Київ")]
        Kyiv,

        [Display(Name = "Львів")]
        Lviv,

        [Display(Name = "Богуслав")]
        Boguslav,

        [Display(Name = "Одеса")]
        Odesa,

        [Display(Name = "Луцьк")]
        Lutsk
    }

}
