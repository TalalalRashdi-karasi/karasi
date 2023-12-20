using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Firebase.Auth;


namespace Shubak_Website.Context;

public class ShubakContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public ShubakContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");

       
    }
    public IDbConnection CreateConnection()
        => new MySqlConnection(_connectionString);
}