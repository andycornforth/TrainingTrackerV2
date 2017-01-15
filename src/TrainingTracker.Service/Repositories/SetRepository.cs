using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using TrainingTracker.Service.Models;

namespace TrainingTracker.Service.Repositories
{
    public class SetRepository : ISetRepository
    {
        private readonly string _connectionString;

        public SetRepository(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            _connectionString = connectionString;
        }

        public int Add(Set set)
        {
            const string command = @"INSERT INTO [Set] 
                                     VALUES (@LogId, @ExerciseId, @Weight, @Reps, @DateAdded, @Position);
                                     SELECT SCOPE_IDENTITY()";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(command, set, commandType: CommandType.Text);
            }
        }

        public void Update(Set set)
        {
            const string command = @"UPDATE [Set] 
                                     SET LogId = @LogId
                                        , ExerciseId = @ExerciseId
                                        , Weight = @Weight
                                        , Reps = @Reps
                                        , Position = @Position
                                     WHERE SetId = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(command, set, commandType: CommandType.Text);
            }
        }

        public Set GetById(int id)
        {
            const string query = @"SELECT [SetId] as Id
                                           ,[LogId]
                                           ,[ExerciseId]
                                           ,[Weight]
                                           ,[Reps]
                                           ,[Position]
                                           ,[DateAdded]
                                    FROM [Set]
                                    WHERE [SetId] = @Id;";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Set>(query, new { id }, commandType: CommandType.Text);
            }
        }

        public IEnumerable<Set> GetSetsByLogId(int logId)
        {
            const string query = @"SELECT [SetId] as Id
                                           ,[LogId]
                                           ,[ExerciseId]
                                           ,[Weight]
                                           ,[Reps]
                                           ,[Position]
                                           ,[DateAdded]
                                    FROM [Set]
                                    WHERE [LogId] = @LogId;";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Set>(query, new {logId}, commandType: CommandType.Text);
            }
        }
    }
}
