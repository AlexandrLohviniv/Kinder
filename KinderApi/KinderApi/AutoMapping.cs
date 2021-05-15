using System.Linq;
using AutoMapper;
using KinderApi.DTOs;
using KinderApi.Models;

namespace KinderApi
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserToReturnDto>()
                .ForMember(dest => dest.mainPhotoUrl,
                            opt => opt.MapFrom(src => src.Images.FirstOrDefault(img => img.IsMain).ImgPath));
            CreateMap<RegisterUserDto, User>();
            CreateMap<ImageDto, Image>();
            CreateMap<Image, PhotoToReturnDto>();
            CreateMap<Preference, PreferenceDto>().ReverseMap();
        }
    }
}