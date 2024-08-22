using AutoMapper;
using BarberShop_Api.Application.DataTransfer;
using BarberShop_Api.Domain.Models;

namespace BarberShop_Api.Application.Mapping
{
    public abstract class Mapping<TSource, TDest> : Profile where TSource : class where TDest : class
    {
        public Mapping()
        {
            CreateMap<TSource, TDest>();
        }
    }

    public class CompanyMapping : Mapping<CompanyModel, CompanyDataTransfer>
    {
    }

    public class CustomerMapping : Mapping<CompanyModel, CustomerDataTransfer>
    {
    }

    public class HaircutMapping : Mapping<HaircutModel, HaircutDataTransfer>
    {
    }

    public class OrdersMapping : Mapping<OrdersModel, OrdersDataTransfer>
    {
    }
}
