using System;
using AutoMapper;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Dal.Entities;

namespace Test.WebApplication.Api.Infrastructure.AutoMapperProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionDto, Transaction>()
                .ForMember(d => d.TransactionId           , opt => opt.Ignore())
                .ForMember(d => d.TransactionIdentificator, opt => opt.MapFrom(s => s.TransactionIdentificator))
                .ForMember(d => d.Amount                  , opt => opt.MapFrom(s => s.Amount))
                .ForMember(d => d.CurrencyCodeId          , opt => opt.MapFrom(s => s.CurrencyCodeId))
                .ForMember(d => d.TransactionStatusId     , opt => opt.MapFrom(s => s.TransactionStatusId))
                .ForMember(d => d.TransactionDate         , opt => opt.MapFrom(s => s.TransactionDate))
                .ForMember(d => d.CreatedBy               , opt => opt.MapFrom(s => Guid.Empty))
                .ForMember(d => d.CreatedDateTime         , opt => opt.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.ModifiedBy              , opt => opt.MapFrom(s => Guid.Empty))
                .ForMember(d => d.ModifiedDateTime        , opt => opt.MapFrom(s => DateTime.UtcNow))
                .ReverseMap();
        }
    }
}
