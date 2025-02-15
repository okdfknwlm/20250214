using System.ComponentModel.DataAnnotations;

namespace Net8CoreMVC.Models
{
    public class EmpModel
    {        
        [Display(Name = "員工編號")]
        public int EmployeeID { get; set; }

        [Display(Name = "姓氏")]
        [Required(ErrorMessage = "必填欄位")]
        public string? LastName { get; set; }

        [Display(Name = "名字")]
        [Required(ErrorMessage = "必填欄位")]
        public string? FirstName { get; set; }

        [Display(Name = "職稱")]
        public string? Title { get; set; }

        [Display(Name = "尊稱")]
        public string? TitleOfCourtesy { get; set; }

        [Display(Name = "出生日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "雇用日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? HireDate { get; set; }

        [Display(Name = "地址")]
        public string? Address { get; set; }

        [Display(Name = "城市")]
        public string? City { get; set; }

        [Display(Name = "地區")]
        public string? Region { get; set; }

        [Display(Name = "郵遞區號")]
        public string? PostalCode { get; set; }

        [Display(Name = "國家")]
        public string? Country { get; set; }

        [Display(Name = "住家電話")]
        public string? HomePhone { get; set; }

        [Display(Name = "分機")]
        public string? Extension { get; set; }

        [Display(Name = "主管編號")]
        public int? ReportsTo { get; set; }
    }
}
