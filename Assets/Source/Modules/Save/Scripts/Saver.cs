using UnityEngine;

namespace Save
{
    public static class Saver
    {
        public static void Save<T>(string key, T saveData)
        {
            string jsonDataString = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(key, jsonDataString);
        }

        public static T Load<T>(string key) where T : new()
        {
            if (PlayerPrefs.HasKey(key))
            {
                string jsonDataString = PlayerPrefs.GetString(key);

                return JsonUtility.FromJson<T>(jsonDataString);
            }
            else
            {
                return new T();
            }
        }

        public static T Unpack<T>(string jsonDataString) =>
            JsonUtility.FromJson<T>(jsonDataString);
    }
}