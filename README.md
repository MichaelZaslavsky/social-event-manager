# Social Event Manager
A project for leaning purposes built by Michael Zaslavsky.

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
        <li><a href="#run-serilog">Run Serilog</a></li>
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

### Run

1. Clone the repo
   ```sh
   git clone https://github.com/MichaelZaslavsky/social-event-manager.git
   ```
2. Open "Developer Command Prompt" and add the connection strings to the secret key manager:
	```sh
   dotnet user-secrets init
   dotnet user-secrets set ConnectionStrings:SocialEventManager "Data Source=.;Initial Catalog=SocialEventManager;Integrated Security=True;MultipleActiveResultSets=True;"
   dotnet user-secrets set ConnectionStrings:SocialEventManagerHangfire "Data Source=.;Initial Catalog=SocialEventManagerHangfire;Integrated Security=True;MultipleActiveResultSets=True;"
   ```
3. Run script to create the initial database
   ```sh
   CREATE DATABASE SocialEventManager;
   ```
4. Set "SocialEventManager.API" project as startup and run it. It will run the Evolve migrations and HangFire which will create/update the relevant databases.

### Run Serilog

* Install Docker.
* Follow these steps: https://hub.docker.com/r/datalust/seq
* Open http://localhost:5341/#/events


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
