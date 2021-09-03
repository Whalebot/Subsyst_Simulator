using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;


public class CleanupMissingScripts : MonoBehaviour
{
    public Component c;
    [Button]
    public void RemoveComponents()
    {
        Component[] components = GetComponentsInChildren(c.GetType(), true);

        foreach (var c in components)
        {
            DestroyImmediate(c);
        }
    }

    public static bool IsNull(Component c) {

        return c == null;
    }
}
