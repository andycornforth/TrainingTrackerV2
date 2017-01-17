using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrainingTracker.Service.Models;
using TrainingTracker.Service.Repositories;
using TrainingTracker.Service.Services;

namespace TrainingTracker.Service.Tests.Services
{
    [TestClass]
    public class SetServiceTests
    {
        private ISetService _service;
        private Mock<ISetRepository> _setRepositoryMock;

        [TestInitialize]
        public void BeforeEach()
        {
            _setRepositoryMock = new Mock<ISetRepository>();
            _service = new SetService(_setRepositoryMock.Object);
        }

        [TestMethod]
        public void Should_add_set_to_repository()
        {
            _setRepositoryMock.Setup(x => x.Add(It.IsAny<Set>())).Returns(1);
            var id = _service.Add(new Set());
            id.Should().Be(1);
            _setRepositoryMock.Verify(x => x.Add(It.IsAny<Set>()), Times.Once());
        }

        [TestMethod]
        public void Should_set_DateAdded_to_utc_now_given_it_is_not_provided()
        {
            var set = new Set();
            _setRepositoryMock.Setup(x => x.Add(It.IsAny<Set>())).Returns(1);
            _service.Add(set);
            set.DateAdded.Should().BeCloseTo(DateTime.UtcNow);
        }

        [TestMethod]
        public void Should_update_set_to_repository()
        {
            _service.Update(new Set());
            _setRepositoryMock.Verify(x => x.Update(It.IsAny<Set>()), Times.Once);
        }

        [TestMethod]
        public void Should_return_set_given_valid_id()
        {
            _setRepositoryMock.Setup(x => x.GetById(1)).Returns(new Set());
            var set = _service.GetById(1);
            set.Should().NotBeNull();
            _setRepositoryMock.Verify(x => x.GetById(1), Times.Once);
        }

        [TestMethod]
        public void Should_return_sets_given_valid_log_id()
        {
            _setRepositoryMock.Setup(x => x.GetSetsByLogId(1)).Returns(new List<Set>());
            var sets = _service.GetSetsByLogId(1);
            sets.Should().NotBeNull();
            _setRepositoryMock.Verify(x => x.GetSetsByLogId(1), Times.Once);
        }
    }
}
