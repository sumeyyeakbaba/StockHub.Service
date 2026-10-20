namespace StockHub.Web.Models
{
    public class DashboardOzetModel
    {
        public int ToplamUrunSayisi { get; set; }
        public int ToplamStokMiktari { get; set; }
        public decimal ToplamStokDegeri { get; set; }
        public int KritikStokUrunSayisi { get; set; }
        public int ToplamGirisHareketSayisi { get; set; }
        public int ToplamCikisHareketSayisi { get; set; }
    }
}