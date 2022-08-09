using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskConsoleApp2.StartNew
{
    public class Content
    {
        public string Site { get; set; }
        public int Len { get; set; }
    }

    public class Status
    {
        public int threadId { get; set; }
        public DateTime date { get; set; }
    }

    internal class Program
    {
        /*StartNew metodu
            * bu metot Run() metodu ile aynı işlemi gerçekleştirir.
            * Farkı ise ->>>> run metoduna task oluştururken bir obje geçiremiyorken, burada startNew metoduna bir obje geçebiliyoruz. Task işlemi bittiğinde ise geçmiş olduğumuz objeyi alabiliyoruz. herhangi bir obje olabilir bu. Objeyi await keyword'ü ile elde edebiliriz.
            */

        private async static Task Main(string[] args)
        {
            var myTask = Task.Factory.StartNew((Obj) =>
            {
                Console.WriteLine("myTask çalıştı");
                var status = Obj as Status; //as keyword'ü Obj'yi Status'e çeviremez ise null geri döner.
                status.threadId = Thread.CurrentThread.ManagedThreadId;
            }, new Status() { date = DateTime.Now });

            await myTask; //myTask artık dolu, yukarıdaki kodlardan dönen bilgiler hazır.

            Status s = myTask.AsyncState as Status;
            Console.WriteLine(s.date);
            Console.WriteLine(s.threadId);




        }
    }
}
