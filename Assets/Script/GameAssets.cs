using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets i;
    public Sprite snakeHeadSprite;

    private void Awake() {
        i = this;
    }
}
