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
            this.CreateMap<Orden, OrdenDto>()
                .ForMember(u => u.Usuario, p => p.MapFrom(m => m.Usuario.Username))
                .ReverseMap()
                .ForMember(u=>u.Usuario, p=>p.Ignore());
            this.CreateMap<DetalleOrden, DetalleOrdenDto>()
                .ForMember(u => u.Producto, p => p.MapFrom(u => u.Producto.Nombre))
                .ReverseMap()
                .ForMember(u => u.Producto, p => p.Ignore());
        }
    }
}
