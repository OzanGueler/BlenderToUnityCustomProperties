using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[BTUProp("Prefab")]
[Serializable]
public class BTUPropPrefabOperator : BTUPropOperator {
    public GameObject prefab;

    public BTUPropPrefabOperator() {
        fj_button_description = "Replace Prefabs";
    }

    public override void Init() {
        prefab = (GameObject) AssetDatabase.LoadAssetAtPath(BTUHelper.GetRelativePath(fj_prop_value.ToString()), typeof(GameObject));
    }

    public override void Execute(GameObject target) {
        GameObject temp = (GameObject) PrefabUtility.InstantiatePrefab(prefab);
        temp.transform.parent = target.transform.parent;
        temp.transform.position = target.transform.position;
        temp.transform.rotation = target.transform.rotation;
        MonoBehaviour.DestroyImmediate(target.gameObject);
    }
}
