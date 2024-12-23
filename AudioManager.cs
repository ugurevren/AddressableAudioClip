using UnityEngine;
public class AudioManager : MonoBehaviour
{
    #region Variables&Instances
    
    private AudioSource _audioSourceMusic;
    private int _currentMusicIndex = 0;
    private int _musicCount = 0;
    public static AudioManager instance;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        _audioSourceMusic = transform.GetComponentInChildren<AudioSource>();
        _audioSourceMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
    }
    
    private void Start()
    {
        _musicCount = AudioClipLoader.instance.assetReferences.Count;
    }

    #endregion

    #region MusicControls
    
    public void PlayMusic()
    {
        _audioSourceMusic.Play();
    }

    public void ShuffleMusic()
    {
        AudioClipLoader.instance.ShuffleSongs();
    }

    public void PauseMusic()
    {
        _audioSourceMusic.Pause();
    }

    public void StopMusic()
    {
        _audioSourceMusic.Stop();
    }

    public void ResumeMusic()
    {
        _audioSourceMusic.UnPause();
    }

    public async void PlayFirstMusic()
    {
        _currentMusicIndex = 0;
        _audioSourceMusic.clip = await AudioClipLoader.instance.CallSong(_currentMusicIndex);
        PlayMusic();
    }
    public async void PlayNextMusic()
    {
        StopMusic();
        AudioClipLoader.instance.UnloadSong();
        if (_currentMusicIndex == _musicCount - 1)
            _currentMusicIndex = 0;
        else
            _currentMusicIndex++;
        _audioSourceMusic.clip = await AudioClipLoader.instance.CallSong(_currentMusicIndex);
        PlayMusic();
    }

    public async void PlayPreviousMusic()
    {
        StopMusic();
        AudioClipLoader.instance.UnloadSong();
        if (_currentMusicIndex == 0)
            _currentMusicIndex = _musicCount - 1;
        else
            _currentMusicIndex--;
        _audioSourceMusic.clip = await AudioClipLoader.instance.CallSong(_currentMusicIndex);
        PlayMusic();
    }

    #endregion
}
