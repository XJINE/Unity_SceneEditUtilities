#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace SceneEditUtilities {
public static class SceneEditUtility
{
    // NOTE:
    // Most functions need EditorApplication.RepaintHierarchyWindow()
    // to show latest status in Hierarchy.

    public static Vector3 WorldToSceneViewPoint(Vector3 position)
    {
        // WARNING:
        // This still have a MAGIC NUMBER.
        // https://discussions.unity.com/t/mouse-position-in-scene-view/540144

        var sceneView = SceneView.currentDrawingSceneView;
        var style     = (GUIStyle) "GV Gizmo DropDown";
        var ribbon    = style.CalcSize(sceneView.titleContent);
            ribbon.y += 8;

        var sceneViewSize = sceneView.position.size;
            sceneViewSize.y -= ribbon.y;

        var viewPortPoint   = sceneView.camera.WorldToViewportPoint(position);
            viewPortPoint.y = 1 - viewPortPoint.y;

        return viewPortPoint * sceneViewSize;
    }

    public static void DisablePickingAll(bool includeInactive = true)
    {
        var gameObjects = FindObjectsOfType<Transform>(includeInactive).Select(t => t.gameObject).ToArray();
        SceneVisibilityManager.instance.DisablePicking(gameObjects, false);
        EditorApplication.RepaintHierarchyWindow();
    }

    public static void EnablePickingAll(bool includeInactive = true)
    {
        var gameObjects = FindObjectsOfType<Transform>(includeInactive).Select(t => t.gameObject).ToArray();
        SceneVisibilityManager.instance.EnablePicking(gameObjects, false);
        EditorApplication.RepaintHierarchyWindow();
    }

    public static void DisablePickingByType<T>(bool includeInactive = true) where T : Component
    {
        var gameObjects = FindObjectsOfType<T>(includeInactive).Select(t => t.gameObject).ToArray();
        SceneVisibilityManager.instance.DisablePicking(gameObjects, false);
        EditorApplication.RepaintHierarchyWindow();
    }

    public static void EnablePickingByType<T>(bool includeInactive = true) where T : Component
    {
        var gameObjects = FindObjectsOfType<T>(includeInactive).Select(t => t.gameObject).ToArray();
        SceneVisibilityManager.instance.EnablePicking(gameObjects, false);
        EditorApplication.RepaintHierarchyWindow();
    }

    public static void TogglePickingByType<T>(bool includeInactive = true) where T : Component
    {
        var gameObjects = FindObjectsOfType<T>(includeInactive).Select(t => t.gameObject).ToArray();
        foreach (var gameObject in gameObjects)
        {
            SceneVisibilityManager.instance.TogglePicking(gameObject, false);
        }
        EditorApplication.RepaintHierarchyWindow();
    }

    public static void HideByType<T>(bool includeInactive = true) where T : Component
    {
        var gameObjects = FindObjectsOfType<T>(includeInactive).Select(t => t.gameObject).ToArray();
        SceneVisibilityManager.instance.Hide(gameObjects, false);
        EditorApplication.RepaintHierarchyWindow();
    }

    public static void ShowByType<T>(bool includeInactive = true) where T : Component
    {
        var gameObjects = FindObjectsOfType<T>(includeInactive).Select(t => t.gameObject).ToArray();
        SceneVisibilityManager.instance.Show(gameObjects, false);
        EditorApplication.RepaintHierarchyWindow();
    }

    public static void ToggleVisibilityByType<T>(bool includeInactive = true) where T : Component
    {
        var gameObjects = FindObjectsOfType<T>(includeInactive).Select(t => t.gameObject).ToArray();
        foreach (var gameObject in gameObjects)
        {
            SceneVisibilityManager.instance.ToggleVisibility(gameObject, false);
        }
        EditorApplication.RepaintHierarchyWindow();
    }

    public static T[] FindObjectsOfType<T>(bool includeInactive = true) where T : Object
    {
        if (!includeInactive)
        {
            return Object.FindObjectsOfType<T>();
        }

        var typeObjects = new List<T>();
        var gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (var gameObject in gameObjects)
        {
            typeObjects.AddRange(gameObject.GetComponentsInChildren<T>(true));
        }

        return typeObjects.ToArray();
    }

    public static bool IsSelected(this GameObject gameObject)
    {
        return Selection.gameObjects.Contains(gameObject);
    }

    public static bool IsOnlySelected(this GameObject gameObject)
    {
        // NOTE:
        // Sometimes selected a parent object of this.
        // So needs to check the instance.
        return Selection.gameObjects.Length == 1 && Selection.gameObjects[0] == gameObject;
    }

    public static T[] GetSelectedObjectsOfType<T>() where T : Component
    {
        return Selection.gameObjects.Select(gameObject => gameObject.GetComponentInChildren<T>())
                                    .Where(component => component != null).ToArray();
    }
}}

#endif