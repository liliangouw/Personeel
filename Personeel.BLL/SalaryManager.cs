using System;
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
    public class SalaryManager:ISalaryManager
    {
        #region 账套管理
        public async Task AddAccount(Guid userId, int basicMoney, int subsidies, int accumulationfund, int socialSecurity, int tax)
        {
            using (IPayRollAccountService payRollAccountService = new PayRollAccountService())
            {
                await payRollAccountService.CreateAsync(new PayRollAccount()
                {
                    UserGuid = userId,
                    Accumulationfund = accumulationfund,
                    BasicSalary = basicMoney,
                    SocialSecurity = socialSecurity,
                    Subsidies = subsidies,
                    Tax = tax
                });
            }
        }

        public async Task<List<SalaryInfoDto>> GetAllAccount()
        {
            using (IPayRollAccountService payRollAccountService = new PayRollAccountService())
            {
              return  await payRollAccountService.GetAllAsync().Select(m => new SalaryInfoDto()
                {
                    UserId = m.UserGuid,
                    UserName = m.User.Name,
                    BasicSalary = m.BasicSalary,
                    Subsidies = m.Subsidies,
                    Accumulationfund = m.Accumulationfund,
                    SocialSecurity = m.SocialSecurity,
                    Tax = m.Tax
                }).ToListAsync();
            }
        }

        public async Task EditAccount(Guid id, int subsidies, int accumulationfund, int socialSecurity, int tax)
        {
            using (IPayRollAccountService payRollAccountService = new PayRollAccountService())
            {
               var account=await payRollAccountService.GetOneByIdAsync(id);
               account.Subsidies = subsidies;
               account.Accumulationfund = accumulationfund;
               account.SocialSecurity = socialSecurity;
               account.Tax = tax;
               await payRollAccountService.EditAsync(account);
            }
        }

        public async Task<SalaryInfoDto> GetOneAccountById(Guid id)
        {
            using (IPayRollAccountService payRollAccountService = new PayRollAccountService())
            {
              return await payRollAccountService.GetAllAsync().Include(m => m.User).Where(m => m.Id == id).Select(m =>
                    new SalaryInfoDto()
                    {
                        UserId = m.UserGuid,
                        UserName = m.User.Name,
                        BasicSalary = m.BasicSalary,
                        Subsidies = m.Subsidies,
                        Accumulationfund = m.Accumulationfund,
                        SocialSecurity = m.SocialSecurity,
                        Tax = m.Tax
                    }).FirstAsync();
            }
            
        }

        public async Task RemoveAccount(Guid id)
        {
            using (IPayRollAccountService payRollAccountService = new PayRollAccountService())
            {
                await payRollAccountService.RemoveAsync(id);
            }
        }


        #endregion

        #region 薪酬管理

        /// <summary>
        /// 批量添加薪酬
        /// </summary>
        /// <returns></returns>
        public async Task AddSalarys()
        {
            using (IUserService userService = new UserService())
            {
                List <User>userList= await userService.GetAllAsync().ToListAsync();
                List<SalaryInfoDto> salaryInfos = new List<SalaryInfoDto>();
                //获取所有用户的考勤，奖惩等信息
                foreach (var item in userList)
                {
                    SalaryInfoDto salaryInfo=await GetInfoByUserId(item.Id);
                    salaryInfos.Add(salaryInfo);
                }
                using (IPayRollAccountService payRollAccountService = new PayRollAccountService()) 
                {
                    //获取所有工资账套
                    List<PayRollAccount>payRollAccounts=await payRollAccountService.GetAllAsync().ToListAsync();
                    for (int i = 0; i < payRollAccounts.Count; i++)
                    {
                       await AddSalary(payRollAccounts[i].UserGuid,payRollAccounts[i].BasicSalary,salaryInfos[i].EncourageOrChastisement,
                           salaryInfos[i].ShouldDays,salaryInfos[i].ActualDays,payRollAccounts[i].Subsidies,payRollAccounts[i].Accumulationfund,
                           payRollAccounts[i].SocialSecurity,payRollAccounts[i].Tax,salaryInfos[i].SalaryDate);
                    }
                }
       
            }
        }      
        
        /// <summary>
        /// 添加单条薪资记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="basicMoney"></param>
        /// <param name="encourageOrChastisement"></param>
        /// <param name="shouldDays"></param>
        /// <param name="actualDays"></param>
        /// <param name="subsidies"></param>
        /// <param name="accumulationfund"></param>
        /// <param name="socialSecurity"></param>
        /// <param name="tax"></param>
        /// <param name="month"></param>
        /// <returns></returns>
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
            //工作日等于当月总天数减去非
            //工作日
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
                            UserName = user.Name,
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
        /// <summary>
        /// 修改薪资
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="basicMoney"></param>
        /// <param name="encourageOrChastisement"></param>
        /// <param name="shouldDays"></param>
        /// <param name="actualDays"></param>
        /// <param name="subsidies"></param>
        /// <param name="accumulationfund"></param>
        /// <param name="socialSecurity"></param>
        /// <param name="tax"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public async Task EditSalary(Guid Id, int basicMoney, int encourageOrChastisement, int shouldDays, int actualDays, int subsidies,
            int accumulationfund, int socialSecurity, int tax, string month)
        {
            using (ISalaryService salaryService = new SalaryService())
            {
               var info= await  salaryService.GetAllAsync().Where(m => m.Id == Id).FirstAsync();
               info.BasicSalary = basicMoney;
               info.EncourageOrChastisement = encourageOrChastisement;
               info.ShouldDays = shouldDays;
               info.ActualDays = actualDays;
               info.Subsidies = subsidies;
               info.Accumulationfund = accumulationfund;
               info.SocialSecurity = socialSecurity;
               info.Tax = tax;
               info.SalaryDate = month;
               info.ActualSalary = basicMoney + encourageOrChastisement + subsidies + accumulationfund +
                                   socialSecurity + tax;
               await salaryService.EditAsync(info);
            }
        }

        public async Task Remove(Guid id)
        {
            using (ISalaryService salaryService = new SalaryService())
            {
                await salaryService.RemoveAsync(id);
            }
        }


        #endregion

        #region 调薪管理
        public async Task<List<UserInfoDto>> GetAllUserBasicSalary()
        {
            using (IUserService userService=new UserService())
            {
               return await userService.GetAllAsync().Select(m => new UserInfoDto()
                {
                   UserId = m.Id,
                   Name = m.Name,
                   Position = m.Position.Posname,
                   BasicMoney = m.Basicmoney,
                   Department = m.Department.Depname
                }).ToListAsync();
            }
        }

        public async Task EditUserBasicSalary(Guid userGuid,string reason, int basicSalary)
        {
            using (IUserService userService = new UserService())
            {
               var user= await userService.GetOneByIdAsync(userGuid);
               
               //添加调薪记录
               using (IChangeSalaryService changeSalaryService = new ChangeSalaryService())
               {
                   await changeSalaryService.CreateAsync(new ChangeSalary()
                   {
                       BeforeSalary = user.Basicmoney,
                       UserId = userGuid,
                       ChangeReason = reason,
                       ChangedSalary = basicSalary
                   });
               }
               //调薪
               user.Basicmoney = basicSalary;
               await userService.EditAsync(user);
            }
        }

        public async Task<UserInfoDto> GetOneBasicSalary(Guid userGuid)
        {
            using (IUserService userService = new UserService())
            {
                return await userService.GetAllAsync().Where(m => m.Id == userGuid).Select(m => new UserInfoDto()
                {
                    UserId = m.Id,
                    Name = m.Name,
                    Position = m.Position.Posname,
                    BasicMoney = m.Basicmoney,
                    Department = m.Department.Depname
                }).FirstAsync();
            }
        }



        #endregion

        #region 调薪记录管理

        public async Task<List<ChangeSalaryInfoDto>> GetAllChangeRecord()
        {
            using (IChangeSalaryService changeSalaryService = new ChangeSalaryService())
            {
                return await changeSalaryService.GetAllAsync().Select(m => new ChangeSalaryInfoDto()
                {
                    Id = m.Id,
                    UserGuid = m.UserId,
                    UserName = m.User.Name,
                    BeforeSalary = m.BeforeSalary,
                    ChangedSalary = m.ChangedSalary,
                    ChangeReason = m.ChangeReason,
                    ChangedDate = m.CreateTime
                }).ToListAsync();
            }
        }

        public async Task<List<ChangeSalaryInfoDto>> GetRecordByUserId(Guid id)
        {
            using (IChangeSalaryService changeSalaryService = new ChangeSalaryService())
            {
                return await changeSalaryService.GetAllAsync().Where(m=>m.UserId==id).Select(m => new ChangeSalaryInfoDto()
                {
                    Id = m.Id,
                    UserGuid = m.UserId,
                    UserName = m.User.Name,
                    BeforeSalary = m.BeforeSalary,
                    ChangedSalary = m.ChangedSalary,
                    ChangeReason = m.ChangeReason,
                    ChangedDate = m.CreateTime
                }).ToListAsync();
            }
        }

        public async Task RemoveRecord(Guid id)
        {
            using (IChangeSalaryService changeSalaryService = new ChangeSalaryService())
            {
                await changeSalaryService.RemoveAsync(id);
            }
        }

        #endregion

    }
}
