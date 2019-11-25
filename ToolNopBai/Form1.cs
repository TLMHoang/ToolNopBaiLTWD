using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KAutoHelper;

namespace ToolNopBai
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        public static void MoApp()
        {
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/C Start HuTechPractice.exe";

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            p.StartInfo = startInfo;
            p.Start();
        }

        private void btnDienThongTin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox1.Text == "")
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Nhập lần nộp");
                }
                else
                {
                    MessageBox.Show("Chọn dường dẫn file");
                }
            }
            else
            {
                IntPtr HWindow = IntPtr.Zero;

                do
                {
                    Process[] p = Process.GetProcessesByName("HuTechPractice");
                    switch (p.Length)
                    {
                        case 0:
                            MoApp();
                            Thread.Sleep(1000);
                            break;
                        case 1:
                            HWindow = p[0].MainWindowHandle;
                            break;
                        default:
                            for (int i = 1; i < p.Length; i++)
                            {
                                p[i].Kill();
                                Thread.Sleep(10);
                            }
                            break;
                    }
                } while (HWindow == IntPtr.Zero);

                IntPtr ChildH = IntPtr.Zero;
                ChildH = AutoControl.FindHandle(HWindow, "WindowsForms10.COMBOBOX.app.0.141b42a_r6_ad1", null);

                AutoControl.SendText(ChildH, comboBox1.Text);
                List<IntPtr> AChild = AutoControl.FindHandles(HWindow, "WindowsForms10.EDIT.app.0.141b42a_r6_ad1", null);

                
                AutoControl.SendText(AChild[0], textBox1.Text);
                AutoControl.SendText(AChild[1], "Lập Trình Window");
                AutoControl.SendText(AChild[2], "17DTHB2");
                AutoControl.SendText(AChild[3], "Trần Lê Minh Hoàng");
                AutoControl.SendText(AChild[4], "fsminhhoangfs@gmail.com");

                IntPtr ChildB = AutoControl.FindHandle(HWindow, "WindowsForms10.BUTTON.app.0.141b42a_r6_ad1", "Nộp bài tập");

                AutoControl.SendClickOnControlByHandle(ChildB);
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = open.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process[] p = Process.GetProcessesByName("HuTechPractice");

            if (p.Length == 0)
            {
                this.Close();
            }
            else
            {
                p[0].Kill();
                this.Close();
            }
        }
    }
}
