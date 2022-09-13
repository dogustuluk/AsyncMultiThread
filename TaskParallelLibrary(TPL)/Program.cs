using System;

namespace TaskParallelLibrary_TPL_
{/*
  * task parallel library(tpl) nedir?
  * thirdparty değildir.
  * bu teknoloji ya da kütüphane ile kodlarımızı birden fazla thread'de zorlanmadan çalıştırabiliriz.
  * yoğun işlemlerimiz varsa ve bunları birden fazla thread ile çok kısa bir sürede sonuç almak istersek kullanabiliriz.
  * eğer kalabalık bir array'imiz var ise, bunların içerisindeki item'ları çeşitli parçalara bölüp her bir parçaya bir thread atarak işlemleri gerçekleştirir. Bu sayede uzun süren işlemlerin işlenme süresini kısaltmış olmaktadır.
  * thread yönetimi ile ilgilenmiyoruz, thread'leri herhangi bir göreve atamakla ilgilenmiyoruz ve bu thread'lerin yaşam döngüleri ile de ilgilenmiyoruz. Bunlarla TPL ilgileniyor olacak.
  */

    /*
     * race condition nedir?
     * çoklu thread uygulamalarda, birden çok thread'in paylaşılan bir hafıza alanına aynı anda erişmeye çalışmasıdır.
     * bu durum illaki olacak diye bir durum yok ama olma ihtimali vardır.
     * engellemek için thread'i kitlemek gerekiyor. tabii bu durumda da performansta ciddi bir düşüş de olacaktır.
     * paylaşımlı data kullanmadan yaparsak kodlarımız daha hızlı çalışacaktır çünkü herhangi bir lag mekanizması olmayacak.
     * örnek bir senaryo vermek gerekirse; a ve b thread'lerimiz ortak bir data üzerinde(int 5değerinde) veri artırımı yapsın. a thread'i t1 zamanda 5 değerini alsın ve b thread'i de t2 zamanında 5 değerini alsın. ardından a thread'i t3 zamanında 5 değerini 1 artırsın ve b thread'i de t4 zamanında 5 değerini 1 arttırsın. her iki thread'in de ulaşacağı nihai sonuç a için t5 ve b için t6 zamanında aynı olan 6 değeri olacaktır. fakat olması gereken sonuç b thread'inin 7 sonucunu döndürmesi olacaktır.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
