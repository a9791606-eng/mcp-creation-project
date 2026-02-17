namespace McpEventPlanner.Models
{
    /// <summary>
    /// מודל קלט לאירוע שהמשתמש יכניס
    /// </summary>
    public class EventInput
    {
        /// <summary>
        /// שם האירוע
        /// </summary>
        public string EventName { get; set; } = string.Empty;

        /// <summary>
        /// תיאור הקהל היעד (מורים, תלמידות, הורים וכו')
        /// </summary>
        public string TargetAudience { get; set; } = string.Empty;

        /// <summary>
        /// תאריך ושעת האירוע
        /// </summary>
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// מיקום האירוע
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// מטרת האירוע - מה חשוב להעביר
        /// </summary>
        public string EventObjective { get; set; } = string.Empty;

        /// <summary>
        /// משך האירוע בדקות
        /// </summary>
        public int DurationMinutes { get; set; } = 120;

        /// <summary>
        /// תקציב משוער (אם קיים)
        /// </summary>
        public decimal? Budget { get; set; }

        /// <summary>
        /// מספר משוער של משתתפים
        /// </summary>
        public int ExpectedAttendees { get; set; } = 50;
    }
}
