# 📚 WPF-Bookstore – Hantering av bokhandel i WPF
## 📌 Beskrivning
Den här applikationen använder Entity Framework (Database First) för att hantera en bokhandelsdatabas. Användaren kan läsa, uppdatera, lägga till och ta bort böcker och författare i databasen. Applikationen kan utvecklas som en Console- eller WPF-app, där funktionaliteten är central.

## 🔧 Funktioner
🏪 Lagersaldo & Butikshantering
- Visa lagersaldo för olika butiker.
- Lägga till och ta bort böcker från butikerna.
- När man lägger till böcker kan man välja från redan existerande böcker i sortimentet.

## 📖 Bokhantering (VG-nivå)
- Lägg till nya titlar i sortimentet, välj bland befintliga författare.
- Lägg till nya författare och mata in information såsom antal sidor, pris och utgivningsdatum.
- Redigera och ta bort titlar samt författare.

## 🖥️ Exempel på funktionalitet
Vid programstart:

- Applikationen kopplar sig till databasen och hämtar befintliga böcker och författare.

## Vid användning:

- Admin-användare kan uppdatera sortimentet, lägga till och ta bort böcker.
- Kunden kan lista lagersaldo och se produktinformation.

## Vid programavslut:

- Alla ändringar sparas i databasen genom Entity Framework.

## 🚀 Teknologier
- C#
- Entity Framework (Database First)
- Microsoft SQL Server
- WPF
