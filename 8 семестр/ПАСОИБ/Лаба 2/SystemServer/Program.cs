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
            const int MAX_COUNT = 10;

            var JokeArr = new string[MAX_COUNT, 2];
            // Заполним массив
            JokeArr[0, 0] = "Купи слона!";
            JokeArr[0, 1] = "https://otvet.mail.ru/question/92324700";

            JokeArr[1, 0] = "Все нажимают эту кнопку, а ты купи слона!";
            JokeArr[1, 1] = "https://otvet.imgsmail.ru/download/695303f47291f3111acc27eeba264a32_i-437.gif";

            JokeArr[2, 0] = "Ну купи, купи уже слона!! Балдахин не должен оставаться один";
            JokeArr[2, 1] = "http://btalk.org/class/oc-content/uploads/0/3.jpg";

            JokeArr[3, 0] = "Не хочешь слона, возьми тогда вьетнамскую вислобрюхую свинью";
            JokeArr[3, 1] = "https://www.avito.ru/izhevsk/drugie_zhivotnye/vetnamskie_vislobryuhie_porosyata_917017334";

            JokeArr[4, 0] = "А ведь слона можно дрессировать!! Вот просыпаешься ты утром, а он тебе уже кофе сварил, тапочки принес и лежит рядышком.. Хвостиком виляет ^_^";
            JokeArr[4, 1] = "http://zooclub.ru/attach/21000/21107.jpg";

            JokeArr[5, 0] = "КУПИ Я СКАЗАЛА!!!!!!!!!";
            JokeArr[5, 1] = "https://otvet.mail.ru/question/92324700";

            JokeArr[6, 0] = "Вот поиграй пока, и подумай над покупкой слона";
            JokeArr[6, 1] = "https://www.google.ru/search?q=do+a+tilt&newwindow=1&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiuzYHYzNDSAhVBXSwKHWERC3kQ_AUIBigB&biw=1366&bih=662#newwindow=1&tbm=isch&q=atari+breakout&*";

            JokeArr[7, 0] = "А в Объединенных Арабских Эммиратах уже просекли фишку и скупают их пачками!!! Успей и ты ;)";
            JokeArr[7, 1] = "http://zooclub.ru/attach/21000/21167.jpg";

            JokeArr[8, 0] = "Когда кто-то не покупает слона, в мире грустит один слоненок :( Купи слона!!";
            JokeArr[8, 1] = "http://ic.pics.livejournal.com/krapan_5/15106335/423156/423156_original.jpg";

            JokeArr[9, 0] = "Ну и фиг с тобой.. Но знай, что из-за таких как ты, вот этот слоненок потерялся.. Нужно вовремя покупать слонов!!";
            JokeArr[9, 1] = "https://www.youtube.com/watch?v=DsHVwwkQPr0";

            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            //IPAddress ipAddr = ipHost.AddressList[0];
            IPAddress ipAddr = IPAddress.Parse("172.16.17.168");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 53379);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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
                        //Process.Start("https://www.google.ru/search?q=do+a+tilt&newwindow=1&source=lnms&tbm=isch&sa=X&ved=0ahUKEwiuzYHYzNDSAhVBXSwKHWERC3kQ_AUIBigB&biw=1366&bih=662#newwindow=1&tbm=isch&q=atari+breakout&*");
                        for (int i = 0; i < MAX_COUNT; ++i)
                        {
                            var text = JokeArr[i, 0];
                            var site = JokeArr[i, 1];
                            Process.Start(site);
                            MessageBox.Show(text, "Купи слона", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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
