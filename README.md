# RPG game

### Project Description
Rpg is back-end of a .NET 6 web application with Web API, Entity Framework Core & SQL Server. It is a small text-based role-playing game where different users can register and create their own characters (mage, knight or cleric), add some skills and a weapon. Characters can fight against each other to see who is the best of them all. API was created while following the Patrick God's udemy course: .NET 6 Web API & Entity Framework Core Jumpstart.

### Project Features
- Users can register and log in
- Authentication (Password hash and salt, JSON Web Token)
- Logged in users can create, list, find specific characters by id, update and delete their own characters
- User may have one or more characters (one-to-many relationship)
- Character may have a weapon (one-to-one relationship)
- Characters may have one or more skills (many-to-many relationship) 
- Characters can fight against each other (automatic fights)
- After each fight highscore is updated and characters are reordered in a highscore list

### Built with
- .NET 6 Web API
- Entity Framework Core
- SQL Server database

### To get a local copy up and running follow these simple steps:
1. Clone the repo `https://github.com/Adela-me/rpg.git`
2. Open it with Visual Studio
3. To view a solution in Solution View `double click on rpg.snl` file
4. Run `dotnet ef database update` command from terminal.
5. Start it with debbuging (F5) or without (Ctrl + F5)
6. Test API with Swagger: `https://localhost:7257/swagger/index.html`
7. Make sure you create a user and few characters before testing the fight.
