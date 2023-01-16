using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]
    [Range(0,10)]
    private float speed = 0.5f;
    private Vector2Int gridPosition;
    private Vector2Int gridMoveDirection;
    private float gridMoveTimer;
    private float gridMoveTimerMax;

    private GameManager gameManager;
    private LevelHandler levelHandler;

    private void Awake() {
        gridPosition = new Vector2Int(1, 1);
        gridMoveTimerMax = 0.5f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);
    }

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        levelHandler = FindObjectOfType<LevelHandler>();
    }

    private void Update()
    {
        KeyboardInput();
        HandleGridMovement();
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimerMax;
        }
        transform.position = new Vector3(gridPosition.x * gameManager.gridMove, gridPosition.y * gameManager.gridMove);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection));
        levelHandler.DetectObject(transform.position);
    }

    private void KeyboardInput()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");
        if(inputHorizontal != 0) {
            if((gridMoveDirection.x == 1 && inputHorizontal == -1) || (gridMoveDirection.x == -1 && inputHorizontal == 1)) return;
            gridMoveDirection.x = (int)inputHorizontal;
            gridMoveDirection.y = 0;
        }
        if(inputVertical != 0) {
            if((gridMoveDirection.y == 1 && inputVertical == -1) || (gridMoveDirection.y == -1 && inputVertical == 1)) return;
            gridMoveDirection.x = 0;
            gridMoveDirection.y = (int)inputVertical;
        }
    }

    private float GetAngleFromVector(Vector2Int direction) {
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(n < 0) n += 360;
        return n;
    }
}
