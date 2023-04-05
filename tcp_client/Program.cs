using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using SharedData;

namespace tcp_client
{
  

    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 8080; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
             
            TcpClient client = new TcpClient();

            
            client.Connect(ipPoint);

         

            try
            {
                Request request = new Request();
                do
                {


                    Console.WriteLine("A:");
                    request.A = double.Parse(Console.ReadLine());
                    Console.WriteLine("B:");
                    request.B = double.Parse(Console.ReadLine());
                    Console.WriteLine("Operation[1-4]:");
                    request.Operation = (OperationType)Enum.Parse(typeof(OperationType), Console.ReadLine());


                    NetworkStream ns = client.GetStream();
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ns, request);





                    StreamReader sr = new StreamReader(ns);
                    string response = sr.ReadLine();

                    Console.WriteLine("server response: " + response);


                } while (request.A != 0 || request.B != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            
                client.Close();
            }
        }
    }
}
