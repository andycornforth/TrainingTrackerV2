using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using TrainingTracker.Service.Models;

namespace TrainingTracker.Service.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly string _connectionString;

        public LogRepository(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            _connectionString = connectionString;
        }

        public int Add(Log log)
        {
            const string command = @"INSERT INTO [LOG] 
                                     VALUES (@UserId, @Title, @DateAdded);
                                     SELECT SCOPE_IDENTITY()";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(command, log, commandType: CommandType.Text);
            }
        }

        public Log GetById(int id)
        {
            const string query = @"SELECT [LogId] as Id
                                           ,[UserId]
                                           ,[Title]
                                           ,[DateAdded]
                                    FROM [Log]
                                    WHERE [LogId] = @Id;";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Log>(query, new {id}, commandType: CommandType.Text);
            }
        }

        public IEnumerable<Log> GetLogsByUserId(int userId)
        {
            const string query = @"SELECT [LogId] as Id
                                           ,[UserId]
                                           ,[Title]
                                           ,[DateAdded]
                                    FROM [Log]
                                    WHERE [UserId] = @UserId;";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Log>(query, new {userId}, commandType: CommandType.Text).ToList();
            }
        }
    }
}
