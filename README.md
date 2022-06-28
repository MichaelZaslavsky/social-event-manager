[![.NET package](https://github.com/MichaelZaslavsky/social-event-manager/actions/workflows/dotnet-build-and-test.yml/badge.svg)](https://github.com/MichaelZaslavsky/social-event-manager/actions/workflows/dotnet-build-and-test.yml)
[![CodeQL](https://github.com/MichaelZaslavsky/social-event-manager/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/MichaelZaslavsky/social-event-manager/actions/workflows/codeql-analysis.yml)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/10e406198cbb439989fc58af01f95263)](https://www.codacy.com/gh/MichaelZaslavsky/social-event-manager/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=MichaelZaslavsky/social-event-manager&amp;utm_campaign=Badge_Grade)
[![Dependabot auto-merge](https://github.com/MichaelZaslavsky/social-event-manager/actions/workflows/auto-merge-dependabot.yml/badge.svg)](https://github.com/MichaelZaslavsky/social-event-manager/actions/workflows/auto-merge-dependabot.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=MichaelZaslavsky_social-event-manager&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=MichaelZaslavsky_social-event-manager)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=MichaelZaslavsky_social-event-manager&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=MichaelZaslavsky_social-event-manager)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=MichaelZaslavsky_social-event-manager&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=MichaelZaslavsky_social-event-manager)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=MichaelZaslavsky_social-event-manager&metric=bugs)](https://sonarcloud.io/summary/new_code?id=MichaelZaslavsky_social-event-manager)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=MichaelZaslavsky_social-event-manager&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=MichaelZaslavsky_social-event-manager)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=MichaelZaslavsky_social-event-manager&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=MichaelZaslavsky_social-event-manager)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=MichaelZaslavsky_social-event-manager&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=MichaelZaslavsky_social-event-manager)
![GitHub last commit](https://img.shields.io/github/last-commit/MichaelZaslavsky/social-event-manager)
![GitHub repo size](https://img.shields.io/github/repo-size/MichaelZaslavsky/social-event-manager)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/MichaelZaslavsky/social-event-manager)
![GitHub](https://img.shields.io/github/license/MichaelZaslavsky/social-event-manager)

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
		<li><a href="#features">Features</a></li>
      </ul>
    </li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

## About The Project

Social Event Manager (SEM) is a social network for organizing events.

Some images for demonstration:

Create Event:
![Create Event](https://user-images.githubusercontent.com/6709378/132340254-dce9e42e-c743-48ce-8a88-d1f489a33608.jpg)

Events Page:
![Main](https://user-images.githubusercontent.com/6709378/132339995-296b005e-16ba-4093-9b25-3cc3bd2001ee.jpg)

Search Events:
![Search Events](https://user-images.githubusercontent.com/6709378/132340171-039d242f-6c11-4c3d-9c50-77aaa373e02d.jpg)

My Events:
![My Events](https://user-images.githubusercontent.com/6709378/132340196-03c27622-a973-45cd-8161-445bff4f689b.jpg)

### Built With

- [.NET Core](https://en.wikipedia.org/wiki/.NET_Core)
- [C#](<https://en.wikipedia.org/wiki/C_Sharp_(programming_language)>)
- [MSSQL](https://en.wikipedia.org/wiki/Microsoft_SQL_Server)
- [Web API](https://en.wikipedia.org/wiki/Web_API)

## Getting Started

### Prerequisites

1.  Install .NET Core
    ```sh
    https://dotnet.microsoft.com/download
    ```
2.  Install SQL Server
    ```sh
    https://www.microsoft.com/en-us/sql-server/sql-server-downloads
    ```
3.  Install Docker 19.03.0+
    ```sh
    https://docs.docker.com/engine/install/
    ```

### Run

1.  Clone the repo
    ```sh
    git clone https://github.com/MichaelZaslavsky/social-event-manager.git
    ```
2.  Open folder `%APPDATA%/Microsoft/UserSecrets`

    - Create "UserSecrets" folder if not exists
    - Enter "UserSecrets" folder
    - Go into the created folder and create a file `secrets.json`
    - Edit the created file. You need to add `Kestrel:Certificates` key \
      For example:

      ```json
      {
        "Kestrel:Certificates:Development:Password": "5cb62bfd-2da5-44f2-964f-d2b0c9af935d"
      }
      ```

3.  Create `.env` file in the same folder where `docker-compose.yml` file is and add the following keys:

    ```yml
    SA_PASSWORD=<SomePassword1>
    DB_USER=<SomeDBUser>
    DB_PASSWORD=<SomePassord2>
    ConnectionStrings__SocialEventManager=Server=sql-server-database;Database=SocialEventManager;User Id=db_admin;Password=${DB_ADMIN_PASSWORD}
    ConnectionStrings__SocialEventManagerHangfire=Server=sql-server-database;Database=SocialEventManagerHangfire;User Id=db_admin;Password=${DB_ADMIN_PASSWORD}
    ConnectionStrings__SocialEventManagerTest=Server=sql-server-database;Database=SocialEventManagerTest;User Id=sa;Password=${SA_PASSWORD}
    REDIS_MASTER_PASSWORD=<SomePassword3>
    REDIS_REPLICA_PASSWORD=<SomePassword4>
    BasicAuthentication__UserName=<SomeSwaggerUserName>,
    BasicAuthentication__Password=<SomePassword5>
    ```

4.  Make sure Docker is installed in your computer and is running

5.  Set docker-compose as startup project and run it
    - You may open the Swagger https://localhost:8080/swagger/index.html
    - You may open Serilog http://localhost:5341/#/events

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

✔️ Distributed cache with Redis

✔️ Response compression

✔️ SignalR

✔️ Versioning

❌ Authorization / Authentication

❌ Security checks

❌ Allow media binders

❌ REST

❌ Elasticsearch

### Features

❌ User profile

❌ Event

❌ Category

❌ Business house

❌ Facebook login

❌ Google Maps

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

## License

Distributed under the GNU General Public License v3.0. See `LICENSE` for more information.

## Contact

Michael Zaslavsky - [https://www.linkedin.com/in/michael-zaslavsky](https://www.linkedin.com/in/michael-zaslavsky)

Project Link: [https://github.com/MichaelZaslavsky/social-event-manager](https://github.com/MichaelZaslavsky/social-event-manager)
