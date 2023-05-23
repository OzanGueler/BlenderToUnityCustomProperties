using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BTUAssetPostProcessor : AssetPostprocessor {
    private void OnPostprocessGameObjectWithUserProperties(GameObject gameObject, string[] propNames, object[] values) {
        //BTUHelper.Init(); //We should call this whenever we import a new script to check if it's of type FroggyPropOperator
        BTUHelper.Init();
        BTU newBtu = gameObject.AddComponent<BTU>();
        newBtu.SetProperties(propNames, values);
        newBtu.Init();
    }

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
        BTUHelper.Init();
    }
}
