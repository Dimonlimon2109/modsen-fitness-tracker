
using AutoMapper;
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Mappers
{
    public class SetProfile : Profile
    {
        public SetProfile()
        {
            CreateMap<SetDTO, Set>();
        }
    }
}
