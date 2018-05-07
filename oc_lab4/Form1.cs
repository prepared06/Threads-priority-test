using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace oc_lab4
{
    public partial class Form1 : Form
    {
        int i_for_bob1;
        int i_for_bob2;
        Thread bob1;
        Thread bob2;
        FileStream fstream1;
        FileStream fstream2;
        public Form1()
        {
            InitializeComponent();          
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bob1 = new Thread(new ThreadStart(read_forbob1));//создаем пустой поток 1
            bob2 = new Thread(new ThreadStart(read_forbob2));//создаем пустой поток 2
            i_for_bob1 = 0;
            i_for_bob2 = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            label3.Text = "Кількість=" + i_for_bob1;
            label4.Text = "Кількість=" + i_for_bob1;
            //выставляем приоритеты потоков
            if (radioButton1.Checked == true)
            {
                bob1.Priority = ThreadPriority.Normal;
                bob2.Priority = ThreadPriority.Normal;
            }
            else if (radioButton2.Checked == true)
            {
                bob1.Priority = ThreadPriority.AboveNormal;
                bob2.Priority = ThreadPriority.Lowest;
            }
            else if (radioButton3.Checked == true)
            {
                bob1.Priority = ThreadPriority.Lowest;
                bob2.Priority = ThreadPriority.AboveNormal;
            }
            //отключаем буттон1 и радиобутоны
            button2.Enabled = true;
            button1.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            bob1.Start();
            bob2.Start();



        }

        private void read_forbob1()//считывание файла для потока 1
        {
            using ( fstream1 = File.OpenRead("myText.txt"))
            {
                int buff;
                for (int i = 0; i < fstream1.Length; i_for_bob1++)
                {
                    buff = fstream1.ReadByte();
                    //сюда
                    textBox1.Invoke((MethodInvoker)delegate()
                    {
                        textBox1.Text += ((char)buff).ToString();
                    });
                   // textBox1.Text += ((char)buff).ToString();
                   // label3.Text = "Кількість=" + i_for_bob1;
                    label3.Invoke((MethodInvoker)delegate()
                    {
                        label3.Text = "Кількість=" + i_for_bob1;
                    });
                }
            }

        }
        private void read_forbob2()////считывание файла для потока 2
        {
            using ( fstream2 = File.OpenRead("myText.txt"))
            {
                int buff;
                for (int i = 0; i < fstream2.Length; i_for_bob2++)
                {
                    buff = fstream2.ReadByte();
                    //сюда
                  //  textBox2.Text += ((char)buff).ToString();
                   // label4.Text = "Кількість=" + i_for_bob1;
                    textBox2.Invoke((MethodInvoker)delegate()
                    {
                        textBox2.Text += ((char)buff).ToString();
                    });
                    label4.Invoke((MethodInvoker)delegate()
                    {
                        label4.Text = "Кількість=" + i_for_bob1;
                    });

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fstream1.Close();
            fstream2.Close();
            bob1.Abort();
            bob2.Abort();
            button1.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            button2.Enabled = false;
            
        }
    }
}
