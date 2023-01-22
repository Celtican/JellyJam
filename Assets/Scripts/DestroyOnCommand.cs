using UnityEngine;

public class DestroyOnCommand : MonoBehaviour
{
    // this just exists for animators to call
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}