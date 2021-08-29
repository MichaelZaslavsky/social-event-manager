# Social Event Manager
A project for learning purposes built by Michael Zaslavsky.

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#run">Run</a></li>
      </ul>
    </li>
    <li>
      <a href="#roadmap">Roadmap</a>
      <ul>
        <li><a href="#infrastructures">Infrastructures</a></li>
      </ul>
    </li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Social Event Manager (SEM) is a social network for organizing events.

### Built With

* [.NET Core](https://en.wikipedia.org/wiki/.NET_Core)
* [C#](https://en.wikipedia.org/wiki/C_Sharp_(programming_language))
* [MSSQL](https://en.wikipedia.org/wiki/Microsoft_SQL_Server)
* [Web API](https://en.wikipedia.org/wiki/Web_API)



<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

1. Install .NET Core
   ```sh
   https://dotnet.microsoft.com/download
   ```
2. Install SQL Server
   ```sh
   https://www.microsoft.com/en-us/sql-server/sql-server-downloads
   ```
3. Install Docker 19.03.0+
   ```sh
   https://docs.docker.com/engine/install/
   ```

### Run

1. Clone the repo
   ```sh
   git clone https://github.com/MichaelZaslavsky/social-event-manager.git
   ```
2. Open folder `%APPDATA%/Microsoft/UserSecrets`
   - Create a folder called `80a155b1-fb7a-44de-8788-4f5759c60ff6`
   - Go into the created folder and create a file `secrets.json`
   - Edit the created file. You need to add `Kestrel:Certificates` key \
	 For example:
	
	 ```json
	 {
		"Kestrel:Certificates:Development:Password": "5cb62bfd-2da5-44f2-964f-d2b0c9af935d"
	 }
	 ```
		
3. Create `.env` file in the same folder where `docker-compose.yml` file is and add the following keys
   (`<SomeDBUser>` with any DB user name you want and `<SomePassword1>` & `<SomePassord2>` with any password you want):
   ```yml
	SA_PASSWORD=<SomePassword1>
	DB_USER=<SomeDBUser>
	DB_PASSWORD=<SomePassord2>
	ConnectionStrings__SocialEventManager=Server=sql-server-database;Database=SocialEventManager;User Id=db_admin;Password=${DB_ADMIN_PASSWORD}
	ConnectionStrings__SocialEventManagerHangfire=Server=sql-server-database;Database=SocialEventManagerHangfire;User Id=db_admin;Password=${DB_ADMIN_PASSWORD}
	ConnectionStrings__SocialEventManagerTest=Server=sql-server-database;Database=SocialEventManagerTest;User Id=sa;Password=${SA_PASSWORD}
   ```
   
4. Make sure Docker is installed in your computer and is running
4. Set docker-compose as startup project and run it
   - You may open the Swagger https://localhost:8080/swagger/index.html
   - You may open Serilog http://localhost:5341/#/events


<!-- ROADMAP -->
## Roadmap

### Infrastructures
✔️ Projects Architecture

✔️ .editorconfig file

✔️ Analyzers including StyleCop & Roslynator analysis

✔️ Swagger documentation

✔️ .NET Core DI setups

✔️ AutoMapper

✔️ Logs with Serilog

✔️ Exceptions Handling

✔️ Dapper ORM

✔️ Repository pattern

✔️ Rate limiting

✔️ Evolve migrations

✔️ Hangfire - Background jobs

✔️ Data annotations middleware

✔️ Basic tests with xUnit

✔️ Secrets

✔️ UnitOfWork

✔️ In memory database tests

✔️ Docker

✔️ Docker Compose

✔️ Health Checks

❌ Authorization / Authentication

❌ SignalR

❌ Distributed cache with Redis

❌ Sentry

❌ Security checks

❌ Allow media binders

❌ REST

❌ Versioning

❌ [Hangfire with Redis](https://github.com/marcoCasamento/Hangfire.Redis.StackExchange)

❌ Extend Swagger documentation




<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the GNU General Public License v3.0. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

Michael Zaslavsky - [https://www.linkedin.com/in/michael-zaslavsky](https://www.linkedin.com/in/michael-zaslavsky)

Project Link: [https://github.com/MichaelZaslavsky/social-event-manager](https://github.com/MichaelZaslavsky/social-event-manager)
