using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Personeel.IBLL
{
    public interface IUserManager
    {
        Task AddUser(string email, string password, string name, int userRight,int basicMoney);
        Task<bool> Login(string email, string password,int userRight);
        Task ChangePassword(string email, string oldPwd, string newPwd);
        Task ChangeInfo(string email,string name, string imagePath, string gender, DateTime birthday, int idNum, string wedlock,
                        string race, string nativePlace, string politic, int phone, string tipTopDegree, string school);
        Task <DTO.UserInfoDto> GetUserByEmail(string email);
    }
}
