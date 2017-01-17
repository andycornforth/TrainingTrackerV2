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
    public class SetController : Controller
    {
        private readonly ISetService _setService;
        private readonly IMapper _mapper;

        public SetController(ISetService setService, IMapper mapper)
        {
            if (setService == null) throw new ArgumentNullException(nameof(setService));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _setService = setService;
            _mapper = mapper;
        }

        [HttpPost]
        public int Post([FromBody]Set request)
        {
            var set = _mapper.Map<Set, Service.Models.Set>(request);
            return _setService.Add(set);
        }

        //[HttpGet("{id}")]
        //public Log Get(int id)
        //{
        //    var log = _setService.GetById(id);
        //    return _mapper.Map<Service.Models.Log, Log>(log);
        //}

        //[HttpGet("user/{id}")]
        //public IEnumerable<Log> GetLogsByUserId(int id)
        //{
        //    var logs = _setService.GetLogsByUserId(id).ToList();
        //    return _mapper.Map<IList<Service.Models.Log>, IList<Log>>(logs);
        //}
    }
}
