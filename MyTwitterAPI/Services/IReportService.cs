using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;

namespace MyTwitterAPI.Services
{
    public interface IReportService
    {
        void AddReport(Report report);
        List<ReportDTO> GetUnReadReports();
        List<ReportDTO> GetReadReports();
        void MarkAsRead(int reportId);
    }
}
