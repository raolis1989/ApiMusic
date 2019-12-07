using AutoMapper;
using JMusik.Dtos;
using JMusik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMusik.WebApi.Profiles
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            this.CreateMap<Producto, ProductoDto>().ReverseMap();
        }
    }
}
