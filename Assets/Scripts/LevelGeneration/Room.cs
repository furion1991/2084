using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private GameObject floorPrefab;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private int minHeight;
    [SerializeField]
    private int maxHeight;
    [SerializeField]
    private int minWidth;
    [SerializeField]
    private int maxWidth;
    [SerializeField]
    private int[,] roomData;
    [SerializeField]
    private float spawnTilesOffset = -0.3f;
    private void Awake()
    {
        CreateRoomFromData(GenerateRoomData());
    }
    private void CreateRoomFromData(int[,] roomData)
    {
        int roomHeightMax = roomData.GetUpperBound(0);
        int roomWidthMax = roomData.GetUpperBound(1);
        for (int i = 0; i <= roomHeightMax; i++)
        {
            for (int j = 0; j <= roomWidthMax; j++)
            {
                if (roomData[i,j] == 1)
                {
                    GameObject go = Instantiate(wallPrefab, new Vector3(j, spawnTilesOffset, -i), Quaternion.identity);
                    go.tag = "Wall";
                }
                if (roomData[i,j] == 0)
                {
                    Instantiate(floorPrefab, new Vector3(j, spawnTilesOffset, -i), Quaternion.Euler(90, 0, 0));
                }
            }
        }
    }
    private int[,] GenerateRoomData()
    {
        int[,] newRoom = new int[Random.Range(minHeight, maxHeight), Random.Range(minWidth, maxWidth)];
        int roomHeightMax = newRoom.GetUpperBound(0);
        int roomWidthMax = newRoom.GetUpperBound(1);

        for (int i = 0; i <= roomHeightMax; i++)
        {
            for (int j = 0; j <= roomWidthMax; j++)
            {
                if (i == 0 || j == 0 || i == roomHeightMax || j == roomWidthMax)
                {
                    newRoom[i, j] = 1;
                }
            }
        }
        roomData = newRoom;
        return newRoom;
    }
}
