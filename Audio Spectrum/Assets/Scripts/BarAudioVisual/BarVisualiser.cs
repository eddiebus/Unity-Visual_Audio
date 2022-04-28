using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarVisualiser : MonoBehaviour
{
    public Song[] songs;
    public int songIndex = 0;
    public GameObject TitleDisplay;
    public GameObject DisplaySprite;


    [Range(0,1)]
    public float volume = 1;
    [Range(0,3)]
    public float spacing = 0;
    public GameObject BarChild;
    public GameObject[] Children = new GameObject[1024];
    public AudioSource audioSource;
    public float[] spectrum = new float[256];
    public float maxHeight = 3;
    public float ChildWidth;
    public Color colour;

    private int barCount = 20;
    
    private void UpdateDisplaySprite()
    {
        if (DisplaySprite)
        {
            FadeInSprite fadeSprite = DisplaySprite.GetComponent<FadeInSprite>();
            SpriteRenderer spriteRenderer = DisplaySprite.GetComponent<SpriteRenderer>();
            if (fadeSprite && spriteRenderer)
            {
                spriteRenderer.sprite = songs[songIndex].DisplaySprite;
            }
        }
    }
    void SkipSong(int Steps)
    {
        songIndex += Steps;
        if (songIndex >= songs.Length)
        {
            songIndex = 0;
        }
        else if (songIndex < 0 )
        {
            songIndex = songs.Length - 1;
        }

        audioSource.Stop();
        audioSource.clip = songs[songIndex].clip;
        audioSource.Play();

        if (DisplaySprite)
        {
            FadeInSprite fadeSprite = DisplaySprite.GetComponent<FadeInSprite>();
            SpriteRenderer spriteRenderer = DisplaySprite.GetComponent<SpriteRenderer>();
            if (fadeSprite && spriteRenderer)
            {
                fadeSprite.SetOpacity(0);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        songs = GetComponents<Song>();
        if (songs.Length == 0)
        {
            Debug.Log($"This BarVisualiser script in object {this.name} has no songs to work with");
            return;
        }

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = songs[0].clip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
      

        //Create the child bars
        for (int i = 0; i < barCount; i++)
        {
            if (BarChild)
            {
                GameObject newObject = Object.Instantiate(BarChild, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                newObject.transform.SetParent(transform);
                Children[i] = newObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (songs.Length == 0)
        {
            return;
        }

        audioSource.volume = volume;
        spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 1, FFTWindow.BlackmanHarris);

        for (int i = 0; i < barCount; i++ )
        {
            BarVisualiserChild scriptRef = Children[i].GetComponent<BarVisualiserChild>();
            ChildWidth = scriptRef.InitWidth;
            scriptRef.SetColour(colour);
            scriptRef.SetMaxHeight(maxHeight);

            int relativeI = i - barCount / 2;
            Children[i].transform.position = transform.position;
            float realSpaceing = (spacing * ChildWidth) * relativeI;
            Children[i].transform.position += new Vector3((ChildWidth * relativeI) + realSpaceing , 0, 0);

            float percentalI = (float)i / barCount;
            percentalI = percentalI * spectrum.Length;
            float newValue = spectrum[(int)percentalI];
            if (newValue > 0)
            {
                scriptRef.SetValue(newValue * 500 );
            }
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            SkipSong(-1);
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            SkipSong(1);
        }

        if (TitleDisplay)
        {
            UnityEngine.UI.Text textComp = TitleDisplay.GetComponent<UnityEngine.UI.Text>();
            textComp.text = songs[songIndex].SongName;
        }

        UpdateDisplaySprite();

        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            Debug.Log("!");
            Application.Quit();
        }
    }
}
