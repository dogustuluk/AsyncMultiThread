using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskFormApp
{
    public partial class Form1 : Form
    {
        public int counter { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Asenktron metotları istersek await ile sonuçlarını hemen alabiliriz, istersek de dönen sonucu bir text'e aktarıp daha sonra alabiliriz.
            
            //herhangi bir asenkron metot illa async-await ikilisine sahip olmak zorunda değil. Bu ikili metot içerisinde async metot kullanacağım zaman yazılır.

            //async ne zaman kullanılmalı ->>>>>>>>> Eğer işlem bitmeden önce metot içerisinde yapmamız gereken farklı işler var ise async kullanmak daha iyi olacaktır.
            //-----------------------------------------------------------------------
           

        }
        private async void BtnReadFile_Click(object sender, EventArgs e)
        {
            //dosya okuma işlemini gerçekleştiriyoruz.
          //Task(async-await ders 1)  // string data = await ReadFileAsync(); //await vermemizin sebebi ise alt satırda bu satırdan gelecek olan data ile ilgili işlem yaptığımız için.
          
            //Task(async-await ders 2)
            string data = String.Empty;
            Task<String> okuma = ReadFileAsync2(); //await kullanmadığımız için "ReadFileAsync()" metodunu çağırdığımız zaman okuma işlemi başlayacaktır.

            richTextBox2.Text = await new HttpClient().GetStringAsync("https://www.google.com"); //await ile alt satıra geçmesi, buraya datanın gelmesine bağlıdır. Mutlaka datanın gelmesini bekleyecek.
            //richTextBox2.Text = "a";
            data = await okuma;

            richTextBox1.Text = data;
        }

        private void BtnCounter_Click(object sender, EventArgs e)
        {
            textBoxCounter.Text = counter++.ToString();
        }

        private string ReadFile()
        {
            string data = string.Empty;

            //bunu ->StreamReader s = new StreamReader()<- using ifadesi ile kullanalım ki işimiz bittikten sonra bunu bellekten atmış olsun.
            using (StreamReader s = new StreamReader("dosya.txt"))
            {
                Thread.Sleep(5000); //test amaçlı kullanıyoruz. //Ana thread'i bloklar bu.
                data = s.ReadToEnd(); //datayı baştan sonra okur, geriye string döner.
            }
            return data;
            
        }

        //asenkron metodumuzu yazalım
        //asenkron metotlar geriye bir şey de dönebilirken dönmeme durumu da vardır.
        //Eğer geriye bir şey dönmeyecek isek sadece "Task" keyword'ü kullanılır. Bu senkron metotlardaki void keyword'üne karşılık  gelir.
        //Eğer geriye bir şey dönecek isek "Task<string, vs.>" kullanılır.
        //>> void ->>>>Task /////////// string -> Task<string> karşılık gelir.

        private async Task<string> ReadFileAsync()
        {
            string data = string.Empty;
            using (StreamReader s = new StreamReader("dosya.txt"))
            {
                Task<string> mytask = s.ReadToEndAsync(); //Taahhüt işlemi bir nevi yapıldı

                //
                //
                //
                await Task.Delay(5000); //Ana thread'i bloklamaz.
                data = await mytask; //burada aldığımız datayı geriye dönmüş oluyoruz. yukarıdaki boşluklarda metottan dönecek data ile ilgili olmayan başka işlemler yapılabilir.

                //await'ten dolayı "data" değişkenimiz aslında geriye bir Task<string> dönmüş olur.

                return data;

            }
        }

        private Task<string> ReadFileAsync2()
        {
            StreamReader s = new StreamReader("dosya.txt");
            
            return s.ReadToEndAsync();
            
            //Önemli -------> streamReader ile çalışırken işimiz bittiğinde mutlaka -> Dispose(kapatmak) etmek lazım -<
        }

       
    }
}
