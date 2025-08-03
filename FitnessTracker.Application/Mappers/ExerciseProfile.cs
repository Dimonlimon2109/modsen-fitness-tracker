
using AutoMapper;
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Mappers
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<ExerciseDTO, Exercise>()
                .ReverseMap();
        }
    }
}
