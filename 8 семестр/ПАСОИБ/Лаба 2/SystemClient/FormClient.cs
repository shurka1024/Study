﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SystemServer
{
    public partial class FormClient : Form
    {
        const int MAX_CONNECTIONS = 10;
        const int ACTIVE_PORT = 53379;


        public FormClient()
        {
            InitializeComponent();
        }

        private void On_Click(object sender, EventArgs e)
        {

        }

        private void SendJoke_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessageFromSocket(ACTIVE_PORT);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
            finally
            {
                //Console.ReadLine();
            }


        }

        private void Off_Click(object sender, EventArgs e)
        {
        }

        static void SendMessageFromSocket(int port)
        {
            // Буфер для входящих данных
            byte[] bytes = new byte[1024];

            // Соединяемся с удаленным устройством

            // Устанавливаем удаленную точку для сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender.Connect(ipEndPoint);

            //Console.Write("Введите сообщение: ");
            string message = "<ShowMustGoOn>";

            //Console.WriteLine("Сокет соединяется с {0} ", sender.RemoteEndPoint.ToString());
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender.Send(msg);

            // Получаем ответ от сервера
            int bytesRec = sender.Receive(bytes);

            //Console.WriteLine("\nОтвет от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));

            // Используем рекурсию для неоднократного вызова SendMessageFromSocket()
            if (message.IndexOf("<TheEnd>") == -1)
                SendMessageFromSocket(port);

            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }

}
