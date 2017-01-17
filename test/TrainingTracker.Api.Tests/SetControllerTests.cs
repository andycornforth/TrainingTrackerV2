using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrainingTracker.Api.Controllers;
using TrainingTracker.Api.Models;
using TrainingTracker.Service.Services;

namespace TrainingTracker.Api.Tests
{
    [TestClass]
    public class SetControllerTests
    {
        private SetController _controller;
        private Mock<ISetService> _setServiceMock;
        private Mock<IMapper> _mapperMock;

        [TestInitialize]
        public void BeforeEach()
        {
            _setServiceMock = new Mock<ISetService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new SetController(_setServiceMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public void Should_create_set_with_valid_post_request()
        {
            _setServiceMock.Setup(x => x.Add(It.IsAny<Service.Models.Set>())).Returns(1);

            var set = new Set
            {
                LogId = 1,
                ExerciseId = 1,
                Weight = 100,
                Reps = 12,
                Position = 1
            };

            var id = _controller.Post(set);

            id.Should().Be(1);
            _setServiceMock.Verify(x => x.Add(It.IsAny<Service.Models.Set>()), Times.Once);
        }
    }
}
