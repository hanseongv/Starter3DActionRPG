using UnityEngine;
using System.Collections.Generic;

public class CameraWallFade : MonoBehaviour
{
    public Transform player;
    public LayerMask wallLayer; // 벽에 해당하는 레이어
    public Material transparentMaterial; // 투명 머티리얼

    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
    private List<Renderer> currentTransparent = new List<Renderer>();

    void LateUpdate()
    {
        // 1. 이전 프레임에서 투명했던 오브젝트 복원
        foreach (var rend in currentTransparent)
        {
            if (rend != null && originalMaterials.ContainsKey(rend))
            {
                rend.materials = originalMaterials[rend];
            }
        }
        currentTransparent.Clear();
        originalMaterials.Clear();

        // 2. 플레이어 방향으로 Raycast
        // Vector3 direction = player.position - transform.position;
        Vector3 from = transform.position;
        Vector3 to = player.position + Vector3.up * 0.5f; // 허리~가슴쯤
        Vector3 direction = to - from;
        float distance = direction.magnitude;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, wallLayer);

        foreach (var hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                // 원래 머티리얼 저장
                if (!originalMaterials.ContainsKey(rend))
                {
                    originalMaterials[rend] = rend.materials;
                }

                // 머티리얼 교체
                Material[] newMats = new Material[rend.materials.Length];
                for (int i = 0; i < newMats.Length; i++)
                {
                    newMats[i] = transparentMaterial;
                }
                rend.materials = newMats;

                currentTransparent.Add(rend);
            }
        }
    }
}
