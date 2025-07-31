using UnityEngine;

namespace Source.Utils
{
    public class BaseSingleton<T> : MonoBehaviour where T : BaseSingleton<T>
    {
        /// <summary>
        /// The singleton instance of this type.
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// True, when the singleton should use Unity's don't destroy on load functionality.
        /// </summary>
        protected virtual bool UseDontDestroyOnLoad => false;

        /// <summary>
        /// True when the singleton instance initialized.
        /// </summary>
        protected bool IsValid;

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;

            if (UseDontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            IsValid = true;
        }
    }
}