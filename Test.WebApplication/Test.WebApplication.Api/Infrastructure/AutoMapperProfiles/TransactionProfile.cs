using System;
using System.Globalization;
using AutoMapper;
using Test.WebApplication.Api.ViewModels;
using Test.WebApplication.Commands.Models;
using Test.WebApplication.Common.Dtos;
using Test.WebApplication.Common.Enums;
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

            CreateMap<SerializableTransaction, TransactionDto>()
                .ForMember(d => d.TransactionIdentificator, opt => opt.MapFrom(s => s.TransactionIdentificator))
                .ForMember(d => d.Amount                  , opt => opt.MapFrom(s => decimal.Parse(s.PaymentDetails.Amount)))
                .ForMember(d => d.CurrencyCodeId          , opt => opt.MapFrom(s => Enum.Parse<CurrencyCode>(s.PaymentDetails.CurrencyCode)))
                .ForMember(d => d.TransactionStatusId     , opt => opt.MapFrom(s => Enum.Parse<TransactionStatusValue>(s.Status)))
                .ForMember(d => d.TransactionDate         , opt => opt.MapFrom((s,d) =>
                {
                    DateTime result;

                    if (DateTime.TryParseExact(s.TransactionDate, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out result) 
                     || DateTime.TryParseExact(s.TransactionDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    {
                        return result;
                    }

                    throw new FormatException("Incorrect date time format");
                }));

            CreateMap<SerializableTransaction, InvalidTransaction>()
                .ForMember(d => d.TransactionIdentificator, opt => opt.MapFrom(s => s.TransactionIdentificator))
                .ForMember(d => d.Status                  , opt => opt.MapFrom(s => s.Status))
                .ForMember(d => d.TransactionDate         , opt => opt.MapFrom(s => s.TransactionDate))
                .ForMember(d => d.Amount                  , opt => opt.MapFrom(s => s.PaymentDetails.Amount))
                .ForMember(d => d.CurrencyCode            , opt => opt.MapFrom(s => s.PaymentDetails.CurrencyCode));

            CreateMap<TransactionDto, TransactionViewModel>()
                .ForMember(d => d.Id     , opt => opt.MapFrom(s => s.TransactionIdentificator))
                .ForMember(d => d.Payment, opt => opt.MapFrom(s => s.Amount.ToString(CultureInfo.InvariantCulture) + " " + s.CurrencyCodeId))
                .ForMember(d => d.Status , opt => opt.MapFrom(s => s.TransactionStatusId.ToUnifiedFormat()));
        }
    }
}
