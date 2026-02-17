# 🔧 הנחיות הגדרה - Setup Instructions

## 1️⃣ הגדרת Gemini API

### דרך A: שימוש ב-.env file

```bash
# 1. העתק את ה-template
cp .env.example .env

# 2. ערוך את ה-.env file וכנס את API Key שלך:
# GEMINI_API_KEY=your_key_here

# 3. ברגע שהקובץ מוכן, התוכנה תקרא ממנו אוטומטית
```

### דרך B: משתנה סביבה

```bash
# Windows PowerShell
$env:GEMINI_API_KEY = "your_key_here"

# Windows CMD
set GEMINI_API_KEY=your_key_here

# Linux/Mac
export GEMINI_API_KEY=your_key_here
```

### דרך C: appsettings.Development.json

```json
{
  "Gemini": {
    "ApiKey": "your_key_here"
  }
}
```

## 2️⃣ הרצת הפרויקט

```bash
# בנה
dotnet build

# הרץ
dotnet run

# הרץ בעיר פיתוח
dotnet run --configuration Development
```

## 3️⃣ הרצת בדיקות

```bash
# כל הבדיקות
dotnet test

# בדיקות ספציפיות
dotnet test --filter FullyQualifiedName~EventPlanControllerTests

# עם פירוט
dotnet test --verbosity detailed
```

## 4️⃣ Rate Limiting (הגבלת בקשות)

המערכת מוגדרת להגביל בקשות:
- **Generate Plan**: 10 בקשות לדקה
- **Health**: 100 בקשות לדקה

אם תקבל `429 Too Many Requests`, חכה דקה.

## 5️⃣ בדיקת Health

```bash
curl http://localhost:7000/api/eventplan/health
```

תשובה צפויה:
```json
{
  "status": "מערכת תכנון אירועים פעילה",
  "timestamp": "2026-02-15T12:30:45Z"
}
```

## 6️⃣ בדיקת Generate Plan

```bash
curl -X POST http://localhost:7000/api/eventplan/generate \
  -H "Content-Type: application/json" \
  -d '{
    "eventName": "אירוע בדיקה",
    "targetAudience": "תלמידות",
    "eventDateTime": "2026-05-20T10:00:00",
    "location": "אולם",
    "eventObjective": "חגיגה",
    "durationMinutes": 120,
    "budget": 5000,
    "expectedAttendees": 100
  }'
```

## ⚠️ טיפים חשובים

1. **לא לשתוף את ה-API Key!** - הוא סודי
2. **לא לעלות .env לגיט** - כבר מוגן ב-.gitignore
3. **משתנה סביבה עדיף** - בייצור, השתמש למשל ב-Docker Secrets
4. **בדוק את ה-quota** - Gemini יש Rate Limit

## 🐛 טרבול שוט

### "API Key not found"
```bash
# בדוק שה-.env קיים:
ls -la .env

# או בדוק משתנה סביבה:
echo $GEMINI_API_KEY  # Linux/Mac
echo %GEMINI_API_KEY%  # Windows CMD
$env:GEMINI_API_KEY  # Windows PowerShell
```

### "429 Too Many Requests"
- חכה דקה
- בדוק את ה-Rate Limit בתוך Gemini console

### "Connection refused"
- וודא שהשרת פעיל: `dotnet run`
- בדוק את ה-port (7000)

---

כשהכל מוגדר, בחן את Swagger: **http://localhost:7000/swagger**
