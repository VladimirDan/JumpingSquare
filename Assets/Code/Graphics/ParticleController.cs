using UnityEngine;

namespace Code.Graphics
{
    public class ParticleController : MonoBehaviour
    {
        public ParticleSystem particleSystem;

        void Start()
        {
            // Автоматический запуск при старте
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
        }

        public void OnButtonPress()
        {
            // Запуск частиц при нажатии кнопки
            particleSystem.Play();
        }
    }
}