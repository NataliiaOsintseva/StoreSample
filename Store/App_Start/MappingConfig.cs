using Store.Domain.Entities;
using Store.Infrastructure.Binders;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.App_Start
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Product, EditProductViewModel>()
                .ReverseMap();
            });
        }
    }
}