using UnityEditor;
using UnityEngine;

namespace Source.Menu.Buttons
{
    public class QuitButton : MonoBehaviour
    {
        private void OnMouseDown()
        {
            QuitApplication();
        }

        private void QuitApplication()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}