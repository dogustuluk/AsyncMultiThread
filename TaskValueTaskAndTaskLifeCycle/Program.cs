using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskValueTaskAndTaskLifeCycle
{
    internal class Program
    {/*
      * ValueTask
      * referans tipler ve value tipler memory'nin farklı bölgelerinde tutulurlar.
      * referans tipler memory'nin heap bölgesinde tutulur.
      * value tipler ise memory'nin stack bölgesinde tutulur.
      * heap bölgesinde tutulan dataların silinmesi için, memory'den silinmesi için garbage collector devreye girer ve belli aralıklarla referans olmayan yapıları siler.
      * orada tutulan datalar maliyetli yapılardır.
      * stack bölgesinde tutulan yapılar ise scope'tan çıktığında bellekten otomatik olarak düşer.
      * Task dönmek mağliyetli olmaktadır. eğer yoğun bir iş gerektirmeyen bir async metottan data dönüldüğünde yeni bir task dönmek yerine belleğin stack'inde tutulacak valueStack dönmemiz belleğin daha performanslı kullanılmasına yol açacaktır.
      * Herhangi bir Task'ten hiç bir farkı yoktur, sadece tipleri farklıdır.
      * hemen data alacağını bildiğin yerde kullanabilirsin.
      */

        /*
         * Task Akış Durumu
         * 
         */

        //ValueTask
        private static int cacheData { get; set; } = 150;
        private async static Task Main(string[] args)
        {
            //ValueTask
            //await GetData();
            //var mytask = GetData();
            //normal bir task dönüyormuş gibi kullanabiliriz, herhangi bir farkları yoktur.


            //task akış
            Console.WriteLine("ilk adım");
            
            var mytask = GetContent(); //burada alttaki metot çalışırken ui thread bloklanmıyor./*
                                       //buradan itibaren aşağıda bulunan GetContent metodunun içerisine giriyor. bu metot içerisinde await keyword'ünü gördükten sonra kontrol tekrardan main metoduna geçmekte.
                                       //main metodunda ise metottan dönecek sonuç beklenmeden bir alt satıra devam ediliyor
                                       //ikinci adım oluştuktan sonra ise bir alt satırda await keyword'ü olduğundan dolayı GetContent metodunun içerisindeki işlemin sonucu alınıncaya değin burada bekleniyor.
                                       //ilgili sonuç alındıktan sonra bir alt satıra geçilir.
                                       //*/

            Console.WriteLine("ikinci adım");

            var content = await mytask;

            Console.WriteLine("üçüncü adım: " + content.Length);
        }

        //ValueTask
        public static ValueTask<int> GetData()
        {
            return new ValueTask<int>(cacheData);
        }

        //Task akış
        public static async Task<string> GetContent()
        {
            var content = await new HttpClient().GetStringAsync("https://www.google.com");

            return content;
        }
    }
}
