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

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            //chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            //for (int i = 0; i < 100; ++i)
            //    chart1.Series[0].Points.AddY(i + 1);
            //Thread.Sleep();
            var processes = Process.GetProcesses();
            foreach (var proc in processes)
            {
                try
                {
                    textBox1.Text += $"{proc.ProcessName} - {proc.StartTime.ToString()} - {proc.MainModule.FileVersionInfo.FileDescription}" + Environment.NewLine;
                }
                catch
                {
                    textBox1.Text += $"{proc.ProcessName} - 0 - \"\"" + Environment.NewLine;
                }
                
            }
        }

    }
}
