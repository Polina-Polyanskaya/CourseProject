using Microsoft.AspNetCore.Mvc;
using IpInfo;
using CourseWebApplication.Models.Requests;
using CourseWebApplication.Models.Responses;
using GeoCoordinatePortable;
using CourseWebApplication.IService;
using CourseWebApplication.Models;

namespace CourseWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly IPointRepository _pointRepository;

        public GeoController(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository;
        }

        [HttpPost("City")]
        public async Task<ActionResult> GetCountry(IpRequest ip)
        {
            using var client = new HttpClient();
            var api = new IpInfoApi("d87cc7ce60086c", client);
            string country = "";
            string city = "";
            try
            {
                var dictionary = await api.GetInformationByIpsAsync(new[]
    {
    ip.Ip,
}, new CancellationToken());

                foreach (var pair in dictionary)
                {
                    country = pair.Value.Country;
                    city = pair.Value.City;
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No such ip");
            }
            return Ok(new IpResponse { Country = country, City = city });
        }


        [HttpPost("NearIp")]
        public async Task<ActionResult> NearCoordinateIp(IpRequest ip)
        {
            using var client = new HttpClient();
            var api = new IpInfoApi("d87cc7ce60086c", client);
            double latitude = 0;
            double longitude = 0;
            try
            {
                var dictionary = await api.GetInformationByIpsAsync(new[]
    {
    ip.Ip,
}, new CancellationToken());

                foreach (var pair in dictionary)
                {
                    string loc = pair.Value.Loc;
                    if (!loc.Contains(','))
                        return BadRequest("Private ip");
                    string[] coordinates = loc.Split(',');
                    latitude = Convert.ToDouble(coordinates[0].Replace('.', ','));
                    longitude = Convert.ToDouble(coordinates[1].Replace('.', ','));
                    Console.WriteLine(latitude+" "+longitude);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("No such ip");
            }

            var coordinatesOfPoints = _pointRepository.GetAllCoordinates();
            if (coordinatesOfPoints.Count != 0)
            {
                (double, double)? foundPoint = null;
                FindNearCoord(coordinatesOfPoints, latitude, longitude, ref foundPoint);
                if (foundPoint != null)
                    return Ok(new NearCoordResponse { FoundCoord = new List<double> { foundPoint.Value.Item1, foundPoint.Value.Item2 } });
            }
            return NotFound();
        }

        [HttpPost("NearCoord")]
        public async Task<ActionResult> NearCoordinate(PointRequest pointRequest)
        {            
            var coordinatesOfPoints = _pointRepository.GetAllCoordinates();
            if (coordinatesOfPoints.Count != 0)
            {
                (double, double)? foundPoint = null;
                FindNearCoord(coordinatesOfPoints, pointRequest.Latitude, pointRequest.Longitude, ref foundPoint);
                if(foundPoint!=null)
                    return Ok(new NearCoordResponse { FoundCoord = new List<double> { foundPoint.Value.Item1, foundPoint.Value.Item2 } });
            }
            return NotFound();
        }

        void FindNearCoord(List<(double, double)> coordinates, double latitude, double longitude, ref (double,double)? foundPoint)
        {
            var personCoord = new GeoCoordinate(latitude, longitude);
            foreach (var point in coordinates)
            {
                var pointCoord = new GeoCoordinate(point.Item1, point.Item2);
                if (personCoord.GetDistanceTo(pointCoord) < 1500)
                {
                    foundPoint= (point.Item1, point.Item2);
                }
            }   
        }

        [HttpPost("AllMessages")]
        public async Task<ActionResult> GetAllowedMessages(PointRequest pointRequest)
        {
            Point point = await _pointRepository.GetPointByCoordinates(pointRequest);

            if (point == null)
                return BadRequest("No point with such coordinates");

            var (messages, error) = await _pointRepository.GetMessages(new GetMessagesRequest { Id = point.Id.ToString(), Date = DateTime.Today.ToShortDateString() });

            if (error != null)
                return BadRequest(error);

            return Ok(new GetMessagesResponse { Messages = messages });
        }
    }
}
