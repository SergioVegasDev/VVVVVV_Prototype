using UnityEngine;

public class ChangeSpriteDirectionBehavior : MonoBehaviour
{
  public void ChangeSpriteDirection(float velocityX)
  {
        if (velocityX < 0 ) { transform.localScale = new Vector3(1, 1, 1); }
        if (velocityX > 0) { transform.localScale = new Vector3(-1, 1, 1); }
   }
    public void ChangeSpriteDirectionWithChangeGravity(float velocityX, bool gravityFlipped)
    {
        if (velocityX < 0) { transform.localScale = new Vector3(1, 1, 1); }
        if (velocityX > 0) { transform.localScale = new Vector3(-1, 1, 1); }
        if (velocityX < 0 && gravityFlipped) { transform.localScale = new Vector3(-1, 1, 1); }
        if (velocityX > 0 && gravityFlipped) { transform.localScale = new Vector3(1, 1, 1); }
    }
}
