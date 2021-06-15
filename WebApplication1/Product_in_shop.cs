//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product_in_shop
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Поле является обязательным")]
        [Display(Name = "Товар")]
        public int Products_id { get; set; }

        [Required(ErrorMessage = "Поле является обязательным")]
        [Display(Name = "Номер магазина")]
        public int Shop_number { get; set; }

        [Required(ErrorMessage = "Поле является обязательным")]
        [Display(Name = "Стоимость")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Стоимость должна быть больше 0")]
        public decimal cost { get; set; }

        [Required(ErrorMessage = "Поле является обязательным")]
        [Display(Name = "Количество")]
        [Range(0, double.MaxValue, ErrorMessage = "Кольчество товара не может быть меньше 0")]
        public double quantity { get; set; }

        [Required(ErrorMessage = "Поле является обязательным")]
        [Display(Name = "Единица измерения")]
        public string unit { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Shop Shop { get; set; }
    }
}