using AutoMapper;
using JWTAuthentication.Entities;
using JWTAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleModel, Role>()
                  .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                  .ForMember(x => x.RoleTitle, o => o.MapFrom(m => m.RoleTitle))
                  .ForMember(x => x.RoleName, o => o.MapFrom(m => m.RoleName));

            CreateMap<Role, RoleModel>()
                .ForMember(x => x.Id, o => o.MapFrom(m => m.Id))
                .ForMember(x => x.RoleTitle, o => o.MapFrom(m => m.RoleTitle))
                .ForMember(x => x.RoleName, o => o.MapFrom(m => m.RoleName));

        }
    }
}
