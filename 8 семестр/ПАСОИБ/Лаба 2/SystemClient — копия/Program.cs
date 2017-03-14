using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemClient
{
    class Program
    {
        static string message = "";

        const int ACTIVE_PORT = 11000;
        static void Main(string[] args)
        {
            try
            {
                SendMessageFromSocket(ACTIVE_PORT);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        static void SendMessageFromSocket(int port)
        {
            

            // Буфер для входящих сообщений
            byte[] bytes = new byte[1024];

            // Соединение с удаленным устройством

            // Установка удаленной точки для сокета
            IPHostEntry ipHostEntry = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHostEntry.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender.Connect(ipEndPoint);

            string message = Dns.GetHostName();
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender.Send(msg);

            int bytesRec = sender.Receive(bytes);

            Console.WriteLine(Encoding.UTF8.GetString(bytes, 0, bytesRec));

            if (message.IndexOf("<TheEnd>") == -1)
            {
                SendMessageFromSocket(port);
            }

            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
