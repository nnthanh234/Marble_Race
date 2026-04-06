using System.Collections;
using UnityEngine;

public class ImageToGO : MonoBehaviour
{
    public Texture2D mapImage;    // Kéo bức ảnh vào đây
    public GameObject cubePrefab; // Prefab bạn muốn tạo (ví dụ: Cube)
    public Transform parentTransform; // Transform cha để tổ chức các GameObject

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        // Duyệt qua chiều rộng (width) và chiều cao (height) của ảnh
        for (int x = 0; x < mapImage.width; x++)
        {
            for (int y = 0; y < mapImage.height; y++)
            {
                GenerateTile(x, y);

            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = mapImage.GetPixel(x, y);

        // Nếu pixel là màu đen (Alpha = 0 hoặc Color.black), ta có thể bỏ qua không spawn
        if (pixelColor == Color.black) return;

        // Tính toán vị trí dựa trên tọa độ pixel
        Vector3 position = new Vector3(x, y, 0);

        // Tạo GameObject tại vị trí đó
        GameObject newTile = Instantiate(cubePrefab, position, Quaternion.identity, parentTransform);

        //// (Tùy chọn) Đổi màu GameObject theo màu pixel
        //newTile.GetComponent<Renderer>().material.color = pixelColor;
    }
}
