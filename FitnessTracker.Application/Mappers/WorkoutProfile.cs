
using AutoMapper;
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Mappers
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<CreateWorkoutRequest, WorkoutEntity>();
            CreateMap<WorkoutEntity, WorkoutDTO>()
                .ForMember(
                dest => dest.WorkoutType,
                opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}
