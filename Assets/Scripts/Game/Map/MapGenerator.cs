using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile emptyTile;

    public int mapWidth;
    public int mapHeight;

    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;

    private const int chunkSize = 16;

    public void GenerateMap()
    {
        // Clear existing tiles
        tilemap.ClearAllTiles();

        // Create the map grid
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), emptyTile);
            }
        }

        // Ensure a path from the first to the last chunk
        CreateMainPath();

        // Divide the map into chunks and generate content
        for (int chunkX = 0; chunkX < mapWidth; chunkX += chunkSize)
        {
            for (int chunkY = 0; chunkY < mapHeight; chunkY += chunkSize)
            {
                GenerateChunk(chunkX, chunkY);
                EnsureAtLeastOneConnection(chunkX, chunkY);
            }
        }

        // Add border walls around the map
        AddBorders();

        // Instantiate start and end objects at the middle of the first and last chunks
        InstantiateStartAndEnd();
    }

    private void CreateMainPath()
    {
        int currentX = 0;
        int currentY = 0;

        while (currentX < mapWidth && currentY < mapHeight)
        {
            // Create a path in the current chunk
            int pathX = currentX + chunkSize / 2;
            for (int y = currentY; y < currentY + chunkSize && y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(pathX, y, 0), emptyTile);
            }

            // Move to the next chunk
            currentX += chunkSize;
            if (currentX < mapWidth)
            {
                int pathY = currentY + chunkSize / 2;
                for (int x = currentX - chunkSize; x < currentX && x < mapWidth; x++)
                {
                    tilemap.SetTile(new Vector3Int(x, pathY, 0), emptyTile);
                }
            }
        }
    }

    private void GenerateChunk(int startX, int startY)
    {
        // Add border walls around the chunk
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

        // Ensure the interior of the chunk is empty
        for (int x = startX + 1; x < startX + chunkSize - 1 && x < mapWidth - 1; x++)
        {
            for (int y = startY + 1; y < startY + chunkSize - 1 && y < mapHeight - 1; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), emptyTile);
            }
        }
    }

    private void EnsureAtLeastOneConnection(int startX, int startY)
    {
        List<System.Action> connectionMethods = new List<System.Action>();

        // Chunk-specific rules
        if (startX == 0 && startY == 0)
        {
            // Skip bottom connections for the first chunk
            connectionMethods.Add(() => ConnectChunksHorizontally(startX, startY));
        }
        else if (startX == mapWidth - chunkSize && startY == mapHeight - chunkSize)
        {
            // Skip bottom connections for the last chunk
            connectionMethods.Add(() => ConnectChunksHorizontally(startX, startY));
        }
        else
        {
            if (startX > 0) // Can connect horizontally to the previous chunk
            {
                connectionMethods.Add(() => ConnectChunksHorizontally(startX, startY));
            }

            if (startY > 0) // Can connect vertically to the previous chunk
            {
                connectionMethods.Add(() => ConnectChunksVertically(startX, startY));
            }
        }

        if (connectionMethods.Count > 0)
        {
            // Ensure at least one connection
            connectionMethods[Random.Range(0, connectionMethods.Count)]();

            // Optionally add additional connections
            foreach (var connectionMethod in connectionMethods)
            {
                if (Random.value > 0.5f)
                {
                    connectionMethod();
                }
            }

            // Add floating walls at connections to ensure the character can jump
            AddFloatingWallsAtConnections(startX, startY);
        }
    }

    private void AddFloatingWallsAtConnections(int startX, int startY)
    {
        // Calculate the random height for floating walls (to ensure character can jump up)
        int floatingWallHeight = Random.Range(2, 5); // Random height between 2 and 4 blocks

        // Create floating walls at connections between chunks
        // Horizontal connection (for horizontally connected chunks)
        if (startX > 0)
        {
            int connectYStart = startY + chunkSize / 2 - Random.Range(4, 7);
            int connectYEnd = connectYStart + Random.Range(2, 12); // Door size between 8 and 12
            for (int y = connectYStart; y < connectYEnd && y < mapHeight - 1; y++)
            {
                // Floating wall (step) without blocking the connection
                tilemap.SetTile(new Vector3Int(startX, y + floatingWallHeight, 0), wallTile); // Floating wall (step)
                tilemap.SetTile(new Vector3Int(startX - 1, y + floatingWallHeight, 0), wallTile); // Floating wall (step)
            }

            // Add random steps (floating platforms) along the connection
            AddPlatfomInChunk(startX, connectYStart);
        }

        if (startY > 0)
        {
            int connectXStart = startX + chunkSize / 2 - Random.Range(4, 7);
            int connectXEnd = connectXStart + Random.Range(2, 12); // Door size between 8 and 12
            for (int x = connectXStart; x < connectXEnd && x < mapWidth - 1; x++)
            {
                tilemap.SetTile(new Vector3Int(x + floatingWallHeight, startY, 0), wallTile); // Floating wall (step)
                tilemap.SetTile(new Vector3Int(x + floatingWallHeight, startY - 1, 0), wallTile); // Floating wall (step)
            }

            // Add random steps (floating platforms) along the connection
            AddPlatfomInChunk(connectXStart, startY);
        }
    }

    private void AddPlatfomInChunk(int startX, int startY)
    {
        int yRandom = Random.Range(2, 4);  // A: khoảng cách ngẫu nhiên giữa các platform, có thể là 2 hoặc 3
    int wallCount = Random.Range(4, 7); // B: số lượng tường ngẫu nhiên từ 4 đến 6

    // Tạo các bước (platforms) với khoảng cách yRandom
    for (int i = yRandom; i < chunkSize - 1; i += yRandom)  // Tăng y theo A (2 hoặc 3)
    {
        List<int> usedXPositions = new List<int>();

        // Tạo ra wallCount tường ngẫu nhiên trên trục hoành tại mỗi giá trị y
        for (int j = 0; j < wallCount; j++)
        {
            // Chọn ngẫu nhiên một vị trí x trong phạm vi của chunk, tránh biên giới
            int offsetX = Random.Range(1, chunkSize - 2); // Tránh biên trái và phải
            int xPos = startX + offsetX;

            // Đảm bảo xPos không trùng lặp
            while (usedXPositions.Contains(xPos))
            {
                offsetX = Random.Range(1, chunkSize - 2);  // Chọn lại nếu trùng
                xPos = startX + offsetX;
            }

            // Lưu trữ xPos đã được sử dụng
            usedXPositions.Add(xPos);

            // Tạo tường tại (xPos, yPos) với yPos là startY + i
            int yPos = startY + i;

            // Đảm bảo yPos không vượt quá phạm vi của chunk (chắc chắn tường nằm trong phạm vi của chunk)
            if (yPos >= startY && yPos < startY + chunkSize)
            {
                // Đặt tile tại vị trí (xPos, yPos)
                tilemap.SetTile(new Vector3Int(xPos, yPos - 3, 0), wallTile);
            }
        }
    }
    }

    private void ConnectChunksHorizontally(int startX, int startY)
    {
        int connectYStart = startY + chunkSize / 2 - Random.Range(4, 7);
        int connectYEnd = connectYStart + Random.Range(2, 14); // Door size between 8 and 12

        for (int y = connectYStart; y < connectYEnd && y < mapHeight - 1; y++)
        {
            tilemap.SetTile(new Vector3Int(startX, y, 0), emptyTile);
            tilemap.SetTile(new Vector3Int(startX - 1, y, 0), emptyTile);
        }
    }

    private void ConnectChunksVertically(int startX, int startY)
    {
        int connectXStart = startX + chunkSize / 2 - Random.Range(4, 7);
        int connectXEnd = connectXStart + Random.Range(2, 14); // Door size between 8 and 12

        for (int x = connectXStart; x < connectXEnd && x < mapWidth - 1; x++)
        {
            tilemap.SetTile(new Vector3Int(x, startY, 0), emptyTile);
            tilemap.SetTile(new Vector3Int(x, startY - 1, 0), emptyTile);
        }
    }

    private void AddBorders()
    {
        // Add top and bottom borders
        for (int x = 0; x < mapWidth; x++)
        {
            tilemap.SetTile(new Vector3Int(x, 0, 0), wallTile);
            tilemap.SetTile(new Vector3Int(x, mapHeight - 1, 0), wallTile);
        }

        // Add left and right borders
        for (int y = 0; y < mapHeight; y++)
        {
            tilemap.SetTile(new Vector3Int(0, y, 0), wallTile);
            tilemap.SetTile(new Vector3Int(mapWidth - 1, y, 0), wallTile);
        }
    }

    private void InstantiateStartAndEnd()
    {
        // Calculate the middle position of the first chunk (start)
        Vector3 startPosition = new Vector3((chunkSize / 2), (chunkSize / 2), 0);
        GameObject startPoint = Instantiate(start, startPosition, Quaternion.identity);
        startPoint.name = start.name;

        // Calculate the middle position of the last chunk (end)
        Vector3 endPosition = new Vector3(mapWidth - (chunkSize / 2) - 1, mapHeight - (chunkSize / 2) - 1, 0);
        GameObject endPoint = Instantiate(end, endPosition, Quaternion.identity);
        endPoint.name = end.name;
    }

    private void Awake()
    {
        GenerateMap();
    }
}
