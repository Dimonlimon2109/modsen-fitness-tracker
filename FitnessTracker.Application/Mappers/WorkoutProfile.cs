
using AutoMapper;
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Mappers
{
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
            CreateMap<CreateWorkoutRequest, WorkoutEntity>();
        }
    }
}
