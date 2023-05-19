using AutoMapper;
using SimApi.Data.Domain;

namespace SimApi.Schema;

public class MapperProfile : Profile 
{
    public MapperProfile()
    {
        CreateMap<Staff, StaffResponse>();
        CreateMap<StaffRequest, Staff>();


    }


}
