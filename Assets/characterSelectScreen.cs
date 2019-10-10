using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterSelectScreen : MonoBehaviour
{
    public Image[] playerIcons;
    int totalPlayers;
    private List<playerInfo>  players;

    public void Start()
    {
        players = new List<playerInfo>();
        totalPlayers = 0;
    }

    public void OnPlayerJoined()
    {
        Debug.Log("Join");
        totalPlayers++;
        playerIcons[totalPlayers].gameObject.SetActive(true);
        
    }

    private int[] shuffleArray(int[] a)
    {
        List<int> input = new List<int>();
        foreach (int x in a)
        {
            input.Add(x);

        }

        List<int> output = new List<int>();
        int failsafe = 0;
        while (input.Count > 0 && failsafe < 100)
        {
            int indexer = Random.Range(0, input.Count);
            output.Add(input[indexer]);
            input.RemoveAt(indexer);
            failsafe++;
        }

        return output.ToArray();

    }

    public void onStartGame()
    {

        switch (totalPlayers)
        {
            case 1:
                {
                    players[0].assignRole(1111);
                    break;
                }
            case 2:
                {
                    int[] a = { 0, 0, 1, 1 };
                    a = shuffleArray(a);
                    int[] b = { 1000, 100, 10, 1 };
                    b = shuffleArray(b);

                    for (int i = 0; i < 4; i++)
                    {
                        players[a[i]].assignRole(b[i]);
                    }

                    break;
                }
        }
    }
}
