namespace StockHub.Services.Dtos
{
    public class HataCevapDto
    {
        // 400, 404 veya 500 gibi durum kodu
        public int DurumKodu { get; set; }

        // Kullanıcıya göstereceğimiz anlaşılır Türkçe mesaj
        public string Mesaj { get; set; }

        // Geliştirme aşamasındayken göreceğimiz detaylı hata açıklaması
        public string Detay { get; set; }
    }
}