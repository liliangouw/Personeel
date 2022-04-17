﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personeel.DAL;
using Personeel.DTO;
using Personeel.IBLL;
using Personeel.IDAL;
using Personeel.Models;

namespace Personeel.BLL
{
    internal class SalaryManager:ISalaryManager
    {
        public async Task AddSalary(Guid userId, int basicMoney, int encourageOrChastisement, int shouldDays, int actualDays, int subsidies, int accumulationfund, int socialSecurity, int tax, string month)
        {
            using (ISalaryService salaryService = new SalaryService())
            {
                await salaryService.CreateAsync(new SalaryInfo()
                {
                    Accumulationfund = accumulationfund,
                    ActualDays = actualDays,
                    ActualSalary = basicMoney + encourageOrChastisement + subsidies + accumulationfund +
                                   socialSecurity + tax,
                    BasicSalary = basicMoney,
                    EncourageOrChastisement = encourageOrChastisement,
                    SalaryDate = month,
                    UserId = userId,
                    ShouldDays = shouldDays,
                    Subsidies = subsidies,
                    SocialSecurity = socialSecurity,
                    Tax = tax
                });
            }
        }
        public int GetWorkingDays(int year, int month)
        {
            //获取该月的第一天
            DateTime dateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
            //获取该月总计天数
            int days = DateTime.DaysInMonth(year, month);
            //休息天数
            int weekDays = 0;
            for (int i = 0; i < days; i++)
            {
                //每逢周六/周日 休息天数增加一天
                switch (dateTime.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        weekDays++;
                        break;
                    case DayOfWeek.Saturday:
                        weekDays++;
                        break;
                    default:
                        break;
                }
                dateTime.AddDays(1);
            }
            //工作日等于当月总天数减去非工作日
            int workDays = days - weekDays;
            return workDays;
        }

        /// <summary>
        /// 根据用户ID获取他的基础薪资，应到天数，实到天数，奖惩
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SalaryInfoDto> GetInfoByUserId(Guid id)
        {
            using (IEncourageService encourageService = new EncourageService())
            {
                //获取当前用户当月奖惩
                int count = 0;
                List<EncourageOrChastisement> encourageInfo = await encourageService.GetAllAsync()
                  .Where(m => m.UserId == id && m.CreateTime.Month == DateTime.Now.Month && m.CreateTime.Year == DateTime.Now.Year)
                  .ToListAsync();
                foreach (var item in encourageInfo)
                {
                    count += item.Money;
                }
                //获取当前用户当月的实到天数与应到天数
                using (ICheckingService checkingService = new CheckingService())
                {
                    //实到天数
                    List<Checking> checking =
                        await checkingService.GetAllAsync()
                            .Where(m => m.UserGuid == id && m.CreateTime.Month == DateTime.Now.Month && m.CreateTime.Year == DateTime.Now.Year)
                            .ToListAsync();
                    int actualDays = checking.Count;
                    //应到天数
                    int shouldDays = GetWorkingDays(DateTime.Now.Year, DateTime.Now.Month);

                    //获取基本工资
                    using (IUserService userService = new UserService())
                    {
                        var user = await userService.GetAllAsync().Where(m => m.Id == id).FirstAsync();
                        int basicMoney = user.Basicmoney;
                        SalaryInfoDto salaryInfo = new SalaryInfoDto()
                        {
                            BasicSalary = basicMoney,
                            EncourageOrChastisement = count,
                            ShouldDays = shouldDays,
                            ActualDays = actualDays,
                            SalaryDate = DateTime.Now.ToString("yyyy-MM-dd")
                        };
                        return salaryInfo;
                    }
                }
            }
        }

        /// <summary>
        /// 根据ID获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SalaryInfoDto> GetInfoById(Guid id)
        {
            using (ISalaryService salaryService = new SalaryService())
            {
               return await salaryService.GetAllAsync().Include(m => m.User).Where(m => m.Id == id).Select(m => new SalaryInfoDto()
                {
                    UserId = m.UserId,
                    Id = m.Id,
                    UserName = m.User.Name,
                    Accumulationfund = m.Accumulationfund,
                    ActualDays = m.ActualDays,
                    ShouldDays = m.ShouldDays,
                    ActualSalary = m.ActualSalary,
                    BasicSalary = m.BasicSalary,
                    EncourageOrChastisement = m.EncourageOrChastisement,
                    SalaryDate = m.SalaryDate,
                    SocialSecurity = m.SocialSecurity,
                    Tax = m.Tax,
                    Subsidies = m.Subsidies
                }).FirstAsync();
            }
        }
        /// <summary>
        /// 获取所有工资信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<SalaryInfoDto>> GetAllSalary()
        {
            using (ISalaryService salaryService = new SalaryService())
            {
                return await salaryService.GetAllAsync().Select(m => new SalaryInfoDto()
                {
                    UserId = m.UserId,
                    Id = m.Id,
                    UserName = m.User.Name,
                    Accumulationfund = m.Accumulationfund,
                    ActualDays = m.ActualDays,
                    ShouldDays = m.ShouldDays,
                    ActualSalary = m.ActualSalary,
                    BasicSalary = m.BasicSalary,
                    EncourageOrChastisement = m.EncourageOrChastisement,
                    SalaryDate = m.SalaryDate,
                    SocialSecurity = m.SocialSecurity,
                    Tax = m.Tax,
                    Subsidies = m.Subsidies
                }).ToListAsync();
            }
        }
        /// <summary>
        /// 根据用户ID获取用户所有工资信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<SalaryInfoDto>> GetAllSalaryByUserId(Guid userGuid)
        {
            using (ISalaryService salaryService = new SalaryService())
            {
                return await salaryService.GetAllAsync().Where(m=>m.UserId==userGuid).Select(m => new SalaryInfoDto()
                {
                    UserId = m.UserId,
                    Id = m.Id,
                    UserName = m.User.Name,
                    Accumulationfund = m.Accumulationfund,
                    ActualDays = m.ActualDays,
                    ShouldDays = m.ShouldDays,
                    ActualSalary = m.ActualSalary,
                    BasicSalary = m.BasicSalary,
                    EncourageOrChastisement = m.EncourageOrChastisement,
                    SalaryDate = m.SalaryDate,
                    SocialSecurity = m.SocialSecurity,
                    Tax = m.Tax,
                    Subsidies = m.Subsidies
                }).ToListAsync();
            }
        }

        public async Task Remove(Guid id)
        {
            using (ISalaryService salaryService = new SalaryService())
            {
                await salaryService.RemoveAsync(id);
            }
        }
    }
}
