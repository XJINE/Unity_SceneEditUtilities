# Unity_SceneEditUtilities

## Importing

You can use Package Manager or import it directly.

```
https://github.com/XJINE/Unity_SceneEditUtilities.git?path=Assets/Packages/SceneEditUtilities
```

## How to Use

`SceneEditUtility` class provides the following functions.

```csharp
public static Vector3 WorldToSceneViewPoint(Vector3 position)
public static void DisablePickingAll(bool includeInactive = true)
public static void EnablePickingAll(bool includeInactive = true)
public static void DisablePickingByType<T>(bool includeInactive = true) where T : Component
public static void EnablePickingByType<T>(bool includeInactive = true) where T : Component
public static void TogglePickingByType<T>(bool includeInactive = true) where T : Component
public static void HideByType<T>(bool includeInactive = true) where T : Component
public static void ShowByType<T>(bool includeInactive = true) where T : Component
public static void ToggleVisibilityByType<T>(bool includeInactive = true) where T : Component
public static T[] FindObjectsOfType<T>(bool includeInactive = true) where T : Object
public static bool IsSelected(this GameObject gameObject)
public static bool IsOnlySelected(this GameObject gameObject)
public static T[] GetSelectedObjectsOfType<T>() where T : Component
```
