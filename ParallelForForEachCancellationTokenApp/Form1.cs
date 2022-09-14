using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelForForEachCancellationTokenApp
{
    public partial class Form1 : Form
    {
        CancellationTokenSource ct;
        private int counter { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
            ct = new CancellationTokenSource();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ct = new CancellationTokenSource(); //işlem iptal edildikten sonra tekrar başlatmak için gerekli olan kod.
            List<string> urls = new List<string>
            {
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com"
            };

            HttpClient client = new HttpClient();

            //for veya foreaach içerisinde cancellation token yerleştirebilmek için ParallelOptions kullanılır.
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = ct.Token;

            Task.Run(() =>
            {
                Parallel.ForEach<string>(urls, parallelOptions, (url) =>
                {//ct'ye erişemezsek parallelOptions üzerinden de işlemlerimizi yapabiliriz.
                    try
                    {
                        string content = client.GetStringAsync(url).Result; //bloklamamızın sebebi->her bir thread çalışacağı için eğer birinde hata ile karşılaşırsak sistem sanki tüm thread'ler hatalı çalışmış gibi davranacaktır ve bunun sonucunda da uygulama handle edemeyeceği için kapanmış olacaktır. bunun önüne geçmek için Result metodunu yazarız. kısaca hataları handle edebilmek için de yazarız.
                        string data = $"{url}:{content.Length}";
                        //parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                        listBox1.Invoke((MethodInvoker)delegate
                        {
                            ct.Token.ThrowIfCancellationRequested();
                            listBox1.Items.Add(data);
                        });
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("İşlem iptal edildi: " + ex.Message);
                    }
                    

                });
            });
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ct.Cancel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = counter++.ToString();
        }
    }
}
