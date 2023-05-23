using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class BTU : MonoBehaviour {
    public string[] propNames;
    public object[] propValues;
    [SerializeReference]
    public List<BTUPropOperator> operators = new List<BTUPropOperator>();

    public void SetProperties(string[] pNames, object[] pValues) {
        propNames = pNames;
        propValues = pValues;

        AddOperators();
    }

    private void AddOperators() {
        for (int i = 0; i < propNames.Length; i++) {
            switch (propNames[i]) {
                case "Prefab":
                    BTUPropPrefabOperator tempOperator = new BTUPropPrefabOperator();
                    tempOperator.fj_prop_name = "Prefab";
                    tempOperator.prefab = (GameObject) AssetDatabase.LoadAssetAtPath(GetRelativePath(propValues[i].ToString()), typeof(GameObject));
                    operators.Add(tempOperator);
                    break;
            }
        }
    }

    public static string GetRelativePath(string absolutePath) {
        int index = absolutePath.IndexOf("Assets\\");
        return absolutePath.Substring(index);
    }
}
