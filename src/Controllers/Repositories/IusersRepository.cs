using MySql.Data.MySqlClient;
using System.Configuration;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shubak_Website.Models;
using MySqlConnector;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Shubak_Website.Repositories
{

    public interface IUsersRepository
    {
        Task<UserModel> LoginUser(string email, string password);
        Task<int> Register(UserModel userModel , string id);
        Task<UserModel> GetUserInformationByUID(string UID);
    }
}