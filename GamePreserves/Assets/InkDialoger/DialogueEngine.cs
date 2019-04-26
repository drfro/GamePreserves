using System;
using System.Collections.Generic;
using System.Linq;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Canvas))]
public class DialogueEngine : MonoBehaviour
{
    public TextAsset stuff;

    private float timeToFinish;
    private float secondsPerCharacter;
    public float charactersPerSecond = 5;
    public speaker defaultSpeaker;
    public List<speaker> speakers;
    public KeyCode continueKey = KeyCode.Space;
    private Dictionary<string, speaker> lookupPeeps;
    private Story storyPlayer;
    public TextMeshProUGUI textTarget;
    public Transform ButtonPrefab;
    public Transform buttonLocation;
    public float buttonDisplacement;
    public Camera profileCamera;
    public Animator TextAnimator;
    public string textAnimBoolTriggerShow;

    private AudioSource audioPlayer;

    public bool dontDrawChoices;
    public bool defaultHasPortrait;

    private string displayText;
    private string wholeText;
    private float progress;
    private bool readyToAdvance;
    private bool choicesDrawn;
    private List<GameObject> currentChoices;
//    private TMP_FontAsset defaultFont;
    private TextMeshProUGUI tmpText;

    public string defaultKnot;

    private bool advance;

    private string currentSpeaker;
    private int charsPerTrigger = 10;
    private int lastTriggeredCount = 0;

    public delegate void TagCallback(List<string> tags);
    private TagCallback fireTagCallback;

