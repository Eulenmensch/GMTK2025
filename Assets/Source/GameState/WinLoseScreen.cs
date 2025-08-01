using UnityEngine;

namespace Source.GameState
{
    public class WinLoseScreen : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;
        
        public void ShowEndScreen(bool won)
        {
            if (won)
                winScreen.SetActive(true);
            else
                loseScreen.SetActive(true);
        }
    }
}