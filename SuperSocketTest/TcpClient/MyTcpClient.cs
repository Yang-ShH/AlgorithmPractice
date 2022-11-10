using SuperSocket.ClientEngine;
using System.Net;
using System.Text;

namespace SuperSocketTest.TcpClient
{
    public class MyTcpClient : BackgroundService
    {
        public EasyClient MyClient;
        public MyTcpClient()
        {
            MyClient = new EasyClient();
            MyClient.Initialize(new TcpFilter(), e => { Console.WriteLine(e.Body); });
        }

        public async void Connect(string ip, int port)
        {
            if (MyClient.IsConnected)
            {
                return;
            }
            var connected = await MyClient.ConnectAsync(new IPEndPoint(IPAddress.Parse(ip), port));
            if (connected)
            {
                Console.WriteLine($"{ip}:{port} connected");
                MyClient.Send(Encoding.UTF8.GetBytes("hello"));
            }
            else
            {
                Console.WriteLine($"连接失败，ip:{ip},port:{port}");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        Connect("127.0.0.1", 6767);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Thread.Sleep(2000);
                }
            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
    }
}
