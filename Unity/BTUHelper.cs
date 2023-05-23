using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;

public class BTUHelper {
    //Dictionary with <propName, List of all operators that react to this propName> structure
    public static Dictionary<String, List<Type>> operators = new Dictionary<string, List<Type>>();

    /*
     * Gets all Types of type BTUPropOperator in the Assembly (except for the abstract class BTUPropOperator itself)
     * and adds them to the operators Dictionary
     */
    public static void Init() {
        operators = new Dictionary<string, List<Type>>();

        foreach (Type t in Assembly.GetAssembly(typeof(BTUPropOperator)).GetTypes()
            .Where(p => typeof(BTUPropOperator).IsAssignableFrom(p) && p != typeof(BTUPropOperator))) {
            try {
                AddToOperators(t.GetAttribute<BTUPropAttribute>().propName, t);
            } catch (Exception e) {
                Debug.Log(string.Format("Class '{0}' could not be registered because it has no BTUPropAttribute to define which custom property to listen to.", t.GetNameWithGenericArguments()));
            }
        }
        
        /*foreach (KeyValuePair<string, List<Type>> kvp in operators) {
            Debug.Log(string.Format("{0} has {1} functions", kvp.Key, kvp.Value.Count));
        }*/
    }

    /*
     * Adds an element to the dictionary
     * Handles adding to the value of the dict, which is a list
     */
    private static void AddToOperators(String keyName, Type t) {
        List<Type> tempList;

        if (!operators.TryGetValue(keyName, out tempList)) {
            tempList = new List<Type>();
        }

        tempList.Add(t);
        operators[keyName] = tempList;
    }

    public static string GetRelativePath(string absolutePath) {
        int index = absolutePath.IndexOf("Assets\\");
        return absolutePath.Substring(index);
    }
}
