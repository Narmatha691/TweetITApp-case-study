using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTwitterAPI.Database;
using MyTwitterAPI.DTO;
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
        List<ReportDTO> IReportService.GetUnReadReports()
        {
            try
            {
                var reports = context.Reports
                    .Where(report => report.Read == 0)
                .ToList();

                return _mapper.Map<List<ReportDTO>>(reports);
            }
            catch (Exception)
            {

                throw;
            }
        }
        List<ReportDTO> IReportService.GetReadReports()
        {
            try
            {
                var reports = context.Reports
                    .Where(report => report.Read == 1)
                .ToList();

                return _mapper.Map<List<ReportDTO>>(reports);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void MarkAsRead(int reportId)
        {
            try
            {
                var report = context.Reports.Find(reportId);
                report.Read = 1;
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
