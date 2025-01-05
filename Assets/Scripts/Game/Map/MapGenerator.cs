using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile emptyTile;

    [Header("Map Decor")]
    public Tile floorTile;
    public Tile ceilTile;
    public Tile leftTile;
    public Tile rightTile;

    public Tile floorRightTile;
    public Tile floorLeftTile;
    public Tile ceilRightTile;
    public Tile ceilLeftTile;

    [Header("Map Size")]
    public int mapWidth;
    public int mapHeight;

    [Header("Connect Size")]
    public int connectSizeX = 5;
    public int connectSizeY = 5;

    [Header("Platform Distance")]
    public int yDistanceMin = 3;
    public int yDistanceMax = 5;

    [Header("Tile Count")]
    public int xWallCountMin = 4;
    public int xWallCountMax = 12;

    [SerializeField] private GameObject PlatformEffector2D;

    [Header("Spawn Point")]
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;

    [Header("Object Per Chunk")]
    [SerializeField] private GameObject light;

    [System.Serializable]
    public class GameObjectEntry
    {
        public string name;
        public GameObject prefab;
        public int quantity;
    }

    [Header("List Object Spawn")]
    [SerializeField] private List<GameObjectEntry> gameObjectEntries;

    private const int chunkSize = 16;

    public void GenerateMap()
    {
        tilemap.ClearAllTiles();
        tilemap.RefreshAllTiles();

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), emptyTile);
            }
        }

        for (int chunkX = 0; chunkX < mapWidth; chunkX += chunkSize)
        {
            for (int chunkY = 0; chunkY < mapHeight; chunkY += chunkSize)
            {
                GenerateChunk(chunkX, chunkY);
                EnsureChunkConnections(chunkX, chunkY);
                AddPlatfomInChunk(chunkX, chunkY);
                AddLightInChunk(chunkX, chunkY);
            }
        }

        AddBorders();

        CreativeMiniMap();

        InstantiateStartAndEnd();

        UpdateWallTiles();

        GenerateAdditionalObjects();
    }

    private void GenerateChunk(int startX, int startY)
    {
        for (int x = startX; x < startX + chunkSize && x < mapWidth; x++)
        {
            tilemap.SetTile(new Vector3Int(x, startY, 0), wallTile);
            tilemap.SetTile(new Vector3Int(x, startY + chunkSize - 1, 0), wallTile);
        }

        for (int y = startY; y < startY + chunkSize && y < mapHeight; y++)
        {
            tilemap.SetTile(new Vector3Int(startX, y, 0), wallTile);
            tilemap.SetTile(new Vector3Int(startX + chunkSize - 1, y, 0), wallTile);
        }

        for (int x = startX + 1; x < startX + chunkSize - 1 && x < mapWidth - 1; x++)
        {
            for (int y = startY + 1; y < startY + chunkSize - 1 && y < mapHeight - 1; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), emptyTile);
            }
        }
    }

    private void EnsureChunkConnections(int startX, int startY)
    {
        if (startX + chunkSize < mapWidth)
        {
            ConnectChunksHorizontally(startX, startY);
        }

        if (startX - chunkSize >= 0)
        {
            ConnectChunksHorizontally(startX - chunkSize, startY);
        }

        if (startY + chunkSize < mapHeight)
        {
            ConnectChunksVertically(startX, startY);
        }

        if (startY - chunkSize >= 0)
        {
            ConnectChunksVertically(startX, startY - chunkSize);
        }
    }


    private void ConnectChunksHorizontally(int startX, int startY)
    {
        int connectY = startY + chunkSize / 2;
        for (int y = connectY - Mathf.CeilToInt(connectSizeY / 2); y <= connectY + Mathf.FloorToInt(connectSizeY / 2); y++)
        {
            tilemap.SetTile(new Vector3Int(startX + chunkSize - 1, y, 0), emptyTile);
            tilemap.SetTile(new Vector3Int(startX + chunkSize, y, 0), emptyTile);
        }
    }

    private void ConnectChunksVertically(int startX, int startY)
    {
        int connectX = startX + chunkSize / 2;
        for (int x = connectX - Mathf.CeilToInt(connectSizeX / 2); x <= connectX + Mathf.FloorToInt(connectSizeX / 2); x++)
        {
            tilemap.SetTile(new Vector3Int(x, startY + chunkSize - 1, 0), emptyTile);
            tilemap.SetTile(new Vector3Int(x, startY + chunkSize, 0), emptyTile);
        }
    }

    private void AddPlatfomInChunk(int startX, int startY)
    {
        int yDistance = Random.Range(yDistanceMin, yDistanceMax);
        int xWallCount = Random.Range(xWallCountMin, xWallCountMax);

        HashSet<int> usedXPositions = new HashSet<int>();

        for (int y = startY + 1; y < startY + chunkSize - 1; y += yDistance)
        {
            usedXPositions.Clear();

            for (int i = 0; i < xWallCount; i++)
            {
                int xPos;
                do
                {
                    xPos = Random.Range(startX + 1, startX + chunkSize - 1);
                } while (usedXPositions.Contains(xPos));

                usedXPositions.Add(xPos);
                tilemap.SetTile(new Vector3Int(xPos, y, 0), wallTile);
            }
        }
    }

    private void AddBorders()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            tilemap.SetTile(new Vector3Int(x, 0, 0), wallTile);
            tilemap.SetTile(new Vector3Int(x, mapHeight - 1, 0), wallTile);
        }

        for (int y = 0; y < mapHeight; y++)
        {
            tilemap.SetTile(new Vector3Int(0, y, 0), wallTile);
            tilemap.SetTile(new Vector3Int(mapWidth - 1, y, 0), wallTile);
        }
    }

    private void CreativeMiniMap()
    {
        GameObject miniMap = Instantiate(PlatformEffector2D, PlatformEffector2D.transform.position, PlatformEffector2D.transform.rotation, PlatformEffector2D.transform.parent);

        miniMap.layer = LayerMask.NameToLayer("MiniMapIcon");

        TilemapRenderer tilemapRendererMinimap = miniMap.GetComponent<TilemapRenderer>();

        tilemapRendererMinimap.sortingLayerID = 0;

        RemoveComponent<TilemapCollider2D>(miniMap);
        RemoveComponent<Rigidbody2D>(miniMap);
        RemoveComponent<CompositeCollider2D>(miniMap);
        RemoveComponent<ShadowCaster2D>(miniMap);
    }

    private void RemoveComponent<T>(GameObject target) where T : Component
    {
        T component = target.GetComponent<T>();
        if (component != null)
        {
            Destroy(component);
        }
    }

    private void InstantiateStartAndEnd()
    {
        Vector3 mapCenter = new Vector3(mapWidth / 2, mapHeight / 2, 0);

        int randomCorner = Random.Range(0, 4);

        Vector3 startPosition = Vector3.zero;

        switch (randomCorner)
        {
            case 0:
                startPosition = new Vector3(chunkSize / 2, mapHeight - chunkSize / 2, 0);
                break;
            case 1:
                startPosition = new Vector3(mapWidth - chunkSize / 2, mapHeight - chunkSize / 2, 0);
                break;
            case 2:
                startPosition = new Vector3(chunkSize / 2, chunkSize / 2, 0);
                break;
            case 3:
                startPosition = new Vector3(mapWidth - chunkSize / 2, chunkSize / 2, 0);
                break;
        }

        GameObject startPoint = Instantiate(start, startPosition + new Vector3(-6, -7, 0), Quaternion.identity);
        startPoint.name = start.name;

        Vector3 endPosition = mapCenter * 2 - startPosition;

        GameObject endPoint = Instantiate(end, endPosition + new Vector3(-6, -7, 0), Quaternion.identity);
        endPoint.name = end.name;
    }

    private void UpdateWallTiles()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase currentTile = tilemap.GetTile(position);

                if (currentTile == wallTile)
                {
                    bool topEmpty = IsEmpty(x, y + 1);
                    bool bottomEmpty = IsEmpty(x, y - 1);
                    bool leftEmpty = IsEmpty(x - 1, y);
                    bool rightEmpty = IsEmpty(x + 1, y);

                    if (leftEmpty && rightEmpty)
                    {
                        tilemap.SetTile(position, floorTile);
                    }
                    else if (topEmpty && rightEmpty)
                    {
                        tilemap.SetTile(position, floorRightTile);
                    }
                    else if (topEmpty && leftEmpty)
                    {
                        tilemap.SetTile(position, floorLeftTile);
                    }
                    else if (bottomEmpty && leftEmpty)
                    {
                        tilemap.SetTile(position, ceilLeftTile);
                    }
                    else if (bottomEmpty && rightEmpty)
                    {
                        tilemap.SetTile(position, ceilRightTile);
                    }
                    else if (topEmpty)
                    {
                        tilemap.SetTile(position, floorTile);
                    }
                    else if (bottomEmpty)
                    {
                        tilemap.SetTile(position, ceilTile);
                    }
                    else if (leftEmpty)
                    {
                        tilemap.SetTile(position, rightTile);
                    }
                    else if (rightEmpty)
                    {
                        tilemap.SetTile(position, leftTile);
                    }
                }
            }
        }
    }

    private bool IsEmpty(int x, int y)
    {
        if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
        {
            TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
            return tile == emptyTile;
        }
        return false;
    }

    private void AddLightInChunk(int startX, int startY)
    {
        int x = Random.Range(startX + 1, startX + chunkSize - 1);
        int y = Random.Range(startY + 1, startY + chunkSize - 1);

        GameObject lightInstance = Instantiate(light, new Vector3(x, y, 0), Quaternion.identity, transform);
        lightInstance.name = "Light_" + startX + "_" + startY;
    }

    private void GenerateAdditionalObjects()
    {
        List<Vector3Int> floorPositions = new List<Vector3Int>();
        for (int x = 1; x < mapWidth - 1; x++)
        {
            for (int y = 1; y < mapHeight - 1; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(position) == floorTile)
                {
                    floorPositions.Add(position);
                }
            }
        }

        foreach (var entry in gameObjectEntries)
        {
            if (entry.quantity > floorPositions.Count)
            {
                Debug.LogWarning($"Not enough floor tiles for {entry.name}. Reduce quantity or increase map size.");
                continue;
            }

            for (int i = 0; i < entry.quantity; i++)
            {
                int randomIndex = Random.Range(0, floorPositions.Count);
                Vector3Int chosenPosition = floorPositions[randomIndex];
                floorPositions.RemoveAt(randomIndex);

                GameObject instance = Instantiate(entry.prefab, new Vector3(chosenPosition.x, chosenPosition.y + 1f, 0), Quaternion.identity, transform);
                instance.name = entry.name + "_" + i;
            }
        }
    }

    private void Awake()
    {
        GenerateMap();
    }
}
