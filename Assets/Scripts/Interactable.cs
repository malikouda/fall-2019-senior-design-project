using UnityEngine;

public class Interactable : MonoBehaviour {
    public void Activate(GameObject player) {
        Debug.Log(player.name);
    }
}
