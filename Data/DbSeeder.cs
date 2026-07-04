using CISConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace CISConnect.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Migration is now handled in Program.cs (MigrateAsync before SeedAsync).
        // EnsureSourceColumnsAsync handles any columns added outside the initial migration.
        await EnsureSourceColumnsAsync(context);

        var seededUpdatePosts = new List<UpdatePost>
        {
            new()
            {
                Title = "Student Pass Reminder for New Arrivals",
                Summary = "Students should keep passport, visa approval letter, and university documents ready before travel.",
                Content = "Before flying to Malaysia, keep printed and digital copies of your passport, visa approval letter, offer letter, and accommodation details. This helps reduce delays during airport checks and university registration.",
                Category = "Important",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1541339907198-e08756dedf3f?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "International students walking on a university campus",
                PublishedAt = new DateTime(2026, 4, 21),
                IsHighlighted = true
            },
            new()
            {
                Title = "MDAC Submission Should Be Completed Before Arrival",
                Summary = "International students are reminded to complete the Malaysia Digital Arrival Card before entering the country.",
                Content = "The Malaysia Digital Arrival Card is an important part of the entry process. Students should complete it before traveling and keep a screenshot or saved confirmation for reference at the airport.",
                Category = "Guides",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1434030216411-0b793f4b4173?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "Student preparing travel documents at a desk",
                PublishedAt = new DateTime(2026, 4, 19),
                IsHighlighted = true
            },
            new()
            {
                Title = "Community Welcome Week Activities",
                Summary = "Several student groups are planning welcome meetups and informal support sessions for new CIS students.",
                Content = "Community-led activities can help students learn about transport, food, budgeting, and adapting to student life. This demo post represents the kind of campus and student group updates CIS Connect will centralize.",
                Category = "Community",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1529156069898-49953e39b3ac?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "Diverse group of students smiling together outdoors",
                PublishedAt = new DateTime(2026, 4, 17),
                IsHighlighted = false
            },
            new()
            {
                Title = "Budget Tip: Set Up a Local SIM Card Early",
                Summary = "A local number makes registration, banking, transport, and communication much easier during the first week.",
                Content = "Students usually benefit from setting up a local SIM card as soon as possible after arrival. It helps with ride-hailing apps, banking verification, and quick communication with university staff and classmates.",
                Category = "Tips",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1512941937669-90a1b58e7e9c?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "Hand holding a smartphone ready for setup",
                PublishedAt = new DateTime(2026, 4, 15),
                IsHighlighted = false
            },
            new()
            {
                Title = "Cross-Campus Welcome Mixer This Weekend",
                Summary = "Students from multiple universities are planning an informal social meetup for networking and support.",
                Content = "This kind of event helps new students build friendships, ask practical questions, and feel more connected across campuses. CIS Connect can surface these events in one easy-to-check place.",
                Category = "Events",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1511578314322-379afb476865?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "Students attending a friendly campus social event",
                PublishedAt = new DateTime(2026, 4, 13),
                IsHighlighted = true
            },
            new()
            {
                Title = "Top Places in Malaysia Students Should Visit",
                Summary = "A simple inspiration post for students who want to explore beyond campus during weekends and semester breaks.",
                Content = "Consider visiting places such as Kuala Lumpur city centre, Penang, Langkawi, Melaka, Cameron Highlands, and Ipoh. These destinations are popular for food, culture, scenery, and short student-friendly trips.",
                Category = "Culture",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "Scenic tropical landscape with green mountains and water",
                PublishedAt = new DateTime(2026, 4, 11),
                IsHighlighted = false
            },
            new()
            {
                Title = "Culture Shock: Dating Rules and Religious Norms",
                Summary = "A short video-based reminder that relationship norms, religion, and public behaviour can be treated differently in Malaysia than in many CIS countries.",
                Content = "Some students may be surprised by how strongly religion and public morality can shape social expectations in Malaysia. This post is not meant to scare students, but to encourage respectful behaviour, careful choices, and awareness that rules can differ by state, situation, and personal status.",
                Category = "Culture",
                MediaType = "youtube",
                MediaUrl = "https://www.youtube.com/embed/hDmBNPjA_7g",
                MediaAltText = "YouTube video about cultural differences, dating rules, and religious norms in Malaysia",
                PublishedAt = new DateTime(2026, 6, 12),
                IsHighlighted = false
            },
            new()
            {
                Title = "Before You Go: Social Rules That May Feel Different",
                Summary = "Culture shock can include sensitive topics such as dating, religion, hotel checks, and public attitudes toward belief.",
                Content = "For CIS students, some Malaysian social and legal expectations may feel unfamiliar. The safest approach is to stay respectful, avoid risky assumptions, check official guidance when needed, and remember that local customs may not work the same way as at home.",
                Category = "Culture",
                MediaType = "youtube",
                MediaUrl = "https://www.youtube.com/embed/R0rII81X9do",
                MediaAltText = "YouTube video explaining culture shock and social rules students may find unfamiliar in Malaysia",
                PublishedAt = new DateTime(2026, 6, 10),
                IsHighlighted = false
            },
            new()
            {
                Title = "EMGS Processing Window Update for April Intake",
                Summary = "Students should keep checking official updates and avoid last-minute travel assumptions when waiting for approval progress.",
                Content = "This update category is useful for official timing notes, process changes, and reminders that affect many students at once. It works well as a general notice stream on the Home page.",
                Category = "Updates",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1450101499163-c8848c66ca85?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "Person reviewing official documents at a desk",
                PublishedAt = new DateTime(2026, 4, 9),
                IsHighlighted = false
            },
            new()
            {
                Title = "After Graduation: Diploma Legalization Checklist",
                Summary = "A practical reminder for students who may need their Malaysian diploma legalized for use back home.",
                Content = """
                    <p>Some graduates may need to legalize their Malaysian diploma before using it for employment, further study, or official paperwork in their home country.</p>

                    <h2>Recommended order</h2>
                    <ul>
                        <li>Step 1: confirm requirements with your home country's embassy or consular section.</li>
                        <li>Step 2: prepare your graduation completion letter, student visa copy, diploma, and academic transcript.</li>
                        <li>Step 3: check whether the document must go through Malaysia's higher education and foreign affairs authorities before embassy legalization.</li>
                        <li>Step 4: keep copies of every stamped or certified document before submitting originals anywhere.</li>
                    </ul>

                    <p>This process can vary by country, so always verify the latest requirements with your embassy before booking appointments or travel.</p>
                """,
                Category = "Important",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1523580846011-d3a5bc25702b?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "Graduates celebrating after completing university",
                PublishedAt = new DateTime(2026, 5, 18),
                IsHighlighted = true
            },
            new()
            {
                Title = "Astana Restaurant Brings Kazakh Cuisine to XMUM Campus",
                Summary = "A dedicated Kazakh food spot is open right on campus at Block B1, G06 — dine-in and takeaway, from midday to late evening.",
                Content = """
                    <p>Students who miss home-style Kazakh cooking now have a spot on their own campus: Astana Restaurant, located at Block B1, G06 at Xiamen University Malaysia. It's already mentioned as a go-to CIS-style food option in our Student Food Spots guide, so we're giving it its own shout-out here.</p>

                    <h2>Good to know</h2>
                    <ul>
                        <li>Open daily from 12:00 PM to 10:00 PM.</li>
                        <li>Both dine-in and takeaway are available.</li>
                        <li>Orders and enquiries go through WhatsApp rather than a walk-up counter, so save the number if you plan to order ahead.</li>
                    </ul>

                    <p>For students craving plov, shashlik, or other familiar dishes without leaving campus, this is worth adding to the regular rotation alongside the other B1 food spots.</p>
                    """,
                Category = "Culture",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1555939594-58d7cb561ad1?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "Skewers of grilled meat and vegetables served on flatbread, Central Asian style",
                PublishedAt = new DateTime(2026, 7, 4),
                IsHighlighted = false
            },
            new()
            {
                Title = "Uzbekistan Youth Day Brings Together Students Across Malaysia",
                Summary = "The Embassy of Uzbekistan and WAYU marked Youth Day with students from APU, XMUM, UCSI, and other universities.",
                Content = """
                    <p>On 30 June 2026, the Embassy of Uzbekistan in Kuala Lumpur, together with the World Association of Youth of Uzbekistan (WAYU), held an event marking Uzbekistan's Youth Day. It brought together Uzbek students from across Malaysia, including those from University of Malaya, Asia Pacific University (APU), City University Malaysia, University of Cyberjaya, UCSI University, INCEIF University, INTI International University, and Xiamen University Malaysia (XMUM).</p>

                    <h2>What happened</h2>
                    <ul>
                        <li>Embassy representatives spoke about Uzbekistan's youth policy priorities and the support measures available to students studying abroad.</li>
                        <li>Several students received letters of appreciation from the Embassy and WAYU for active participation in embassy events and strong academic results over the year.</li>
                        <li>The embassy noted that the number of Uzbek students in Malaysia has grown to more than 1,000.</li>
                    </ul>

                    <p>Events like this are a good reminder that WAYU is active well beyond casual meetups — see our CIS Community guide for how to follow and join in.</p>
                    """,
                Category = "Events",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1523580494863-6f3031224c94?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "Students seated at a community event listening to a speaker on stage",
                PublishedAt = new DateTime(2026, 7, 1),
                IsHighlighted = true
            },
            new()
            {
                Title = "XMUM Marked Its First Nauryz With Music, Games, and Dance",
                Summary = "The XMUM CIS Community held the university's first Nauryz celebration, the Central Asian spring festival, with embassy guests attending.",
                Content = """
                    <p>In April 2025, the XMUM CIS Community organized the university's first-ever celebration of Nauryz — the Central Asian spring festival marking the arrival of spring and a new year, widely celebrated across Kazakhstan, Kyrgyzstan, Tajikistan, Uzbekistan, and beyond.</p>

                    <h2>What happened</h2>
                    <ul>
                        <li>The evening event was held at B1-114 and brought together students from Kazakhstan, Uzbekistan, Tajikistan, and other Central Asian countries.</li>
                        <li>Students showcased the region's culture through traditional games, music, and dance performances.</li>
                        <li>The celebration was attended by several diplomatic guests, including Mr. Kyran Orynbekov, Counsellor at the Embassy of Kazakhstan; H.E. Mr. Anvar Anarbaev, Ambassador of the Kyrgyz Republic to Malaysia; and Mr. Tugral Khayrulloev, Consul at the Embassy of Tajikistan.</li>
                    </ul>

                    <p>If your campus doesn't already have a Nauryz celebration, it's worth asking your local CIS Community or student association whether one is being planned — events like XMUM's inaugural one tend to grow year over year once someone starts organizing.</p>
                    """,
                Category = "Culture",
                MediaType = "image",
                MediaUrl = "https://images.unsplash.com/photo-1547153760-18fc86324498?auto=format&fit=crop&w=1200&q=80",
                MediaAltText = "A dancer mid-performance under stage lighting",
                PublishedAt = new DateTime(2026, 6, 28),
                IsHighlighted = false
            }
        };

        var existingUpdates = context.UpdatePosts.ToList();
        var existingUpdatesByTitle = existingUpdates.ToDictionary(post => post.Title, post => post);

        foreach (var seededPost in seededUpdatePosts)
        {
            ApplyUpdatePostSource(seededPost);

            if (existingUpdatesByTitle.TryGetValue(seededPost.Title, out var existingPost))
            {
                existingPost.Summary = seededPost.Summary;
                existingPost.Content = seededPost.Content;
                existingPost.Category = seededPost.Category;
                existingPost.MediaType = seededPost.MediaType;
                existingPost.MediaUrl = seededPost.MediaUrl;
                existingPost.MediaAltText = seededPost.MediaAltText;
                existingPost.SourceName = seededPost.SourceName;
                existingPost.SourceUrl = seededPost.SourceUrl;
                existingPost.LastVerifiedAt = seededPost.LastVerifiedAt;
                existingPost.PublishedAt = seededPost.PublishedAt;
                existingPost.IsHighlighted = seededPost.IsHighlighted;
            }
            else
            {
                context.UpdatePosts.Add(seededPost);
            }
        }

        await context.SaveChangesAsync();

        if (!context.MenuSections.Any())
        {
            var menuSections = new List<MenuSection>
            {
                new()
                {
                    Name = "Pre-Arrival",
                    Slug = "pre-arrival",
                    Description = "Preparation guidance before coming to Malaysia.",
                    GuideArticles = new List<GuideArticle>
                    {
                        new()
                        {
                            Title = "Pre-Arrival Checklist for CIS Students",
                            Summary = "A simple list of the most important items to prepare before departure.",
                            Content = "Prepare your passport, student visa documents, university offer letter, accommodation address, medical records if needed, and enough Malaysian currency or an international payment option for your first days.",
                            CreatedAt = new DateTime(2026, 4, 1)
                        },
                        new()
                        {
                            Title = "Documents to Keep in Print and Digital Form",
                            Summary = "Avoid travel stress by preparing backup copies.",
                            Content = "Keep printed and digital copies of your passport, visa approval letter, offer letter, emergency contacts, and travel itinerary. Store one digital copy online and one on your phone.",
                            CreatedAt = new DateTime(2026, 4, 1)
                        }
                    }
                },
                new()
                {
                    Name = "Arrival & Setup",
                    Slug = "arrival-setup",
                    Description = "Tasks to complete during the first days after arriving.",
                    GuideArticles = new List<GuideArticle>
                    {
                        new()
                        {
                            Title = "First 48 Hours in Malaysia",
                            Summary = "What most students should prioritize immediately after arrival.",
                            Content = "Complete your MDAC if not already done, confirm transport from the airport, check into accommodation, get a local SIM card, and review your university registration schedule.",
                            CreatedAt = new DateTime(2026, 4, 2)
                        },
                        new()
                        {
                            Title = "Bank Account and SIM Card Basics",
                            Summary = "A beginner-friendly guide to two common setup tasks.",
                            Content = "Most students need a working phone number before setting up banking-related services. Keep your passport and university documents ready because service providers may ask for proof of identity or student status.",
                            CreatedAt = new DateTime(2026, 4, 2)
                        }
                    }
                },
                new()
                {
                    Name = "Living in Malaysia",
                    Slug = "living-in-malaysia",
                    Description = "Everyday lifestyle guidance for students.",
                    GuideArticles = new List<GuideArticle>
                    {
                        new()
                        {
                            Title = "Student-Friendly Daily Living Tips",
                            Summary = "Small habits that make day-to-day life smoother.",
                            Content = "Learn basic cashless payment options, identify the nearest grocery store, install local transport and food delivery apps, and track your monthly spending early.",
                            CreatedAt = new DateTime(2026, 4, 2)
                        },
                        new()
                        {
                            Title = "Getting Around: Public Transport and Ride-Hailing",
                            Summary = "A simple overview of common transport choices.",
                            Content = "Students often use trains, buses, and ride-hailing apps depending on campus location. Keep your destination pinned, estimate travel time in advance, and avoid rushing during peak hours when possible.",
                            CreatedAt = new DateTime(2026, 4, 2)
                        }
                    }
                },
                new()
                {
                    Name = "CIS Community",
                    Slug = "cis-community",
                    Description = "Community support, groups, and student activities.",
                    GuideArticles = new List<GuideArticle>
                    {
                        new()
                        {
                            Title = "Why Student Communities Matter",
                            Summary = "Community groups help new students adapt faster.",
                            Content = "Student communities often share housing tips, transport advice, event updates, and real-life experiences that help new arrivals feel less isolated.",
                            CreatedAt = new DateTime(2026, 4, 4)
                        }
                    }
                },
                new()
                {
                    Name = "Deals & Recommendations",
                    Slug = "deals-recommendations",
                    Description = "Affordable and practical recommendations for students.",
                    GuideArticles = new List<GuideArticle>
                    {
                        new()
                        {
                            Title = "Budget Meals and Everyday Recommendations",
                            Summary = "Useful starter recommendations for students managing expenses.",
                            Content = "Look for campus-area meal deals, student promotions, and affordable grocery options. For an MVP, this content is seed data that demonstrates how recommendations can be stored and displayed later.",
                            CreatedAt = new DateTime(2026, 4, 5)
                        }
                    }
                },
                new()
                {
                    Name = "Important Contacts",
                    Slug = "important-contacts",
                    Description = "Emergency and support contacts relevant to students.",
                    GuideArticles = new List<GuideArticle>
                    {
                        new()
                        {
                            Title = "Who to Contact First When You Need Help",
                            Summary = "A quick guide for choosing the right contact point.",
                            Content = "For visa or immigration-related concerns, refer to official sources first. For registration or campus matters, contact your university support office. For emergencies, use verified emergency contacts immediately.",
                            CreatedAt = new DateTime(2026, 4, 5)
                        }
                    }
                }
            };

            context.MenuSections.AddRange(menuSections);
        }

        await context.SaveChangesAsync();
        await UpsertGuideArticlesAsync(context);

        if (!context.Universities.Any())
        {
            var universities = new List<University>
            {
                new()
                {
                    Name = "Xiamen University Malaysia",
                    Location = "Sepang, Selangor",
                    Description = "A private university campus known for its large dedicated grounds and international student population.",
                    CampusMapLink = "https://www.xmu.edu.my/"
                },
                new()
                {
                    Name = "Asia Pacific University (APU)",
                    Location = "Bukit Jalil, Kuala Lumpur",
                    Description = "A technology-focused university with strong student activity and easy urban access.",
                    CampusMapLink = "https://www.apu.edu.my/"
                },
                new()
                {
                    Name = "Sunway University",
                    Location = "Bandar Sunway, Selangor",
                    Description = "A well-known urban campus with nearby commercial areas, transport options, and student facilities.",
                    CampusMapLink = "https://university.sunway.edu.my/"
                },
                new()
                {
                    Name = "Taylor's University",
                    Location = "Subang Jaya, Selangor",
                    Description = "A student-focused campus with an active surrounding area and a wide range of facilities.",
                    CampusMapLink = "https://university.taylors.edu.my/"
                },
                new()
                {
                    Name = "UCSI University",
                    Location = "Cheras, Kuala Lumpur",
                    Description = "A city-based university with access to daily essentials and urban transport routes.",
                    CampusMapLink = "https://www.ucsiuniversity.edu.my/"
                }
            };

            context.Universities.AddRange(universities);
            await context.SaveChangesAsync();

            var universitySections = new List<UniversitySection>
            {
                new() { UniversityId = universities[0].Id, SectionType = "Campus Map", Title = "Main Campus Orientation", Content = "The campus has a large layout, so new students should identify residence halls, registration points, shuttle pick-up areas, and food outlets early." },
                new() { UniversityId = universities[0].Id, SectionType = "Useful Contacts", Title = "Student Support Contacts", Content = "Keep the international office and registration support contacts saved before your first week begins." },
                new() { UniversityId = universities[0].Id, SectionType = "Cafeteria / Food", Title = "Food Options Nearby", Content = "Students usually look for affordable campus food and nearby eateries with flexible hours." },
                new() { UniversityId = universities[0].Id, SectionType = "Facilities", Title = "Key Student Facilities", Content = "Library, study spaces, transport points, and student service offices are useful first-stop facilities." },
                new() { UniversityId = universities[0].Id, SectionType = "Local Updates", Title = "Transport and Campus Notices", Content = "Local updates may include shuttle changes, registration reminders, and orientation notices." },

                new() { UniversityId = universities[1].Id, SectionType = "Campus Map", Title = "APU Campus Overview", Content = "Students should note the academic blocks, food court areas, and arrival points around Bukit Jalil." },
                new() { UniversityId = universities[1].Id, SectionType = "Useful Contacts", Title = "International and Student Affairs", Content = "The international support and student affairs teams are useful contacts during onboarding." },
                new() { UniversityId = universities[1].Id, SectionType = "Cafeteria / Food", Title = "Student Food Choices", Content = "Food court options and nearby budget meals are often part of daily student planning." },
                new() { UniversityId = universities[1].Id, SectionType = "Facilities", Title = "Study and Student Spaces", Content = "Students should identify library access, classrooms, study areas, and support counters early." },
                new() { UniversityId = universities[1].Id, SectionType = "Local Updates", Title = "Area Tips for New Students", Content = "This section can store notices about transport, nearby services, and student events." },

                new() { UniversityId = universities[2].Id, SectionType = "Campus Map", Title = "Sunway Area Navigation", Content = "Students benefit from learning the campus layout and surrounding Sunway area early." },
                new() { UniversityId = universities[2].Id, SectionType = "Useful Contacts", Title = "Campus Support Directory", Content = "Useful contacts may include student services, academic offices, and emergency campus support." },
                new() { UniversityId = universities[2].Id, SectionType = "Cafeteria / Food", Title = "Food and Nearby Dining", Content = "Bandar Sunway offers many food choices, from campus options to affordable nearby stalls." },
                new() { UniversityId = universities[2].Id, SectionType = "Facilities", Title = "Daily Use Facilities", Content = "Important spaces include learning areas, admin counters, transport pick-up zones, and convenience stores." },
                new() { UniversityId = universities[2].Id, SectionType = "Local Updates", Title = "Student Area Announcements", Content = "This section can highlight local promotions, transport advisories, or student notices." },

                new() { UniversityId = universities[3].Id, SectionType = "Campus Map", Title = "Taylor's Campus Guide", Content = "Students should note lecture blocks, admin services, and common gathering areas." },
                new() { UniversityId = universities[3].Id, SectionType = "Useful Contacts", Title = "Important Office Contacts", Content = "Keep student support and international office contact details available during the first semester." },
                new() { UniversityId = universities[3].Id, SectionType = "Cafeteria / Food", Title = "Food Spots for Students", Content = "Students often compare on-campus convenience with more budget-friendly nearby choices." },
                new() { UniversityId = universities[3].Id, SectionType = "Facilities", Title = "Academic and Student Facilities", Content = "Classrooms, libraries, transport access, and service counters matter most during the first weeks." },
                new() { UniversityId = universities[3].Id, SectionType = "Local Updates", Title = "Campus and Area Updates", Content = "This section can show notices, community tips, and area-specific reminders." },

                new() { UniversityId = universities[4].Id, SectionType = "Campus Map", Title = "UCSI Campus Navigation", Content = "New students should identify the main academic buildings, student services, and nearby transport points." },
                new() { UniversityId = universities[4].Id, SectionType = "Useful Contacts", Title = "Key Support Contacts", Content = "International office and student affairs contacts are useful for registration and support needs." },
                new() { UniversityId = universities[4].Id, SectionType = "Cafeteria / Food", Title = "Affordable Meals and Quick Options", Content = "Students often value low-cost food options and predictable daily meal choices." },
                new() { UniversityId = universities[4].Id, SectionType = "Facilities", Title = "Essential Student Facilities", Content = "Libraries, admin offices, study spaces, and support desks are priority facilities for new students." },
                new() { UniversityId = universities[4].Id, SectionType = "Local Updates", Title = "City-Based Student Updates", Content = "This section can track local transport notes, area guidance, and campus reminders." }
            };

            context.UniversitySections.AddRange(universitySections);
        }

        await context.SaveChangesAsync();
        await UpsertUniversitySectionsAsync(context);

        // Always refresh FAQ items so content stays in English
        context.FAQItems.RemoveRange(context.FAQItems.ToList());
        await context.SaveChangesAsync();

        var faqItems = new List<FAQItem>
        {
            new()
            {
                Category = "Visa",
                Question = "What documents should I prepare before traveling to Malaysia as a student?",
                Answer = "At minimum, keep your passport, student visa or visa approval documents, offer letter, accommodation details, and important university contacts ready in both printed and digital form.",
                DisplayOrder = 1
            },
            new()
            {
                Category = "Visa",
                Question = "Do I need to complete the Malaysia Digital Arrival Card (MDAC) before flying?",
                Answer = "Yes. The MDAC must be completed online before you enter Malaysia. Keep a screenshot or PDF of your confirmation to show at the airport. You can find the form at the official immigration portal.",
                DisplayOrder = 2
            },
            new()
            {
                Category = "Arrival & Setup",
                Question = "What should I do in my first few days after arriving?",
                Answer = "Focus on arrival formalities, accommodation check-in, getting a local SIM card, reviewing your registration schedule, and confirming any university onboarding tasks.",
                DisplayOrder = 3
            },
            new()
            {
                Category = "Arrival & Setup",
                Question = "How do I get from the airport to my university or accommodation?",
                Answer = "The most reliable option for most campuses is Grab (ride-hailing app) — install it before landing. For XMUM, take KLIA Transit to Salak Tinggi then Grab to campus. Save your destination address in English before arrival.",
                DisplayOrder = 4
            },
            new()
            {
                Category = "Living in Malaysia",
                Question = "Is it necessary to get a Malaysian SIM card early?",
                Answer = "Yes. A local SIM card makes communication, ride-hailing, banking verification, and daily student life much easier. Buy one at KLIA on arrival — Maxis, Celcom, or Digi are the main providers.",
                DisplayOrder = 5
            },
            new()
            {
                Category = "Living in Malaysia",
                Question = "What is the typical monthly budget for a student in Malaysia?",
                Answer = "A realistic budget is RM 1,200–1,800 per month including accommodation, food, transport, and personal expenses. This varies by city and lifestyle. KL is more expensive than campus-based cities like Sepang.",
                DisplayOrder = 6
            },
            new()
            {
                Category = "University",
                Question = "Why does each university page have separate sections?",
                Answer = "Each university has different campus layouts, nearby food choices, facilities, and local updates, so separating them helps students find information faster.",
                DisplayOrder = 7
            },
            new()
            {
                Category = "University",
                Question = "When should I contact EMGS about my student pass?",
                Answer = "EMGS (Education Malaysia Global Services) handles student pass issuance. Your university initiates the process after enrolment. Allow 4–8 weeks for processing. Track your status at emgs.com.my and do not book flights until your pass is confirmed.",
                DisplayOrder = 8
            },
            new()
            {
                Category = "Support",
                Question = "What if I cannot find the answer I need on CIS Connect?",
                Answer = "Check the Important Contacts page for direct support lines — it lists emergency numbers, EMGS, immigration, and each university's international office. For urgent issues, always contact your university's international student affairs team first.",
                DisplayOrder = 9
            },
            new()
            {
                Category = "Support",
                Question = "What should I do in a medical emergency in Malaysia?",
                Answer = "Call 999 (police and ambulance) or 112 (works from any mobile, even without a SIM). Keep the number for the nearest hospital to your campus saved in your phone before you arrive.",
                DisplayOrder = 10
            }
        };

        context.FAQItems.AddRange(faqItems);

        // Always refresh contact data so real numbers are kept current
        context.ContactItems.RemoveRange(context.ContactItems.ToList());
        await context.SaveChangesAsync();

        var contactItems = new List<ContactItem>
        {
            // ── Emergency ───────────────────────────────────────────────────
            new()
            {
                Category = "Emergency",
                Name = "Police & Ambulance (Malaysia)",
                Phone = "999",
                Email = "",
                Address = "Nationwide",
                Notes = "Single emergency number for police, ambulance, and rescue. Works from any phone including mobile."
            },
            new()
            {
                Category = "Emergency",
                Name = "Fire & Rescue (Bomba)",
                Phone = "994",
                Email = "",
                Address = "Nationwide",
                Notes = "Malaysia Fire and Rescue Department. Also call 999 if unsure."
            },
            new()
            {
                Category = "Emergency",
                Name = "Emergency (Mobile / Roaming)",
                Phone = "112",
                Email = "",
                Address = "Nationwide",
                Notes = "GSM emergency number — works even without a SIM or with no signal on your network. Connects to 999."
            },
            // ── Official Support ────────────────────────────────────────────
            new()
            {
                Category = "Official Support",
                Name = "Education Malaysia Global Services (EMGS)",
                Phone = "+60 3-2782 5888",
                Email = "enquiry@emgs.com.my",
                Address = "Bangunan EMGS, Avenue 3, Bangsar South, Kuala Lumpur",
                Notes = "Issues and renewals of student passes (eVAL). Check emgs.com.my for your application status."
            },
            new()
            {
                Category = "Official Support",
                Name = "Immigration Department of Malaysia (JIM)",
                Phone = "+60 3-8880 1000",
                Email = "pusat.panggilan@imi.gov.my",
                Address = "Persiaran Perdana, Presint 2, Putrajaya",
                Notes = "Student pass queries, visa extensions, and entry permits. Visit imi.gov.my for forms."
            },
            // ── Student Support ─────────────────────────────────────────────
            new()
            {
                Category = "Student Support",
                Name = "XMUM — International Student Affairs",
                Phone = "+60 3-8946 8888",
                Email = "international@xmu.edu.my",
                Address = "Jalan Sunsuria, Bandar Sunsuria, 43900 Sepang, Selangor",
                Notes = "For student pass, registration, and campus support. Office hours Mon–Fri 8:30 am–5:30 pm."
            },
            new()
            {
                Category = "Student Support",
                Name = "APU — International Office",
                Phone = "+60 3-8992 5000",
                Email = "international@apu.edu.my",
                Address = "Technology Park Malaysia, Bukit Jalil, 57000 Kuala Lumpur",
                Notes = "Student pass and academic support for international students."
            },
            new()
            {
                Category = "Student Support",
                Name = "Sunway University — International Student Services",
                Phone = "+60 3-7491 8622",
                Email = "intl@sunway.edu.my",
                Address = "Jalan Universiti, Bandar Sunway, 47500 Subang Jaya, Selangor",
                Notes = "Student pass, accommodation, and arrival support."
            },
            new()
            {
                Category = "Student Support",
                Name = "Taylor's University — International Office",
                Phone = "+60 3-5629 5000",
                Email = "international@taylors.edu.my",
                Address = "No. 1, Jalan Taylor's, 47500 Subang Jaya, Selangor",
                Notes = "Visa, student pass, and welfare support for international students."
            },
            new()
            {
                Category = "Student Support",
                Name = "UCSI University — International Affairs",
                Phone = "+60 3-9101 8880",
                Email = "international@ucsiuniversity.edu.my",
                Address = "No. 1 Jalan Menara Gading, UCSI Heights, 56000 Cheras, Kuala Lumpur",
                Notes = "Student pass processing and international student support."
            },
        };

        context.ContactItems.AddRange(contactItems);

        await context.SaveChangesAsync();
    }

    private static void ApplyUpdatePostSource(UpdatePost post)
    {
        var verifiedAt = new DateTime(2026, 6, 18);

        (post.SourceName, post.SourceUrl) = post.Title switch
        {
            "Student Pass Reminder for New Arrivals" => (
                "Education Malaysia Global Services (EMGS)",
                "https://educationmalaysia.gov.my/"
            ),
            "MDAC Submission Should Be Completed Before Arrival" => (
                "Malaysia Digital Arrival Card (MDAC)",
                "https://imigresen-online.imi.gov.my/mdac/main"
            ),
            "EMGS Processing Window Update for April Intake" => (
                "Education Malaysia Global Services (EMGS)",
                "https://educationmalaysia.gov.my/"
            ),
            "After Graduation: Diploma Legalization Checklist" => (
                "Embassy / consular legalization guidance",
                "https://www.gov.kz/memleket/entities/mfa-kuala-lumpur"
            ),
            "Culture Shock: Dating Rules and Religious Norms" => (
                "YouTube cultural explainer video",
                "https://youtu.be/hDmBNPjA_7g"
            ),
            "Before You Go: Social Rules That May Feel Different" => (
                "YouTube cultural explainer video",
                "https://youtu.be/R0rII81X9do"
            ),
            "Astana Restaurant Brings Kazakh Cuisine to XMUM Campus" => (
                "Astana Xiamen — Instagram",
                "https://www.instagram.com/astana_xiamen/"
            ),
            "Uzbekistan Youth Day Brings Together Students Across Malaysia" => (
                "Embassy of Uzbekistan in Kuala Lumpur / WAYU",
                "https://dunyo.info/en/daty/v-malayzii-otmetili-den-molodyozhi-uzbekistana-kulturno-prosvetitelskim"
            ),
            "XMUM Marked Its First Nauryz With Music, Games, and Dance" => (
                "Xiamen University Malaysia — News",
                "https://www.xmu.edu.my/2025/0423/c16257a492128/page.htm"
            ),
            _ => (
                "CIS Connect demo content",
                string.Empty
            )
        };

        post.LastVerifiedAt = string.IsNullOrWhiteSpace(post.SourceUrl) ? null : verifiedAt;
    }

    private static async Task UpsertUniversitySectionsAsync(ApplicationDbContext context)
    {
        var sections = BuildUniversitySections();

        foreach (var section in sections)
        {
            var university = await context.Universities
                .FirstOrDefaultAsync(item => item.Name == section.UniversityName);

            if (university is null)
            {
                continue;
            }

            var (sourceName, sourceUrl) = GetUniversitySource(university.Name);
            var verifiedAt = new DateTime(2026, 6, 18);

            var existingSection = await context.UniversitySections
                .FirstOrDefaultAsync(item =>
                    item.UniversityId == university.Id &&
                    item.SectionType == section.SectionType);

            if (existingSection is null)
            {
                context.UniversitySections.Add(new UniversitySection
                {
                    UniversityId = university.Id,
                    SectionType = section.SectionType,
                    Title = section.Title,
                    Content = section.Content,
                    SourceName = sourceName,
                    SourceUrl = sourceUrl,
                    LastVerifiedAt = verifiedAt
                });

                continue;
            }

            existingSection.Title = section.Title;
            existingSection.Content = section.Content;
            existingSection.SourceName = sourceName;
            existingSection.SourceUrl = sourceUrl;
            existingSection.LastVerifiedAt = verifiedAt;
        }

        await context.SaveChangesAsync();
    }

    private static (string SourceName, string SourceUrl) GetUniversitySource(string universityName)
    {
        return universityName switch
        {
            "Xiamen University Malaysia" => ("Xiamen University Malaysia official website", "https://www.xmu.edu.my/"),
            "Asia Pacific University (APU)" => ("Asia Pacific University official website", "https://www.apu.edu.my/"),
            "Sunway University" => ("Sunway University official website", "https://sunwayuniversity.edu.my/"),
            "Taylor's University" => ("Taylor's University official website", "https://university.taylors.edu.my/en.html"),
            "UCSI University" => ("UCSI University official website", "https://www.ucsiuniversity.edu.my/"),
            _ => ("Official university website", string.Empty)
        };
    }

    private static List<UniversitySectionSeed> BuildUniversitySections()
    {
        var list = new List<UniversitySectionSeed>();

        void Add(string universityName, string sectionType, string title, string content)
        {
            list.Add(new UniversitySectionSeed(universityName, sectionType, title, content));
        }

        Add("Xiamen University Malaysia", "Overview", "XMUM at a Glance", """
            <p>Xiamen University Malaysia (XMUM) is a branch campus of Xiamen University China, located in Bandar Sunsuria, Sepang, Selangor — about 45 minutes from central KL. It opened in 2016 and offers foundation, undergraduate, and postgraduate programmes on a large self-contained campus.</p>
            <ul>
                <li><strong>QS World Ranking 2026:</strong> #341 globally, #50 in East Asia. Top 1% ESI in Engineering, Clinical Medicine, Chemistry, and Neuroscience.</li>
                <li><strong>Student profile:</strong> ~7,000 students, significant international student community including CIS students.</li>
                <li><strong>Campus character:</strong> Quiet, campus-based lifestyle. Everything — accommodation, food, sports, library, clinic — is on site. Good for students who prefer structure over city access.</li>
                <li><strong>Language:</strong> All programmes taught in English except Chinese Studies and Traditional Chinese Medicine.</li>
                <li>Official website: <a href="https://www.xmu.edu.my/" target="_blank" rel="noopener">xmu.edu.my</a></li>
            </ul>
            """);
        Add("Xiamen University Malaysia", "Campus Map", "Campus Layout and Key Locations", """
            <p>The campus has two main gates and a clear block system. Learn this on Day 1 — it will save a lot of confusion in your first week.</p>

            <h3 style="font-size:15px;font-weight:600;margin:20px 0 10px;">Campus Entrances</h3>
            <ul>
                <li><strong>South Gate (Main Entrance):</strong> The primary entrance. After entering, the Tan Kah Kee statue is directly in front — turn right to reach Block A3 (Library + Student Recruitment Office).</li>
                <li><strong>East Gate (Side Entrance):</strong> Side entrance near Block B1. Useful for the Student Recruitment Office (B1-G10) if you have course enquiries.</li>
            </ul>

            <h3 style="font-size:15px;font-weight:600;margin:20px 0 10px;">Key Blocks</h3>
            <ul>
                <li><strong>Block A3 (Library / A3-G01):</strong> Robert Kuok Library — 9 floors, 2,000+ seats. The Student Recruitment Office is on the Ground Floor (A3-G01). If A3-G01 is closed, visit the library on Level 2.</li>
                <li><strong>Block B1 (B1-G10):</strong> Near the East Gate. Student Recruitment Office for course enquiries is here (B1-G10, Ground Floor).</li>
                <li><strong>Blocks D1–D6:</strong> Academic faculty blocks in the north-east area of campus. D6 is furthest north — locate your faculty block early.</li>
                <li><strong>Blocks LY1–LY9:</strong> Student hostel blocks in the north-west of campus. Know your assigned LY block number before arriving.</li>
                <li><strong>Blocks A1, A2, A4, A5:</strong> Academic and support buildings surrounding the Library (A3).</li>
                <li><strong>Musical Island:</strong> Central outdoor feature between the hostel area and the academic core.</li>
                <li><strong>Outdoor Courts:</strong> Futsal, Tennis, Volleyball, Basketball — east side of campus near the Field &amp; Track.</li>
                <li><strong>Hotel &amp; Conference Centre / Auditorium:</strong> West side of campus near A1.</li>
                <li><strong>Postgraduate Research Centre:</strong> Far west, beyond the Hotel block.</li>
            </ul>

            <h3 style="font-size:15px;font-weight:600;margin:20px 0 10px;">Finding the Student Recruitment Office</h3>
            <p style="font-size:14px;line-height:1.6;margin-bottom:10px;"><strong>Via South Gate:</strong> Enter → pass Tan Kah Kee statue → turn right at first junction → left → underground car park (near A3) → lift to Ground Floor (G) → A3-G01.</p>
            <p style="font-size:14px;line-height:1.6;"><strong>Via East Gate:</strong> Enter → turn left immediately → park at outdoor car park → walk to Block B1 → Ground Floor (G10), first office facing East Gate.</p>

            <p style="margin-top:16px;">360° virtual tour: <a href="https://vt.xmu.edu.my/" target="_blank" rel="noopener">vt.xmu.edu.my</a> — walk through the campus before you arrive.</p>
            """);
        Add("Xiamen University Malaysia", "Programs", "Programmes and Schools", """
            <p>XMUM offers foundation, undergraduate, and postgraduate programmes across 9 schools.</p>
            <ul>
                <li><strong>Economics &amp; Management</strong> — Accounting, Finance, Marketing, International Business, Management.</li>
                <li><strong>Communication</strong> — Media, Journalism, Public Relations, Digital Marketing.</li>
                <li><strong>Arts &amp; Social Sciences</strong> — Psychology, Sociology, Chinese Studies, Language programmes.</li>
                <li><strong>Engineering</strong> — Civil, Electrical, Electronic, Chemical, Mechanical Engineering.</li>
                <li><strong>Computing &amp; Data Science</strong> — Computer Science, Data Science, Software Engineering, Information Systems.</li>
                <li><strong>AI &amp; Robotics</strong> — One of the few Malaysian campuses with a dedicated AI school.</li>
                <li><strong>Mathematics &amp; Physics</strong> — Pure and Applied Maths, Physics programmes.</li>
                <li><strong>Marine Sciences</strong> — Unique offering; one of few marine-focused programmes in Malaysia.</li>
                <li><strong>Traditional Chinese Medicine (TCM)</strong> — 5-year programme; taught in both English and Chinese. 40+ partnered clinics for practicals.</li>
            </ul>
            <p>Full programme list: <a href="https://www.xmu.edu.my/undergraduate-programmes/" target="_blank" rel="noopener">XMUM Undergraduate Programmes</a>. Foundation and postgraduate options are also listed on the admissions pages.</p>
            """);
        Add("Xiamen University Malaysia", "Requirements", "Entry Requirements", """
            <p>Requirements vary by programme level. The following apply to most undergraduate programmes for international students.</p>
            <ul>
                <li><strong>Academic:</strong> Minimum CGPA 2.5 / 4.0 (or equivalent). Accepted qualifications: A-Level, IB, foundation, STPM, UEC, diploma, or national equivalent.</li>
                <li><strong>English:</strong> IELTS 6.0 or TOEFL iBT 80. <em>Not mandatory</em> if you have a grade C or above in O-level English, or a Medium of Instruction (MOI) certificate from your previous institution, or scored 60%+ in English at matriculation level.</li>
                <li><strong>TCM programme:</strong> Additional requirement — basic knowledge of Chinese language is recommended.</li>
                <li><strong>Documents to prepare:</strong> Academic transcripts (certified), passport copy, offer letter, proof of payment, scholarship documents (if applicable), and EMGS application documents.</li>
                <li><strong>EMGS / Student Pass:</strong> XMUM initiates the EMGS process after enrolment. Allow 4–8 weeks for processing. Do not travel before your student pass is confirmed.</li>
                <li>Application portal: <a href="https://apply.xmu.edu.my/" target="_blank" rel="noopener">apply.xmu.edu.my</a></li>
            </ul>
            """);
        Add("Xiamen University Malaysia", "Deadlines", "Academic Calendars 2026–2027", """
            <p>Real academic calendars from XMUM for the 2026–2027 academic year. <em>Note: semester dates are subject to change — always confirm with the university.</em></p>

            <h3 style="font-size:15px;font-weight:600;margin:24px 0 12px;">Foundation Studies — August 2026 Intake</h3>
            <div style="overflow-x:auto;">
                <table style="width:100%;border-collapse:collapse;font-size:13px;">
                    <thead>
                        <tr style="background:rgba(10,14,31,0.05);">
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;">Semester</th>
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;">Period</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);font-weight:500;">Semester 1</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">Aug 2026 – Dec 2026</td></tr>
                        <tr style="background:rgba(10,14,31,0.02);"><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);font-weight:500;">Semester 2</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">Jan 2027 – Apr 2027</td></tr>
                        <tr><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);font-weight:500;">Semester 3</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">May 2027 – Jul 2027</td></tr>
                    </tbody>
                </table>
            </div>

            <h3 style="font-size:15px;font-weight:600;margin:24px 0 12px;">Undergraduate — Academic Calendar 2026</h3>
            <div style="overflow-x:auto;">
                <table style="width:100%;border-collapse:collapse;font-size:13px;">
                    <thead>
                        <tr style="background:rgba(10,14,31,0.05);">
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;">Semester</th>
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;">Dates</th>
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;">Key Events</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);font-weight:500;white-space:nowrap;">February Semester</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);white-space:nowrap;">20 Feb – 3 Apr 2026</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">Registration: 20–21 Feb · Orientation: 22 Feb · Exams: 30 Mar–3 Apr</td>
                        </tr>
                        <tr style="background:rgba(10,14,31,0.02);">
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);font-weight:500;white-space:nowrap;">April Semester</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);white-space:nowrap;">3 Apr – 31 Jul 2026</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">Registration: 3–4 Apr · Orientation: 5 Apr · Revision: 13–19 Jul · Exams: 20–31 Jul · Break: 1 Aug–24 Sep</td>
                        </tr>
                        <tr>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);font-weight:500;white-space:nowrap;">September Semester</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);white-space:nowrap;">25 Sep 2026 – 21 Jan 2027</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">Registration: 25–26 Sep · Orientation: 27 Sep · Revision: 4–10 Jan · Exams: 11–21 Jan · Break: 23 Jan–11 Feb 2027</td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <h3 style="font-size:15px;font-weight:600;margin:24px 0 12px;">Postgraduate MBA — Academic Calendar 2026</h3>
            <div style="overflow-x:auto;">
                <table style="width:100%;border-collapse:collapse;font-size:13px;">
                    <thead>
                        <tr style="background:rgba(10,14,31,0.05);">
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;">Semester</th>
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;">Dates</th>
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;">Key Events</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);font-weight:500;white-space:nowrap;">January Semester</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);white-space:nowrap;">12 Jan – 26 Apr 2026</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">Registration: 10 Jan · Networking: 7 Feb · Mid-sem break: 16–22 Mar · Study week: 13–19 Apr · Exams: 20–26 Apr · Break: 27 Apr–10 May</td>
                        </tr>
                        <tr style="background:rgba(10,14,31,0.02);">
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);font-weight:500;white-space:nowrap;">May Semester</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);white-space:nowrap;">11 May – 23 Aug 2026</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">Registration: 9 May · Networking: 27 Jun · Mid-sem break: 29 Jun–5 Jul · Study week: 10–16 Aug · Exams: 17–23 Aug · Break: 24 Aug–6 Sep</td>
                        </tr>
                        <tr>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);font-weight:500;white-space:nowrap;">September Semester</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);white-space:nowrap;">7 Sep – 20 Dec 2026</td>
                            <td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">Registration: 5 Sep · Networking: 31 Oct · Mid-sem break: 2–8 Nov · Study week: 7–13 Dec · Exams: 14–20 Dec · Break: 21 Dec–10 Jan 2027</td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <h3 style="font-size:15px;font-weight:600;margin:24px 0 12px;">Public Holidays (Malaysia) — 2026 &amp; 2027</h3>
            <div style="display:grid;grid-template-columns:1fr 1fr;gap:0;overflow-x:auto;">
                <div>
                    <p style="font-size:12px;font-weight:600;letter-spacing:0.06em;text-transform:uppercase;color:var(--ink-400);margin-bottom:8px;">2026</p>
                    <ul style="font-size:13px;line-height:1.8;list-style:none;padding:0;margin:0;">
                        <li>17–18 Feb &nbsp;—&nbsp; Chinese New Year</li>
                        <li>7 Mar &nbsp;—&nbsp; Nuzul Al-Quran</li>
                        <li>21–23 Mar &nbsp;—&nbsp; Hari Raya Aidilfitri</li>
                        <li>1 May &nbsp;—&nbsp; Labour Day</li>
                        <li>27 May &nbsp;—&nbsp; Hari Raya Haji</li>
                        <li>31 May – 1 Jun &nbsp;—&nbsp; Wesak + Agong's Birthday</li>
                        <li>17 Jun &nbsp;—&nbsp; Awal Muharram</li>
                        <li>25 Aug &nbsp;—&nbsp; Prophet Muhammad's Birthday</li>
                        <li>31 Aug &nbsp;—&nbsp; Merdeka Day (National Day)</li>
                        <li>16 Sep &nbsp;—&nbsp; Malaysia Day</li>
                        <li>8–9 Nov &nbsp;—&nbsp; Deepavali</li>
                        <li>11 Dec &nbsp;—&nbsp; Sultan of Selangor's Birthday</li>
                        <li>25 Dec &nbsp;—&nbsp; Christmas Day</li>
                    </ul>
                </div>
                <div style="padding-left:24px;border-left:1px solid rgba(10,14,31,0.08);">
                    <p style="font-size:12px;font-weight:600;letter-spacing:0.06em;text-transform:uppercase;color:var(--ink-400);margin-bottom:8px;">2027</p>
                    <ul style="font-size:13px;line-height:1.8;list-style:none;padding:0;margin:0;">
                        <li>1 Jan &nbsp;—&nbsp; New Year's Day</li>
                        <li>22 Jan &nbsp;—&nbsp; Thaipusam</li>
                        <li>6–8 Feb &nbsp;—&nbsp; Chinese New Year</li>
                        <li>24 Feb &nbsp;—&nbsp; Nuzul Al-Quran</li>
                        <li>10–11 Mar &nbsp;—&nbsp; Hari Raya Aidilfitri</li>
                        <li>1 May &nbsp;—&nbsp; Labour Day</li>
                        <li>17 May &nbsp;—&nbsp; Hari Raya Haji</li>
                        <li>20 May &nbsp;—&nbsp; Wesak Day</li>
                        <li>6 Jun &nbsp;—&nbsp; Awal Muharram</li>
                        <li>7 Jun &nbsp;—&nbsp; Agong's Birthday</li>
                    </ul>
                </div>
            </div>
            <p style="font-size:12px;color:var(--ink-400);margin-top:16px;">* Public holidays subject to change. Dates from official XMUM academic calendars 2026–2027.</p>

            <p style="margin-top:20px;">Official academic calendar: <a href="https://www.xmu.edu.my/admissions/academic-calendar" target="_blank" rel="noopener">XMUM Academic Calendar</a> &nbsp;·&nbsp; Application portal: <a href="https://apply.xmu.edu.my/" target="_blank" rel="noopener">apply.xmu.edu.my</a></p>
            """);
        Add("Xiamen University Malaysia", "Fees & Prices", "Tuition Fees and Budget", """
            <p>Tuition fees vary by programme. The university publishes a full fee schedule each academic year.</p>
            <ul>
                <li><strong>Application &amp; registration package (international):</strong> RM 3,068 — includes application fee, hostel application fee, and international student fee. Payable on acceptance.</li>
                <li><strong>SST (Sales &amp; Service Tax):</strong> From 1 July 2025, a 6% SST applies to tuition for non-Malaysian citizens. This is included in quoted fee figures for the 2026 academic year.</li>
                <li><strong>Hostel:</strong> Block D twin room — RM 360/month · Block LY twin room — RM 460/month · Single en-suite — RM 560/month · Twin en-suite — RM 340/student/month. Deposit RM 500 + RM 100 hostel application fee (non-refundable).</li>
                <li><strong>Food budget estimate:</strong> RM 400–700/month depending on eating habits (campus cafeterias, Grab delivery, off-campus).</li>
                <li><strong>Transport:</strong> Budget for occasional trips to KL (ERL from Salak Tinggi ~RM 10 each way) and Grab rides.</li>
                <li>Full tuition fee schedule: <a href="https://www.xmu.edu.my/admissions/tuition-fees" target="_blank" rel="noopener">XMUM Tuition Fees</a> — download the PDF for your programme level.</li>
            </ul>
            """);
        Add("Xiamen University Malaysia", "Scholarships", "Scholarships and Financial Aid", """
            <p>XMUM offers merit-based scholarships for international students at foundation and undergraduate level.</p>
            <ul>
                <li><strong>Award range:</strong> 10% to 50% tuition fee waiver. Outstanding applicants may receive up to 100% in exceptional cases.</li>
                <li><strong>Eligibility:</strong> Based on <em>final</em> academic results (not predicted grades). Must hold an unconditional offer.</li>
                <li><strong>Renewal:</strong> Must maintain a required CGPA (varies by scholarship tier) each semester to continue receiving the award. Check your specific conditions in your offer letter.</li>
                <li><strong>Other aid:</strong> Study grants and bursaries for financial need — check the admissions download page for the current-year PDF.</li>
                <li><strong>External scholarships:</strong> Some CIS governments and organisations offer study-abroad grants — check with your country's Ministry of Education before applying.</li>
                <li>Official page: <a href="https://www.xmu.edu.my/admissions/scholarships-financial-aid" target="_blank" rel="noopener">Scholarships &amp; Financial Aid</a></li>
            </ul>
            """);
        Add("Xiamen University Malaysia", "Housing", "Accommodation on Campus", """
            <p>XMUM has on-campus hostels that are the most convenient option, especially for first-year students. Apply early — rooms fill up before each intake.</p>
            <ul>
                <li><strong>Block D (standard twin):</strong> RM 360/month. Shared bathroom. Air-conditioned.</li>
                <li><strong>Block LY (twin):</strong> RM 460/month. Closer to the academic blocks.</li>
                <li><strong>En-suite twin:</strong> RM 340/student/month. Private bathroom shared between 2.</li>
                <li><strong>En-suite single:</strong> RM 560/month. Private room and bathroom.</li>
                <li><strong>Deposit:</strong> RM 500 (refundable at end of stay). Hostel application fee: RM 100 (non-refundable).</li>
                <li><strong>What's included:</strong> Bed, study desk, chair, wardrobe, Wi-Fi, air-conditioning. Each floor has a shared pantry with microwave, refrigerator, kettle, and water dispenser.</li>
                <li><strong>Tip:</strong> Bring a power strip (UK plug type used in Malaysia), a padlock for your wardrobe, and flip-flops for shared bathrooms.</li>
                <li>Accommodation FAQ: <a href="https://www.xmu.edu.my/campus-life/accommodation-services/accommodation-faq" target="_blank" rel="noopener">Accommodation FAQ</a></li>
            </ul>
            """);
        Add("Xiamen University Malaysia", "Useful Contacts", "Key Contacts to Save", """
            <p>Save all of these before you travel. Real numbers posted on campus — every digit verified.</p>

            <h3 style="font-size:15px;font-weight:600;margin:20px 0 10px;">XMUM University Contacts</h3>
            <ul>
                <li><strong>General enquiries:</strong> +60 3-8800 6800 · <a href="mailto:enquiry@xmu.edu.my">enquiry@xmu.edu.my</a></li>
                <li><strong>Courses / enrolment:</strong> +60 3-7610 2079</li>
                <li><strong>International Student Affairs:</strong> <a href="mailto:international@xmu.edu.my">international@xmu.edu.my</a></li>
                <li><strong>LINC (Library &amp; IT):</strong> <a href="https://linc.xmu.edu.my/" target="_blank" rel="noopener">linc.xmu.edu.my</a></li>
                <li>Contact directory: <a href="https://www.xmu.edu.my/contact-us" target="_blank" rel="noopener">xmu.edu.my/contact-us</a></li>
            </ul>

            <h3 style="font-size:15px;font-weight:600;margin:20px 0 10px;">Student Affairs — Emergency Hotlines</h3>
            <p style="font-size:13px;color:var(--ink-500);margin-bottom:10px;">For emergencies from 6pm onwards, weekends, and public holidays — contact assistant wardens on duty or security.</p>
            <ul>
                <li><strong>Student Affairs Hotline 1:</strong> +60 13-917 6801</li>
                <li><strong>Student Affairs Hotline 2:</strong> +60 13-517 6801</li>
                <li><strong>Security Guard Hotline:</strong> 019-348 9999 · 019-295 9998</li>
                <li><strong>Lost key / room unlock:</strong> Contact assistant wardens (6pm–10pm only). Fee: RM 20 during office hours · RM 30 outside office hours.</li>
            </ul>

            <h3 style="font-size:15px;font-weight:600;margin:20px 0 10px;">Local Emergency Numbers (Sepang Area)</h3>
            <ul>
                <li><strong>Police / Ambulance (national):</strong> 999 &nbsp;·&nbsp; mobile: 112</li>
                <li><strong>Fire &amp; Rescue / Bomba (national):</strong> 994 &nbsp;·&nbsp; mobile: 112</li>
                <li><strong>Civil Defence:</strong> 991</li>
                <li><strong>Sepang District Police HQ:</strong> 03-8777 4222</li>
                <li><strong>Sepang Police Station:</strong> 03-3142 1222</li>
                <li><strong>Bandar Baru Salak Tinggi Police:</strong> 03-8777 4484</li>
                <li><strong>Dengkil Police Station:</strong> 03-8768 6222</li>
                <li><strong>KLIA Fire &amp; Rescue:</strong> 03-8787 4970</li>
                <li><strong>Dengkil Fire &amp; Rescue:</strong> 03-8768 7806</li>
                <li><strong>Sepang Civil Defence:</strong> 010-312 1662</li>
            </ul>

            <h3 style="font-size:15px;font-weight:600;margin:20px 0 10px;">Nearby Hospitals</h3>
            <ul>
                <li><strong>Putrajaya Hospital:</strong> 03-8312 4200</li>
                <li><strong>Cyberjaya Hospital:</strong> 03-887 33500</li>
                <li><strong>Aurelius Hospital:</strong> 06-850 5000</li>
                <li><strong>St John's Ambulance:</strong> 03-9285 1576</li>
                <li><strong>Malaysian Red Crescent Ambulance:</strong> 03-2142 8122</li>
            </ul>
            """);
        Add("Xiamen University Malaysia", "Cafeteria / Food", "Food on Campus", """
            <p>The campus has several food options. Prices are generally affordable by Malaysian standards.</p>
            <ul>
                <li><strong>D6 Cafeteria:</strong> Main cafeteria near the D block academic area. Serves rice dishes, noodles, and drinks. Open during the day. Popular for lunch.</li>
                <li><strong>LY3 Cafeteria:</strong> Located near the LY hostel block. Convenient for residents. Often open for breakfast and dinner.</li>
                <li><strong>Lake Cafe:</strong> A quieter café-style spot near the lake area. Good for a coffee or light meal between classes.</li>
                <li><strong>Astana Restaurant:</strong> Serves rice and dishes that CIS students often find familiar (grilled meats, soups). Located near campus or the surrounding area — ask seniors for directions.</li>
                <li><strong>Grab Food / delivery:</strong> Works well for campus delivery. Popular apps: Grab, Shopee Food. Save a nearby convenience store location (7-Eleven, 99 Speedmart) for late-night basics.</li>
                <li><strong>Cash tip:</strong> Some campus stalls are cash only. Keep RM 50–100 in cash at all times, especially in your first week before your bank card is activated.</li>
                <li><strong>Halal:</strong> Most campus food is halal. Check for the JAKIM halal logo at each stall if this matters to you.</li>
            </ul>
            """);
        Add("Xiamen University Malaysia", "Facilities", "Campus Facilities", """
            <p>All sports and fitness facilities are <strong>free</strong> for registered students. Access them with your student card.</p>
            <ul>
                <li><strong>Library (People's Great Hall):</strong> 9 floors, 36,000 sqm, 2,000+ seats, 1M+ books and journals. Group study rooms, innovation labs, exhibition areas, seminar rooms. Open daily.</li>
                <li><strong>LINC:</strong> Learning and Innovation Centre — e-databases, digital resources, IT support at <a href="https://linc.xmu.edu.my/" target="_blank" rel="noopener">linc.xmu.edu.my</a>.</li>
                <li><strong>Sports:</strong> Olympic-size swimming pool · Indoor and outdoor basketball courts · Football field (Olympic-size) · 400m athletics track · Gymnasium (weights, cardio) · Badminton courts · Table tennis · Yoga room · Martial arts room · Gymnastic room. All free.</li>
                <li><strong>24-hour clinic:</strong> On campus. Student personal accident insurance is included in your registration — keep the insurance document accessible.</li>
                <li><strong>Prayer room:</strong> Surau (prayer room) available on campus for Muslim students.</li>
                <li><strong>Wi-Fi:</strong> Campus-wide. Get your login credentials from the IT helpdesk during orientation.</li>
                <li><strong>Student portal / Moodle:</strong> Save your login before classes start. All course materials and announcements are posted there.</li>
                <li>Facilities overview: <a href="https://www.xmu.edu.my/campus-life" target="_blank" rel="noopener">Campus Life</a></li>
            </ul>
            """);
        Add("Xiamen University Malaysia", "Local Updates", "Getting Around and Settling In", """
            <p>Practical logistics for new arrivals. The campus is in Sepang — not in central KL — so transport planning matters from Day 1.</p>
            <ul>
                <li><strong>Nearest train station:</strong> ERL Salak Tinggi (KLIA Transit line). From KL Sentral: ~30 min, ~RM 10. Take the KLIA Transit — <em>not</em> the KLIA Express (it skips Salak Tinggi).</li>
                <li><strong>From KLIA airport:</strong> Take KLIA Transit to Salak Tinggi, then Grab to campus (~RM 8–12).</li>
                <li><strong>Grab:</strong> Most reliable option. Install before landing. Save "XMUM South Gate" as a destination.</li>
            </ul>

            <h3 style="font-size:15px;font-weight:600;margin:20px 0 10px;">Sunsuria City Shuttle — Free Campus Shuttle</h3>
            <p style="font-size:13px;color:var(--ink-500);margin-bottom:12px;">Runs Mon–Sat, 07:00–18:00 only. <strong>No service on Sundays and Public Holidays.</strong> Scheduled service — no booking required. Route: ERL Salak Tinggi → XMUM East Gate → XMUM South Gate → Sunsuria City area. Inquiries: 012-660 9283.</p>

            <div style="overflow-x:auto;margin-bottom:8px;">
                <table style="width:100%;border-collapse:collapse;font-size:13px;font-family:var(--font-mono);">
                    <thead>
                        <tr style="background:rgba(10,14,31,0.05);">
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;white-space:nowrap;">From ERL<br/>Salak Tinggi</th>
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;white-space:nowrap;">XMUM<br/>East Gate</th>
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;white-space:nowrap;background:rgba(194,65,12,0.07);">XMUM<br/>South Gate ★</th>
                            <th style="padding:8px 12px;text-align:left;border:1px solid rgba(10,14,31,0.12);font-weight:600;white-space:nowrap;">Sunsuria City<br/>Celebration Ctr</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">07:00</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">07:07</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);background:rgba(194,65,12,0.04);font-weight:500;">07:37</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">07:50</td></tr>
                        <tr style="background:rgba(10,14,31,0.02);"><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">08:00</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">08:07</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);background:rgba(194,65,12,0.04);font-weight:500;">08:37</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">08:50</td></tr>
                        <tr><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">09:00</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">09:07</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);background:rgba(194,65,12,0.04);font-weight:500;">09:37</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">09:50</td></tr>
                        <tr style="background:rgba(10,14,31,0.02);"><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">11:00</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">11:07</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);background:rgba(194,65,12,0.04);font-weight:500;">11:37</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">11:50</td></tr>
                        <tr><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">12:00</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">12:07</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);background:rgba(194,65,12,0.04);font-weight:500;">12:37</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">12:50</td></tr>
                        <tr style="background:rgba(10,14,31,0.02);"><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">13:00</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">13:07</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);background:rgba(194,65,12,0.04);font-weight:500;">13:37</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">13:50</td></tr>
                        <tr><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">14:00</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">14:07</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);background:rgba(194,65,12,0.04);font-weight:500;">14:37</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">14:50</td></tr>
                        <tr style="background:rgba(10,14,31,0.02);"><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">16:00</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">16:07</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);background:rgba(194,65,12,0.04);font-weight:500;">16:37</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">16:50</td></tr>
                        <tr><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">17:00</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">17:07</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);background:rgba(194,65,12,0.04);font-weight:500;">17:37</td><td style="padding:7px 12px;border:1px solid rgba(10,14,31,0.08);">17:50</td></tr>
                    </tbody>
                </table>
            </div>
            <p style="font-size:12px;color:var(--ink-400);margin-bottom:20px;">★ XMUM South Gate = Point 12 on the Sunsuria City Shuttle route. Shuttle inquiries: 012-660 9283.</p>

            <ul>
                <li><strong>SIM card:</strong> Buy one at KLIA on arrival (Maxis, Celcom, or Digi). Student packages start around RM 30–50/month.</li>
                <li><strong>Banking:</strong> Nearest ATMs are on campus or in the Sunsuria Avenue mall nearby. Open a Malaysian bank account (Maybank or CIMB) within your first 2 weeks.</li>
                <li><strong>Orientation checklist:</strong> Student ID card → Moodle login → clinic location → campus Wi-Fi → hostel laundry → shuttle timetable if any → nearby 7-Eleven.</li>
            </ul>
            """);

        Add("Asia Pacific University (APU)", "Overview", "APU at a Glance", """
            <p>APU is a technology-focused university in Bukit Jalil, Kuala Lumpur, with strong computing, business, engineering, media, and technology-related pathways.</p>
            <ul>
                <li>Good fit for students who want city access and a tech-oriented campus culture.</li>
                <li>Official starting point: <a href="https://www.apu.edu.my/" target="_blank" rel="noopener">APU official website</a>.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Campus Map", "Campus and Location", """
            <p>APU is located around Technology Park Malaysia / Bukit Jalil. Before arrival, check building access, residence transport, and campus shuttle options.</p>
            <ul>
                <li>Official location and contact links are available through the APU site navigation.</li>
                <li>Save your accommodation-to-campus route before your first class week.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Programs", "Programmes to Explore", """
            <p>APU lists programmes across foundation, diploma, undergraduate, master, PhD, language, computing, technology, business, finance, engineering, design, media, psychology, and international relations areas.</p>
            <ul>
                <li>Official course list: <a href="https://www.apu.edu.my/our-courses" target="_blank" rel="noopener">APU Our Courses</a>.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Requirements", "Entry Requirements and Application", """
            <p>Entry requirements vary by programme. Use the application procedure pages and talk to admissions before preparing final documents.</p>
            <ul>
                <li>Check academic requirements, English requirement, passport validity, and visa process.</li>
                <li>International students should confirm EMGS and insurance requirements with APU.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Deadlines", "Intakes and Calendar", """
            <p>APU publishes intake calendar information through its official study pages.</p>
            <ul>
                <li>Check intake calendar before paying fees or booking flights.</li>
                <li>Leave time for offer letter, visa processing, accommodation booking, and airport arrival support.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Fees & Prices", "Fees and Refund Policy", """
            <p>Use APU official fees and refund pages for exact figures. CIS Connect should only show planning guidance until verified prices are entered.</p>
            <ul>
                <li>Budget categories: tuition, registration, accommodation, food, transport, visa, insurance, device/laptop needs, and deposits.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Scholarships", "Scholarships and Financial Aid", """
            <p>APU lists merit scholarships and international student scholarship information in its study menu.</p>
            <ul>
                <li>Check eligibility carefully: awards may depend on intake, nationality, grades, programme, or application deadline.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Housing", "APU Residence Options", """
            <p>APU provides on-campus and off-campus residence information through its Life at APU section.</p>
            <ul>
                <li>Compare room type, transport, deposit, utilities, contract length, and distance to classes.</li>
                <li>Ask whether residence arrival support is available for late-night flights.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Useful Contacts", "Student Services and International Support", """
            <p>Save admissions, international student services, accommodation, counselling, and emergency campus contacts before travel.</p>
            <ul>
                <li>APU also lists student services, international student guide, and student welcome resources in its website navigation.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Cafeteria / Food", "Food Around Bukit Jalil", """
            <p>Students usually use a mix of campus food, nearby malls, delivery apps, and budget restaurants around Bukit Jalil.</p>
            <ul>
                <li>Ask seniors for halal-friendly, late-night, and low-budget options near your residence.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Facilities", "Facilities to Check", """
            <p>APU highlights learning spaces and specialised facilities such as computing, engineering, design, media, and student support areas.</p>
            <ul>
                <li>Identify library/study zones, labs, student services, residence shuttle points, and counselling support.</li>
            </ul>
            """);
        Add("Asia Pacific University (APU)", "Local Updates", "Bukit Jalil Student Notes", """
            <p>This section can hold area tips: transport changes, nearby services, residence notices, and student events.</p>
            <ul>
                <li>Useful first-week checks: SIM card, e-wallet, campus ID, timetable, and route from accommodation.</li>
            </ul>
            """);

        Add("Sunway University", "Overview", "Sunway at a Glance", """
            <p>Sunway University is based in Bandar Sunway and offers a broad range of diploma, undergraduate, postgraduate, and professional pathways in a highly connected urban area.</p>
            <ul>
                <li>Good fit for students who want campus life close to shops, transport, food, and city facilities.</li>
                <li>Official starting point: <a href="https://sunwayuniversity.edu.my/" target="_blank" rel="noopener">Sunway University official website</a>.</li>
            </ul>
            """);
        Add("Sunway University", "Campus Map", "Campus and City Navigation", """
            <p>Sunway provides a campus tour and location information. Learn the route between campus, residence, shops, and transport points early.</p>
            <ul>
                <li>Official campus tour link is available from the Sunway University homepage.</li>
                <li>Bandar Sunway is busy, so save safe walking routes and Grab pickup points.</li>
            </ul>
            """);
        Add("Sunway University", "Programs", "Courses to Explore", """
            <p>Sunway lists courses across diploma, undergraduate, postgraduate, micro-credentials, short courses, and study abroad options.</p>
            <ul>
                <li>Official course page: <a href="https://sunwayuniversity.edu.my/courses" target="_blank" rel="noopener">Sunway Courses</a>.</li>
            </ul>
            """);
        Add("Sunway University", "Requirements", "Admissions Requirements", """
            <p>Entry requirements depend on the course and qualification route. International students should check admission and visa requirements directly with Sunway.</p>
            <ul>
                <li>Prepare academic transcripts, passport, English requirement documents, and certified translations if needed.</li>
            </ul>
            """);
        Add("Sunway University", "Deadlines", "Intakes, Orientation, and Calendar", """
            <p>Sunway publishes admissions, academic calendar, orientation, events, and open day information on its website.</p>
            <ul>
                <li>Check the latest intake and orientation dates before confirming flights.</li>
            </ul>
            """);
        Add("Sunway University", "Fees & Prices", "Fees and Living Budget", """
            <p>Use official Sunway programme pages and admissions resources for exact fees. Prices can differ by programme and intake.</p>
            <ul>
                <li>Budget for tuition, accommodation, food, transport, visa/insurance, books, laptop, and personal spending.</li>
            </ul>
            """);
        Add("Sunway University", "Scholarships", "Scholarships", """
            <p>Sunway links to scholarship information through its admissions menu.</p>
            <ul>
                <li>Official scholarship portal: <a href="https://scholarship.sunway.edu.my/" target="_blank" rel="noopener">Sunway Scholarships</a>.</li>
            </ul>
            """);
        Add("Sunway University", "Housing", "Accommodation Near Campus", """
            <p>Sunway accommodation resources are available from the admissions menu, including residence information.</p>
            <ul>
                <li>Compare residence distance, rental, deposit, security, laundry, utilities, and access to campus.</li>
            </ul>
            """);
        Add("Sunway University", "Useful Contacts", "Campus Support Contacts", """
            <p>Save admissions, student services, accommodation, emergency, and campus security contacts.</p>
            <ul>
                <li>Sunway lists contact information and campus details in its official footer and contact pages.</li>
            </ul>
            """);
        Add("Sunway University", "Cafeteria / Food", "Food Around Bandar Sunway", """
            <p>Bandar Sunway has many food options, from campus meals to mall food courts and student-budget restaurants.</p>
            <ul>
                <li>Ask seniors for affordable daily meal spots, not only tourist-style restaurants.</li>
            </ul>
            """);
        Add("Sunway University", "Facilities", "Facilities and Student Life", """
            <p>Sunway highlights campus life, libraries, student organisations, clubs, and learning facilities.</p>
            <ul>
                <li>Find library access, study spaces, academic services, counselling, career support, and clubs during orientation.</li>
            </ul>
            """);
        Add("Sunway University", "Local Updates", "Bandar Sunway Notes", """
            <p>This section can collect local updates such as transport changes, student events, promotions, and safety reminders.</p>
            <ul>
                <li>Useful for CIS students: halal food, currency exchange, phone shops, clinics, and banking nearby.</li>
            </ul>
            """);

        Add("Taylor's University", "Overview", "Taylor's at a Glance", """
            <p>Taylor's University is based in Subang Jaya and is known for a large lakeside campus, student facilities, and a wide programme portfolio.</p>
            <ul>
                <li>Good fit for students who want an active campus with nearby food, housing, and city access.</li>
                <li>Official starting point: <a href="https://university.taylors.edu.my/en.html" target="_blank" rel="noopener">Taylor's University official website</a>.</li>
            </ul>
            """);
        Add("Taylor's University", "Campus Map", "Campus and Lakeside Navigation", """
            <p>Use the official website and campus resources to understand academic blocks, student central areas, transport points, and support offices.</p>
            <ul>
                <li>Save your class block and ride-hailing pickup point before the first week.</li>
            </ul>
            """);
        Add("Taylor's University", "Programs", "Programmes to Explore", """
            <p>Taylor's offers programmes across foundation, diploma, undergraduate, postgraduate, professional, and specialised study areas.</p>
            <ul>
                <li>Official programme search: <a href="https://university.taylors.edu.my/en/study/explore-all-programmes.html" target="_blank" rel="noopener">Explore All Programmes</a>.</li>
            </ul>
            """);
        Add("Taylor's University", "Requirements", "Entry Requirements", """
            <p>Entry requirements vary by programme. Students should check the selected programme page and confirm English or foundation pathway requirements.</p>
            <ul>
                <li>Prepare academic documents, passport, translations, and proof of English if required.</li>
            </ul>
            """);
        Add("Taylor's University", "Deadlines", "Intakes and Admission Timing", """
            <p>Check latest intake dates and application steps on Taylor's official admissions pages before making travel plans.</p>
            <ul>
                <li>For international students, allow enough time for offer, visa, accommodation, and arrival arrangement.</li>
            </ul>
            """);
        Add("Taylor's University", "Fees & Prices", "Fees and Budget Planning", """
            <p>Exact fees should be confirmed from the programme page and official admissions team.</p>
            <ul>
                <li>Budget for tuition, application/registration fees, accommodation, food, transport, visa, insurance, and course materials.</li>
            </ul>
            """);
        Add("Taylor's University", "Scholarships", "Scholarships and Aid", """
            <p>Taylor's provides scholarship and financial aid information through official admissions resources.</p>
            <ul>
                <li>Check award eligibility, application deadlines, and whether international students can apply.</li>
            </ul>
            """);
        Add("Taylor's University", "Housing", "Housing and Residence", """
            <p>Students commonly compare on-campus or nearby residence options around Subang Jaya.</p>
            <ul>
                <li>Check rental, deposit, contract period, utilities, transport, and safety before paying.</li>
            </ul>
            """);
        Add("Taylor's University", "Useful Contacts", "Support Contacts", """
            <p>Save admissions, international office, student services, accommodation, campus security, and emergency support contacts.</p>
            <ul>
                <li>Ask for the correct office for visa renewal before your student pass nears expiry.</li>
            </ul>
            """);
        Add("Taylor's University", "Cafeteria / Food", "Food Around Campus", """
            <p>Students can use campus food options and surrounding Subang Jaya food areas.</p>
            <ul>
                <li>Build a simple list of daily-budget meals, halal options, and late-night food before classes get busy.</li>
            </ul>
            """);
        Add("Taylor's University", "Facilities", "Campus Facilities", """
            <p>Find the library, student central services, study areas, labs, counselling, sports, and career support early.</p>
            <ul>
                <li>Use orientation week to locate spaces you will use every week.</li>
            </ul>
            """);
        Add("Taylor's University", "Local Updates", "Subang Jaya Notes", """
            <p>This section can collect local transport tips, student events, nearby deals, and residence reminders.</p>
            <ul>
                <li>Useful first-week checks: local SIM, e-wallet, banking, grocery, and clinic options.</li>
            </ul>
            """);

        Add("UCSI University", "Overview", "UCSI at a Glance", """
            <p>UCSI University is a city-based private university with campuses and programmes across multiple academic areas, including music, medicine, health sciences, business, engineering, IT, and hospitality-related pathways.</p>
            <ul>
                <li>Good fit for students who want Kuala Lumpur access and practical city living.</li>
                <li>Official starting point: <a href="https://www.ucsiuniversity.edu.my/" target="_blank" rel="noopener">UCSI official website</a>.</li>
            </ul>
            """);
        Add("UCSI University", "Campus Map", "Campus Navigation", """
            <p>UCSI students should identify the correct campus/building, transport points, admin offices, and nearby services before the first week.</p>
            <ul>
                <li>Check official campus and contact pages for the latest directions.</li>
            </ul>
            """);
        Add("UCSI University", "Programs", "Programmes to Explore", """
            <p>UCSI lists programmes through faculties and study levels. Check the official programme pages for exact entry requirements and fees.</p>
            <ul>
                <li>Official starting point: <a href="https://www.ucsiuniversity.edu.my/" target="_blank" rel="noopener">UCSI programme navigation</a>.</li>
            </ul>
            """);
        Add("UCSI University", "Requirements", "Admission Requirements", """
            <p>Requirements depend on programme, level, and qualification background.</p>
            <ul>
                <li>Prepare passport, academic documents, English proof if required, and programme-specific materials such as portfolio or interview items if applicable.</li>
            </ul>
            """);
        Add("UCSI University", "Deadlines", "Intakes and Timing", """
            <p>Check official intake dates and admissions instructions before finalising travel and accommodation.</p>
            <ul>
                <li>International students should allow time for offer letter, visa processing, health screening, and arrival setup.</li>
            </ul>
            """);
        Add("UCSI University", "Fees & Prices", "Fees and Student Budget", """
            <p>Use official UCSI programme pages and admissions contacts for exact fees.</p>
            <ul>
                <li>Budget for tuition, registration, accommodation, meals, transport, visa, insurance, books, and personal spending.</li>
            </ul>
            """);
        Add("UCSI University", "Scholarships", "Scholarships and Financial Aid", """
            <p>UCSI publishes scholarship and financial aid information through its official website.</p>
            <ul>
                <li>Check whether your programme and nationality are eligible before applying.</li>
            </ul>
            """);
        Add("UCSI University", "Housing", "Housing and Daily Living", """
            <p>Students should compare UCSI accommodation options and nearby rentals based on distance, safety, transport, and monthly cost.</p>
            <ul>
                <li>Ask about contract length, deposit, utilities, Wi-Fi, cooking rules, and public transport access.</li>
            </ul>
            """);
        Add("UCSI University", "Useful Contacts", "Student Support Contacts", """
            <p>Save admissions, international office, student affairs, residence, emergency, and campus security contacts.</p>
            <ul>
                <li>For visa renewal, ask your university office about document deadlines early.</li>
            </ul>
            """);
        Add("UCSI University", "Cafeteria / Food", "Food Around Cheras", """
            <p>Cheras has many local food options, but new international students should still build a simple safe list first.</p>
            <ul>
                <li>Track affordable meals, halal-friendly restaurants, groceries, and delivery areas around your accommodation.</li>
            </ul>
            """);
        Add("UCSI University", "Facilities", "Facilities to Find First", """
            <p>Identify student services, academic offices, library, study areas, labs/studios, clinics, and counselling support during orientation.</p>
            <ul>
                <li>Keep portal, timetable, Wi-Fi, and student ID instructions saved offline.</li>
            </ul>
            """);
        Add("UCSI University", "Local Updates", "Cheras and KL Notes", """
            <p>This section can store local transport, safety, events, nearby services, and community updates for students around UCSI.</p>
            <ul>
                <li>Useful first-week checks: SIM card, MRT/LRT route, Grab points, banking, and clinic options.</li>
            </ul>
            """);

        return list;
    }

    private sealed record UniversitySectionSeed(
        string UniversityName,
        string SectionType,
        string Title,
        string Content);

    private static async Task UpsertGuideArticlesAsync(ApplicationDbContext context)
    {
        await UpsertGuideArticleAsync(
            context,
            "pre-arrival",
            new GuideArticle
            {
                Title = "Before Departure to Malaysia",
                Summary = "Check your university, program accreditation, visa process, and travel readiness before buying tickets.",
                Content = """
                    <p>This pre-arrival guide helps CIS students prepare before leaving for Malaysia. It is especially useful for students applying for foundation, undergraduate, postgraduate, PhD, language course, or summer program study routes.</p>

                    <h2>For language courses and summer camps</h2>
                    <ul>
                        <li>Sign a clear agreement with the course provider before payment.</li>
                        <li>Check refund conditions, student responsibilities, and what support is included.</li>
                        <li>Confirm whether a student visa is required for your study period before travelling.</li>
                    </ul>

                    <h2>For foundation, degree, postgraduate, and PhD students</h2>
                    <ul>
                        <li>Verify that your university is officially registered with Malaysia's Ministry of Higher Education: <a href="https://jpt.mohe.gov.my/portal/index.php/en/hei/private-hei/25-list-of-private-hei-registration-and-statistic" target="_blank" rel="noopener">Private HEI registration list</a>.</li>
                        <li>Check whether your academic program appears in the Malaysian Qualifications Register: <a href="https://www2.mqa.gov.my/esisraf/kelayakan.cfm" target="_blank" rel="noopener">MQA programme accreditation search</a>.</li>
                        <li>Wait for your official offer letter before starting the visa process.</li>
                        <li>Submit the required documents to your university so they can begin your student visa application.</li>
                        <li>Your university will submit your documents to <a href="https://educationmalaysia.gov.my/" target="_blank" rel="noopener">Education Malaysia Global Services (EMGS)</a>.</li>
                        <li>Track your application through EMGS. A 70% application status usually means EMGS has approved the student visa stage, but students should still wait for official university instructions before making final travel plans.</li>
                    </ul>

                    <h2>Before buying flight tickets</h2>
                    <ul>
                        <li>Wait for prior visa approval and entry permission/student pass instructions.</li>
                        <li>Keep printed and digital copies of your passport, offer letter, visa approval documents, accommodation address, and university contact details.</li>
                        <li>Ask your university whether airport pickup is arranged and where the representative will meet you.</li>
                    </ul>
                    """,
                CreatedAt = new DateTime(2026, 5, 20)
            });

        await UpsertGuideArticleAsync(
            context,
            "pre-arrival",
            new GuideArticle
            {
                Title = "Upon Arrival in Malaysia: ISAC and Immigration",
                Summary = "What to do at KLIA before passing immigration as an international student.",
                Content = """
                    <p>When you arrive in Malaysia as an international student, do not rush straight through immigration. First, report to the International Student Arrival Centre (ISAC).</p>

                    <h2>Where to go</h2>
                    <ul>
                        <li>KLIA Terminal 1: Level 3, Arrival Hall.</li>
                        <li>KLIA Terminal 2: Level 3A.</li>
                        <li>Official reference: <a href="https://educationmalaysia.gov.my/plan-your-studies/start-to-prepare/international-student-arrival-centre-(isac)" target="_blank" rel="noopener">EMGS International Student Arrival Centre (ISAC)</a>.</li>
                    </ul>

                    <h2>Why ISAC matters</h2>
                    <ul>
                        <li>You should report to ISAC before immigration control.</li>
                        <li>ISAC officers verify your student documents and assist with the arrival process.</li>
                        <li>After verification, students are escorted to immigration.</li>
                        <li>International students may be guided through a priority lane, which can make clearance faster.</li>
                    </ul>

                    <p>After immigration, proceed to the arrival hall. If your university arranged airport transfer, look for the university representative at the agreed meeting point.</p>
                    """,
                CreatedAt = new DateTime(2026, 5, 19)
            });

        await UpsertGuideArticleAsync(
            context,
            "arrival-setup",
            new GuideArticle
            {
                Title = "During Your Stay: Visa Renewal and Embassy Registration",
                Summary = "Complete orientation, monitor your student pass, and keep your embassy contact saved.",
                Content = """
                    <p>After arrival, your priority is to complete university onboarding and keep your legal student status in order.</p>

                    <h2>First university steps</h2>
                    <ul>
                        <li>Attend your university orientation program. For most international students, orientation is mandatory.</li>
                        <li>Save your international office contact and ask them when student pass renewal documents should be submitted.</li>
                        <li>Monitor your student visa or student pass expiry date. Renewal can take around 1-2 months, so do not wait until the last week.</li>
                        <li>For immigration matters, check official Malaysian Immigration updates: <a href="https://www.imi.gov.my/" target="_blank" rel="noopener">Immigration Department of Malaysia</a>.</li>
                    </ul>

                    <h2>Embassy and consular links for CIS students</h2>
                    <ul>
                        <li>Kazakhstan: <a href="https://www.gov.kz/memleket/entities/mfa-kuala-lumpur?lang=en" target="_blank" rel="noopener">Embassy of Kazakhstan in Malaysia</a>; consular registration note: <a href="https://www.gov.kz/memleket/entities/mfa-kuala-lumpur/press/article/details/73080?lang=ru" target="_blank" rel="noopener">registration information</a>.</li>
                        <li>Uzbekistan: <a href="https://gov.uz/en/mfa/sections/view/52419" target="_blank" rel="noopener">Embassy of Uzbekistan in Malaysia</a>.</li>
                        <li>Kyrgyzstan: <a href="https://mfa.gov.kg/en/dm/Embassy-of-the-Kyrgyz-Republic-in-the-Malaysia" target="_blank" rel="noopener">Embassy of the Kyrgyz Republic in Malaysia</a>.</li>
                        <li>Tajikistan: <a href="https://www.mfa.tj/en/main/view/1259/embassy-of-the-republic-of-tajikistan-in-malaysia" target="_blank" rel="noopener">Embassy of Tajikistan in Malaysia</a>.</li>
                        <li>Turkmenistan: <a href="https://malaysia.tmembassy.gov.tm/" target="_blank" rel="noopener">Embassy of Turkmenistan in Malaysia</a>.</li>
                        <li>Azerbaijan: <a href="https://kualalumpur.mfa.gov.az/en" target="_blank" rel="noopener">Embassy of Azerbaijan in Malaysia</a>.</li>
                        <li>Russia: <a href="https://malaysia.mid.ru/" target="_blank" rel="noopener">Embassy of Russia in Malaysia</a>.</li>
                        <li>Armenia: <a href="https://www.mfa.am/en/embassies/my" target="_blank" rel="noopener">Embassy of Armenia accredited to Malaysia</a>.</li>
                        <li>Georgia: <a href="https://malaysia.mfa.gov.ge/en/contact" target="_blank" rel="noopener">Embassy of Georgia in Malaysia</a>.</li>
                    </ul>

                    <p>If your country is not listed, use your Ministry of Foreign Affairs directory and confirm whether the embassy is resident in Malaysia or accredited from another country.</p>
                    """,
                CreatedAt = new DateTime(2026, 5, 18)
            });

        await UpsertGuideArticleAsync(
            context,
            "arrival-setup",
            new GuideArticle
            {
                Title = "First-Week Essentials: Where to Go After Arrival",
                Summary = "XMUM-specific video guide for newly arrived students: food, cash, SIM cards, basic shopping, and medical help.",
                UniversityTag = "XMUM",
                Content = """
                    <div class="article-video-block">
                        <iframe
                            class="post-media-youtube"
                            src="https://www.youtube-nocookie.com/embed/4qgBXPA81WM"
                            title="First-Week Essentials: Where to Go After Arrival"
                            loading="lazy"
                            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
                            referrerpolicy="strict-origin-when-cross-origin"
                            allowfullscreen>
                        </iframe>
                    </div>

                    <p>This XMUM-specific arrival guide is for students who have just reached Xiamen University Malaysia and need practical first-week help. It focuses on the places that matter immediately: where to eat, where to withdraw cash, where to buy basic items, where to get a SIM card, and where to go if medical support is needed.</p>

                    <h2>What this video covers</h2>
                    <ul>
                        <li>Nearby food options for the first few days after arrival.</li>
                        <li>Mini-market and basic shopping points for essentials.</li>
                        <li>ATM or cash withdrawal locations.</li>
                        <li>SIM card and communication setup tips.</li>
                        <li>Where students can look for basic medical help or support.</li>
                    </ul>

                    <p>Use this as a quick orientation before walking around campus or the nearby area. For official registration, visa, and health screening requirements, always follow your university's latest instructions.</p>
                    """,
                CreatedAt = new DateTime(2026, 5, 21)
            });

        await UpsertGuideArticleAsync(
            context,
            "cis-community",
            new GuideArticle
            {
                Title = "Joining Kazakh Communities in Malaysia",
                Summary = "Kazakh students can use diaspora and student association groups for community support.",
                Content = """
                    <p>Community groups are useful for asking practical questions, finding support, and staying aware of student events. These links are especially relevant for Kazakh students in Malaysia.</p>

                    <h2>Kazakh diaspora in Malaysia</h2>
                    <ul>
                        <li>WhatsApp group: <a href="https://chat.whatsapp.com/DXLyBDJTKNlBnpr9DJEFXs?mode=wwc%20" target="_blank" rel="noopener">Kazakh diaspora WhatsApp group</a>.</li>
                        <li>Community contact: Nishan Bayan.</li>
                        <li>Phone: +60 12-684-2090.</li>
                    </ul>

                    <p>Please join community chats respectfully: introduce yourself, avoid spam, and verify important legal or visa information through official sources before acting on it.</p>
                    """,
                CreatedAt = new DateTime(2026, 5, 17)
            });

        await UpsertGuideArticleAsync(
            context,
            "cis-community",
            new GuideArticle
            {
                Title = "Kazakh Students Association in Malaysia",
                Summary = "A student-focused community channel for Kazakh students studying in Malaysia.",
                Content = """
                    <p>The Kazakh Students Association can help students connect with peers, ask everyday questions, and hear about student activities.</p>

                    <h2>Student association contact</h2>
                    <ul>
                        <li>WhatsApp chat: <a href="https://chat.whatsapp.com/J1h2HtaevhVJgoMg20FdYj?mode=gi_t" target="_blank" rel="noopener">Kazakh Students Association WhatsApp chat</a>.</li>
                        <li>Contact person: Issabekova Akerke.</li>
                        <li>Phone: +60 11-6976-1034.</li>
                    </ul>

                    <p>For official matters such as passport, consular registration, or legal documents, contact the embassy or consular section directly.</p>
                    """,
                CreatedAt = new DateTime(2026, 5, 16)
            });

        await UpsertGuideArticleAsync(
            context,
            "deals-recommendations",
            new GuideArticle
            {
                Title = "Student Food Spots: D6, LY3, Lake Cafe, and B1",
                Summary = "XMUM-specific food guide covering D6, LY3, Lake Cafe, and B1, including Astana Restaurant for CIS-style food.",
                UniversityTag = "XMUM",
                Content = """
                    <div class="article-video-block">
                        <iframe
                            class="post-media-youtube"
                            src="https://www.youtube-nocookie.com/embed/iAbdCnIlvkk"
                            title="Student Food Spots: D6, LY3, Lake Cafe, and B1"
                            loading="lazy"
                            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
                            referrerpolicy="strict-origin-when-cross-origin"
                            allowfullscreen>
                        </iframe>
                    </div>

                    <p>This XMUM food-focused guide helps students quickly understand where they can eat around campus and nearby student areas. It is especially useful when you are new, tired after classes, or simply trying to find familiar food options.</p>

                    <h2>Food spots mentioned</h2>
                    <ul>
                        <li><strong>D6</strong> — a convenient student food point for everyday meals.</li>
                        <li><strong>LY3</strong> — another useful option when students want something quick nearby.</li>
                        <li><strong>Lake Cafe</strong> — a casual spot for food, drinks, and study breaks.</li>
                        <li><strong>B1</strong> — includes more food options, including <strong>Astana Restaurant</strong>, which is useful for students looking for CIS-style cuisine.</li>
                    </ul>

                    <p>Prices, opening hours, and menus can change, so treat this as a friendly student recommendation and double-check current availability when you visit.</p>
                    """,
                CreatedAt = new DateTime(2026, 5, 22)
            });

        await UpsertGuideArticleAsync(
            context,
            "deals-recommendations",
            new GuideArticle
            {
                Title = "After Graduation: Diploma Legalization",
                Summary = "A checklist for graduates who may need official legalization before using Malaysian documents abroad.",
                Content = """
                    <p>This is not a discount post, but it fits the Deals & Recommendations section because it helps graduates avoid unnecessary costs, repeat appointments, and document delays after finishing university.</p>

                    <h2>Before starting</h2>
                    <ul>
                        <li>Ask your embassy whether diploma legalization is required for your country.</li>
                        <li>Confirm whether original documents, certified copies, translations, or appointment booking are needed.</li>
                        <li>Do not submit your only copy without scanning it first.</li>
                    </ul>

                    <h2>Typical document set</h2>
                    <ul>
                        <li>Graduation completion letter, preferably issued within the accepted validity period.</li>
                        <li>Copy of student visa or student pass.</li>
                        <li>Diploma certificate.</li>
                        <li>Academic transcript.</li>
                    </ul>

                    <h2>Possible legalization flow</h2>
                    <ul>
                        <li>Step 1: verify education-related document requirements with the relevant Malaysian authority or your university.</li>
                        <li>Step 2: check requirements with the Ministry of Foreign Affairs Malaysia if foreign-affairs authentication is needed.</li>
                        <li>Step 3: submit to your home country's embassy or consular section if embassy legalization is required.</li>
                    </ul>

                    <p>For Kazakhstan-specific cases, students should check the Consular Section of the Embassy of Kazakhstan in Malaysia. For other countries, use the embassy links in the Arrival & Setup guide and confirm the latest process directly.</p>
                    """,
                CreatedAt = new DateTime(2026, 5, 15)
            });

        await UpsertGuideArticleAsync(
            context,
            "pre-arrival",
            new GuideArticle
            {
                Title = "Pre-Arrival Checklist for CIS Students",
                Summary = "A practical packing list for CIS students getting ready for Malaysia's climate and daily life.",
                Content = """
                    <p>Prepare your passport, student visa documents, university offer letter, accommodation address, and enough Malaysian currency or an international payment option for your first days.</p>

                    <h2>What to actually pack</h2>
                    <ul>
                        <li><strong>Medication.</strong> Bring a supply of any prescription medicine you take regularly, plus basics like painkillers and cold medicine in brands you trust. Pharmacies in Malaysia are excellent, but matching your exact medication or dosage can take time in the first weeks.</li>
                        <li><strong>A light bedding set.</strong> Most hostel and condo rooms come with a mattress but not sheets, a pillowcase, or a blanket. Bring a compact set so you're not sleeping on a bare mattress on night one.</li>
                        <li><strong>A few warm layers.</strong> Malaysia is tropical outside, but almost every indoor space — lecture halls, libraries, malls, buses — runs air conditioning aggressively cold. A hoodie or light cardigan ends up living in your bag daily, even though you'll never need a winter coat.</li>
                    </ul>

                    <h2>An honest warning from students who already moved</h2>
                    <p>No official checklist mentions this, but it deserves one: you will not easily find syrki (glazed curd snacks), grechka, crab-stick chips, or the exact Lay's flavors you grew up with. Malaysia has excellent food — just not that food. If any of these are non-negotiable for your happiness, pack a small stash in your suitcase. Future you, three weeks in, will be very grateful.</p>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "pre-arrival",
            new GuideArticle
            {
                Title = "How to Get Your Malaysia Student Visa: The Full Process",
                Summary = "A complete step-by-step walkthrough of the EMGS visa process, from your offer letter to landing in Malaysia.",
                Content = """
                    <p>Every international student, regardless of nationality, goes through the same government system to study in Malaysia: EMGS (Education Malaysia Global Services). This guide walks through the full process from offer letter to arrival.</p>

                    <h2>Step 1 — Get your offer letter</h2>
                    <ul>
                        <li>Apply and get accepted to an MQA-accredited university or college in Malaysia.</li>
                        <li>Your university will issue an official offer/acceptance letter — this is required before anything else can start.</li>
                    </ul>

                    <h2>Step 2 — EMGS application and the Visa Approval Letter (VAL)</h2>
                    <ul>
                        <li>Your university submits your application to EMGS on the official portal (visa.educationmalaysia.gov.my).</li>
                        <li>EMGS runs an academic screening, then forwards an approved application to the Immigration Department of Malaysia.</li>
                        <li>You receive a <strong>Visa Approval Letter (VAL)</strong> — this is the key document. It confirms you're cleared to travel to Malaysia as a student.</li>
                        <li>Typical processing time is 4–8 weeks, longer for a few nationalities that require extra security clearance, so start early.</li>
                        <li>Your passport must be valid for at least 18 months from your intended travel date.</li>
                    </ul>

                    <h2>Step 3 — Check if you need a Single Entry Visa (SEV)</h2>
                    <p>This is the one step that differs by nationality. Some countries are on EMGS's official SEV-required list — if yours is, you must get a physical visa sticker from a Malaysian embassy or consulate <em>before</em> flying. If your country is not on that list, you travel on your VAL alone and get stamped in on arrival. See the country-specific guide for your nationality for the exact steps.</p>

                    <h2>Step 4 — Before you fly</h2>
                    <ul>
                        <li>Print (and save digitally) your VAL, offer letter, and SEV if applicable.</li>
                        <li>Book flights only after your VAL is confirmed — never before.</li>
                        <li>Complete the Malaysia Digital Arrival Card (MDAC) online shortly before departure.</li>
                    </ul>

                    <h2>Step 5 — Arrival and Student Pass</h2>
                    <ul>
                        <li>Report to the ISAC (International Student Arrival Centre) counter at KLIA before immigration, as covered in the Arrival & Setup guide.</li>
                        <li>Complete a Post-Arrival Medical Examination within 7 days of landing.</li>
                        <li>Once medical results clear, your passport is endorsed with the actual Student Pass sticker, which is what makes your stay legal for the length of your studies.</li>
                    </ul>

                    <p>Keep every document from this process (VAL, SEV, medical report, Student Pass) — you'll need copies for bank accounts, SIM cards, and visa renewal later.</p>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "pre-arrival",
            new GuideArticle
            {
                Title = "Malaysia Student Visa Process for Kazakhstan Citizens",
                Summary = "Kazakhstan is on EMGS's Single Entry Visa list — here's what that means for your application.",
                CountryTag = "Kazakhstan",
                Content = """
                    <p>The overall process is the same EMGS system every student goes through (see the full visa guide), but Kazakhstan is currently listed by EMGS as a <strong>Single Entry Visa (SEV) required</strong> country.</p>

                    <h2>What this means for you</h2>
                    <ul>
                        <li>After your university submits your application and EMGS issues your Visa Approval Letter (VAL), you cannot simply fly to Malaysia with just the VAL.</li>
                        <li>You need to take your VAL to the nearest Malaysian embassy or consulate to obtain the physical SEV sticker in your passport before departure.</li>
                        <li>For students based in Kazakhstan, this is normally arranged through the Malaysian mission covering your region — confirm the exact office and appointment process directly with them, since procedures can change.</li>
                    </ul>

                    <h2>Good to know</h2>
                    <ul>
                        <li>Start the process early — combining EMGS processing time with an embassy SEV appointment can take longer than students expect.</li>
                        <li>Keep both your VAL and SEV printed and with your passport when you travel; immigration checks both on arrival.</li>
                        <li>For consular registration and any passport issues once you're in Malaysia, the Embassy of Kazakhstan in Malaysia is your main point of contact.</li>
                    </ul>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "pre-arrival",
            new GuideArticle
            {
                Title = "Malaysia Student Visa Process for Kyrgyzstan Citizens",
                Summary = "Kyrgyzstan is not on EMGS's Single Entry Visa list, which simplifies your pre-departure steps.",
                CountryTag = "Kyrgyzstan",
                Content = """
                    <p>The overall process is the same EMGS system every student goes through (see the full visa guide). The good news for Kyrgyzstan citizens: your country is <strong>not</strong> currently on EMGS's Single Entry Visa (SEV) required list.</p>

                    <h2>What this means for you</h2>
                    <ul>
                        <li>Once your university and EMGS issue your Visa Approval Letter (VAL), you can travel to Malaysia on that letter alone — no separate embassy visa appointment is needed before flying.</li>
                        <li>Bring a printed and digital copy of your VAL and present it at Malaysian immigration on arrival, along with your passport.</li>
                        <li>Kyrgyzstani citizens generally get short-stay visa exemption for entry into Malaysia, but the VAL is still what confirms your student status and purpose of travel — carry it regardless.</li>
                    </ul>

                    <h2>Good to know</h2>
                    <ul>
                        <li>SEV requirements can change, so double-check the current list on the EMGS website close to your travel date rather than relying only on this guide.</li>
                        <li>You'll still need to complete the Malaysia Digital Arrival Card (MDAC) online before you fly.</li>
                        <li>For consular matters after arrival, the Embassy of the Kyrgyz Republic in Malaysia is your main point of contact.</li>
                    </ul>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "pre-arrival",
            new GuideArticle
            {
                Title = "Malaysia Student Visa Process for Russia Citizens",
                Summary = "Russia is on EMGS's Single Entry Visa list — here's what that means for your application.",
                CountryTag = "Russia",
                Content = """
                    <p>The overall process is the same EMGS system every student goes through (see the full visa guide), but Russia is currently listed by EMGS as a <strong>Single Entry Visa (SEV) required</strong> country.</p>

                    <h2>What this means for you</h2>
                    <ul>
                        <li>After your university submits your application and EMGS issues your Visa Approval Letter (VAL), you'll need to obtain a physical SEV sticker in your passport before departure.</li>
                        <li>Take your VAL to the nearest Malaysian embassy or consulate to apply for the SEV — confirm the current appointment process with them directly, as it can change.</li>
                        <li>If there's no Malaysian mission near you, check whether a neighboring country's Malaysian embassy handles applications from your region.</li>
                    </ul>

                    <h2>Good to know</h2>
                    <ul>
                        <li>Start early — EMGS processing plus an embassy SEV appointment can take longer than expected, especially during intake peak periods.</li>
                        <li>Carry both your VAL and SEV, printed and with your passport, when you travel.</li>
                        <li>For consular registration and passport matters after arrival, the Embassy of Russia in Malaysia is your main point of contact.</li>
                    </ul>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "pre-arrival",
            new GuideArticle
            {
                Title = "Malaysia Student Visa Process for Tajikistan Citizens",
                Summary = "Tajikistan is not on EMGS's Single Entry Visa list, which simplifies your pre-departure steps.",
                CountryTag = "Tajikistan",
                Content = """
                    <p>The overall process is the same EMGS system every student goes through (see the full visa guide). The good news for Tajikistan citizens: your country is <strong>not</strong> currently on EMGS's Single Entry Visa (SEV) required list.</p>

                    <h2>What this means for you</h2>
                    <ul>
                        <li>Once your university and EMGS issue your Visa Approval Letter (VAL), you can travel to Malaysia on that letter alone — no separate embassy visa appointment is needed before flying.</li>
                        <li>Bring a printed and digital copy of your VAL and present it at Malaysian immigration on arrival, along with your passport.</li>
                        <li>The VAL is what confirms your student status and purpose of travel, so keep it accessible, not buried in checked luggage.</li>
                    </ul>

                    <h2>Good to know</h2>
                    <ul>
                        <li>SEV requirements can change, so double-check the current list on the EMGS website close to your travel date rather than relying only on this guide.</li>
                        <li>You'll still need to complete the Malaysia Digital Arrival Card (MDAC) online before you fly.</li>
                        <li>For consular matters after arrival, the Embassy of Tajikistan in Malaysia is your main point of contact.</li>
                    </ul>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "pre-arrival",
            new GuideArticle
            {
                Title = "Malaysia Student Visa Process for Uzbekistan Citizens",
                Summary = "Uzbekistan is on EMGS's Single Entry Visa list — here's what that means for your application.",
                CountryTag = "Uzbekistan",
                Content = """
                    <p>The overall process is the same EMGS system every student goes through (see the full visa guide), but Uzbekistan is currently listed by EMGS as a <strong>Single Entry Visa (SEV) required</strong> country.</p>

                    <h2>What this means for you</h2>
                    <ul>
                        <li>After your university submits your application and EMGS issues your Visa Approval Letter (VAL), you'll need to obtain a physical SEV sticker in your passport before departure.</li>
                        <li>Take your VAL to the nearest Malaysian embassy or consulate to apply for the SEV — confirm the current appointment process with them directly, as it can change.</li>
                        <li>Build extra time into your plans for this step, especially if the nearest Malaysian mission is not in your home city.</li>
                    </ul>

                    <h2>Good to know</h2>
                    <ul>
                        <li>Start early — EMGS processing plus an embassy SEV appointment can take longer than expected.</li>
                        <li>Carry both your VAL and SEV, printed and with your passport, when you travel.</li>
                        <li>For consular registration and passport matters after arrival, the Embassy of Uzbekistan in Malaysia is your main point of contact.</li>
                    </ul>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "living-in-malaysia",
            new GuideArticle
            {
                Title = "Everyday Apps: Food Delivery and Online Shopping",
                Summary = "The apps most students end up using every week for food and shopping in Malaysia.",
                Content = """
                    <p>Within the first few days, almost every student ends up installing the same four apps. Getting them set up early saves a lot of hassle.</p>

                    <h2>Food delivery</h2>
                    <ul>
                        <li><strong>Grab</strong> — the biggest platform in Malaysia: food delivery (GrabFood), ride-hailing, and its own e-wallet (GrabPay) that many small stalls and vending machines accept. Worth setting up even if you don't order food often.</li>
                        <li><strong>Foodpanda</strong> — Grab's main competitor, often has different restaurant partners and its own regular promo codes, so it's worth comparing prices between the two before ordering.</li>
                    </ul>

                    <h2>Online shopping</h2>
                    <ul>
                        <li><strong>Shopee</strong> — the most-used shopping app for students: cheap daily essentials, dorm items, electronics accessories, and frequent flash sales.</li>
                        <li><strong>Lazada</strong> — similar to Shopee, sometimes better for electronics and larger brand items, and useful to check for price comparison before buying.</li>
                    </ul>

                    <p>Tip: both delivery apps and both shopping apps regularly run new-user and student promo codes, so it's worth checking for a voucher before your first order rather than paying full delivery fee.</p>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "deals-recommendations",
            new GuideArticle
            {
                Title = "Bukit Jalil Food & Discount Spots Near APU",
                Summary = "Where APU students actually eat and shop around Technology Park Malaysia and Bukit Jalil.",
                UniversityTag = "APU",
                Content = """
                    <p>APU sits inside Technology Park Malaysia (TPM) in Bukit Jalil, and the campus shuttle runs to Bukit Jalil LRT station, which puts a full mall and a wide food scene within easy reach.</p>

                    <h2>Nearby food</h2>
                    <ul>
                        <li><strong>Endah Parade</strong> — a shopping mall directly across from Bukit Jalil Sports Complex and the LRT station, with a food court, fast food chains, bookstores, a karaoke centre, futsal courts, and a gym.</li>
                        <li>Within about a 15-minute walk of TPM you'll also find Ramly Halal Kiosk at Axiata Arena, Cockroach Cafeteria, Picante Café, Subway, and Sri Petaling Seafood Village — a decent spread for both quick halal bites and sit-down meals.</li>
                    </ul>

                    <h2>Student discounts worth knowing about</h2>
                    <ul>
                        <li><strong>Popular Bookstore</strong> — 20% off Popular/Pilihan Popular titles, also valid at HARRIS bookstores and CD-RAMA, especially useful for cheap stationery and textbooks.</li>
                        <li><strong>GSC Cinemas</strong> — student-priced tickets (as low as RM12 for weekday shows before 6pm) with a valid student ID — good for a cheap study break.</li>
                        <li><strong>ISIC card</strong> — if you get an International Student Identity Card, it unlocks thousands of extra deals across food, retail, and travel beyond just the campus area.</li>
                    </ul>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "deals-recommendations",
            new GuideArticle
            {
                Title = "Sunway Pyramid Student Discounts for Sunway University Students",
                Summary = "How to use Sunway Pyramid and Sunway GEO Avenue for cheap food and real student discounts.",
                UniversityTag = "Sunway",
                Content = """
                    <p>Sunway University sits right inside Sunway City, which means students have direct access to Sunway Pyramid Mall, Sunway GEO Avenue, and Sunway Lagoon without needing transport.</p>

                    <h2>Everyday food</h2>
                    <ul>
                        <li>The on-campus cafeteria and the food courts, cafés, and coffee shops around campus keep meal prices around RM5–RM15, and all food served in the campus cafeteria and hostel is halal.</li>
                        <li>For a change of scenery, Sunway Pyramid and Sunway GEO Avenue are both walkable from campus and have a much wider range of restaurants and food courts.</li>
                    </ul>

                    <h2>Student discounts worth knowing about</h2>
                    <ul>
                        <li><strong>Popular Bookstore</strong> — up to 20% off with a two-year student membership that costs just RM10, useful across multiple visits.</li>
                        <li><strong>The Face Shop</strong> (Sunway Pyramid) — exclusive student discounts up to 50% off skincare, hair, and nail care products.</li>
                        <li>Both Sunway Pyramid and Sunway GEO Avenue regularly rotate student promotions across retail stores — it's worth checking in with a valid student ID even at shops that don't advertise a discount.</li>
                    </ul>

                    <p>Average monthly living costs (food and accommodation) for Sunway students run around RM2,000, though this depends heavily on personal habits and how often you eat off campus.</p>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await UpsertGuideArticleAsync(
            context,
            "cis-community",
            new GuideArticle
            {
                Title = "Uzbek Student Community: World Association of Youth Uzbekistan (WAYU)",
                Summary = "An active Uzbek student community in Malaysia running regular meetups, games, and events.",
                Content = """
                    <p>The World Association of Youth Uzbekistan (WAYU) runs an active Uzbek student community in Malaysia. They organize regular meetups and events where students get together to play games, socialize, and support each other outside of university life.</p>

                    <h2>What they do</h2>
                    <ul>
                        <li>Regular community meetups and social events for Uzbek students across Malaysia.</li>
                        <li>Game nights and casual hangouts — a good way to meet other Uzbek students outside your own university.</li>
                        <li>Community support and information sharing between students.</li>
                    </ul>

                    <h2>How to join</h2>
                    <p>Follow their Instagram for event announcements and to see what the community has been up to: <a href="https://www.instagram.com/wayu_malaysia" target="_blank" rel="noopener noreferrer">instagram.com/wayu_malaysia</a>.</p>
                    """,
                CreatedAt = new DateTime(2026, 7, 4)
            });

        await context.SaveChangesAsync();
    }

    private static async Task EnsureSourceColumnsAsync(ApplicationDbContext context)
    {
        await EnsureColumnAsync(context, "GuideArticles", "UniversityTag", "varchar(100) NULL DEFAULT NULL");
        await EnsureColumnAsync(context, "GuideArticles", "CountryTag", "varchar(100) NULL DEFAULT NULL");
        await EnsureColumnAsync(context, "GuideArticles", "SourceName", "varchar(200) NOT NULL DEFAULT ''");
        await EnsureColumnAsync(context, "GuideArticles", "SourceUrl", "varchar(1000) NOT NULL DEFAULT ''");
        await EnsureColumnAsync(context, "GuideArticles", "LastVerifiedAt", "datetime(6) NULL DEFAULT NULL");

        await EnsureColumnAsync(context, "UpdatePosts", "SourceName", "varchar(200) NOT NULL DEFAULT ''");
        await EnsureColumnAsync(context, "UpdatePosts", "SourceUrl", "varchar(1000) NOT NULL DEFAULT ''");
        await EnsureColumnAsync(context, "UpdatePosts", "LastVerifiedAt", "datetime(6) NULL DEFAULT NULL");

        await EnsureColumnAsync(context, "UniversitySections", "SourceName", "varchar(200) NOT NULL DEFAULT ''");
        await EnsureColumnAsync(context, "UniversitySections", "SourceUrl", "varchar(1000) NOT NULL DEFAULT ''");
        await EnsureColumnAsync(context, "UniversitySections", "LastVerifiedAt", "datetime(6) NULL DEFAULT NULL");
    }

    private static async Task EnsureColumnAsync(
        ApplicationDbContext context,
        string tableName,
        string columnName,
        string columnDefinition)
    {
        var conn = context.Database.GetDbConnection();
        var wasOpen = conn.State == System.Data.ConnectionState.Open;
        if (!wasOpen)
        {
            await conn.OpenAsync();
        }

        await using var checkCmd = conn.CreateCommand();
        checkCmd.CommandText =
            "SELECT COUNT(*) FROM information_schema.columns " +
            $"WHERE table_schema = DATABASE() AND table_name = '{tableName}' AND column_name = '{columnName}'";
        var count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());

        if (count == 0)
        {
            await using var alterCmd = conn.CreateCommand();
            alterCmd.CommandText = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnDefinition}";
            await alterCmd.ExecuteNonQueryAsync();
        }

        if (!wasOpen)
        {
            await conn.CloseAsync();
        }
    }

    private static async Task UpsertGuideArticleAsync(ApplicationDbContext context, string sectionSlug, GuideArticle article)
    {
        ApplyGuideArticleSource(sectionSlug, article);

        var section = await context.MenuSections
            .FirstOrDefaultAsync(item => item.Slug == sectionSlug);

        if (section is null)
        {
            return;
        }

        var existingArticle = await context.GuideArticles
            .FirstOrDefaultAsync(item => item.MenuSectionId == section.Id && item.Title == article.Title);

        if (existingArticle is null)
        {
            article.MenuSectionId = section.Id;
            context.GuideArticles.Add(article);
            return;
        }

        existingArticle.Summary = article.Summary;
        existingArticle.Content = article.Content;
        existingArticle.CreatedAt = article.CreatedAt;
        existingArticle.UniversityTag = article.UniversityTag;
        existingArticle.SourceName = article.SourceName;
        existingArticle.SourceUrl = article.SourceUrl;
        existingArticle.LastVerifiedAt = article.LastVerifiedAt;
    }

    private static void ApplyGuideArticleSource(string sectionSlug, GuideArticle article)
    {
        var verifiedAt = new DateTime(2026, 6, 18);

        (article.SourceName, article.SourceUrl) = article.Title switch
        {
            "Before Departure to Malaysia" => (
                "MOHE / MQA / EMGS official guidance",
                "https://educationmalaysia.gov.my/"
            ),
            "Upon Arrival in Malaysia" => (
                "EMGS International Student Arrival Centre (ISAC)",
                "https://educationmalaysia.gov.my/plan-your-studies/start-to-prepare/international-student-arrival-centre-(isac)"
            ),
            "During Your Stay in Malaysia" => (
                "Immigration Department of Malaysia / embassy guidance",
                "https://www.imi.gov.my/"
            ),
            "First-Week Essentials: Where to Go After Arrival" => (
                "XMUM student orientation demo video",
                "https://www.xmu.edu.my/"
            ),
            "Student Food Spots: D6, LY3, Lake Cafe, and B1" => (
                "XMUM student campus food demo video",
                "https://www.xmu.edu.my/"
            ),
            "Kazakh Communities in Malaysia" => (
                "Embassy of Kazakhstan in Malaysia",
                "https://www.gov.kz/memleket/entities/mfa-kuala-lumpur"
            ),
            "Kazakh Students Association in Malaysia" => (
                "Kazakh Students Association community contact",
                string.Empty
            ),
            "How to Get Your Malaysia Student Visa: The Full Process" => (
                "Education Malaysia Global Services (EMGS)",
                "https://visa.educationmalaysia.gov.my/guidelines"
            ),
            "Malaysia Student Visa Process for Kazakhstan Citizens" => (
                "Embassy of Kazakhstan in Malaysia",
                "https://www.gov.kz/memleket/entities/mfa-kuala-lumpur?lang=en"
            ),
            "Malaysia Student Visa Process for Kyrgyzstan Citizens" => (
                "Embassy of the Kyrgyz Republic in Malaysia",
                "https://mfa.gov.kg/en/dm/Embassy-of-the-Kyrgyz-Republic-in-the-Malaysia"
            ),
            "Malaysia Student Visa Process for Russia Citizens" => (
                "Embassy of Russia in Malaysia",
                "https://malaysia.mid.ru/"
            ),
            "Malaysia Student Visa Process for Tajikistan Citizens" => (
                "Embassy of Tajikistan in Malaysia",
                "https://www.mfa.tj/en/main/view/1259/embassy-of-the-republic-of-tajikistan-in-malaysia"
            ),
            "Malaysia Student Visa Process for Uzbekistan Citizens" => (
                "Embassy of Uzbekistan in Malaysia",
                "https://gov.uz/en/mfa/sections/view/52419"
            ),
            "Bukit Jalil Food & Discount Spots Near APU" => (
                "Asia Pacific University (APU) student guide",
                "https://www.apu.edu.my/guide-new-apu-students"
            ),
            "Sunway Pyramid Student Discounts for Sunway University Students" => (
                "Sunway University — Experience Sunway",
                "https://sunwayuniversity.edu.my/campus-life"
            ),
            "Uzbek Student Community: World Association of Youth Uzbekistan (WAYU)" => (
                "WAYU Malaysia — Instagram community",
                "https://www.instagram.com/wayu_malaysia"
            ),
            _ => GetGuideFallbackSource(sectionSlug)
        };

        article.LastVerifiedAt = string.IsNullOrWhiteSpace(article.SourceUrl) ? null : verifiedAt;
    }

    private static (string SourceName, string SourceUrl) GetGuideFallbackSource(string sectionSlug)
    {
        return sectionSlug switch
        {
            "pre-arrival" => ("Education Malaysia Global Services (EMGS)", "https://educationmalaysia.gov.my/"),
            "arrival-setup" => ("Education Malaysia Global Services (EMGS)", "https://educationmalaysia.gov.my/"),
            "living-in-malaysia" => ("CIS Connect curated student guide", string.Empty),
            "cis-community" => ("CIS Connect community guide", string.Empty),
            "deals-recommendations" => ("CIS Connect student recommendations", string.Empty),
            "important-contacts" => ("Official contact websites", string.Empty),
            _ => ("CIS Connect demo content", string.Empty)
        };
    }
}
