using UnityEngine;

public class Ceiling : MonoBehaviour {
    [HideInInspector]
    public bool fadeIn = false;
    [HideInInspector]
    public bool fadeOut = false;
    private Color alphaColor;
    public float timeToFade = 10.0f;

    public GameManager gameManager;

    public void Start() {
        alphaColor = GetComponent<MeshRenderer>().material.color;
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Update() {
        if (fadeOut) {
            alphaColor.a = 0;
            if (GetComponent<MeshRenderer>().material.color.a <= 0.01f) {
                GetComponent<MeshRenderer>().material.color = alphaColor;
                fadeOut = false;
            } else {
                GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
            }
        }

        if (fadeIn) {
            alphaColor.a = 1;
            if (GetComponent<MeshRenderer>().material.color.a >= .99f) {
                GetComponent<MeshRenderer>().material.color = alphaColor;
                fadeIn = false;
            } else {
                GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
            }
        }
    }
}
