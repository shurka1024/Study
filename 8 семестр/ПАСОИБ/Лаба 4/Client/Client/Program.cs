using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        static void SendFiles(Socket sender)
        {
            // Получим все файлы в директории
            var folderPath = "C:\\StatisticInfo";
            if (Directory.Exists(folderPath))
            {
                // Буфер для входящих данных
                byte[] bytes = new byte[1024];
                int bytesRec = 0;

                var files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    byte[] msg1 = Encoding.UTF8.GetBytes(file);
                    sender.Send(msg1);

                    // Получаем ответ от сервера
                    bytesRec = sender.Receive(bytes);

                    int filesize = 0;
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string str = sr.ReadToEnd() + "<FileEnd>";
                        filesize = str.Length;
                        byte[] msg = Encoding.UTF8.GetBytes(str);
                        sender.Send(msg);
                    }
                    // Получаем ответ от сервера
                    bytesRec = sender.Receive(bytes);

                    File.Delete(file);
                }
                byte[] msgend = Encoding.UTF8.GetBytes("<End>");
                sender.Send(msgend);
                bytesRec = sender.Receive(bytes);
            }
        }

        static void SendStatisticsInfo()
        {
            int port = 53379;

            // Соединяемся с удаленным устройством

            // Устанавливаем удаленную точку для сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            //IPAddress ipAddr = ipHost.AddressList[0];
            IPAddress ipAddr = IPAddress.Parse("172.16.17.56");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender.Connect(ipEndPoint);

            SendFiles(sender);


            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                SendStatisticsInfo();
            }
            catch(Exception ex)
            { }

            var currentDate = DateTime.Today.ToShortDateString();
            var folderPath = "C:\\StatisticInfo\\" + currentDate;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string CPUTempfilePath = folderPath + "\\CPUTemp.txt";
            string RAMfilePath = folderPath + "\\RAM.txt";
            string CPUfilePath = folderPath + "\\CPU.txt";
            string infoFilePath = folderPath + "\\Info.txt";

            using (StreamWriter swInfo = File.AppendText(infoFilePath))
            {
                string machineName = "",
                    userName = "", OS = "",
                    processorName = "";

                double op = 0;

                machineName = Environment.MachineName;
                userName = Environment.UserName;
                OS = Environment.OSVersion.ToString();

                // Процессор
                ManagementObjectSearcher searcher8 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject queryObj in searcher8.Get())
                {
                    processorName = queryObj["Name"].ToString();
                }

                // Оперативная память
                ManagementObjectSearcher searcher12 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
                foreach (ManagementObject queryObj in searcher12.Get())
                {
                    op += Math.Round(System.Convert.ToDouble(queryObj["Capacity"]) / 1024 / 1024 / 1024, 2);
                }

                // Основная
                swInfo.WriteLine(DateTime.Now);
                swInfo.WriteLine(machineName);
                swInfo.WriteLine(userName);
                swInfo.WriteLine(OS);

                // Процессор
                swInfo.WriteLine(processorName);

                // Оперативная память
                swInfo.WriteLine(op);
            }

            #region data
            ManagementObjectSearcher cpuTempMonitor = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");

            ManagementObjectSearcher ramMonitor = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");

            ManagementObjectSearcher cpuMonitor = new ManagementObjectSearcher("SELECT LoadPercentage  FROM Win32_Processor");

            while (true)
            {
                foreach (ManagementObject Mo in cpuTempMonitor.Get())
                {
                    double CPUtprt = 0;
                    CPUtprt = Convert.ToDouble(Convert.ToDouble(Mo.GetPropertyValue("CurrentTemperature".ToString())) - 2732) / 10.0;

                    using (StreamWriter swCPUTemp = File.AppendText(CPUTempfilePath))
                    {
                        swCPUTemp.Write(Convert.ToChar(Convert.ToInt32(CPUtprt)));
                    }
                }

                foreach (ManagementObject objram in ramMonitor.Get())
                {
                    ulong totalRam = Convert.ToUInt64(objram["TotalVisibleMemorySize"]);                //общая память ОЗУ
                    ulong busyRam = totalRam - Convert.ToUInt64(objram["FreePhysicalMemory"]);         //занятная память = (total-free)
                    using (StreamWriter swRAM = File.AppendText(RAMfilePath))
                    {
                        swRAM.Write(Convert.ToChar(Convert.ToInt32((busyRam * 100) / totalRam)));
                    }

                }
                foreach (ManagementObject obj in cpuMonitor.Get())
                {
                    using (StreamWriter swCPU = File.AppendText(CPUfilePath))
                    {
                        swCPU.Write(Convert.ToChar(Convert.ToInt32(obj["LoadPercentage"])));
                    }
                }
                Thread.Sleep(4000);
            }
            #endregion

        }
    }
}
