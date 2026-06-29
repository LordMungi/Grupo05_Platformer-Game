using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    private Bootstrapper() { }

    public static Bootstrapper instance { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ServiceProvider.Instance.AddService<CustomSceneManager>(new GameObject("SceneManager").AddComponent<CustomSceneManager>());
        DontDestroyOnLoad(ServiceProvider.Instance.GetService<CustomSceneManager>());

        ServiceProvider.Instance.GetService<CustomSceneManager>().LoadLevel();
    }
}
