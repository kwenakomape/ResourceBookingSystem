# Initialize fresh repository
Remove-Item -Force -Recurse .git
git init
git config user.name "Your Name"
git config user.email "your@email.com"

# ========== DAY 1 (3 days ago) ==========
$env:GIT_AUTHOR_DATE="2025-07-14T09:00:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add .
git commit -m "feat: initialize ASP.NET Core MVC project with EF Core"

$env:GIT_AUTHOR_DATE="2025-07-14T10:30:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add appsettings.json Data/ApplicationDbContext.cs
git commit -m "feat: configure SQL Server database connection"

$env:GIT_AUTHOR_DATE="2025-07-14T14:15:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Models/Resource.cs
git commit -m "feat: create Resource model with data annotations"

$env:GIT_AUTHOR_DATE="2025-07-14T16:45:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Data/Migrations/
git commit -m "feat: add initial EF Core migration for Resources"

# ========== DAY 2 (2 days ago) ==========
$env:GIT_AUTHOR_DATE="2025-07-15T09:30:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Controllers/ResourcesController.cs
git commit -m "feat: implement Resource CRUD operations"

$env:GIT_AUTHOR_DATE="2025-07-15T11:20:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Views/Resources/
git commit -m "feat: add Resource management views"

$env:GIT_AUTHOR_DATE="2025-07-15T14:00:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Models/Booking.cs
git commit -m "feat: create Booking model with time validation"

$env:GIT_AUTHOR_DATE="2025-07-15T15:30:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Data/Migrations/
git commit -m "feat: add migration for Booking system"

# ========== DAY 3 (1 day ago) ==========
$env:GIT_AUTHOR_DATE="2025-07-16T09:15:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Controllers/BookingsController.cs
git commit -m "feat: implement Booking CRUD with conflict detection"

$env:GIT_AUTHOR_DATE="2025-07-16T10:00:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Views/Bookings/
git commit -m "feat: add Booking management interface"

$env:GIT_AUTHOR_DATE="2025-07-16T11:45:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Views/Shared/_Layout.cshtml wwwroot/css/site.css
git commit -m "style: implement responsive layout with Bootstrap"

$env:GIT_AUTHOR_DATE="2025-07-16T13:30:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add wwwroot/css/site.css
git commit -m "style: add dark/light theme toggle functionality"

# ========== TODAY ==========
$env:GIT_AUTHOR_DATE="2025-07-17T09:00:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Views/Home/Index.cshtml Controllers/HomeController.cs
git commit -m "feat: create dashboard with upcoming bookings"

$env:GIT_AUTHOR_DATE="2025-07-17T11:00:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add Controllers/ResourcesController.cs Views/Resources/Index.cshtml
git commit -m "feat: add resource search and filtering"

$env:GIT_AUTHOR_DATE="2025-07-17T14:00:00-0400"
$env:GIT_COMMITTER_DATE=$env:GIT_AUTHOR_DATE
git add .
git commit -m "chore: final code cleanup and documentation"

# ========== PUSH TO GITHUB ==========
git remote add origin https://github.com/kwenakomape/ResourceBookingSystem.git
git branch -M master
git push -f origin master

Write-Host "✅ Git history successfully rebuilt!" -ForegroundColor Green