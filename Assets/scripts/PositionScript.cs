using UnityEngine;

public class PositionScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Notifier le script CheckersGame lors d'un clic sur une position
        CheckersGame.Instance.OnPositionSelected(this.gameObject);
    }
}