    // Use this for initialization
    void Start () {
        lookupPeeps = new Dictionary<string, speaker>();
	    foreach (var speaker in speakers)
	    {
	        lookupPeeps[speaker.speakerName] = speaker;
	    }
	    lookupPeeps["default"] = defaultSpeaker;


        storyPlayer = new Story(stuff.text);
	    progress = 0f;
	    readyToAdvance = false;
	    advance = true;
	    textTarget.text = "";
        currentChoices = new List<GameObject>();

	    if (TextAnimator == null)
	    {
	        TextAnimator = this.GetComponent<Animator>();
	    }
	    currentSpeaker = "default";

	    tmpText = GetComponentInChildren<TextMeshProUGUI>();
        defaultSpeaker.speakerFont = tmpText.font;


        if (!String.IsNullOrEmpty(defaultKnot))
	    {
            storyPlayer.ChoosePathString(defaultKnot);
	    }

	    audioPlayer = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update ()
    {

        secondsPerCharacter = 1 / charactersPerSecond;
	    if (stuff != null)
	    {
	        if (advance)
	        {
	            if (storyPlayer.canContinue)
	            {
	                wholeText = storyPlayer.Continue();
	                SetPortrait();
	                if (TextAnimator != null) TextAnimator.SetBool(textAnimBoolTriggerShow, true);
	            }

	            advance = false;
	            progress = 0;
	            choicesDrawn = false;

	            if (storyPlayer.currentTags.Contains("end"))
	            {
                    TextAnimator.SetBool(textAnimBoolTriggerShow, false);
	                lookupPeeps[currentSpeaker].speakerAnimator?.SetBool(lookupPeeps[currentSpeaker].portraitAnimBoolTriggerShow, false);
	            }

            }


            if (readyToAdvance && storyPlayer.currentChoices != null && storyPlayer.currentChoices.Count > 0)
	        {
	            if (!choicesDrawn)
	            {
	                int i = 0;
	                var basePos = buttonLocation.transform.position;

                    if(!dontDrawChoices)
                    {
                        clearChoices();
                        foreach (var currentChoice in storyPlayer.currentChoices)
                        {
                            var thing = Instantiate(ButtonPrefab, this.gameObject.transform);
                            var closureCount = i;
                            thing.gameObject.GetComponentInChildren<Text>().text = currentChoice.text;
                            thing.gameObject.GetComponent<Button>().onClick.AddListener(delegate {chosenChoiceAction(closureCount);});
                            thing.transform.position = new Vector3(basePos.x, basePos.y + i * buttonDisplacement, basePos.z);
                            currentChoices.Add(thing.gameObject);
                            i++;
                        }
                    }

	                choicesDrawn = true;
	            }

	        }
            else if (readyToAdvance && Input.GetKeyUp(continueKey))
	        {
	            advance = true;
	        }

	        if (storyPlayer.currentChoices != null && storyPlayer.currentChoices.Count == 0 && currentChoices.Count != 0)
	        {
                clearChoices();
	        }

            var charCount = wholeText.Length;
	        timeToFinish = secondsPerCharacter * charCount;
	        var lengthOfDisplayedText = Mathf.CeilToInt( Mathf.Lerp(0, charCount - 1,  Mathf.Clamp(progress / timeToFinish, 0, 1)));
	        FireTypingTriggers(lengthOfDisplayedText);
            if(!String.IsNullOrEmpty(wholeText))
            {
                displayText = wholeText.Substring(0, lengthOfDisplayedText);
                textTarget.text = displayText;
            }

	        if (progress > timeToFinish)
	        {
	            readyToAdvance = true;
	        }
	        else
	        {
	            progress += Time.deltaTime;
            }
        }
    }

    private void FireTypingTriggers(int length)
    {
        if (defaultHasPortrait && currentSpeaker == "")
        {
            currentSpeaker = "default";
        }

        int triggerIndex = 1;

            if (length % charsPerTrigger == 0 && length != lastTriggeredCount)
            {

                if(currentSpeaker != "" &&
                   lookupPeeps.ContainsKey(currentSpeaker) &&
                   lookupPeeps[currentSpeaker].babelTriggers.Count >= 1 &&
                   lookupPeeps[currentSpeaker].speakerAnimator != null)
                {

                    triggerIndex = Mathf.CeilToInt(Random.value * lookupPeeps[currentSpeaker].babelTriggers.Count) -1;
                    Debug.Log(lookupPeeps[currentSpeaker].babelTriggers[triggerIndex]);
                    lookupPeeps[currentSpeaker].speakerAnimator.SetTrigger(lookupPeeps[currentSpeaker].babelTriggers[triggerIndex]);

                }

                if (lookupPeeps.ContainsKey(currentSpeaker) &&
                    lookupPeeps[currentSpeaker].speakNoise != null
                    && audioPlayer != null)
                {
                    audioPlayer.PlayOneShot(lookupPeeps[currentSpeaker].speakNoise, .4f);
                }

                lastTriggeredCount = length;
            }
        
    }

    public void subscribeToTagChanges(TagCallback callback)
    {
        if (fireTagCallback != null)
        {
            fireTagCallback = callback;
            return;
        }

        fireTagCallback += callback;
    }

    private void SetPortrait()
    {
        var hasPortrait = false;
        foreach (var speakerIndex in speakers)
        {
            bool speakerChange = false;
            foreach (var tag in storyPlayer.currentTags)
            {
                if (speakerIndex.speakerName == tag)
                {
                    currentSpeaker = tag;
                    speakerChange = true;
                    break;
                }
                
            }
        
            if (speakerChange)
            {
                if (profileCamera != null)
                {
                    profileCamera.transform.position = lookupPeeps[speakerIndex.speakerName].camAngle.position;
                    profileCamera.transform.rotation = lookupPeeps[speakerIndex.speakerName].camAngle.rotation;
                }
                currentSpeaker = speakerIndex.speakerName;

                if (speakerIndex.speakerFont != null)
                {
                    tmpText.font = speakerIndex.speakerFont;
                }
                else
                {
                    tmpText.font = defaultSpeaker.speakerFont;
                }
            }
            else
            {
                if (profileCamera != null)
                {
                    profileCamera.transform.position = defaultSpeaker.camAngle.position;
                    profileCamera.transform.rotation = defaultSpeaker.camAngle.rotation;
                }
                tmpText.font = defaultSpeaker.speakerFont;

            }

            var lookupPeep = lookupPeeps[currentSpeaker];
            hasPortrait = lookupPeep.forceSpeakerViewer || profileCamera != null;
            var isCurrent = speakerIndex.speakerName == currentSpeaker;
            if (lookupPeep.speakerAnimator != null)
            {
                lookupPeep.speakerAnimator.SetBool(lookupPeep.portraitAnimBoolTriggerShow, hasPortrait && isCurrent);
            }
            
        }

        if (!hasPortrait)
        {
            tmpText.font = defaultSpeaker.speakerFont;
        }
    }



    private void clearChoices()
    {
        foreach (var currentChoice in currentChoices)
        {
            //TODO: let these things destroy themselves
            Destroy(currentChoice);
        }

        currentChoices = new List<GameObject>();
    }

    public void chosenChoiceAction(int choiceNum)
    {
        Debug.Log("chose choice " + choiceNum);
        storyPlayer.ChooseChoiceIndex(choiceNum);
        advance = true;
        choicesDrawn = false;
    }
}

[Serializable]
public class speaker
{
    public string speakerName;
    public Transform camAngle;
    public Animator speakerAnimator;
    public List<string> babelTriggers;
    public string portraitAnimBoolTriggerShow;
    public TMP_FontAsset speakerFont;
    public bool forceSpeakerViewer;
    public AudioClip speakNoise;
}
