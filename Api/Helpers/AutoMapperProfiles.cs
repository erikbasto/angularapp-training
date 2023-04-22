using System;
using System.Linq;
using Api.DTOs;
using Api.Entities;
using Api.Extensions;
using AutoMapper;

namespace Api.Helpers
{
    public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<AppUser, MemberDto>()
				.ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
				.ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
				.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));

			CreateMap<Photo, PhotoDto>();
			CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));
            CreateMap<Message, MessageDto>()
				.ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url))
				.ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url));
			CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
            CreateMap<DateTime?, DateTime?>().ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);
        }
	}
}

