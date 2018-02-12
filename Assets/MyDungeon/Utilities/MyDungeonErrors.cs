namespace MyDungeon.Utilities
{
    public static class MyDungeonErrors
    {
        internal static void DungeonManagerNotFound()
        {
            UnityEngine.Debug.LogError("You must include a GameObject with the DungeonManager tag in the scene with any MovingDungeonObject including the PlayerDungeon");
        }

        internal static void RigidBody2DOrBoxCollider2DNotFound(string objectName)
        {
            UnityEngine.Debug.LogError(objectName + " must contain a Rigidbody2D component and a BoxCollider2D component");
        }

        internal static void GridGeneratorOnDungeonManagerNotFound(string objectName)
        {
            UnityEngine.Debug.LogError(objectName + " must contain a GridGenerator component");
        }

        internal static void CreatureControllerNotFound(string objectName)
        {
            UnityEngine.Debug.LogError(objectName + " must contain a CreatureController component");
        }
    }
}
