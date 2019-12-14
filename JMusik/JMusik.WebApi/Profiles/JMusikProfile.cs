using AutoMapper;
using JMusik.Dtos;
using JMusik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMusik.WebApi.Profiles
{
    public class JMusikProfile : Profile
    {
        public JMusikProfile()
        {
            this.CreateMap<Producto, ProductoDto>().ReverseMap();
            this.CreateMap<Perfil, PerfilDto>().ReverseMap();
        }
    }
}
