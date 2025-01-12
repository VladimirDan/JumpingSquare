using UnityEngine;

namespace Code.Gameplay.Level
{
    [CreateAssetMenu(fileName = "LevelPrefabs", menuName = "ScriptableObjects/LevelPrefabs", order = 1)]
    public class LevelPrefabs : ScriptableObject
    {
        public GameObject[] levelPrefabs;

        public GameObject GetLevelPrefab(int index)
        {
            if (index >= 0 && index < levelPrefabs.Length)
            {
                return levelPrefabs[index];
            }

            Debug.LogError($"Индекс {index} вне диапазона. Проверьте массив префабов.");
            return null;
        }
    }
}