using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.Models
{
    
     public class User:BaseEntity
    {
       /// <summary>
       /// 是否部门主管
       /// </summary>
        public bool IsManager { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Required,StringLength(maximumLength:40),Column(TypeName ="varchar")]
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required,StringLength(30),Column(TypeName ="varchar")]
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Required,StringLength(300),Column(TypeName ="varchar")]
        public string ImagePath { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [Required]
        [ForeignKey(nameof(UserRight))]
        public Guid UserRightID { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public UserRight UserRight { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserNum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required,StringLength(10),Column(TypeName = "varchar")]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [StringLength(4), Column(TypeName = "varchar")]
        public string Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public int IDNumber { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        [StringLength(5),Column(TypeName = "varchar")]
        public string Wedlock { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [StringLength(5),Column(TypeName = "varchar")]
        public string Race { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        [StringLength(30), Column(TypeName = "varchar")]
        public string Nativeplace { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        [StringLength(5),Column(TypeName = "varchar")]
        public string Politic { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public int Phone { get; set; }
        /// <summary>
        /// 部门外键
        /// </summary>
        [ForeignKey(nameof(Department))]
        public Guid DepartmentID { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public Department Department { get; set; }
        /// <summary>
        /// 岗位外键
        /// </summary>
        [ForeignKey(nameof(Position))]
        public Guid PositionID { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public Position Position { get; set; }
        /// <summary>
        /// 基本工资
        /// </summary>
        public int Basicmoney { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
         [StringLength(5),Column(TypeName = "varchar")]
        public string Tiptopdegree { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        [StringLength(30), Column(TypeName = "varchar")]
        public string School { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime Beginworkdate { get; set; }=DateTime.Now;
        /// <summary>
        /// 转正日期
        /// </summary>
        public DateTime Beformdate { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime Notworkdate { get; set; }
    }
}
