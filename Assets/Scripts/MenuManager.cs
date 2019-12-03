using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] playerIcons;

    private List<PLAYERID> usedIDs;
    private InputSystemUIInputModule inputModule;
    private PlayerInputManager manager;
    private List<RawPlayerInput> players;
    private bool joiningPlayers;
    public int levelIndex;
    public GameObject characterSelect;
    public GameObject mainMenu;
    public GameObject loadingScreen;
    public AudioFade fader;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<PlayerInputManager>();
        usedIDs = new List<PLAYERID>();
        inputModule = FindObjectOfType<InputSystemUIInputModule>();
        players = new List<RawPlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joiningPlayers)
        {
            foreach (RawPlayerInput p in players)
            {
                if (p.controller.start)
                {
                    startGame();
                }
                if (p.controller.b)
                {
                    toMain();
                }
            }
        }
    }

    //Assign a player input their unique color on creation
    public void assignID(RawPlayerInput newPlayer)
    {
        //If the playerID isn't being used, assign it to a player
        if (!usedIDs.Contains(PLAYERID.BLUE))
        {
            newPlayer.assignId(PLAYERID.BLUE);
            usedIDs.Add(PLAYERID.BLUE);
            return;
        }
        if (!usedIDs.Contains(PLAYERID.RED))
        {
            newPlayer.assignId(PLAYERID.RED);
            usedIDs.Add(PLAYERID.RED);
            return;
        }
        if (!usedIDs.Contains(PLAYERID.GREEN))
        {
            newPlayer.assignId(PLAYERID.GREEN);
            usedIDs.Add(PLAYERID.GREEN);
            return;
        }
        if (!usedIDs.Contains(PLAYERID.YELLOW))
        {
            newPlayer.assignId(PLAYERID.YELLOW);
            usedIDs.Add(PLAYERID.YELLOW);
            return;
        }

        Debug.LogError("More players than there should be");
    }

    //Quit button
    public void endGame()
    {
        Application.Quit();
    }

    //Go to the character selection screen
    public void toCharacterSelect()
    {
        joiningPlayers = true;
        manager.EnableJoining();
        inputModule.DisableAllActions();
    }

    //Back to the first screen from character select
    public void toMain()
    {
        joiningPlayers = false;
        manager.DisableJoining();
        inputModule.EnableAllActions();
        inputModule.UpdateModule();

        characterSelect.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void startGame()
    {
        fader.fadeSounds();
        StartCoroutine(loadScene());
    }

    public void test(string message)
    {
        Debug.Log(message);
    }

    private void OnPlayerJoined(PlayerInput p)
    {
        RawPlayerInput playerInput = p.gameObject.GetComponent<RawPlayerInput>();
        players.Add(playerInput);
        assignID(playerInput);
        foreach(RawPlayerInput player in players)
        {
            switch (player.playerId)
            {
                case PLAYERID.BLUE:
                    playerIcons[0].SetActive(true);
                    break;
                case PLAYERID.RED:
                    playerIcons[1].SetActive(true);
                    break;
                case PLAYERID.GREEN:
                    playerIcons[2].SetActive(true);
                    break;
                case PLAYERID.YELLOW:
                    playerIcons[3].SetActive(true);
                    break;
            }
        
                
        }
        //playerInput.assignMenuControls(inputModule);


    }

    private IEnumerator loadScene()
    {
        loadingScreen.SetActive(true);
       AsyncOperation sceneloader = SceneManager.LoadSceneAsync(1);
       while(!sceneloader.isDone)
        {
            yield return null;
        }
    }
}
