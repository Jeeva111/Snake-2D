using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    
    [SerializeField]
    private Sprite foodSprite;
    [SerializeField]
    [Range(0f, 1f)]
    private float offset = 0.5f;
    private float gridX = 0;
    private float gridY = 0;
    private GameObject foodGameObj;
    private int width = 5;
    private int height = 10;
    private bool merged = false;

    private void Start() {
        SpawnFood();
    }

    private void SpawnFood() {
        merged = false;
        gridX = GetRandomFloat(0, width);
        gridY = GetRandomFloat(0, height);
        if(gridX == width) {
            gridX -= offset;
        } else if(gridX == 0) {
            gridX += offset;
        }
        if(gridY == height) {
            gridY -= offset;
        } else if(gridY == 0) {
            gridY += offset;
        }

        foodGameObj = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObj.GetComponent<SpriteRenderer>().sprite = foodSprite;
        foodGameObj.transform.position = new Vector3(gridX, gridY);
    }

    public float GetRandomFloat(int min, int max, float value = 0.5f)
    {
        //int multipliedMin = (int) (max / value);
        int multipliedMax = (int) (max / value);
        float random = Random.Range(min, multipliedMax);
        return random * value;
    }

    public void DetectObject(Vector2 playerPosition) {
        if(!merged && playerPosition == (Vector2) foodGameObj.transform.position) {
            merged = true;
            Object.Destroy(foodGameObj);
            SpawnFood();
        }
    }

}
