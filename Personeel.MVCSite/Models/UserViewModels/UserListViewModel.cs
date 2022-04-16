using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.UserViewModels
{
    public class UserListViewModel
    {
        public Guid UserId { get; set; }
        [Display(Name = "工号")]
        public  int UserNum { get; set; }
        [Display(Name = "姓名")]
        public  string Name { get; set; }
        [Display(Name = "部门")]
        public  string Department { get; set; }
        [Display(Name = "职位")]
        public  string Position { get; set; }
        [Display(Name = "邮箱")]
        public  string Email { get; set; }
        [Display(Name = "角色")]
        public  int UserPower { get; set; }
        [Display(Name = "生日")]
        public DateTime Birthday { get; set; }
        [Display(Name = "身份证号")]
        public int IdNumber { get; set; }
        [Display(Name = "婚姻状况")]
        public string Wedlock { get; set; }
        [Display(Name = "民族")]
        public string Race { get; set; }
        [Display(Name = "籍贯")]
        public string NativePlace { get; set; }
        [Display(Name = "政治面貌")]
        public string Politic { get; set; }
        [Display(Name = "手机号")]
        public int Phone { get; set; }
        [Display(Name = "基本工资")]
        public int BasicMoney { get; set; }
        [Display(Name = "学历")]
        public string TipTopDegree { get; set; }
        [Display(Name = "毕业院校")]
        public string School { get; set; }
        [Display(Name = "入职日期")]
        public DateTime BeginWorkDate { get; set; }
        [Display(Name = "转正日期")]
        public DateTime BeFormDate { get; set; }
        [Display(Name = "离职日期")]
        public DateTime NotWorkDate { get; set; }
        [Display(Name = "性别")]
        public string Gender { get; set; }
        [Display(Name = "权限")]
        public Guid Right { get; set; }

    }
}