using System.Collections.Generic;
using UnityEngine;

public static class GameHelper
{
    public static List<T> ShuffleList<T>(List<T> list)
    {
        List<T> shuffledList = new List<T>(list); // Copy the original list
        int n = shuffledList.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1); // Get a random index
            T value = shuffledList[k]; // Swap elements
            shuffledList[k] = shuffledList[n];
            shuffledList[n] = value;
        }
        return shuffledList;
    }

    public static GameObject GetChildWithTag(this GameObject parent, string tag)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null; // No child with the specified tag found
    }

    public static T GetComponentInAllChildren<T>(this Transform parent) where T : Component
    {
        // Check if the parent itself has the component
        T comp = parent.GetComponent<T>();
        if (comp != null)
        {
            return comp;
        }

        // Iterate through all children and their descendants
        foreach (Transform child in parent)
        {
            // Recursively search for the component in the child
            comp = child.transform.GetComponentInAllChildren<T>();
            if (comp != null)
            {
                return comp;
            }
        }

        // If no component found in children, return null
        return null;
    }

    public static List<T> GetComponentsInAllChildren<T>(this Transform parent) where T : Component
    {
        // Initialize an empty list to store the components
        List<T> components = new List<T>();

        // Check if the parent itself has the component
        T parentComponent = parent.GetComponent<T>();
        if (parentComponent != null)
        {
            components.Add(parentComponent);
        }

        // Iterate through all children and their descendants
        foreach (Transform child in parent)
        {
            // Recursively search for the component in the child
            List<T> childComponents = child.transform.GetComponentsInAllChildren<T>();
            components.AddRange(childComponents);
        }

        return components;
    }

    public static float MapValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        // Calculate the width of each range
        float inputSpan = inputMax - inputMin;
        float outputSpan = outputMax - outputMin;

        // Normalize the input value to a 0-1 range
        float normalizedValue = (value - inputMin) / inputSpan;

        // Map the normalized value to the output range
        float mappedValue = outputMin + (normalizedValue * outputSpan);

        return mappedValue;
    }

}
