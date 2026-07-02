# CIS Connect Manual Test Plan

This test plan can be used during development, demonstration, and Final Year Project evaluation.

## Test Environment

| Item | Value |
| --- | --- |
| Application | CIS Connect |
| Framework | ASP.NET Core MVC |
| Database | MySQL |
| Browser | Safari / Chrome / Edge |
| Local HTTPS URL | `https://localhost:7043` |
| Local HTTP URL | `http://localhost:5043` |
| Device sizes | Desktop, tablet, mobile |

## Test Data

The application uses seed/demo data from `Data/DbSeeder.cs`.

Test data includes:

- Home update posts.
- Menu guide articles.
- University profiles.
- University tab sections.
- FAQ items.
- Contact items.
- University-specific articles for XMUM.
- Image and video media examples.

## Core Functional Test Cases

| ID | Feature | Test Steps | Expected Result | Status |
| --- | --- | --- | --- | --- |
| TC-01 | Home page loads | Open `/` or `https://localhost:7043`. | Home feed is displayed with latest updates and navigation bar. | Not tested |
| TC-02 | Home filter chips | Click each Home filter chip. | Posts are filtered without reloading the page. Active chip is highlighted. | Not tested |
| TC-03 | Home search button | Click search icon in navbar. | Search page opens. | Not tested |
| TC-04 | Global search | Search for `visa`, `XMUM`, or `food`. | Matching update posts and guide articles are shown. | Not tested |
| TC-05 | Menu dropdown | Open Menu from navbar. | Dropdown shows guide sections, My Checklist, and Q&A. | Not tested |
| TC-06 | Pre-Arrival page | Open `/Menu/PreArrival`. | Pre-arrival articles are listed. | Not tested |
| TC-07 | Arrival & Setup page | Open `/Menu/ArrivalSetup`. | Arrival articles are listed with university filter chips. | Not tested |
| TC-08 | Arrival university filter | Click `XMUM` filter on Arrival & Setup. | Only XMUM-specific arrival posts are shown. | Not tested |
| TC-09 | Deals page | Open `/Menu/Deals`. | Deals and recommendation articles are listed with filters. | Not tested |
| TC-10 | Deals university filter | Click `XMUM` filter on Deals. | XMUM-specific food guide post is shown. | Not tested |
| TC-11 | Living in Malaysia page | Open `/Menu/LivingInMalaysia`. | Living guide articles and visual card are displayed correctly. | Not tested |
| TC-12 | CIS Community page | Open `/Menu/CISCommunity`. | Community articles are displayed. | Not tested |
| TC-13 | Contacts page | Open `/Menu/Contacts`. | Important contacts and country support hub are displayed. | Not tested |
| TC-14 | FAQ page | Open `/FAQ`. | Grouped FAQ items are displayed. | Not tested |
| TC-15 | FAQ search | Search a question keyword. | FAQ list updates based on the search term. | Not tested |
| TC-16 | Universities list | Open `/Universities`. | University cards are displayed for all selected universities. | Not tested |
| TC-17 | University details | Open `/Universities/Details/1`. | XMUM details page opens with tabs. | Not tested |
| TC-18 | University tabs | Click Overview, Programs, Housing, Fees, Scholarships, etc. | Correct tab content is displayed without layout breakage. | Not tested |
| TC-19 | Compare Universities | Open `/Universities/Compare`. | Two universities can be selected and compared side by side. | Not tested |
| TC-20 | Compare selectors | Change first and second university dropdowns. | Logo, name, location, description, and comparison fields update. | Not tested |
| TC-21 | Post details page | Click any post card. | `/post/{id}` opens and shows full content. | Not tested |
| TC-22 | Post details back link | Click back link on post details. | User returns to the correct previous section. | Not tested |
| TC-23 | Previous/Next post | Use previous or next links on post details. | User navigates to another related post. | Not tested |
| TC-24 | Related stories | Scroll to related stories on post details. | Related posts appear as compact links. | Not tested |
| TC-25 | Image media | Open a post with image media. | Image loads correctly and is responsive. | Not tested |
| TC-26 | Video media | Open a post with video media. | Video player appears and can play the local video. | Not tested |
| TC-27 | Saved posts | Click save button on a post. | Post is saved and appears on `/Saved`. | Not tested |
| TC-28 | Unsave post | Click saved button again. | Post is removed from saved posts. | Not tested |
| TC-29 | Checklist | Open `/Checklist` and mark items complete. | Checklist progress is saved in the same browser. | Not tested |
| TC-30 | About page | Open `/Home/About`. | About project and contact details are displayed. | Not tested |

