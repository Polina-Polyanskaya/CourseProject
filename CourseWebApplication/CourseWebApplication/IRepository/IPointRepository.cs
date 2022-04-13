using CourseWebApplication.Models;
using CourseWebApplication.Models.Requests;

namespace CourseWebApplication.IService
{
    public interface IPointRepository
    {
        Task<string> AddPoint(Point point);

        Task<bool> SaveChanges();

        Task<Point> GetPointByCoordinates(PointRequest pointRequest);

        Task<string> AddPointInfo(InfoRequest info);

        Task<(List<string>, string)> GetMessages(GetMessagesRequest messagesRequest);

        List<(double, double)> GetAllCoordinates();
    }
}
