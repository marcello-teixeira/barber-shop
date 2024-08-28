using AutoMapper;
using BarberShop_Api.Application.DataTransfer;
using BarberShop_Api.Domain.Models;

namespace BarberShop_Api.Application.Mapping
{
    public abstract class Mapping<TSource, TDest> : Profile where TSource : class where TDest : class
    {
        // <summary>
        //  Makes a DTO with dependecy injection to generic entities
        // </summary>
        public Mapping()
        {
            // TSource is origin and TDest is receiver DTO.
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
