using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Menu.Buttons
{
    public class QuitButton : MonoBehaviour
    {
        private Button _quitButton;
        
        private void Awake()
        {
            _quitButton = GetComponent<Button>();
            _quitButton.onClick.AddListener(QuitApplication);
        }
        private void OnDestroy()
        {
            if (_quitButton != null)
            {
                _quitButton.onClick.RemoveListener(QuitApplication);
            }
        }
        
        private void QuitApplication()
        {
            Debug.Log("QuitApplication called.");
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}