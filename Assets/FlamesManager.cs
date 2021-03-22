using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlameContainer
{
    public GameObject flame;
    public float activateAt = 0.25f;
}

public class FlamesManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    protected FlameContainer[] flames;

    public void UpdateWithLife(float ratio) {

        foreach (FlameContainer flameContainer in flames)
        {
            if (ratio <= flameContainer.activateAt) {
                if (!flameContainer.flame.activeInHierarchy) flameContainer.flame.SetActive(true);
            } else {
                if (flameContainer.flame.activeInHierarchy) flameContainer.flame.SetActive(false);
            }
        }
    }

    public void ReplaceFlames(Vector3[] placements) {
        if (placements.Length != flames.Length)
            throw new System.Exception("Placement length need to match flames length");
        
        for (int i = 0; i != placements.Length; i++)
            flames[i].flame.transform.localPosition = placements[i];
    }
}
