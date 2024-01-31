using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyTwitterAPI.Entities
{
    [Table("Reports")]
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        [Required]
        public string ReportUserId { get; set; }

        [ForeignKey("ReportUserId")]
        public User ReportUser { get; set; }

        [Required]
        public string ReportContent { get; set; }

        public DateTime ReportTime { get; set; }

        public int Read { get; set; } = 0;
    }
}
