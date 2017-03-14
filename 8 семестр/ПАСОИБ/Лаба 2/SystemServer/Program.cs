using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemClient
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 53379);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    //Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // Показываем данные на консоли
                    // Console.Write("Полученный текст: " + data + "\n\n");

                    // Отправляем ответ клиенту\
                    string reply = "Спасибо за запрос в " + data.Length.ToString()
                            + " символов";
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        // Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }

                    if (data.IndexOf("<ShowMustGoOn>") > -1)
                    {
                        //Process.Start("https://www.youtube.com/watch?v=6uj-1t89QUU");
                        Process.Start("https://www.google.ru/search?q=do+a+tilt&newwindow=1&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiuzYHYzNDSAhVBXSwKHWERC3kQ_AUIBigB&biw=1366&bih=662#newwindow=1&tbm=isch&q=atari+breakout&*");
                        var text = "Купи слона";
                        for (int i = 0; i < 10; ++i)
                        {
                            Clipboard.SetText("КУПИТЬ СЛОНА!!!!");
                            MessageBox.Show(text, "Купи слона", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            text = "Все нажимают ОК, а ты купи слона";
                            Process.Start("https://www.google.ru/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#newwindow=1&q=do+a+barrel+roll&*");
                        }
                        //Process.Start("bsod.bat");
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
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
    }
}
