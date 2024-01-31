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
        //[Authorize(Roles = "User")]
        //
        public IActionResult AddReport(Report report)
        {
            try
            {
                report.ReportTime = DateTime.Now;
               reportService.AddReport(report);
                return StatusCode(200, report);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
