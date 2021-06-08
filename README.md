# social-event-manager
A project for leaning purposes built by Michael Zaslavsky

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
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li>
      <a href="#roadmap">Roadmap</a>
      <ul>
        <li><a href="#infrastructures-roadmap">Infrastructures</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
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

* Run script to create the initial database
  ```sh
  CREATE DATABASE SocialEventManager;
  ```
* Set "SocialEventManager.API" project as startup and run it. It will run the Evolve migrations and HangFire which will create/update the relevant databases.

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/MichaelZaslavsky/social-event-manager.git
   ```



<!-- ROADMAP -->
## Roadmap

### Infrastructures
✅ Projects Architecture

✅ .editorconfig file

✅ Analyzers including StyleCop & Roslynator analysis

✅ Swagger documentation

✅ .NET Core DI setups

✅ AutoMapper

✅ Logs with Serilog

✅ Exceptions Handling

✅ Dapper ORM

✅ Repository pattern

✅ Rate limiting

✅ Evolve migrations

✅ Hangfire - Background jobs

✅ Data annotations middleware

❌ Secrets

❌ SignalR

❌ Distributed cache with Redis

❌ Sentry

❌ Security checks

❌ UnitOfWork

❌ Allow media binders

❌ Authorization / Authentication

❌ REST

❌ Versioning

❌ [Hangfire with Redis](https://github.com/marcoCasamento/Hangfire.Redis.StackExchange)

❌ Extend Swagger documentation

❌ In memory database tests




<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

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
