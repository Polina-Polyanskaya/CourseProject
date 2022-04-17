using AutoMapper;
using CourseWebApplication.IService;
using CourseWebApplication.Models;
using CourseWebApplication.Models.Requests;
using CourseWebApplication.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CourseWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointInfoController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IPointRepository _pointRepository;

        public PointInfoController(IMapper mapper, IPointRepository pointRepository)
        {
            _mapper = mapper;
            _pointRepository = pointRepository;
        }

        [HttpPost("AddPoint")]
        public async Task<ActionResult> AddPoint(PointRequest pointRequest)
        {
            var model = _mapper.Map<Point>(pointRequest);
            string error = await _pointRepository.AddPoint(model);

            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);

            return Ok();
        }

        [HttpPost("Info")]
        public async Task<ActionResult> AddInfo(InfoRequest infoRequest)
        {
            if (!Guid.TryParse((ReadOnlySpan<char>)infoRequest.Id.ToString(), out _))
            {
                return BadRequest("It is not Guid");
            }

            if (!DateTime.TryParse(infoRequest.Date, out _))
            {
                return BadRequest("It is not date");
            }

            string error = await _pointRepository.AddPointInfo(infoRequest);

            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);

            return Ok();
        }


        [HttpPost("GetPoint")]
        public async Task<ActionResult> GetPoint(PointRequest pointRequest)
        {
            Point point = await _pointRepository.GetPointByCoordinates(pointRequest);

            if (point == null)
                return NotFound();

            return Ok(new PointResponse { Id = point.Id });
        }

        [HttpPost("GetMessages")]
        public async Task<ActionResult> GetMessages(GetMessagesRequest messagesRequest)
        {
            var (messages, error) = await _pointRepository.GetMessages(messagesRequest);

            if (error != null)
                return BadRequest(error);

            return Ok(new GetMessagesResponse { Messages = messages });
        }


        [HttpGet]
        public ActionResult GetAllCoordinates()
        {
            var coord = _pointRepository.GetAllCoordinates();
            var latitudes = new List<double>();
            var longitudes = new List<double>();
            foreach(var item in coord)
            {
                latitudes.Add(item.Item1);
                longitudes.Add(item.Item2);
            }    

            return Ok(new GetAllCoordinatesResponse {Latitude=latitudes, Longitude=longitudes  });
        }
    }
}
