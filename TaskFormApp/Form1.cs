using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        }
        private void BtnReadFile_Click(object sender, EventArgs e)
        {
            //dosya okuma işlemini gerçekleştiriyoruz.
            string data = ReadFile();

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
                Thread.Sleep(5000); //test amaçlı kullanıyoruz.
                data = s.ReadToEnd(); //datayı baştan sonra okur, geriye string döner.
            }
            return data;
            
        }
       
    }
}
