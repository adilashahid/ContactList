using System.ComponentModel.DataAnnotations;

namespace ContactList.Model
{
    public class ActivityLog
    {
        [Key]
        public int Id { get; set; }
        public string Action { get; set; }
        public string EntityName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
    }
}
