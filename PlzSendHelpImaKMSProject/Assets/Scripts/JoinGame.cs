using UnityEngine;
using Mirror;

public class JoinGame : MonoBehaviour
{

    [SerializeField]
    private UnityEngine.UI.InputField input;

    private NetworkManager networkManager;


    void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void Join()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            StartButtons();
        }
    }

    void StartButtons()
    {
        networkManager.networkAddress = input.text == "" ?  "localhost" : input.text;
        networkManager.StartClient();
        
    }
}