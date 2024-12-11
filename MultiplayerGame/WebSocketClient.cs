using System;
using System.Net.WebSockets;
using System.Text;
namespace MultiplayerGame;
using System.Threading;
using MultiplayerGame;
 public class WebSocketClient
{
    private static List<WebSocketClient> Clients = new List<WebSocketClient>();

    private ClientWebSocket webSocket = new ClientWebSocket();
    private string URL;
    public string LastMessageReceived;
    public WebSocketClient(string URL = "ws://10.135.217.32:5000/ws/\"")
    {
        this.URL = URL;
        CreateWebSocket();
        
    }

    private async Task CreateWebSocket()
    {
        Clients.Add(this);
        Uri serverUri = new Uri(URL);
        await webSocket.ConnectAsync(serverUri, CancellationToken.None);
        Console.WriteLine("Connected to WebSocket server!");
    }
    
    public async Task sendMessage(string data)
    {
        // Send a message to the server;
        byte[] sendBuffer = Encoding.UTF8.GetBytes(data);
        await webSocket.SendAsync(new ArraySegment<byte>(sendBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
     
    }

    public async Task ReceiveDataAsync()
    {
        if (webSocket.State != WebSocketState.Open)
        {
            Console.WriteLine("WebSocket is not open.");
            return;
        }

        byte[] buffer = new byte[1024];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
        LastMessageReceived = receivedMessage;

    }

    public static async void CloseAllConnections()
    {
        foreach (var client in Clients)
        {
            await client.webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
            Console.WriteLine("WebSocket connection closed.");
        }   
    }

}