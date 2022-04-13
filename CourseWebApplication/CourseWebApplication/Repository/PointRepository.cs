using CourseWebApplication.IService;
using CourseWebApplication.Models;
using CourseWebApplication.Models.Requests;
using Microsoft.EntityFrameworkCore;
using WebApp.Jwt;

namespace CourseWebApplication.Repository
{
    public class PointRepository : IPointRepository
    {

        private readonly WebAppContext _context;

        public PointRepository(WebAppContext context)
        {
            _context = context;
        }

        public async Task<string> AddPoint(Point point)
        {
            if (await _context.Points.AnyAsync(x => x.Longitude == point.Longitude && x.Latitude == point.Latitude))
                return "Point with such coordinates already exists.";

            await _context.Points.AddAsync(point);

            if (!await SaveChanges())
                return "Failed to add point.";

            return null;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<Point> GetPointByCoordinates(PointRequest pointRequest)
        {
            return await _context.Points.SingleOrDefaultAsync(x => x.Longitude == pointRequest.Longitude && x.Latitude == x.Latitude);
        }

        public async Task<string> AddPointInfo(InfoRequest info)
        {
            var foundPoint = await _context.Points.SingleOrDefaultAsync(x =>x.Id.ToString() == info.Id );
            if (foundPoint == null)
                return "Did not found point with this id";
            var foundPointInfo = await _context.PointsInformation.AnyAsync(x => x.PointId.ToString() == info.Id && x.Message == info.Message && x.Date==info.Date);
            if (foundPointInfo)
                return "Already exists this info";
            await _context.PointsInformation.AddAsync(new PointInformation
            {
                Id = new Guid(),
                PointId=Guid.Parse((ReadOnlySpan<char>)info.Id),
                Date =info.Date,
                Message=info.Message,
            });

            if (!await SaveChanges())
                return "Failed to add point.";

            return null;
        }

        public async Task<(List<string>, string)> GetMessages(GetMessagesRequest messagesRequest )
        {
            string error = null;
            var foundPoint = await _context.Points.SingleOrDefaultAsync(x => x.Id.ToString() == messagesRequest.Id);
            if (foundPoint == null)
            {
                error = "Did not found point with this id";
                return (null, error);
            }

            var pointInformation = _context.PointsInformation.Where(x => x.Id.ToString()==messagesRequest.Id && x.Date == messagesRequest.Date).ToList();    
            var messages=new List<string>();
            foreach(var message in pointInformation)
                messages.Add(message.Message);
            return (messages, null);
        }

        public List<(double, double)> GetAllCoordinates()
        {
            var coordinates=new List<(double, double)>();
            foreach(var point in _context.Points)
            {
                coordinates.Add((point.Latitude, point.Longitude));
            }
            return coordinates;
        }
    }
}
