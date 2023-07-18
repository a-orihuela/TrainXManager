using AutoMapper;
using CleanArchitecture.Application.Features.AppOptions.Command.Create;
using CleanArchitecture.Application.Features.AppOptions.Command.Delete;
using CleanArchitecture.Application.Features.AppOptions.Command.Update;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //TODO: Implement mapping....
            //CreateMap<Video, VideosVideoModel>();
            //CreateMap<Video, VideosWithIncludesVideoModel>()
            //    .ForMember(p => p.DirectorNombreCompleto, x => x.MapFrom(a => a.Director!.NombreCompleto))
            //    .ForMember(p => p.StreamerNombre, x => x.MapFrom(a => a.Streamer!.Nombre))
            //    .ForMember(p => p.Actores, x => x.MapFrom(a => a.Actores));
            //CreateMap<Streamer, StreamersVm>();
            CreateMap<CreateAppOptionsCommand, AppOption>();
            CreateMap<UpdateAppOptionsCommand, AppOption>();
            CreateMap<DeleteAppOptionsCommand, AppOption>();
        }
    }
}
