using McpEventPlanner.Models;
using System.Text.Json;

namespace McpEventPlanner.Services
{
    /// <summary>
    /// שירות עיקרי לתכנון אירועים בקהילה החרדית - מופעל על ידי Gemini
    /// </summary>
    public class EventPlanningOrchestrator
    {
        private readonly HttpClient _httpClient;
        private const string GEMINI_API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent";
        private readonly string _apiKey;

        public EventPlanningOrchestrator(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["Gemini:ApiKey"] ?? string.Empty;
        }

        /// <summary>
        /// יוצר תכנית הפקה שלמה לאירוע
        /// </summary>
        public async Task<EventProductionPlan> GenerateEventPlan(EventInput eventInput)
        {
            var plan = new EventProductionPlan { EventDetails = eventInput };

            // יצור רעיונות לתוכניות
            plan.Programming = await GenerateProgramming(eventInput);

            // יצור הצעות מזון
            plan.Food = await GenerateFoodSuggestions(eventInput);

            // יצור ברנדינג
            plan.Branding = await GenerateBrandingStrategy(eventInput);

            // יצור תוכנית מפורטת
            plan.Timeline = await GenerateDetailedTimeline(eventInput, plan);

            // יצור טיפים
            plan.SuccessTips = await GenerateSuccessTips(eventInput);

            return plan;
        }

        private async Task<ProgrammingIdeas> GenerateProgramming(EventInput eventInput)
        {
            var prompt = $@"
אתה מתכנן אירועים מעולה בקהילה החרדית. עבור האירוע הזה, צור רעיונות לתוכניות פעילויות:

📋 פרטי האירוע:
- שם: {eventInput.EventName}
- קהל יעד: {eventInput.TargetAudience}
- מטרה/עיקרון: {eventInput.EventObjective}
- משך: {eventInput.DurationMinutes} דקות
- מיקום: {eventInput.Location}
- מספר משתתפים: {eventInput.ExpectedAttendees}

בנה רעיונות שהם:
✓ משמעותיים וערכיים לקהילה חרדית
✓ מותאמים למטרה של הארוע
✓ מעוררי השראה ורגשונות

צור בדיוק פורמט זה (JSON חכם בעברית):
{{
  ""openingActivities"": [""רעיון 1"", ""רעיון 2"", ""רעיון 3""],
  ""mainProgram"": [""אקטיביטי 1"", ""אקטיביטי 2"", ""אקטיביטי 3""],
  ""specialMoments"": [""רגע 1"", ""רגע 2""],
  ""closingActivities"": [""סיום 1"", ""סיום 2""],
  ""narrativeTheme"": ""נושא מרכזי חזק""
}}";

            return await CallGeminiAPI<ProgrammingIdeas>(prompt);
        }

        private async Task<FoodSuggestions> GenerateFoodSuggestions(EventInput eventInput)
        {
            var prompt = $@"
אתה שף וחזון בתזונה בקהילה החרדית. תכלול תפריט אלגנטי וטעים לאירוע:

📋 פרטי האירוע:
- שם: {eventInput.EventName}
- קהל יעד: {eventInput.TargetAudience}
- מספר משתתפים: {eventInput.ExpectedAttendees}

הצע תפריט שהוא:
✓ בהכל כשר
✓ אלגנטי וטעים
✓ בריא ומנומס
✓ מתאים לקהילה חרדית

צור בדיוק פורמט זה (JSON בעברית):
{{
  ""kosherOptions"": [""אפשרות 1"", ""אפשרות 2"", ""אפשרות 3""],
  ""desserts"": [""קינוח 1"", ""קינוח 2"", ""קינוח 3""],
  ""beverages"": [""משקה 1"", ""משקה 2""],
  ""healthyAlternatives"": [""בריא 1"", ""בריא 2""],
  ""menuTheme"": ""נושא התפריט""
}}";

            return await CallGeminiAPI<FoodSuggestions>(prompt);
        }

        private async Task<BrandingStrategy> GenerateBrandingStrategy(EventInput eventInput)
        {
            var prompt = $@"
אתה מעצב ברנדינג יצירתי לאירועים בקהילה החרדית. צור אסטרטגיית ברנדינג ייחודית:

📋 פרטי האירוע:
- שם: {eventInput.EventName}
- מטרה עמוקה: {eventInput.EventObjective}
- קהל: {eventInput.TargetAudience}
- מיקום: {eventInput.Location}

צור ברנדינג שהוא:
✓ ייחודי לאירוע הזה
✓ משמעותי וערכי
✓ מתשומת לב לקהילה החרדית
✓ עיצוב מלא

צור בדיוק פורמט זה (JSON בעברית):
{{
  ""eventTitle"": ""כותרת מרהיבה"",
  ""tagline"": ""סלוגן קצר ועוצמתי"",
  ""colors"": {{
    ""primaryColor"": ""שם צבע וקוד"",
    ""secondaryColor"": ""שם צבע וקוד"",
    ""accentColor"": ""שם צבע וקוד"",
    ""meaning"": ""המשמעות העמוקה של הצבעים""
  }},
  ""logo"": {{
    ""concept"": ""קונספט הלוגו"",
    ""visualDescription"": ""תיאור ויזואלי"",
    ""designTips"": [""טיפ 1"", ""טיפ 2""]
  }},
  ""souvenir"": {{
    ""itemDescription"": ""מה המזכרת"",
    ""printingIdea"": ""מה יודפס עליה"",
    ""personalizationOptions"": [""אפשרות 1"", ""אפשרות 2""]
  }},
  ""overallStyle"": ""תיאור הסגנון הכללי""
}}";

            return await CallGeminiAPI<BrandingStrategy>(prompt);
        }

