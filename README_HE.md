# 🎉 MCP לתכנון אירועים - שילוב Gemini API

## 📖 תיעוד בעברית

מערכת ברת נתונים לתכנון אירועים בקהילה חרדית בעזרת AI של Google Gemini.

---

## ⚡ התחלה מהירה

### 1️⃣ קבל את מפתח ה-API של Gemini
- עבור ל: https://ai.google.dev
- צור מפתח API חדש
- העתק את המפתח

### 2️⃣ צור קובץ `.env`
בשורש הפרויקט, צור קובץ בשם `.env`:
```
GEMINI_API_KEY=YOUR_GEMINI_API_KEY_HERE
```

**חשוב:** לא לשתוף או להעלות את קובץ ה-.env לגיט! (מוגן ב-.gitignore)

### 3️⃣ הרץ את השרת
```bash
dotnet run
```

### 4️⃣ בדוק שהשרת פעיל
```bash
curl http://localhost:7000/api/eventplan/health
```

---

## 🚀 קריאות API

### בדוק בריאות המערכת (Health Check)

```bash
curl http://localhost:7000/api/eventplan/health
```

**תשובה:**
```json
{
  "status": "מערכת תכנון אירועים פעילה",
  "timestamp": "2026-02-15T10:30:45.123Z"
}
```

### צור תכנית אירוע (Generate Event Plan)

שלח בקשה POST ל-`/api/eventplan/generate` עם פרטי האירוע:

**מבנה הבקשה:**
```json
{
  "eventName": "שם האירוע",
  "targetAudience": "קהל היעד (מורים, תלמידות, הורים וכו')",
  "eventDateTime": "תאריך ושעה: YYYY-MM-DDTHH:MM:SS",
  "location": "מיקום האירוע",
  "eventObjective": "מטרה ותיאור האירוע",
  "durationMinutes": 120,
  "budget": 5000,
  "expectedAttendees": 80
}
```

**דוגמה - אירוע סוף שנה:**
```bash
curl -X POST http://localhost:7000/api/eventplan/generate \
  -H "Content-Type: application/json" \
  -d '{
    "eventName": "אירוע סוף שנה - הערכה למורות",
    "targetAudience": "מורות וחברות הנהלה",
    "eventDateTime": "2026-06-15T09:00:00",
    "location": "אולם בנין הישיבה",
    "eventObjective": "בטוי הערכה עמוקה למורים על תרומתם המשמעותית",
    "durationMinutes": 120,
    "budget": 5000,
    "expectedAttendees": 80
  }'
```

### מה Gemini יספק:

✅ **רעיונות לתוכנית** - פעילויות מותאמות לאירוע שלך
✅ **הצעות מזון** - תפריט מותאם לתקציב
✅ **אסטרטגיית ברנדינג** - צבעים, לוגו, מזכרות
✅ **תוכנית זמנים** - לוח זמנים מפורט
✅ **זרימת התוכנית** - סדר הרגעים המשמעותיים
✅ **טיפים להצלחה** - המלצות ייחודיות לאירוע שלך

---

## 🔐 הגבלת בקשות (Rate Limiting)

המערכת מוגדרת להגביל בקשות כדי להגן על ה-API:

| Endpoint | הגבלה | חלון זמן |
|----------|--------|---------|
| `/api/eventplan/generate` | 10 בקשות | 1 דקה |
| `/api/eventplan/health` | 100 בקשות | 1 דקה |

**אם קיבלת שגיאה `429 Too Many Requests`:**
```json
{
  "error": "הגעת ללימיט של בקשות. נסה שוב בעוד דקה.",
  "retryAfter": "60"
}
```

---

## ⚙️ הגדרות

### קבצי Configuration
- `appsettings.json` - הגדרות כלליות
- `appsettings.Development.json` - הגדרות לפיתוח
- `.env` - משתנים סודיים (API Keys)

### קבלת Gemini API Key

**דרך א: קובץ .env**
```bash
cp .env.example .env
# ערוך את ה-.env וכנס את ה-API Key
```

**דרך ב: משתנה סביבה**
```bash
# Windows PowerShell
$env:GEMINI_API_KEY = "your_key_here"

# Windows CMD
set GEMINI_API_KEY=your_key_here

# Linux/Mac
export GEMINI_API_KEY=your_key_here
```

**דרך ג: appsettings.Development.json**
```json
{
  "Gemini": {
    "ApiKey": "your_key_here"
  }
}
```

---

## 🧪 בדיקות (Unit Tests)

הפרויקט כולל 20 בדיקות יחידה:

```bash
# הרץ את כל הבדיקות
dotnet test

# בדיקות ספציפיות
dotnet test --filter FullyQualifiedName~EventPlanControllerTests

# עם פירוט מלא
dotnet test --verbosity detailed
```

**סוגי בדיקות:**
- ✅ **EventPlanControllerTests** - בדיקות ה-controller
- ✅ **EventInputValidationTests** - בדיקות ה-input
- ✅ **ConfigurationTests** - בדיקות הגדרות
- ✅ **HealthCheckTests** - בדיקות ה-health endpoint

