using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

class WebSocketClient
{
    private static List<WebSocketClient> Clients = new List<WebSocketClient>();

    private ClientWebSocket webSocket = new ClientWebSocket();
    private string URL;
    public WebSocketClient(string URL = "ws://localhost:5000/ws/")
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
        Console.WriteLine($"Sent: {data}");
    }

    public async Task<string?> ReceiveDataAsync()
    {
        if (webSocket.State != WebSocketState.Open)
        {
            Console.WriteLine("WebSocket is not open.");
            return null;
        }

        byte[] receiveBuffer = new byte[1024];

        try
        {
            // Wait for a message from the server
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
        
            // Process the message
            if (result.MessageType == WebSocketMessageType.Text)
            {
                string receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                Console.WriteLine($"Received: {receivedMessage}");
                return receivedMessage; // Return the received message
            }
            if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine("WebSocket connection closed by the server.");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                return null;
            }
        }
        catch (WebSocketException ex)
        {
            Console.WriteLine($"WebSocket error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        return null; // Return null in case of an error or disconnection
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