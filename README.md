# Datatable Project

Bu proje **Angular (Frontend)**, **.NET Core Web API (Backend)** ve **MSSQL (Database)** teknolojileri kullanılarak geliştirilmiştir.  
Amaç: Link tablosu üzerinde **CRUD işlemlerini** (ekleme, silme, güncelleme, listeleme) yönetmektir.

---

## 🚀 Gereksinimler

- Node.js >= 18.x  
- Angular CLI >= 20.x  
- .NET SDK >= 8.0  
- SQL Server (Local veya Remote)

---

## 📦 Kurulum Adımları

### 1. Backend Kurulumu (.NET API)

Proje klasörüne gidin ve bağımlılıkları yükleyin:

```bash
cd backend
dotnet restore
```

.gitignore nedeniyle projede bulunmayan `DatatableDbContext.cs` dosyasını aşağıdaki içerikle `DataAccess/Concrete/DatatableDbContext.cs` yoluna ekleyin:

```csharp
namespace DataAccess.Concrete
{
    public class DatatableDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost;" +
                "Database=DatatableDb;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;"
            );
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e=>e.Url).IsRequired().HasMaxLength(100);
            });
        }
    }
}
```

Ayrıca `appsettings.json` dosyasını `backend` klasörüne ekleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DatatableDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Migration klasörü zaten bulunduğundan, aşağıdaki komutla veritabanını oluşturun:

```bash
dotnet ef database update
```

Backend uygulamasını çalıştırın:

```bash
dotnet run
```

**Backend başarıyla başlatıldığında:**

- Swagger arayüzü: https://localhost:7170/swagger

---

### 2. Frontend Kurulumu (Angular)

Frontend klasörüne gidin ve bağımlılıkları yükleyin:

```bash
cd frontend
npm install
```

TailwindCSS ve ikonlar `package.json` içinde tanımlıdır:

- tailwindcss  
- @tailwindcss/postcss  
- lucide-angular  

Uygulamayı başlatın:

```bash
ng serve
```

Frontend varsayılan olarak `http://localhost:4200` adresinde çalışır.  
Tarayıcınızda bu adrese giderek uygulamayı görebilirsiniz.

---

## 🔗 CORS Yapılandırması

`Program.cs` içinde CORS ayarı aşağıdaki gibidir:

```csharp
app.UseCors(builder =>
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials()
);
```

> **Not:** Eğer frontend farklı bir portta çalışacaksa `WithOrigins` adresini değiştirin.

---

## 🧱 Build & Deployment

### Development Build

Frontend için development build:

```bash
ng build --configuration development
```

Backend için watch mode ile çalıştırma:

```bash
dotnet watch run
```

### Production Build

Frontend için production build:

```bash
ng build --configuration production
```

Backend için production build ve publish:

```bash
dotnet publish -c Release -o publish
```

## 🧠 Ek Bilgiler

- `DatatableDbContext.cs` ve `appsettings.json` dosyaları `.gitignore`’da olduğu için manuel eklenmelidir.  
- `Migrations` klasörü mevcut olduğundan `dotnet ef database update` komutu yeterlidir.  
- Frontend bağımlılıkları (Tailwind, Lucide vb.) `npm install` ile otomatik yüklenir.

---

## ✨ Kullanılan Teknolojiler

- **Frontend:** Angular 20, TailwindCSS, Lucide Angular  
- **Backend:** ASP.NET Core 8.0, Autofac, Swagger  
- **Database:** MSSQL  
- **ORM:** Entity Framework Core  
- **Design Pattern:** Layered Architecture

---

## 📝 Additional Resources

- [Angular CLI Documentation](https://angular.io/cli)  
- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core)  
- [Entity Framework Core Documentation](https://learn.microsoft.com/ef/core)