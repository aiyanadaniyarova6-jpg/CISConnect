# CIS Connect

CIS Connect is a web-based information and support platform for CIS students in Malaysia. The project centralizes important student information such as arrival preparation, university guidance, lifestyle tips, community updates, FAQs, contacts, saved posts, checklists, and university comparison.

This application was built as a Final Year Project MVP using ASP.NET Core MVC, Entity Framework Core, MySQL, Razor Views, and Bootstrap 5.

## Project Purpose

International and CIS students often receive important information from many separate sources: university websites, embassy pages, WhatsApp groups, student chats, social media, and personal recommendations. This makes it difficult for new students to know what to do before arrival, during the first week, and throughout university life in Malaysia.

CIS Connect solves this problem by providing one organized platform for:

- Pre-arrival preparation.
- Arrival and setup guidance.
- Living in Malaysia tips.
- CIS community information.
- Deals and recommendations.
- Important contacts.
- University-specific guidance.
- Frequently asked questions.
- Search, saved posts, checklist, and comparison support.

## Target Users

- Current CIS students studying in Malaysia.
- Prospective CIS students preparing to study in Malaysia.
- New students who need first-week and arrival guidance.
- Students comparing universities before making a decision.

## Selected Universities

- Xiamen University Malaysia.
- Asia Pacific University.
- Sunway University.
- Taylor's University.
- UCSI University.

## Technology Stack

- ASP.NET Core MVC.
- Entity Framework Core (with EF Core Migrations).
- MySQL (Pomelo.EntityFrameworkCore.MySql).
- Razor Views.
- HTML, CSS, and JavaScript.

## Main Features

- Home feed with update posts and topic filters.
- Menu pages for student guide sections.
- University-specific guide pages with internal tabs.
- Compare Universities page with two selectable universities.
- FAQ page with searchable questions.
- Search page for updates and guide articles.
- Saved posts using browser local storage.
- My Checklist using browser local storage.
- Post details page with next/previous navigation.
- Image and video media support for posts.
- Source and last-verified labels for official guidance and university information.
- Responsive navigation with desktop and mobile menu behavior.
- Seed/demo data for MVP demonstration.

## Project Structure

```text
CISConnect2/
  Controllers/
    ChecklistController.cs
    FAQController.cs
    HomeController.cs
    MenuController.cs
    PostController.cs
    SavedController.cs
    SearchController.cs
    UniversitiesController.cs
    UpdatesController.cs

  Data/
    ApplicationDbContext.cs
    DbSeeder.cs
    Migrations/
      20260619141326_InitialCreate.cs

  Helpers/
    YouTubeHelper.cs

  Models/
    ContactItem.cs
    FAQItem.cs
    GuideArticle.cs
    MenuSection.cs
    University.cs
    UniversitySection.cs
    UpdatePost.cs

  ViewModels/
    FAQViewModel.cs
    HomeViewModel.cs
    MenuPageViewModel.cs
    PostCardViewModel.cs
    PostDetailsViewModel.cs
    SearchViewModel.cs
    UniversityDetailsViewModel.cs

  Views/
    Checklist/
    FAQ/
    Home/
    Menu/
    Post/
    Saved/
    Search/
    Shared/
    Universities/
    Updates/

  wwwroot/
    css/
    images/
    js/
    lib/
    media/

  docs/
    ARCHITECTURE.md
    TEST_PLAN.md
```

## Main Routes

| Route | Purpose |
| --- | --- |
| `/` | Home feed with latest updates. |
| `/Home/About` | About CIS Connect and project contact information. |
| `/Menu/PreArrival` | Pre-arrival preparation articles. |
| `/Menu/ArrivalSetup` | Arrival and first-week setup articles. |
| `/Menu/LivingInMalaysia` | Daily life and student lifestyle guidance. |
| `/Menu/CISCommunity` | CIS community groups, events, and useful links. |
| `/Menu/Deals` | Deals, recommendations, and student tips. |
| `/Menu/Contacts` | Important contacts and country support hub. |
| `/Universities` | List of selected universities. |
| `/Universities/Details/{id}` | University guide page with internal tabs. |
| `/Universities/Compare` | Side-by-side university comparison tool. |
| `/FAQ` | Searchable Q&A / FAQ page. |
| `/Search` | Global search page. |
| `/Saved` | Saved posts stored on the user's device. |
| `/Checklist` | Student checklist stored on the user's device. |
| `/post/{id}` | Post details page for updates and guide articles. |

## Data Entities

- `UpdatePost`: home feed updates, reminders, notices, and highlighted posts.
- `MenuSection`: top-level student guide categories.
- `GuideArticle`: article content inside menu sections.
- `University`: selected university profile.
- `UniversitySection`: university page tab content.
- `FAQItem`: searchable FAQ question and answer.
- `ContactItem`: important contact information.

## How to Run Locally

1. Open the project folder:

```bash
cd "/Users/aiyana/Documents/New project/CISConnect2"
```

2. Make sure MySQL is running.

3. Set the database connection string using User Secrets (the value in `appsettings.json` is intentionally blank):

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;database=cisconnectdb;user=root;password=YOUR_PASSWORD"
```

Current local database name:

```text
cisconnectdb
```

4. Restore packages:

```bash
dotnet restore
```

5. Run the project:

```bash
dotnet run
```

6. Open the browser:

```text
https://localhost:7043
```

or:

```text
http://localhost:5043
```

## Important Local Note

If the project says that port `7043` is already in use, another copy of the app is still running. Check the process:

```bash
lsof -i :7043
```

Then stop the running process:

```bash
kill -9 PID_NUMBER
```

Replace `PID_NUMBER` with the real process id shown by `lsof`.

## Current MVP Limitations

- Most data is seeded demo content for MVP presentation.
- Some university details need final verification from official university sources.
- Saved posts and checklist are stored in the browser using local storage, not user accounts.
- There is no admin dashboard yet for reviewing student-submitted tips or questions.

## Recommended Future Enhancements

- Add a simple admin dashboard (CRUD) for managing posts and guide articles.
- Add a submission form for students to suggest tips or ask questions.
- Add pagination for the home feed and search results.
- Improve university comparison with more verified data fields.
- Add full-text search index on MySQL for better search performance.
- Add user accounts only if saved posts need to sync across devices.
- Add accessibility testing for keyboard navigation and reduced motion.

## Documentation

Additional project documentation is available in:

- `docs/ARCHITECTURE.md`
- `docs/TEST_PLAN.md`
