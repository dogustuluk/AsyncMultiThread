using System;
using System.Linq;
using System.Threading;
using TaskParallelLanguageIntegratedQuery_PLINQ_.Models;

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
     * 
     * 
     * Array.AsParallel() -> yazmak zorundayız.
     * Paralel olan işlemlerin işlenmesi verilecek olan array içerisindeki sıraya göre ilerlemez, farklı bir sıralama ile gelebilmektedir. burada senkron bir yapı bulunmamaktadır.
     * 
     * 
     * 
     * ForAll-----------
     * bu metot, plinq sorgusundan dönen sonuçları eğer birden fazla thread'de çalıştırmak istersek kullanırız.
     * bu metot multithread bir metottur.
     * ForEach gibi çalışır ama forEach tek bir thread'te tek tek işlerken bu metot ise birden fazla thread'de ilgili işlemi gerçekleştirir.
     * forAll metodu tipi ParallelIQuery olduğunda kullanılır.
     * sonuç: eğer plinq kullanıyorsak ve gelen datadaki sonuçlarda dönmek istiyorsak normal foreach kullanmayız, daha performanslı olması için forall ile döneriz.
     * senkron bir yapıda çalışmaz.
     * her zaman parallel sorgu yazıp daha yüksek bir performans elde edebilmemiz olası değildir. kendi senaryomuza göre bir yapı kurmalıyız. eğer az sayıda veri içeren bir arrayimiz varsa(100,200) gibi senkron çalıştırmak daha performanslı olur lakin 10bin 100bin gibi kalabalık değerlerden oluşan bir arrayimiz varsa parallel sorgu daha performanslı ve doğru bir yol olmaktadır. veya şu durum da olabilir; array'imizin içerisinde az değer vardır lakin içerisinde yoğun işlemler yapılıyordur, bu durumda da parallel bir yapı kurmalıyız.
     * 
     * 
     * 
     * WithDegreeOfParalleism metodu
     * bu metot sorgularımızı paralel olarak kaç tane işlemcide çalışacağına imkan vermektedir.
     * 
     */
    internal class Program
    {
        private static void WriteLog(Product p)
        {
            //log'a yazma
            Console.WriteLine(p.Name + "log'a kaydedildi.");
        }

        //asParallel
        //private static bool İslem(int x)
        //{
        //    return x % 2 == 0;
        //}
        static void Main(string[] args)
        {
            //var array = Enumerable.Range(1, 500).ToList();

            //var newArray = array.AsParallel().Where(İslem);

            ////asParallel
            ////newArray.ToList().ForEach(x =>
            ////{
            ////    Console.WriteLine(x);
            ////});
            ////asParallel

            ////ForAll
            //newArray.ToList().ForEach(x =>
            //{
            //    Thread.Sleep(2000);
            //    Console.WriteLine(x);
            //    //bu şekilde kullanırsak performans kaybı yaşarız çünkü bu yapı senkron bir şekilde tek bir thread üzerinde çalışmaktadır.
            //    //her bir veri tek tek 2sn içerisinde yazılır.
            //});

            //newArray.ForAll(x =>
            //{
            //    Thread.Sleep(2000);
            //    Console.WriteLine(x);
            //    //o anda kaç tane thread çalışıyor ise her 2sn'de bir o kadar veri yazılır. 5 thread çalışıyor ise 2sn'de 5 adet veriyi ekrana yazar.
            //});



            //database first
            //parallel sonrası yazılan kodlar sql server'a gitmiyor.
            //çünkü plinq'in amacı elimde varolan bir array üzerinde bir işlem yapmak istiyorsak ve bu işlemleri de paralel olarak yapmak istersek kullanırız.
            //neden ihtiyaç duyaruz? -> elimizde bir array varsa, g
            AdventureWorks2019Context context = new AdventureWorks2019Context();

            var product = (from p in context.Products.AsParallel()
                           where p.ListPrice > 10M
                           select p).Take(10);
            product.ForAll(p =>
            {
                Console.WriteLine(p.Name);
            });



            //tek seferde yazabiliriz. Ama çok büyük bir sorgu var ise okunabilirlik azalacaktır. komplek sorgularda yukarıdaki gibi yazarsak daha fazla okunabilir olacaktır.
            var product2 = context.Products.AsParallel().Where(p => p.ListPrice > 10M).Take(10);



            context.Products.AsParallel().ForAll(p =>
            {
                WriteLog(p);
            });


            //WithDegreeOfParalleism metodu
            context.Products.AsParallel().WithDegreeOfParallelism(2).ForAll(p =>
            {
                //iki işlemcide çalışır.
                WriteLog(p);
            });


            
           

            //database first
        }
    }
}
