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
    public class LogRepositoryTests
    {
        private string _connectionString;
        private ILogRepository _repository;

        [TestInitialize]
        public void BeforeEach()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
            _connectionString = configuration.GetSection("ConnectionStrings")["TrainingTracker"];

            _repository = new LogRepository(_connectionString);
        }

        [TestCleanup]
        public void AfterEach()
        {
            const string command = @"DELETE FROM [Log]";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(command, commandType: CommandType.Text);
            }
        }

        [TestMethod]
        public void Should_add_log_and_return_id()
        {
            var log = TestLog;
            var id = _repository.Add(log);
            id.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void Should_return_log_given_log_exists()
        {
            var log = TestLog;

            var id = _repository.Add(log);
            var newLog = _repository.GetById(id);

            newLog.UserId.Should().Be(log.UserId);
            newLog.Title.Should().Be(log.Title);
            newLog.DateAdded.Should().BeSameDateAs(log.DateAdded);
        }

        [TestMethod]
        public void Should_return_all_logs_for_a_user_given_valid_user_id()
        {
            var log = TestLog;
            _repository.Add(log);
            _repository.Add(log);

            var logs = _repository.GetLogsByUserId(log.UserId);

            logs.ToList().Count().Should().Be(2);
        }

        [TestMethod]
        public void Should_return_empty_list_when_user_has_zero_logs()
        {
            var logs = _repository.GetLogsByUserId(1);
            logs.Should().NotBeNull();
        }

        private static Log TestLog => new Log
        {
            UserId = 1,
            Title = "Legs",
            DateAdded = DateTime.Now
        };
    }
}
