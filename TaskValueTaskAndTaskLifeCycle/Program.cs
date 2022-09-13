using System;
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
        private static int cacheData { get; set; } = 150;
        private async static Task Main(string[] args)
        {
            await GetData();
            var mytask = GetData();
            //normal bir task dönüyormuş gibi kullanabiliriz, herhangi bir farkları yoktur.
        }
        public static ValueTask<int> GetData()
        {
            return new ValueTask<int>(cacheData);
        }

    }
}
