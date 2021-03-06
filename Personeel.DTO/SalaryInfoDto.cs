using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personeel.DTO
{
    public class SalaryInfoDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public int BasicSalary { get; set; }

        /// <summary>
        /// 奖惩
        /// </summary>
        public int EncourageOrChastisement { get; set; }
        /// <summary>
        /// 应到天数
        /// </summary>
        public int ShouldDays { get; set; }
        /// <summary>
        /// 实到天数
        /// </summary>
        public int ActualDays { get; set; }
        /// <summary>
        /// 补助
        /// </summary>
        public int Subsidies { get; set; }
        /// <summary>
        /// 公积金
        /// </summary>
        public int Accumulationfund { get; set; }
        /// <summary>
        /// 社保
        /// </summary>
        public int SocialSecurity { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        public int Tax { get; set; }
        /// <summary>
        /// 实发薪资
        /// </summary>
        public int ActualSalary { get; set; }
        /// <summary>
        /// 发薪日期
        /// </summary>
        public string SalaryDate { get; set; }
        public DateTime Date { get; set; }
    }
}