ראה [McpCodeExplainer.Tests/README.md](McpCodeExplainer.Tests/README.md) לפרטים שלמים.

---

## 📝 הערות חשובות

1. **כל תשובה ייחודית** - Gemini יוצר תוכנית חדשה עבור כל בקשה
2. **מותאם לקלט שלך** - שם האירוע, קהל היעד, מטרה ותקציב משפיעים על התשובה
3. **ללא דוגמאות קבועות** - כל בקשה מייצרת תכנית טרייה בהתאם לדרישות

---

## 🔧 פתרון בעיות

### שגיאה: "API Key not found"
```bash
# בדוק שקובץ ה-.env קיים:
ls -la .env

# או בדוק משתנה סביבה:
echo $GEMINI_API_KEY        # Linux/Mac
echo %GEMINI_API_KEY%       # Windows CMD
$env:GEMINI_API_KEY         # Windows PowerShell
```

### שגיאה: "Failed to parse JSON"
- וודא שה-JSON מעוצב בעברית
- בדוק תווים מיוחדים שייתכן שיש להקיף

### שגיאה: "429 Too Many Requests"
- חכה דקה אחת
- בדוק את הקוטה ב-Gemini console

### שגיאה: "Connection refused"
- וודא שהשרת פעיל: `dotnet run`
- בדוק את ה-port (7000)

---

## 📁 מבנה הפרויקט

```
McpCodeExplainer/
├── Controllers/              # בקרים API
│   └── EventPlanController.cs
├── Models/                   # מודלים
│   ├── EventInput.cs
│   └── EventProductionPlan.cs
├── Services/                 # שירותים
│   └── EventPlanningOrchestrator.cs
├── McpCodeExplainer.Tests/   # בדיקות יחידה
│   ├── EventPlanControllerTests.cs
│   ├── EventInputValidationTests.cs
│   ├── ConfigurationTests.cs
│   └── HealthCheckTests.cs
├── Program.cs                # נקודת הכניסה
├── appsettings.json          # הגדרות כלליות
├── .env.example              # template ל-.env
├── README_HE.md              # קובץ זה
├── SETUP.md                  # הנחיות setup מפורטות
└── EXAMPLES.md              # דוגמאות קריאות
```

---

## 🎯 דרישות

- **.NET 9.0 SDK** ומעלה
- **Gemini API Key**
- **Visual Studio Code** או **Visual Studio** (אופציונלי)

---

## 📚 קבצי תיעוד נוסף

| קובץ | תיאור |
|------|---------|
| [SETUP.md](SETUP.md) | הנחיות התקנה מפורטות |
| [EXAMPLES.md](EXAMPLES.md) | דוגמאות קריאות API |
| [IMPLEMENTATION_SUMMARY.md](IMPLEMENTATION_SUMMARY.md) | סיכום השינויים בפרויקט |
| [McpCodeExplainer.Tests/README.md](McpCodeExplainer.Tests/README.md) | תיעוד בדיקות |

---

## ✨ תכונות

- 🤖 **AI מופעל** - Gemini API לתכנון חכם
- 📱 **REST API** - קריאות HTTP סטנדרטיות
- 🔐 **Rate Limiting** - הגבלת בקשות לבטיחות
- 🧪 **Unit Tests** - 20 בדיקות כולל
- 📖 **Swagger** - توثيق אפליקציה אינטראקטיבית
- 📝 **עברית מלאה** - תמיכה בעברית בכל המערכת

---

## 💡 דוגמאות שימוש

**אירוע סוף שנה:**
```json
{
  "eventName": "אירוע סוף שנה - הערכה למורות",
  "targetAudience": "מורות וחברות הנהלה",
  "eventDateTime": "2026-06-15T09:00:00",
  "location": "אולם בנין הישיבה",
  "eventObjective": "בטוי הערכה עמוקה למורים",
  "durationMinutes": 120,
  "budget": 5000,
  "expectedAttendees": 80
}
```

**ערב תרבות:**
```json
{
  "eventName": "ערב תרבות וחיזוק קהילה",
  "targetAudience": "הורים ותלמידות",
  "eventDateTime": "2026-03-20T18:00:00",
  "location": "אודיטוריום בית הספר",
  "eventObjective": "חיזוק קשרי משפחה וקהילה",
  "durationMinutes": 180,
  "budget": 8000,
  "expectedAttendees": 200
}
```

---

## 🆘 עזרה

יש בעיה? בדוק את:
1. [SETUP.md](SETUP.md) - הנחיות setup
2. [EXAMPLES.md](EXAMPLES.md) - דוגמאות
3. סעיף "פתרון בעיות" למעלה

---

## 📄 רישיון

פרויקט זה משתמש בשילוב ASP.NET Core ו-Google Gemini API.

---

**עדכון אחרון:** 15 בפברואר 2026

**סטטוס:** ✅ מוכן לשימוש
