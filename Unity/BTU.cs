using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;

/*
 * This component is added during the importing process
 */
public class BTU : MonoBehaviour {
    [HideInInspector]
    public string[] propNames;
    [HideInInspector]
    public object[] propValues;
    [SerializeReference]
    [HideInInspector]
    public List<BTUPropOperator> operators = new List<BTUPropOperator>();

    /*
     * Called with all custom property names and values when the asset is imported
     */
    public void SetProperties(string[] pNames, object[] pValues) {
        propNames = pNames;
        propValues = pValues;
    }

    public void Init() {
        AddOperators();
    }

    private void AddOperators() {
        List<Type> tempList;
        for (int i = 0; i < propNames.Length; i++) {
            if (BTUHelper.operators.TryGetValue(propNames[i], out tempList)) {
                foreach (Type t in tempList) {
                    BTUPropOperator newOp = (BTUPropOperator) Activator.CreateInstance(t);
                    newOp.fj_prop_name = propNames[i];
                    newOp.fj_prop_value = propValues[i];
                    newOp.Init();
                    operators.Add(newOp);
                }
            }
        }
    }

    public void ExecuteProp(string propName) {
        foreach (BTUPropOperator op in operators) {
            if (op.fj_prop_name == propName) {
                op.Execute(this.gameObject);
            }
        }
    }
}
