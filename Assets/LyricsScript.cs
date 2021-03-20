using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LyricsScript : MonoBehaviour {

    public KMAudio audio;
    public KMBombInfo bomb;

    public KMSelectable ModuleSelectable;
    public KMSelectable[] buttons;

    private string[] songnames = { "Starstrukk by 3OH3 feat. Katy Perry", "Starstrukk by 3OH3 feat. Katy Perry", "Starstrukk by 3OH3 feat. Katy Perry", "Wonderland by Caravan Palace" };
    private string[][] lyrics = { new string[] { "Cause", "I", "Just", "Set", "Them", "Up", "Just", "Set", "Them", "Up", "Just", "Set", "Them", "Up", "To", "Knock", "Them", "Down", "Just", "Set", "Them", "Up", "Just", "Set", "Them", "Up", "Just", "Set", "Them", "Up", "To", "Knock", "Them", "Down" }, new string[] { "Nice", "Legs", "Daisy", "Dukes", "Makes", "A", "Man", "Go", "", "That's", "The", "Way", "They", "All", "Come", "Through", "Like", "", "Low-cut", "See-through", "Shirts", "That", "Make", "Ya", "", "That's", "The", "Way", "She", "Come", "Through", "Like" }, new string[] { "I", "Think", "I", "Should", "Know", "", "How", "", "To", "Make", "Love", "To", "Something", "Innocent", "Without", "Leaving", "My", "Fingerprints", "Out", "", "Now", "", "L", "O", "V", "E", "Is", "Just", "Another", "Word", "I", "Never", "Learned", "To", "Pronounce" }, new string[] { "I", "Got", "The", "Whip"/**, "Got", "The", "Pitch", "Thought", "I'd", "Keep", "It", "Undercover", "All"*/ } };
    private float[][] waittimes = { new float[] { 0.2f, 0.3f, 0.7f, 0.4f, 0.3f, 0.3f, 0.7f, 0.4f, 0.3f, 0.3f, 0.7f, 0.4f, 0.3f, 0.3f, 0.7f, 0.4f, 0.4f, 0.4f, 0.5f, 0.4f, 0.3f, 0.3f, 0.7f, 0.4f, 0.3f, 0.3f, 0.7f, 0.4f, 0.3f, 0.3f, 0.7f, 0.4f, 0.4f, 0.4f, 0.4f }, new float[] { 0.2f, 0.4f, 0.5f, 0.4f, 0.5f, 0.2f, 0.2f, 0.2f, 0.2f, 0.9f, 0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 1.6f, 0.8f, 0.8f, 0.3f, 0.2f, 0.2f, 0.2f, 0.9f, 0.3f, 0.2f, 0.2f, 0.2f, 0.4f, 0.2f, 0.2f }, new float[] { 0.2f, 0.2f, 0.25f, 0.25f, 0.25f, 0.9f, 0.8f, 0.9f, 0.3f, 0.2f, 0.4f, 0.3f, 0.2f, 0.4f, 0.6f, 0.5f, 0.45f, 0.2f, 0.6f, 0.9f, 0.8f, 1.1f, 0.8f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.3f, 0.4f, 0.3f, 0.2f, 0.4f, 0.25f, 0.3f, 0.5f }, new float[] { 0.2f, 0.15f, 0.175f, 0.175f, 0.25f/**, 0.2f, 0.2f, 0.25f, 0.3f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f*/ } };
    private string[] musicfiles = { "starstrukk1", "starstrukk2", "starstrukk3", "wonderland1" };
    private int indexOfLyrics;

    private List<int> missingIndexes = new List<int>();
    private List<int> usedMissingIndexes = new List<int>();
    private int completed = 0;
    private List<string> entered = new List<string>();

    public Text display;

    private bool input = false;
    private bool playing = false;
    private bool striking = false;
    private bool focused = false;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        moduleSolved = false;
        foreach(KMSelectable obj in buttons){
            KMSelectable pressed = obj;
            pressed.OnInteract += delegate () { PressButton(pressed); return false; };
        }
        ModuleSelectable.OnFocus += delegate () { focused = true; };
        ModuleSelectable.OnDefocus += delegate () { focused = false; };
        if (Application.isEditor)
            focused = true;
    }

    void Start () {
        pickSong();
        selectMissing();
    }

    void Update()
    {
        if (input && !striking && focused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                handleKey("Q");
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                handleKey("W");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                handleKey("E");
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                handleKey("R");
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                handleKey("T");
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                handleKey("Y");
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                handleKey("U");
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                handleKey("I");
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                handleKey("O");
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                handleKey("P");
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                handleKey("A");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                handleKey("S");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                handleKey("D");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                handleKey("F");
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                handleKey("G");
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                handleKey("H");
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                handleKey("J");
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                handleKey("K");
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                handleKey("L");
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                handleKey("Z");
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                handleKey("X");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                handleKey("C");
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                handleKey("V");
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                handleKey("B");
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                handleKey("N");
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                handleKey("M");
            }
            else if (Input.GetKeyDown(KeyCode.Quote))
            {
                handleKey("'");
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                handleKey("-");
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                handleBack();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                handleEnter();
            }
        }
    }

    void PressButton(KMSelectable pressed)
    {
        if(moduleSolved != true && playing != true && striking != true)
        {
            if (pressed == buttons[0] && input != true)
            {
                audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, pressed.transform);
                pressed.AddInteractionPunch(0.5f);
                StartCoroutine(player());
            }
            if (pressed == buttons[1])
            {
                audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, pressed.transform);
                pressed.AddInteractionPunch(0.25f);
                if (input == true)
                {
                    input = false;
                    display.text = "";
                }
                else
                {
                    input = true;
                    display.text = entered[completed];
                }
            }
        }
    }

    private void pickSong()
    {
        //Debug line used for lyric lengths
        //Debug.Log(lyrics[2].Length+" "+waittimes[2].Length);
        indexOfLyrics = UnityEngine.Random.Range(0, lyrics.Length-1);
        Debug.LogFormat("[Lyrics #{0}] The picked lyrics are from {1}!", moduleId, songnames[indexOfLyrics]);
        Debug.LogFormat("[Lyrics #{0}] Lyrics: {1}", moduleId, lyrics[indexOfLyrics].Join(" ").Replace("  ", " "));
    }

    private void selectMissing()
    {
        int amt = UnityEngine.Random.Range(3, 6);
        for (int i = 0; i < amt; i++)
        {
            int rando = UnityEngine.Random.Range(0, lyrics[indexOfLyrics].Length);
            while (missingIndexes.Contains(rando) || lyrics[indexOfLyrics][rando].Equals(""))
            {
                rando = UnityEngine.Random.Range(0, lyrics[indexOfLyrics].Length);
            }
            missingIndexes.Add(rando);
        }
        missingIndexes.Sort();
        string missing = "";
        for (int i = 0; i < missingIndexes.Count(); i++)
        {
            string under = "";
            for (int j = 0; j < lyrics[indexOfLyrics][missingIndexes[i]].Length; j++)
            {
                under += "_";
            }
            entered.Add(under);
            missing += lyrics[indexOfLyrics][missingIndexes[i]] + " (lyric " + (missingIndexes[i]+1) + "), ";
        }
        missing = missing.Substring(0, missing.Length-2);
        Debug.LogFormat("[Lyrics #{0}] The missing lyrics are: {1}", moduleId, missing);
        Debug.LogFormat("[Lyrics #{0}] Now waiting for lyric {1} submission...", moduleId, missingIndexes[completed] + 1);
    }

    private void handleKey(string s)
    {
        if (entered[completed].Contains("_"))
        {
            audio.PlaySoundAtTransform("keypress", transform);
            int index = 0;
            for (int i = 0; i < entered[completed].Length; i++)
            {
                if (entered[completed].ElementAt(i).Equals('_'))
                {
                    index = i;
                    break;
                }
            }
            entered[completed] = entered[completed].Remove(index, 1);
            entered[completed] = entered[completed].Insert(index, s);
            display.text = entered[completed];
        }
    }

    private void handleBack()
    {
        if (entered[completed].ElementAt(0).Equals('_'))
            return;
        audio.PlaySoundAtTransform("keypress", transform);
        int index = 0;
        for (int i = entered[completed].Length - 1; i >= 0; i--)
        {
            if (!entered[completed].ElementAt(i).Equals('_'))
            {
                index = i;
                break;
            }
        }
        entered[completed] = entered[completed].Remove(index, 1);
        entered[completed] = entered[completed].Insert(index, "_");
        display.text = entered[completed];
    }

    private void handleEnter()
    {
        audio.PlaySoundAtTransform("keypress", transform);
        if (entered[completed].Equals(lyrics[indexOfLyrics][missingIndexes[completed]].ToUpper()))
        {
            Debug.LogFormat("[Lyrics #{0}] Submitted lyric {1} as {2}. That is correct.", moduleId, missingIndexes[completed] + 1, entered[completed]);
            audio.PlaySoundAtTransform("correct", transform);
            usedMissingIndexes.Add(missingIndexes[completed]);
            completed++;
            if (completed == missingIndexes.Count())
            {
                Debug.LogFormat("[Lyrics #{0}] All missing lyrics have been filled in, module disarmed!", moduleId);
                moduleSolved = true;
                input = false;
                display.text = "";
                GetComponent<KMBombModule>().HandlePass();
            }
            else
            {
                Debug.LogFormat("[Lyrics #{0}] Now waiting for lyric {1} submission...", moduleId, missingIndexes[completed] + 1);
                display.text = entered[completed];
            }
        }
        else
        {
            Debug.LogFormat("[Lyrics #{0}] Submitted lyric {1} as {2}. That is incorrect, strike!", moduleId, missingIndexes[completed] + 1, entered[completed]);
            StartCoroutine(strike());
            GetComponent<KMBombModule>().HandleStrike();
        }
    }

    private IEnumerator player()
    {
        playing = true;
        audio.PlaySoundAtTransform(musicfiles[indexOfLyrics], transform);
        float t = 0f;
        while (t < waittimes[indexOfLyrics][0])
        {
            yield return null;
            t += Time.deltaTime;
        }
        for (int i = 1; i < lyrics[indexOfLyrics].Length+1; i++)
        {
            if (missingIndexes.Contains(i - 1) && !usedMissingIndexes.Contains(i - 1))
            {
                string under = "";
                for (int j = 0; j < lyrics[indexOfLyrics][i - 1].Length; j++)
                {
                    under += "_";
                }
                display.text = under;
            }
            else
            {
                display.text = lyrics[indexOfLyrics][i - 1];
            }
            t = 0f;
            while (t < waittimes[indexOfLyrics][i])
            {
                yield return null;
                t += Time.deltaTime;
            }
        }
        display.text = "";
        playing = false;
    }

    private IEnumerator strike()
    {
        striking = true;
        for (int i = 0; i < 5; i++)
        {
            display.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            display.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        string under = "";
        for (int i = 0; i < entered[completed].Length; i++)
        {
            under += "_";
        }
        display.text = under;
        entered[completed] = under;
        striking = false;
    }

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} play/start [Presses the 'Start Lyrics' button] | !{0} submit <lyr> [Submits the specified lyric 'lyr']";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        if (Regex.IsMatch(command, @"^\s*press play\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(command, @"^\s*press start\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(command, @"^\s*play\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(command, @"^\s*start\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (striking || playing)
            {
                yield return "sendtochaterror Cannot start the lyrics while the display is animating!";
            }
            else
            {
                if (input)
                {
                    buttons[1].OnInteract();
                    yield return new WaitForSeconds(0.1f);
                }
                buttons[0].OnInteract();
            }
            yield break;
        }
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*submit\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (playing || striking)
            {
                yield return "sendtochaterror Cannot submit a lyric while the display is animating!";
                yield break;
            }
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                parameters[1] = parameters[1].ToUpper();
                if (Regex.IsMatch(parameters[1], @"[A-Z]", RegexOptions.CultureInvariant))
                {
                    if (parameters[1].Length > entered[completed].Length)
                    {
                        yield return "sendtochaterror!f The lyric entered '" + parameters[1] + "' is too long!";
                        yield break;
                    }
                    if (!input)
                    {
                        buttons[1].OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    while (!entered[completed].ElementAt(0).Equals('_')) { handleBack(); yield return new WaitForSeconds(0.1f); }
                    for (int i = 0; i < parameters[1].Length; i++)
                    {
                        handleKey(parameters[1].ElementAt(i)+"");
                        yield return new WaitForSeconds(0.1f);
                    }
                    if (!entered[completed].Equals(lyrics[indexOfLyrics][missingIndexes[completed]].ToUpper()))
                    {
                        yield return "strike";
                    }
                    else if (completed+1 == missingIndexes.Count())
                    {
                        yield return "solve";
                    }
                    handleEnter();
                }
                else
                {
                    yield return "sendtochaterror!f The specified lyric '" + parameters[1] + "' is invalid!";
                }
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the lyric to submit!";
            }
            yield break;
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (playing || striking)
        {
            yield return true;
        }
        int start = completed;
        for (int i = start; i < missingIndexes.Count(); i++)
        {
            yield return ProcessTwitchCommand("submit " + lyrics[indexOfLyrics][missingIndexes[i]]);
        }
    }
}
