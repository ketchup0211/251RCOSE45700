using UnityEngine;
using UnityEngine.Tilemaps;

public class TileShifter : MonoBehaviour
{
    public Tilemap tilemap;                  // 타일맵 참조
    public Transform player;                 // 플레이어 위치 참조
    public Vector3Int targetCell;            // 움직일 타일의 셀 좌표
    public float triggerDistance = 1f;       // 반응 거리 (1블럭)
    private bool hasShifted = false;

    void Update()
    {
        if (hasShifted) return;

        // 타일 좌표의 세계 위치 계산
        Vector3 worldPos = tilemap.CellToWorld(targetCell) + tilemap.cellSize / 2;

        // 거리 측정
        float distance = Vector2.Distance(player.position, worldPos);

        if (distance <= triggerDistance)
        {
            ShiftTileRight();
            hasShifted = true;
        }
    }

    void ShiftTileRight()
    {
        TileBase tile = tilemap.GetTile(targetCell);
        if (tile != null)
        {
            Vector3Int newCell = targetCell + new Vector3Int(1, 0, 0); // 우측으로 1칸 이동
            tilemap.SetTile(newCell, tile);      // 새 위치에 타일 세팅
            tilemap.SetTile(targetCell, null);   // 기존 위치 타일 제거
        }
    }
}