        private async Task<DetailedTimelineAndPresentation> GenerateDetailedTimeline(EventInput eventInput, EventProductionPlan plan)
        {
            var prompt = $@"
אתה מתכנן אירועים מנוסה. צור תוכנית זמנים מפורטת ותוכנית הצגה משמעותית:

📋 פרטי האירוע:
- שם: {eventInput.EventName}
- תאריך/שעה התחלה: {eventInput.EventDateTime:g}
- משך: {eventInput.DurationMinutes} דקות
- קהל: {eventInput.TargetAudience}
- מטרה: {eventInput.EventObjective}
- מיקום: {eventInput.Location}

צור תוכנית שהיא:
✓ מדויקת בזמנים
✓ משמעותית ומעוררת השראה
✓ הוגנת לכל משתתף
✓ עם מטלות ברורות

צור בדיוק פורמט זה (JSON בעברית):
{{
  ""schedule"": [
    {{""time"": ""09:00"", ""activity"": ""פעילות"", ""description"": ""תיאור"", ""responsibility"": ""אחראי""}},
    {{""time"": ""09:15"", ""activity"": ""..."", ""description"": ""..."", ""responsibility"": ""...""}}
  ],
  ""presentation"": {{
    ""openingMessage"": ""הודעת פתיחה חזקה"",
    ""mainPoints"": [""נקודה 1"", ""נקודה 2""],
    ""emotionalMoments"": [""רגע רגשי 1"", ""רגע רגשי 2""],
    ""closingMessage"": ""סיום משמעותי"",
    ""speakingTips"": [""טיפ 1"", ""טיפ 2""]
  }},
  ""decorIdeas"": [""עיסוד 1"", ""עיסוד 2""],
  ""technicalRequirements"": [""דרישה טכנית 1"", ""דרישה טכנית 2""]
}}";

            return await CallGeminiAPI<DetailedTimelineAndPresentation>(prompt);
        }

        private async Task<List<string>> GenerateSuccessTips(EventInput eventInput)
        {
            var prompt = $@"
תן 7-8 טיפים מעשיים ייחודיים להפקה מוצלחת של אירוע זה בקהילה החרדית:

📋 פרטי האירוע:
- שם: {eventInput.EventName}
- סוג/מטרה: {eventInput.EventObjective}
- קהל: {eventInput.TargetAudience}
- משך: {eventInput.DurationMinutes} דקות

הטיפים צריכים להיות:
✓ מעשיים וישימים מיד
✓ מחשיבים ערכים חרדיים
✓ מעודדים התחברות בקהילה
✓ ביצוע מלא של עצמאות

תן תשובה כ-JSON של מערך מחרוזות בעברית בלבד:
[""טיפ מעשי 1"", ""טיפ מעשי 2"", ""טיפ מעשי 3"", ""טיפ מעשי 4"", ""טיפ מעשי 5""]";

            var tips = await CallGeminiAPI<List<string>>(prompt);
            return tips ?? new List<string>();
        }

        private async Task<T?> CallGeminiAPI<T>(string prompt)
        {
            try
            {
                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new
                                {
                                    text = prompt
                                }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.7,
                        topK = 40,
                        topP = 0.95,
                        maxOutputTokens = 2000
                    }
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var url = $"{GEMINI_API_URL}?key={_apiKey}";
                var response = await _httpClient.PostAsync(url, jsonContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Gemini API Error: {response.StatusCode} - {responseContent}");
                    return default;
                }

                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                var textContent = jsonResponse.GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                // חלץ JSON מהתשובה אם מעטפת בקודים
                textContent = ExtractJson(textContent);

                var result = JsonSerializer.Deserialize<T>(textContent);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling Gemini API: {ex.Message}");
                return default;
            }
        }

        private string ExtractJson(string? text)
        {
            if (string.IsNullOrEmpty(text)) return text ?? "";

            // חוזה JSON מבלוק קוד אם קיים
            var jsonMatch = System.Text.RegularExpressions.Regex.Match(text, @"```(?:json)?\s*([\s\S]*?)\s*```");
            if (jsonMatch.Success)
            {
                return jsonMatch.Groups[1].Value;
            }

            return text;
        }
    }
}
