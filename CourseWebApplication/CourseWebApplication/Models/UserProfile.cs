using AutoMapper;
using CourseWebApplication.Models;
using CourseWebApplication.Models.Requests;
using WebApp.Models;
using WebApp.Models.Requests;

namespace WebApp.Jwt
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<ChangePasswordRequest, User>();
            CreateMap<PointRequest, Point>();
        }
    }
}
