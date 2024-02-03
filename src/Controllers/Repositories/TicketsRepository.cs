using System.Data;  
using MySql.Data.MySqlClient;
using System.Configuration;  
using Dapper; 
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shubak_Website.Models;
using MySqlConnector;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Shubak_Website.Context;


namespace Shubak_Website.Repositories
{

    public class TicketsRepository
    {
        private readonly ShubakContext _context;

        public TicketsRepository(ShubakContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(TicketModel entity)
        {
            using var connection = _context.CreateConnection();

            var result = await connection.ExecuteAsync(

                "user_tickets" , new {

                    entity.EventDate,
                    entity.EventId,
                    entity.EventName,
                    entity.EventTime,
                    entity.TicketCount,
                    entity.TicketPrice,
                    entity.EventType,
                    entity.UserId,
                    entity.FirstName,
                    entity.Email

                },

                commandType: CommandType.StoredProcedure

            );

            return result;
           
        }

        public async Task<IEnumerable<TicketModel>> GetByIdAsync(int id){

            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.QueryAsync<TicketModel>
                (
                    "UserTicketCart",
                    new
                    {
                        id
                    },
                    commandType: CommandType.StoredProcedure
                );

                return result;

            }
            catch (Exception ex)
            {


                throw;
            }

        }


        public async Task<IEnumerable<TicketModel>> GetUserticketByEventId(int evId){

            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.QueryAsync<TicketModel>
                (
                    "UserticketByEventId",
                    new
                    {
                        evId
                    },
                    commandType: CommandType.StoredProcedure
                );

                return result;

            }
            catch (Exception ex)
            {


                throw;
            }

        }


            public async Task<IEnumerable<TicketModel>> GetAllTicket(){

            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.QueryAsync<TicketModel>
                (
                    "GetAllTicket",
                 
                    commandType: CommandType.StoredProcedure
                );

                return result;

            }
            catch (Exception ex)
            {


                throw;
            }

        }




        public async Task<IEnumerable<TicketModel>> GetRemainingSeats(int? evId){


                using var connection = _context.CreateConnection();
                var result = await connection.QueryAsync<TicketModel>
                (
                    "GetRemainingSeats",
                    new
                    {
                        evId
                    },
                    commandType: CommandType.StoredProcedure
                );

                return result;

            
            

        }

    }
}