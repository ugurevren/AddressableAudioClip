using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
{
    public AssetReferenceAudioClip(string guid) : base(guid) { }
}
public class AudioClipLoader : MonoBehaviour
{
    public List<AssetReferenceAudioClip> assetReferences;
    private AssetReferenceAudioClip _loadedSong;
    
    // Singleton
    public static AudioClipLoader instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }
    
    private void OnSceneChanged(Scene current, Scene next)
    {
        UnloadSong();
    }

    public async Task<AudioClip> CallSong(int index)
    {
        var handle = assetReferences[index].LoadAssetAsync<AudioClip>();
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadedSong= assetReferences[index];
            return handle.Result;
        }
        else
        {
            Debug.LogError("Song Load Failed");
            return null;
        }
    }
   
    public void UnloadSong()
    {
        if(_loadedSong == null)
            return;
        
        _loadedSong.ReleaseAsset();
        _loadedSong = null;
    }
    
    public void ShuffleSongs()
    {
        // Null check
        if (assetReferences == null || assetReferences.Count == 0)
            return;
        // Fisher-Yates Shuffle
        for (int i = 0; i < assetReferences.Count; i++)
        {
            var temp = assetReferences[i];
            var randomIndex = UnityEngine.Random.Range(i, assetReferences.Count);
            assetReferences[i] = assetReferences[randomIndex];
            assetReferences[randomIndex] = temp;
        }
    }
    
}
