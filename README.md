
## 🔗 Redirect

Repository developed to practice knowledge in ASP .NET Core using C#, .NET 6, Clean Architecture, CQRS, Entity Framework Core, Dapper and Repository Standard. This project is a URL shortener and uses PostgreSQL as a database.

## 📫  Routes

### Url Shortener Controller

<img src="https://img.shields.io/badge/-POST-%2349CC90" height="30" />

**"/generate-new-shortened-url"**

_Generates a new shortened url code for the original informed url_

**body:**
```
{
   "originalUrl": string
}
```

**response:**
```
{
   "shortenedUrl": string
}
```

<hr>

<img src="https://img.shields.io/badge/-GET-%2361AFFE" height="30" />

**"/{code}"**

_Redirects to the original url from the entered short url code, if the code does not exist in the database, 404 is returned._

**route params:**

`code: string`

**response:**

_Redirect Result to the original url_

## 🌐 Status
<p>Finished project ✅</p>

## 🧰 Prerequisites

- .NET 6.0 or +

- Connection string to PostgreSQL BD in redirect/Redirect.API/appsettings.json named as ConnectionStrings.RedirectCs

## <img src="https://icon-library.com/images/database-icon-png/database-icon-png-13.jpg" width="20" /> Database

_Create a database in PostgreSQL that contains the table created from the following script:_

```
CREATE TABLE public."shortenedURLs"
(
    "Code" character varying(10) NOT NULL,
    "OriginalURL" character varying(2048) NOT NULL,
    "Expiration" timestamp without time zone NOT NULL,
    PRIMARY KEY ("Code")
);
```

## 🔧 Installation

`$ git clone https://github.com/AllanDutra/redirect.git`

`$ cd redirect/Redirect.API`

`$ dotnet restore`

`$ dotnet run`

**Server listenning at  [https://localhost:7272/](https://localhost:7272/)!**

## 🔨 Tools used

<div>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" width="80" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="80" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/postgresql/postgresql-original.svg" width="80" />

</div>