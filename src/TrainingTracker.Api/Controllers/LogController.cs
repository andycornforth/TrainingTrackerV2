using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingTracker.Api.Models;
using TrainingTracker.Service.Services;

namespace TrainingTracker.Api.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public LogController(ILogService logService, IMapper mapper)
        {
            if (logService == null) throw new ArgumentNullException(nameof(logService));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _logService = logService;
            _mapper = mapper;
        }

        [HttpPost]
        public int Post([FromBody]Log request)
        {
            var log = _mapper.Map<Log, Service.Models.Log>(request);
            return _logService.Add(log);
        }

        [HttpGet("{id}")]
        public Log Get(int id)
        {
            var log = _logService.GetById(id);
            return _mapper.Map<Service.Models.Log, Log>(log);
        }

        [HttpGet("user/{id}")]
        public IEnumerable<Log> GetLogsByUserId(int id)
        {
            var logs = _logService.GetLogsByUserId(id).ToList();
            return _mapper.Map<IList<Service.Models.Log>, IList<Log>>(logs);
        }
    }
}
