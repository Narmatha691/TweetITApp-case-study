using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTwitterAPI.DTO;
using MyTwitterAPI.Entities;
using MyTwitterAPI.Services;

namespace MyTwitterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly ILog _logger;

        public ReportController(IReportService reportService, IMapper mapper, IConfiguration configuration, ILog logger)
        {
            this.reportService = reportService;
            this._mapper = mapper;
            this.configuration = configuration;
            this._logger = logger;
        }
        [HttpPost, Route("AddReport")]
        [Authorize(Roles = "User")]
        //
        public IActionResult AddReport(ReportWithoutIDDTO report)
        {
            try
            {
                Report reportdto = _mapper.Map<Report>(report);
                reportdto.ReportTime = DateTime.Now;
                reportService.AddReport(reportdto);
                return StatusCode(200, report);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetUnReadReports")]
        [Authorize(Roles = "Admin")]
        //
        public IActionResult GetUnReadReports()
        {
            try
            {
                List<ReportDTO> reports = reportService.GetUnReadReports();
                return StatusCode(200, reports);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetReadReports")]
        [Authorize(Roles = "Admin")]
        //
        public IActionResult GetReadReports()
        {
            try
            {
                List<ReportDTO> reports = reportService.GetReadReports();
                return StatusCode(200, reports);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut, Route("MarkAsRead/{reportId}")]
        [Authorize(Roles = "Admin")]
        //
        public IActionResult MarkAsRead(int reportId)
        {
            try
            {
                reportService.MarkAsRead(reportId);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
