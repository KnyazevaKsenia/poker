using System.Net.Sockets;
using System.Text;
using ConsolePlayer;
using OmahaPocker;
using OmahaPokerServer;

var user = new Client();
int userId;

try
{
    await user.Connect();
    await user.GetResponse();

    await CommandReader.LoginOrRegist(user);
    await user.GetResponse();

    await CommandReader.CreateSessionOrJoin(user);
    await user.GetResponse();
    do
    {
        await user.GetResponse();

    }
    while (user.clientSocket.Connected);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    user.Disconnect();
}





