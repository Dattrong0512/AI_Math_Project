using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.AnswerDto;
using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Application.Dto.QuestionDto;
using AIMathProject.Application.Dto.UserDto;
using AIMathProject.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserDto>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Chapter, ChapterDto>();
        }


    }
}
