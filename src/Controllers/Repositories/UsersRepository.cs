using System.Data;
using Dapper;
using Shubak_Website.Models;
using Shubak_Website.Context;

namespace Shubak_Website.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ShubakContext _context;

        public UsersRepository(ShubakContext context)
        {
            _context = context;
        }

        public async Task<UserModel> LoginUser(string useremail, string password)
        {
            
            using var connection = _context.CreateConnection();

            var result =  await connection.QueryFirstOrDefaultAsync<UserModel>
            (
                "LoginUser", new {
                    useremail,
                    password
                },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }


        public async Task<int> Register( UserModel userModel , string id ){

            using var connection = _context.CreateConnection();

            var result = await connection.ExecuteAsync(
            
                "AddUserinterests", new {

                    uid = id,
                    firstName = userModel.Firstname,
                    artisticevents = userModel.Artisticevents,
                    sportsevents = userModel.Sportsevents,
                    scientificevents = userModel.Scientificevents,
                    entertainmentevents = userModel.Entertainmentevents,
                    userType = userModel.UserType,
                    sex = userModel.Sex,
                    emil = userModel.Email

                },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<UserModel> GetUserInformationByUID(string UID)
        {

            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.QueryAsync<UserModel>
                (
                    "UserInformationByUID",
                    new
                    {
                        UID
                    },
                    commandType: CommandType.StoredProcedure
                );

                return result.FirstOrDefault();

            }
            catch (Exception ex)
            {


                throw;
            }
        }

    }
}