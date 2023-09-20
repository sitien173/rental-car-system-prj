using AutoMapper;
using NGOT.API.Converters;
using NGOT.Common.Models;

namespace NGOT.API;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<IFormFile, UploadFileRequest>().ConvertUsing<FormFileToUploadFileRequestConverter>();
    }
}