## Responsive Design Test Cases

| ID | Feature | Test Steps | Expected Result | Status |
| --- | --- | --- | --- | --- |
| RT-01 | Desktop navigation | Open site on wide desktop screen. | Navbar shows Home, Menu, Universities, icons, and Get Help. | Not tested |
| RT-02 | Mobile navigation | Resize to mobile width. | Hamburger menu appears and navigation remains usable. | Not tested |
| RT-03 | Mobile dropdowns | Open mobile menu and expand sections. | Menu and university links are accessible. | Not tested |
| RT-04 | Mobile Home page | View Home page on mobile. | Text, cards, filters, and images fit screen width. | Not tested |
| RT-05 | Mobile post details | Open a post on mobile. | Content reads comfortably and media does not overflow. | Not tested |
| RT-06 | Mobile university details | Open a university page on mobile. | Tabs are usable and content stays readable. | Not tested |
| RT-07 | Mobile compare page | Open Compare Universities on mobile. | Two comparison columns remain readable and aligned. | Not tested |
| RT-08 | Video responsiveness | Open vertical and horizontal video posts on mobile. | Video player fits without cropping important content. | Not tested |

## Data and Database Test Cases

| ID | Feature | Test Steps | Expected Result | Status |
| --- | --- | --- | --- | --- |
| DB-01 | Database connection | Start project with MySQL running. | App connects successfully and pages load data. | Not tested |
| DB-02 | Seed data | Run project with empty database. | Required demo data is created. | Not tested |
| DB-03 | Existing data update | Restart app after seed data exists. | Existing seed records are not duplicated. | Not tested |
| DB-04 | Missing post | Open `/post/guide-99999`. | App returns NotFound instead of crashing. | Not tested |
| DB-05 | Missing university | Open `/Universities/Details/99999`. | App returns NotFound instead of crashing. | Not tested |

## Accessibility Test Cases

| ID | Feature | Test Steps | Expected Result | Status |
| --- | --- | --- | --- | --- |
| AX-01 | Keyboard navigation | Use Tab key through navbar and buttons. | Interactive elements can be reached by keyboard. | Not tested |
| AX-02 | Focus states | Tab through links and buttons. | Visible focus indicators are present. | Not tested |
| AX-03 | Image alt text | Inspect post and university images. | Meaningful images have alt text or decorative images are hidden. | Not tested |
| AX-04 | Reduced motion | Enable reduced motion in OS/browser. | Animations should be reduced or not distracting. | Not tested |
| AX-05 | Color contrast | Check text and button colors. | Text remains readable against background colors. | Not tested |

## Suggested User Acceptance Test

Ask 3 to 5 CIS students or prospective students to complete these tasks:

1. Find what to do before travelling to Malaysia.
2. Find what to do after arriving at KLIA.
3. Find a university-specific post for XMUM.
4. Compare two universities.
5. Save a useful post.
6. Use the checklist.
7. Search for a FAQ answer.

Record:

- Whether they completed the task.
- How long it took.
- Any confusing page or label.
- Suggested improvements.

## Known Risks to Check Before Final Submission

- Broken image paths.
- Videos that do not play because of unsupported format.
- MySQL not running.
- Port `7043` already in use.
- Browser local storage cleared, causing saved posts/checklist reset.
- Demo content not fully verified from official sources.
- Database password visible in `appsettings.json`.

## Recommended Evidence for Final Report

- Screenshots of each main page.
- Screenshots of mobile navigation.
- Screenshots of university comparison.
- Screenshot of database tables.
- ERD and architecture diagram from `docs/ARCHITECTURE.md`.
- Completed version of this test plan with actual results.
- Short user feedback summary from real or simulated users.

