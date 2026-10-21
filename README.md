# 📦 StockHub - Modern Stok & Ürün Yönetim Sistemi

StockHub, işletmelerin stok ve ürün süreçlerini güvenli, hızlı ve ölçeklenebilir bir şekilde yönetebilmesi için geliştirilmiş uçtan uca bir platformdur. Proje, arka planda modüler bir **RESTful Web API** servisi (`StokService`) ve kullanıcı etkileşimi sağlayan **Web Arayüzü** (`stockHub.Web`) olmak üzere iki ana bileşenden oluşur.

---

## 🏗 Proje Mimarisi ve Bileşenler

Proje monorepo mimarisine uygun olarak tek bir depo altında hem arka plan servislerini hem de ön yüz bileşenlerini barındırmaktadır:

### 1. ⚙️ StokService (Backend / ASP.NET Core Web API)
* **RESTful API Mimarisi:** HTTP standartlarına uygun, modüler ve yüksek performanslı uç noktalar (`endpoints`).
* **NoSQL Veri Tabanı (MongoDB):** Esnek şema yapısı ve hızlı veri işleme kabiliyeti için `MongoDB.Driver` entegrasyonu.
* **JWT (JSON Web Token) Güvenliği:** Yetkisiz erişimleri engelleyen, güvenli ve rola dayalı kimlik doğrulama mekanizması.
* **Yapılandırılmış Loglama (Serilog):** Uygulama içi olayların, kullanıcı hareketlerinin ve istisnaların (`exceptions`) detaylı loglanması.
* **DTO & Clean Code:** Katmanlar arası güvenli veri transferi için DTO (Data Transfer Object) kullanımı ve esnek Dependency Injection yapılandırması.

### 2. 💻 stockHub.Web (Frontend / MVC)
* **Web Arayüzü:** API servisleriyle haberleşerek kullanıcıya anlık stok durumları, ürün listeleri ve yönetim panelleri sunan dinamik arayüz katmanı.

---

## 🛠 Kullanılan Teknolojiler

| Alan | Teknolojiler |
| **Backend & Servisler** | C#, .NET, ASP.NET Core Web API, DTOs |
| **Veri Tabanı** | MongoDB |
| **Güvenlik & Yetki** | JWT (JSON Web Tokens), Authentication & Authorization |
| **Loglama & İzleme** | Serilog |
| **Frontend / Arayüz** | ASP.NET Core MVC, HTML5, CSS3, JavaScript |
| **Mimari Standartlar** | Katmanlı Mimari, Dependency Injection |

---

## ⚙️ Kurulum ve Çalıştırma

1. Projeyi bilgisayarınıza klonlayın:
   ```bash
   git clone [https://github.com/sumeyyeakbaba/StockHub-API.git](https://github.com/sumeyyeakbaba/StockHub-API.git)
