using AutoMapper;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;

namespace MyTwitterAPI.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportWithoutIDDTO>();
            CreateMap<ReportWithoutIDDTO, Report>();
            CreateMap<ReportDTO, Report>();
            CreateMap<Report, ReportDTO>();
        }
    }
}
