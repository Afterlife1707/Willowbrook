#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class EditorGPUInstancing
{
    [MenuItem("CustomEditor/Enable GPU Instancing")]
    static void EnableGPUInstancing()
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
        Debug.Log(gameObjects.Length + " is the number of objects");
        int count = 0;
        foreach (var obj in gameObjects)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

            foreach (var renderer in renderers)
            {
                Material material = renderer.sharedMaterial;

                // Enable GPU instancing if the material supports it
                if (material != null && material.enableInstancing == false)
                {
                    material.enableInstancing = true;
                    count++;
                }
            }
        }
        Debug.Log("number of enabled objs :" + count);
    }
}

#endif