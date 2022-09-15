using System;
using System.Linq;

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
     * 
     */
    internal class Program
    {
        private static bool İslem(int x)
        {
            return x % 2 == 0;
        }
        static void Main(string[] args)
        {
            var array = Enumerable.Range(1, 500).ToList();

            var newArray = array.AsParallel().Where(İslem);
            /*
             * Paralel olan işlemlerin işlenmesi verilecek olan array içerisindeki sıraya göre ilerlemez, farklı bir sıralama ile gelebilmektedir. burada senkron bir yapı bulunmamaktadır.
             */

            newArray.ToList().ForEach(x =>
            {
                Console.WriteLine(x);
            });


        }
    }
}
