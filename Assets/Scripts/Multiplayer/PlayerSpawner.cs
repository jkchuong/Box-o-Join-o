using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    
    private const string PLAYER_AVATAR = "playerAvatar";

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        GameObject playerToSpawn;
        
        // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
        if (PhotonNetwork.LocalPlayer.CustomProperties[PLAYER_AVATAR] != null)
        {
            playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties[PLAYER_AVATAR]];
        }
        else
        {
            playerToSpawn = playerPrefabs[0]; // Spawn default player if no custom properties exist
        }

        PhotonNetwork.Instantiate(playerToSpawn.name, randomPosition, Quaternion.identity);

    }
}
