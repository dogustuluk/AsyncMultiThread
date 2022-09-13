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
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
