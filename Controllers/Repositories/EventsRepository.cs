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
using Shubak_Website;

namespace Shubak_Website.Repositories
{
    public class EventsRepository 
    {
        private readonly ShubakContext _context;

        public EventsRepository(ShubakContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(EventDto entity)
        {
            try
            {
                using var connection = _context.CreateConnection();

                var result = await connection.ExecuteAsync
                (
                "AddEvent", new
                {

                    EvImage = entity.EvImage,
                     //entity.EvImage?.ConvertToByteArray(),
                    // entity.EvImage,
                    entity.EventName,
                    entity.UserId,
                    entity.AboutEvent,
                    entity.EventPlace,
                    entity.EventPrice,
                    entity.EventTime,
                    entity.EventDate,
                    entity.Target,
                    entity.SeatCount,
                    entity.EventType,
                    entity.IsActive

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

        public async Task<IEnumerable<EventViewModel>> DeleteEventById(int EvID)
        {
 
            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.QueryAsync<EventDto>
                (
                    "RemoveEventByID",
                    new
                    {
                        EvID
                    },
                    commandType: CommandType.StoredProcedure
                );

                return result.Select(x => x.MapToViewModel()).ToList();

            }
            catch (Exception ex)
            {


                throw;
            }
            
        }   

        public async Task<IReadOnlyList<EventViewModel>> GetAllAsync()
        {
            var query = "select * from EVENTOM.events";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EventDto>
            (
                query
            );

            return result.Select(x => x.MapToViewModel()).ToList();
        }

        public async Task<IReadOnlyList<EventViewModel>> GetAllEventsAsync()
        {
            var query = "select * from EVENTOM.events";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EventDto>
            (
                query
            );

            return result.Select(x => x.MapToViewModel()).ToList();
        }

        public async Task<IEnumerable<EventViewModel>> GetByIdAsync(int EvID)
        {

            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.QueryAsync<EventDto>
                (
                    "GetEventsByID",
                    new
                    {
                        EvID
                    },
                    commandType: CommandType.StoredProcedure
                );

                return result.Select(x => x.MapToViewModel()).ToList();

            }
            catch (Exception ex)
            {


                throw;
            }



        }


        public async Task<IEnumerable<EventViewModel>> GetAllEventByUserID( string UID){

            try
            {
                using var connection = _context.CreateConnection();
                var result = await connection.QueryAsync<EventDto>
                (
                    "GetAllEventByUserID",
                    new
                    {
                        UID
                    },
                    commandType: CommandType.StoredProcedure
                );

                return result.Select(x => x.MapToViewModel()).ToList();

            }
            catch (Exception ex)
            {


                throw;
            }

        }



        public async Task<int> UpdateEventAsync(EventDto entity)
        {
            try
            {
                using var connection = _context.CreateConnection();

                var result = await connection.ExecuteAsync
                (
                "ْUpdateEvent", new
                {

                    EvImage = entity.EvImage,
                     //entity.EvImage?.ConvertToByteArray(),
                    // entity.EvImage,
                    entity.EventName ,
                    entity.UserId,
                    entity.AboutEvent,
                    entity.EventPlace,
                    entity.EventPrice,
                    entity.EventTime,
                    entity.EventDate,
                    entity.Target,
                    entity.SeatCount,
                    entity.EventType,
                    entity.IsActive,
                    entity.Id
                    

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


    }
}