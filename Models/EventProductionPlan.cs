namespace McpEventPlanner.Models
{
    /// <summary>
    /// מודל הפקת האירוע המלא
    /// </summary>
    public class EventProductionPlan
    {
        /// <summary>
        /// פרטי האירוע המקורי
        /// </summary>
        public EventInput EventDetails { get; set; } = new();

        /// <summary>
        /// רעיונות לתוכניות ופעילויות
        /// </summary>
        public ProgrammingIdeas Programming { get; set; } = new();

        /// <summary>
        /// הצעות מזון וקינוח
        /// </summary>
        public FoodSuggestions Food { get; set; } = new();

        /// <summary>
        /// ברנדינג וסטייל האירוע
        /// </summary>
        public BrandingStrategy Branding { get; set; } = new();

        /// <summary>
        /// תכנית מפורטת לאירוע
        /// </summary>
        public DetailedTimelineAndPresentation Timeline { get; set; } = new();

        /// <summary>
        /// טיפים לביצוע מוצלח
        /// </summary>
        public List<string> SuccessTips { get; set; } = new();
    }

    /// <summary>
    /// רעיונות לתוכניות ופעילויות באירוע
    /// </summary>
    public class ProgrammingIdeas
    {
        public List<string> OpeningActivities { get; set; } = new();
        public List<string> MainProgram { get; set; } = new();
        public List<string> SpecialMoments { get; set; } = new();
        public List<string> ClosingActivities { get; set; } = new();
        public string NarrativeTheme { get; set; } = string.Empty;
    }

    /// <summary>
    /// הצעות מזון מתאימות לציבור החרדי
    /// </summary>
    public class FoodSuggestions
    {
        public List<string> KosherOptions { get; set; } = new();
        public List<string> Desserts { get; set; } = new();
        public List<string> Beverages { get; set; } = new();
        public List<string> HealthyAlternatives { get; set; } = new();
        public string MenuTheme { get; set; } = string.Empty;
    }

    /// <summary>
    /// ברנדינג וסטייל
    /// </summary>
    public class BrandingStrategy
    {
        public string EventTitle { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public ColorPalette Colors { get; set; } = new();
        public LogoIdea Logo { get; set; } = new();
        public Souvenir Souvenir { get; set; } = new();
        public string OverallStyle { get; set; } = string.Empty;
    }

    /// <summary>
    /// פלטת צבעים
    /// </summary>
    public class ColorPalette
    {
        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;
        public string AccentColor { get; set; } = string.Empty;
        public string Meaning { get; set; } = string.Empty;
    }

    /// <summary>
    /// רעיון לוגו
    /// </summary>
    public class LogoIdea
    {
        public string Concept { get; set; } = string.Empty;
        public string VisualDescription { get; set; } = string.Empty;
        public List<string> DesignTips { get; set; } = new();
    }

    /// <summary>
    /// מזכרת לאירוע
    /// </summary>
    public class Souvenir
    {
        public string ItemDescription { get; set; } = string.Empty;
        public string PrintingIdea { get; set; } = string.Empty;
        public List<string> PersonalizationOptions { get; set; } = new();
    }

    /// <summary>
    /// תוכנית זמנים מפורטת והצגה
    /// </summary>
    public class DetailedTimelineAndPresentation
    {
        public List<TimelineItem> Schedule { get; set; } = new();
        public PresentationOutline Presentation { get; set; } = new();
        public List<string> DecorIdeas { get; set; } = new();
        public List<string> TechnicalRequirements { get; set; } = new();
    }

    /// <summary>
    /// פריט בתוכנית הזמנים
    /// </summary>
    public class TimelineItem
    {
        public string Time { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Responsibility { get; set; } = string.Empty;
    }

    /// <summary>
    /// תוכנית המצגת
    /// </summary>
    public class PresentationOutline
    {
        public string OpeningMessage { get; set; } = string.Empty;
        public List<string> MainPoints { get; set; } = new();
        public List<string> EmotionalMoments { get; set; } = new();
        public string ClosingMessage { get; set; } = string.Empty;
        public List<string> SpeakingTips { get; set; } = new();
    }
}
