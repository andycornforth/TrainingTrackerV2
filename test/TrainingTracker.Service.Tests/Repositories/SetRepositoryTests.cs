using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FluentAssertions;
using Microsoft.Framework.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingTracker.Service.Models;
using TrainingTracker.Service.Repositories;

namespace TrainingTracker.Service.Tests.Repositories
{
    [TestClass]
    public class SetRepositoryTests
    {
        private string _connectionString;
        private ISetRepository _setRepository;
        private ILogRepository _logRepository;

        [TestInitialize]
        public void BeforeEach()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
            _connectionString = configuration.GetSection("ConnectionStrings")["TrainingTracker"];

            _setRepository = new SetRepository(_connectionString);
            _logRepository = new LogRepository(_connectionString);
        }

        [TestCleanup]
        public void AfterEach()
        {
            const string command = @"DELETE FROM [Set]
                                     DELETE FROM [Log]";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(command, commandType: CommandType.Text);
            }
        }

        [TestMethod]
        public void Should_add_set_and_return_id()
        {
            var logId = AddLog();
            var set = Set(logId);

            var id = _setRepository.Add(set);
            id.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void Should_get_set_by_id()
        {
            var logId = AddLog();
            var set = Set(logId);

            var id = _setRepository.Add(set);

            var setFromDb = _setRepository.GetById(id);

            setFromDb.Id.Should().Be(id);
            setFromDb.LogId.Should().Be(set.LogId);
            setFromDb.ExerciseId.Should().Be(set.ExerciseId);
            setFromDb.Weight.Should().Be(set.Weight);
            setFromDb.Reps.Should().Be(set.Reps);
            setFromDb.Position.Should().Be(set.Position);
            setFromDb.DateAdded.Should().BeCloseTo(set.DateAdded);
        }

        [TestMethod]
        public void Should_update_set()
        {
            var logId = AddLog();
            var set = Set(logId);

            var id = _setRepository.Add(set);

            var changedSet = new Set
            {
                Id = id,
                LogId = logId,
                ExerciseId = 2,
                Weight = 100,
                Reps = 12,
                Position = 2,
                DateAdded = DateTime.MaxValue
            };

            _setRepository.Update(changedSet);

            var setFromDb = _setRepository.GetById(id);

            setFromDb.Id.Should().Be(changedSet.Id);
            setFromDb.LogId.Should().Be(changedSet.LogId);
            setFromDb.ExerciseId.Should().Be(changedSet.ExerciseId);
            setFromDb.Weight.Should().Be(changedSet.Weight);
            setFromDb.Reps.Should().Be(changedSet.Reps);
            setFromDb.Position.Should().Be(changedSet.Position);
        }

        [TestMethod]
        public void Should_get_all_sets_for_log_id()
        {
            var logId = AddLog();
            var set = Set(logId);

            _setRepository.Add(set);
            _setRepository.Add(set);

            var sets = _setRepository.GetSetsByLogId(logId).ToList();

            sets.Count.Should().Be(2);
            sets[0].Id.Should().NotBe(sets[1].Id);
            sets[0].LogId.Should().Be(set.LogId);
            sets[0].ExerciseId.Should().Be(set.ExerciseId);
            sets[0].Weight.Should().Be(set.Weight);
            sets[0].Reps.Should().Be(set.Reps);
            sets[0].Position.Should().Be(set.Position);
            sets[0].DateAdded.Should().BeCloseTo(set.DateAdded);
            sets[1].LogId.Should().Be(set.LogId);
            sets[1].ExerciseId.Should().Be(set.ExerciseId);
            sets[1].Weight.Should().Be(set.Weight);
            sets[1].Reps.Should().Be(set.Reps);
            sets[1].Position.Should().Be(set.Position);
            sets[1].DateAdded.Should().BeCloseTo(set.DateAdded);
        }

        private int AddLog()
        {
            return _logRepository.Add(new Log
            {
                UserId = 1,
                Title = "Legs",
                DateAdded = DateTime.Now
            });
        }

        private static Set Set(int logId)
        {
            return new Set
            {
                LogId = logId,
                ExerciseId = 1,
                Weight = 50,
                Reps = 10,
                Position = 1,
                DateAdded = DateTime.UtcNow
            };
        }
    }
}
