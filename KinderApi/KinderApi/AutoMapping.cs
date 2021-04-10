using AutoMapper;
using KinderApi.DTOs;
using KinderApi.Models;

namespace KinderApi
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserToReturnDto>();
        }
    }
}