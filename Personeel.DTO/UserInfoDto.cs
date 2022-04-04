using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class UserInfoDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public int UserNum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public int IdNumber { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string Wedlock { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Race { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string Politic { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public int Phone { get; set; }
        /// <summary>
        /// 基本工资
        /// </summary>
        public int BasicMoney { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string TipTopDegree { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime BeginWorkDate { get; set; }
        /// <summary>
        /// 转正日期
        /// </summary>
        public DateTime BeFormDate { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime NotWorkDate { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public int UserPower { get; set; }



    }
}
