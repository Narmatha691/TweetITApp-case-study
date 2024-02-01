namespace MyTwitterAPI.DTO
{
    public class ReportDTO
    {
        public int ReportId { get; set; }
        public string SenderId { get; set; }

        public string ReportUserId { get; set; }

        public string ReportContent { get; set; }


        public DateTime ReportTime { get; set; }

        public int Read { get; set; } = 0;
    }
}

