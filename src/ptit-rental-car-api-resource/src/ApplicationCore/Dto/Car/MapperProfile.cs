using AutoMapper;
using NGOT.ApplicationCore.Entities;

namespace NGOT.ApplicationCore.Dto.Car;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateCarRequest, Entities.Car>()
            .ForMember(d => d.CarImages,
                opt =>
                    opt.MapFrom(s => s.Images.Select(x => new CarImage { Image = x })))
            .ForMember(d => d.CarFeatures, opt =>
                opt.MapFrom(s => s.FeatureIds.Select(x => new CarFeature { FeatureId = x })))
            .ForMember(d => d.CarAdditionalFees, opt =>
                opt.MapFrom(s => s.AdditionalFeeIds.Select(x => new CarAdditionalFees { AdditionalFeesId = x })))
            .ForMember(d => d.CarRentalDocuments, opt =>
                opt.MapFrom(s => s.RentalDocumentIds.Select(x => new CarRentalDocuments { RentalDocumentId = x })));

        CreateMap<Entities.Car, CarResponse>()
            .ForMember(x => x.Features, opt => opt.MapFrom(x => x.CarFeatures.Select(y => y.Feature)))
            .ForMember(x => x.Images, opt => opt.MapFrom(x => x.CarImages.Select(y => y.Image)))
            .ForMember(x => x.AdditionalFees,
                opt => opt.MapFrom(x => x.CarAdditionalFees.Select(y => y.AdditionalFees)))
            .ForMember(x => x.RentalDocuments,
                opt => opt.MapFrom(x => x.CarRentalDocuments.Select(y => y.RentalDocuments)));
    }
}