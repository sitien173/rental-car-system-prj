using AutoMapper;
using NGOT.Common.Interfaces;

namespace NGOT.ApplicationCore;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        ScanAndMap();
    }

    private void ScanAndMap()
    {
        var type = typeof(ISimpleMap<,>);
        var name = type.Name;
        var types = typeof(MapperProfile)
            .Assembly
            .ExportedTypes
            .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == type));

        foreach (var t in types)
        {
            CreateMap(t.GetInterface(name, true)!.GetGenericArguments()[0],
                    t.GetInterface(name, true)!.GetGenericArguments()[1])
                .ForAllMembers(opt => { opt.PreCondition(x => x != null); });

            CreateMap(t.GetInterface(name, true)!.GetGenericArguments()[1],
                    t.GetInterface(name, true)!.GetGenericArguments()[0])
                .ForAllMembers(opt => { opt.PreCondition(x => x != null); });
        }
    }
}