namespace MyDungeon.Utilities
{
    public static class MyDungeonErrors
    {
        internal static void DungeonManagerNotFound()
        {
            if (!UnityEngine.Debug.isDebugBuild) return;
            UnityEngine.Debug.LogError("You must include a GameObject with the DungeonManager tag in the scene with any MovingDungeonObject including the PlayerDungeon");
        }

        internal static void RigidBody2DOrBoxCollider2DNotFound(string objectName)
        {
            if (!UnityEngine.Debug.isDebugBuild) return;
            UnityEngine.Debug.LogError(objectName + " must contain a Rigidbody2D component and a BoxCollider2D component");
        }

        internal static void GridGeneratorOnDungeonManagerNotFound(string objectName)
        {
            if (!UnityEngine.Debug.isDebugBuild) return;
            UnityEngine.Debug.LogError(objectName + " must contain a GridGenerator component");
        }

        internal static void CreatureControllerNotFound(string objectName)
        {
            if (!UnityEngine.Debug.isDebugBuild) return;
            UnityEngine.Debug.LogError(objectName + " must contain a CreatureController component");
        }

        internal static void PlayerDungeonMustBeSpawnedInDungeon()
        {
            if (!UnityEngine.Debug.isDebugBuild) return;
            UnityEngine.Debug.LogError("GameObject with type inherited from PlayerDungeon must be spawned in a dungeon");
        }

        internal static void SaveLoadComponentNotFound(string objectName)
        {
            if (!UnityEngine.Debug.isDebugBuild) return;
            UnityEngine.Debug.LogError(objectName + " must contain a SaveLoad component");
        }
    }
}
