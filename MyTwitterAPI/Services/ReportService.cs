using AutoMapper;
using MyTwitterAPI.Database;
using MyTwitterAPI.Entities;

namespace MyTwitterAPI.Services
{
    public class ReportService:IReportService
    {
        private readonly MyContext context;
        private readonly IMapper _mapper;

        public ReportService(MyContext context, IMapper mapper)
        {
            this.context = context;
            this._mapper = mapper;
        }
        public void AddReport(Report report)
        {
            context.Reports.Add(report);
            context.SaveChanges();
        }
    }
}
