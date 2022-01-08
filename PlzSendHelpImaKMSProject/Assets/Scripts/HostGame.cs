using UnityEngine;
using Mirror;

public class HostGame : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 6;

	private string roomName;

	private NetworkManager networkManager;

	void Start ()
	{
		networkManager = NetworkManager.singleton;
	}

	public void CreateRoom ()
	{

        if (networkManager.isNetworkActive)
        {
            networkManager.StopHost();
        }
        networkManager.StartHost();

	}

}
