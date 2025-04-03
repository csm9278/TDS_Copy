using UnityEngine;

public enum SoundsType
{
    GunShot = 0,
    EnemyAttack,
    BrokenBox
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public bool isInit = false;
    public int maxAudios = 10;
    int playIdx;

    AudioSource[] audios;
    AudioSource bgmAudio;
    AudioSource chatAudio;

    public AudioClip[] soundClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("�̹� ���� �Ŵ����� �����մϴ�.");
            Destroy(this.gameObject);
        }
        soundClip = Resources.LoadAll<AudioClip>("Sounds");
        Debug.Log(soundClip.Length);
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
        if (!instance.isInit)
        {
            audios = new AudioSource[maxAudios];

            for (int i = 0; i < maxAudios; i++)
            {
                GameObject obj = new GameObject();
                obj.transform.parent = this.transform;
                audios[i] = obj.AddComponent<AudioSource>();
                audios[i].playOnAwake = false;
            }

            //BGM Player
            GameObject bgmobj = new GameObject();
            bgmobj.name = "BGMPlayer";
            bgmobj.transform.parent = this.transform;
            bgmAudio = bgmobj.AddComponent<AudioSource>();
            bgmAudio.playOnAwake = false;
            bgmAudio.loop = true;

            //ChatPlayer
            GameObject chatobj = new GameObject();
            chatobj.name = "ChatPlayer";
            chatobj.transform.parent = this.transform;
            chatAudio = chatobj.AddComponent<AudioSource>();
            chatAudio.loop = true;

            instance.isInit = true;

        }
    }

    public void PlayBgm(AudioClip clip, float volume = 1.0f)
    {
        bgmAudio.clip = clip;
        bgmAudio.volume = volume;
        bgmAudio.Play();
    }

    public void PlayEffSound(string SoundName, float volume = 1.0f)
    {
        audios[playIdx].clip = Resources.Load(SoundName) as AudioClip;
        audios[playIdx].volume = volume;
        audios[playIdx].Play();
        playIdx++;
        if (playIdx >= maxAudios)
            playIdx = 0;
    }

    public void PlayEffSound(string[] SoundName, float volume = 1.0f)
    {
        int rand = Random.Range(0, SoundName.Length);
        audios[playIdx].clip = Resources.Load(SoundName[rand]) as AudioClip;
        audios[playIdx].volume = volume;
        audios[playIdx].Play();
        playIdx++;
        if (playIdx >= maxAudios)
            playIdx = 0;
    }

    public void PlayEffSound(AudioClip clip, float volume = 1.0f)
    {
        audios[playIdx].clip = clip;
        audios[playIdx].volume = volume;
        audios[playIdx].Play();
        playIdx++;
        if (playIdx >= maxAudios)
            playIdx = 0;
    }

    public void StopBeforeSound()
    {
        if (playIdx == 0)
            audios[audios.Length - 1].Stop();

        audios[playIdx - 1].Stop();
    }
}