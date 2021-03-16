using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaterManager))]
class WaterManagerEditor : Editor {

    public void Generate(GameObject basePrefab, Transform transform, GameObject waterPrefab, uint xSize, uint zSize)
    {
        transform.ClearChilds();
        Vector3 size = waterPrefab.GetComponent<Renderer>().bounds.size;
        size.x -= 0.05f;
        size.z -= 0.05f;
        Debug.Log("size" + size.ToString());
        float xStartPos = -(float)xSize / 2.0f * size.x + size.x / 2.0f;
        float zStartPos = -(float)zSize / 2.0f * size.z + size.z / 2.0f;
        for (int x = 0; x != xSize; x++) {
            Debug.Log("x" + x);
            for (int z = 0; z != zSize; z++) {
            Debug.Log("z" + z);
                Vector3 pos = new Vector3(xStartPos + x * size.x, 0, zStartPos + z * size.z);
                var water = PrefabUtility.InstantiatePrefab(waterPrefab, transform) as GameObject;
                water.transform.position = pos;
                //  Instantiate(waterPrefab, pos, Quaternion.identity, transform);
            }
        }
        EditorUtility.SetDirty(basePrefab);
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if(GUILayout.Button("Generate")) {
            WaterManager myScript = (WaterManager)target;

            Generate(myScript.GameplayBasePrefab, myScript.transform, myScript.waterPrefab, myScript.xSize, myScript.zSize);
        }
    }
}