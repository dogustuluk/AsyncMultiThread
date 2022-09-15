using System;
using System.Linq;
using System.Threading;

namespace TaskParallelLanguageIntegratedQuery_PLINQ_
{
    /*
     * Parallel Language - Integrated Query (PLINQ)
     * paralel bir şekilde linq sorgularımızı çalıştırılmasına imkan vermektedir.
     * linq nedir? -> array'ler üzerinde sorgulama yapmamıza imkan veren bir teknolojidir. bunun farklı versiyonları vardır - linq to entity, linq to xml
     * linq, okunabilirliği çok yüksek olan sorgular yazmamıza imkan sağlamaktadır.
     * .net framework 4.0 ile beraber gelmiştir.
     * bununla beraber linq sorgularımızı paralel bir şekilde çalıştırabiliriz. yani sorgularımızı eş zamanlı olarak  birden fazla thread üzerinde çalıştırabiliriz.
     * plinq sorgularının performansı -> sorgunun çalıştırıldığı sistemin işlemci sayısına ve çekirdek sayısına bağlıdır. ne kadar yüksek ise sorgular o kadar performanslı çalışmaktadır.
     * 
     * Array.AsParallel() -> yazmak zorundayız.
     * Paralel olan işlemlerin işlenmesi verilecek olan array içerisindeki sıraya göre ilerlemez, farklı bir sıralama ile gelebilmektedir. burada senkron bir yapı bulunmamaktadır.
     * 
     * ForAll-----------
     * bu metot, plinq sorgusundan dönen sonuçları eğer birden fazla thread'de çalıştırmak istersek kullanırız.
     * bu metot multithread bir metottur.
     * ForEach gibi çalışır ama forEach tek bir thread'te tek tek işlerken bu metot ise birden fazla thread'de ilgili işlemi gerçekleştirir.
     * forAll metodu tipi ParallelIQuery olduğunda kullanılır.
     * sonuç: eğer plinq kullanıyorsak ve gelen datadaki sonuçlarda dönmek istiyorsak normal foreach kullanmayız, daha performanslı olması için forall ile döneriz.
     * senkron bir yapıda çalışmaz.
     * her zaman parallel sorgu yazıp daha yüksek bir performans elde edebilmemiz olası değildir. kendi senaryomuza göre bir yapı kurmalıyız. eğer az sayıda veri içeren bir arrayimiz varsa(100,200) gibi senkron çalıştırmak daha performanslı olur lakin 10bin 100bin gibi kalabalık değerlerden oluşan bir arrayimiz varsa parallel sorgu daha performanslı ve doğru bir yol olmaktadır. veya şu durum da olabilir; array'imizin içerisinde az değer vardır lakin içerisinde yoğun işlemler yapılıyordur, bu durumda da parallel bir yapı kurmalıyız.
     */
    internal class Program
    {
        //asParallel
        private static bool İslem(int x)
        {
            return x % 2 == 0;
        }
        static void Main(string[] args)
        {
            var array = Enumerable.Range(1, 500).ToList();

            var newArray = array.AsParallel().Where(İslem);

            //asParallel
            //newArray.ToList().ForEach(x =>
            //{
            //    Console.WriteLine(x);
            //});
            //asParallel

            //ForAll
            newArray.ToList().ForEach(x =>
            {
                Thread.Sleep(2000);
                Console.WriteLine(x);
                //bu şekilde kullanırsak performans kaybı yaşarız çünkü bu yapı senkron bir şekilde tek bir thread üzerinde çalışmaktadır.
                //her bir veri tek tek 2sn içerisinde yazılır.
            });

            newArray.ForAll(x =>
            {
                Thread.Sleep(2000);
                Console.WriteLine(x);
                //o anda kaç tane thread çalışıyor ise her 2sn'de bir o kadar veri yazılır. 5 thread çalışıyor ise 2sn'de 5 adet veriyi ekrana yazar.
            });


        }
    }
}
