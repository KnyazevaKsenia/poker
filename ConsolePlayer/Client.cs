using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using OmahaPokerServer;
using OmahaPokerServer.ProtocolServices;

namespace OmahaPocker
{
    public class Client

    {
        public Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public async Task Connect()
        {
            await clientSocket.ConnectAsync("localhost", 5001);
            var connectPackage = OmahaPackageHelper.CreatePackageToConnect();
            await clientSocket.SendAsync(new ArraySegment<byte>(connectPackage), SocketFlags.None);
            
        }

        public async Task Regist(string nickname, string password)
        {
            var contentString = $"{nickname}&{password}";
            byte[] bytes = Encoding.UTF8.GetBytes(contentString!);
            var registPackage = OmahaPackageHelper.CreatePackage(bytes, OmahaPokerServer.Enums.Commands.Regist, OmahaPokerServer.Enums.QueryType.Request, OmahaPokerServer.Enums.StatusName.None);
            await clientSocket.SendAsync(new ArraySegment<byte>(registPackage), SocketFlags.None);
        }

        public async Task Login(string username, string password) 
        {
            var contentString = $"{username}&{password}";
            byte[] bytes = Encoding.UTF8.GetBytes(contentString!);
            var registPackage = OmahaPackageHelper.CreatePackage(bytes, OmahaPokerServer.Enums.Commands.Login, OmahaPokerServer.Enums.QueryType.Request, OmahaPokerServer.Enums.StatusName.Success);
            await clientSocket.SendAsync(new ArraySegment<byte>(registPackage), SocketFlags.None);
        }  

        public async Task<string> GetResponse()
        {
            var buffer = new byte[OmahaPokerServer.ProtocolServices.OmahaPackageHelper.MaxPacketSize];
            var recievedLength = await clientSocket.ReceiveAsync(buffer, SocketFlags.None);
            if (buffer.Length != recievedLength)
            {
                buffer = buffer.Take(recievedLength).ToArray();
            }
            byte[] content = OmahaPackageHelper.GetContent(buffer,recievedLength );
            string result = Encoding.UTF8.GetString(content);
            Console.WriteLine(result);
            return result;
        }

        public async void Disconnect()
        {
            await clientSocket.DisconnectAsync(false);
            clientSocket.Dispose();
        }

        public async Task CreateSession(string name, int amountOfPlayers)
        {
            var contentString = $"{name}&{amountOfPlayers}";
            byte[] bytes = Encoding.UTF8.GetBytes(contentString!);
            var registPackage = OmahaPackageHelper.CreatePackage(bytes, OmahaPokerServer.Enums.Commands.CreateSession, OmahaPokerServer.Enums.QueryType.Request, OmahaPokerServer.Enums.StatusName.None);
            await clientSocket.SendAsync(new ArraySegment<byte>(registPackage), SocketFlags.None);
        }

    }
}
