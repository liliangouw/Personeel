using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Personeel.IBLL
{
    public interface IUserManager
    {
        Task AddUser(string email, string password, string name, Guid userRight,int basicMoney,Guid department,Guid positionId);
        bool Login(string email, string password,out Guid userId,out string userName,out Guid userRight,out bool isManager);
        Task ChangePassword(Guid id,string email, string oldPwd, string newPwd);
        Task ChangeInfo(Guid id,string email,string name, string gender, DateTime birthday, int idNum, string wedlock,
                        string race, string nativePlace, string politic, int phone, string tipTopDegree, string school);
        Task <DTO.UserInfoDto> GetUserById(Guid id);
        Task<DTO.UserInfoDto> GetUserByEmail(string email);
        Task<List<DTO.UserInfoDto>> GetAllUser();
        Task<List<DTO.UserInfoDto>> GetAllUserByPage(int pageIndex, int pageSize);
        Task<int> GetDataCount();
        Task DeleteUser(Guid id);
        Task ChangePower(Guid id, Guid powerId);
        Task ChangeIsManager(Guid id, bool isManager);
    }
}
