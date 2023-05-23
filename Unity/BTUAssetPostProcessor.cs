using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

public class BTUAssetPostProcessor : AssetPostprocessor {
    private void OnPostprocessGameObjectWithUserProperties(GameObject gameObject, string[] propNames, object[] values) {
        BTU newFroggy = gameObject.AddComponent<BTU>();
        newFroggy.SetProperties(propNames, values);
    }
}
