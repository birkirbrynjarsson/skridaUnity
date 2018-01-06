using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;

public class ChallengeScript : MonoBehaviour {

    public List<Challenge> challenges = new List<Challenge>(){
        new Challenge(){
            question = "Hvaða ár var Skriðuklausturskirkjan vígð?",
            answers = new string[] {"1512"}
        },
		new Challenge(){
            question = "Hvað er grunnflötur klausturbygginganna stór?",
            answers = new string[] {"yfir 1500 fermetrar",
									"yfir 1500 m2",
									"yfir 1500 m^2",
									"yfir 1500",
									"meira en 1500 fermetrar",
									"meira en 1500 m2",
									"meira en 1500 m^2",
									"meira en 1500",
                                    "stærra en 1500 fermetrar",
									"stærra en 1500 m2",
									"stærra en 1500 m^2",
									"stærra en 1500",
                                    "1500 fermetrar",
									"1500", 
                                    "1500 m2",
                                    "1500 m^2",
                                    "1500+ fermetrar",
									"1500+", 
                                    "1500+ m2",
                                    "1500+ m^2",
                                    "> 1500 fermetrar",
									"> 1500", 
                                    "> 1500 m2",
                                    "> 1500 m^2"}
        },
		new Challenge(){
            question = "Hver leigði klaustrið og allar eignir þess frá danska kónginum sem markaði formleg lok á klausturhaldi að Skriðu?",
            answers = new string[] {"sr. einar árnason",
									"séra einar árnason",
									"einar árnason",
                                    "einar"}
        },
		new Challenge(){
            question = "Hvaða ár var lútherstrú lögleidd í Danmörku?",
            answers = new string[] {"1537"}
        },
		new Challenge(){
            question = "Hvaða ár var minni kirkja byggð á rústum klausturkirkjunnar sem hafði hrakað með árunum?",
            answers = new string[] {"1792"}
        },
		new Challenge(){
            question = "Hversu oft á sólahring fór bænahald reglubræðra fram?",
            answers = new string[] {"8",
                                    "átta",
                                    "8 sinnum",
                                    "átta sinnum",
                                    "8 sinnum á sólahring",
                                    "átta sinnum á sólahring"}
        },
		new Challenge(){
            question = "Hvað voru bænaguðsþjónustur sem hluti af trúariðkun reglubræðra kallaðar?",
            answers = new string[] {"tíðir"}
        },
		new Challenge(){
            question = "Klukkan hvað var eyktartíð?",
            answers = new string[] {"15",
									"15:00",
									"klukkan 15",
									"klukkan 15:00",
									"kl 15",
									"kl 15:00",

									"3",
                                    "3:00",
									"klukkan 3",
                                    "klukkan 3:00",
									"kl 3",
                                    "kl 3:00",
									"3 eftir hádegi",
                                    "3:00 eftir hádegi",
									"klukkan 3 eftir hádegi",
                                    "klukkan 3:00 eftir hádegi",
									"kl 3 eftir hádegi",
                                    "kl 3:00 eftir hádegi",
                                    
                                    "3 e.h.",
                                    "3:00 e.h.",
									"klukkan 3 e.h.",
                                    "klukkan 3:00 e.h.",
									"kl 3 e.h.",
                                    "kl 3:00 e.h.",

                                    "þrjú",
									"klukkan þrjú",
									"kl þrjú",
									"þrjú eftir hádegi",
									"klukkan þrjú eftir hádegi",
									"kl þrjú eftir hádegi",

									"þrjú e.h.",
									"klukkan þrjú e.h.",
									"kl þrjú e.h.",

                                    "fimmtán",
									"klukkan fimmtán",
									"kl fimmtán",
									"fimmtán eftir hádegi",
									"klukkan fimmtán eftir hádegi",
									"kl fimmtán eftir hádegi",
                                    
                                    "fimmtán e.h.",
									"klukkan fimmtán e.h.",
									"kl fimmtán e.h."}
        },
		new Challenge(){
            question = "Á hvaða mánaðardegi er allra heilagra messa?",
            answers = new string[] {"1. nóvember",
									"1. nóv",
									"1 nóvember",
									"1 nóv",
									"fyrsti nóvember",
									"fyrsti nóv",
                                    "01/11",
                                    "1/11",
                                    "11/1",
                                    "11/01",
                                    "01.11",
                                    "1.11",
                                    "11.01",
                                    "11.1"}
        },
		new Challenge(){
            question = "Hverskonar stofnun var rekin í klaustrinu á Skriðu?",
            answers = new string[] {"sjúkrahús",
									"hæli sjúkra",
									"hæli",
									"læknamiðstöð",
                                    "lækningar",
                                    "læknir",
                                    "sjúkra",
                                    "hjúkrunarheimili",
                                    "hjúkrun",
                                    "læknun",
                                    "hospítal"}
        },
		new Challenge(){
            question = "Hvaða príor á Skriðuklaustri gegndi því hlutverki styðst?",
            answers = new string[] {"jón markússon",
                                    "jón"}
        },
		new Challenge(){
            question = "Hver gaf kirkjunni jörðina að Skriðu?",
            answers = new string[] {"sesselja þorsteinsdóttir",
									"sesselja þorsteins",
									"sesselja þorsteinsd",
                                    "sesselja þorsteinsd.",
                                    "sesselja"}
        },
		new Challenge(){
            question = "Hvaða ár er talið að klaustrið á Skriðu hafi verið stofnað?",
            answers = new string[] {"1493"}
        },
		new Challenge(){
            question = "Hvaða synd hafði Sesselja drýgt?",
            answers = new string[] {"kvæntist frænda sínum",
									"giftist frænda sínum",
                                    "gifst frænda sínum",
                                    "kvæntist skyldmenni",
                                    "giftist skyldmenni",
                                    "gifst skyldmenni",
                                    "kvæntist frænda",
                                    "giftist frænda",
                                    "gifst frænda",
                                    "gekk í hjónaband með frænda",
                                    "gekk í hjónaband með frænda sínum",
                                    "gekk í hjónaband með skyldmenni",
                                    "gekk í hjónaband",
                                    "gifst",
                                    "giftist",
                                    "kvæntist",
                                    "skyldleiki",
                                    "skyldleiki hjóna",
                                    "giftist einari ormssyni loftssyni hins ríka",
                                    "giftist einari",
                                    "giftist einari ormssyni",
                                    "giftist einari ormssyni loftssyni",
                                    "giftist einari frænda",
                                    "kvæntist einari ormssyni loftssyni hins ríka",
                                    "kvæntist einari",
                                    "kvæntist einari ormssyni",
                                    "kvæntist einari ormssyni loftssyni",
                                    "kvæntist einari frænda",
                                    "giftist fjórmenningi",
                                    "kvæntist fjórmenningi",
                                    "ósiðlegt hjónaband",
                                    
                                    "hún kvæntist frænda sínum",
									"hún giftist frænda sínum",
                                    "hún kvæntist skyldmenni",
                                    "hún giftist skyldmenni",
                                    "hún kvæntist frænda",
                                    "hún giftist frænda",
                                    "hún gekk í hjónaband með frænda",
                                    "hún gekk í hjónaband með frænda sínum",
                                    "hún gekk í hjónaband með skyldmenni",
                                    "hún gekk í hjónaband",
                                    "hún giftist",
                                    "hún kvæntist",
                                    "hún giftist einari ormssyni loftssyni hins ríka",
                                    "hún giftist einari",
                                    "hún giftist einari ormssyni",
                                    "hún giftist einari ormssyni loftssyni",
                                    "hún giftist einari frænda",
                                    "hún kvæntist einari ormssyni loftssyni hins ríka",
                                    "hún kvæntist einari",
                                    "hún kvæntist einari ormssyni",
                                    "hún kvæntist einari ormssyni loftssyni",
                                    "hún kvæntist einari frænda",
                                    "hún giftist fjórmenningi",
                                    "hún kvæntist fjórmenningi",
                                    "hún stofnaði til ósiðlegs hjónabands"}
        },
		new Challenge(){
            question = "Af hverju gaf Sesselja jörðina Skriðu til stofnunar klausturs?",
            answers = new string[] {"til friðþægingar gagnvart guði",
									"friðþægingar gagnvart guði",
                                    "friðþægingar",
									"friðþæging gagnvart guði",
									"friðþæging",
									"til friðþægingar",
                                    "friðþægingu gagnvart guði",
                                    "friðþægingu",
                                    "syndaaflausn",
                                    "syndaaflausnar",
                                    "til syndaaflausnar"}
        },
		new Challenge(){
            question = "Hvenær er gjafabréf þeirra Hallsteins og Sesselju dagsett?",
            answers = new string[] {"8. júní 1500",
									"8 júní 1500",
									"8. júní, 1500",
									"8 júní, 1500",
									"8. júní árið 1500",
									"8 júní árið 1500",
									"áttunda júní 1500",
									"áttunda júní árið 1500",
                                    "08/06/1500",
                                    "8/6/1500",
                                    "08/6/1500",
                                    "8/06/1500",
                                    "06/08/1500",
                                    "6/08/1500",
                                    "06/8/1500",
                                    "6/8/1500",
                                    "árið 1500",
                                    "1500",
                                    "8 júní",
                                    "08/06",
                                    "8/6",
                                    "08/6",
                                    "8/06",
                                    "06/08",
                                    "6/08",
                                    "06/8",
                                    "6/8"}
        },
		new Challenge(){
            question = "Hver var Skálholtsbiskup þegar Skriðuklaustur var stofnað?",
            answers = new string[] {"stefán jónsson",
                                    "stefán"}
        },
		new Challenge(){
            question = "Hvað hét fyrsti príor Skriðuklausturs?",
            answers = new string[] {"narfi jónsson",
                                    "narfi"}
        },
		new Challenge(){
            question = "Eftir hvaða klausturreglu starfaði Skriðuklaustur?",
            answers = new string[] {"ágústínusarreglu",
									"ágústínusarregla",
                                    "reglu ágústínusar",
                                    "augustinus",
                                    "augustinusarreglu",
                                    "augustinusarregla",
                                    "regla augustinusar",
                                    "regla augustinus",
                                    "augustinusar",
                                    "ágústínusar"}
        },
		new Challenge(){
            question = "Hvað kallast sá er æðstur kórbræðra?",
            answers = new string[] {"príor"}
        },
		new Challenge(){
            question = "Hvaða ár var fyrsta klaustrið að reglu Ágústínusar stofnað á Íslandi?",
            answers = new string[] {"1168"}
        },
		new Challenge(){
            question = "Hvaða klaustur var stofnað síðast þeirra klaustra sem störfuðu á Íslandi á kaþólskum tíma?",
            answers = new string[] {"skriðuklaustur",
                                    "skriðu",
                                    "að skriðu",
                                    "klaustrið að skriðu",
                                    "skriða"}
        },
		new Challenge(){
            question = "Hvað voru margir einstaklingar grafnir upp í klausturkirkjugarðinum?",
            answers = new string[] {"295",
                                    "tvöhundruðníutíuogfimm"}
        },
        new Challenge(){
            question = "Botnaðu ljóðið:\n\nKalt er við kórbak,\nkúrir þar Jón Hrak.\nÝtar snúa austur og vestur\n...",
            answers = new string[] {"allir nema jón hrak"}
        },
        new Challenge(){
            question = "?",
            answers = new string[] {}
        },
        new Challenge(){
            question = "?",
            answers = new string[] {}
        },
    };

    public Text question;
    public Image image;
    public InputField answerInput;
    public string[] answers;
    public Button submitAnswerButton;
    private int currentChallengeIndex;
    private int challengeNr;
    private int itemIndex;
    private int itemLevel;

    public Sprite completedImage;
    public Sprite incompleteImage;

    public PlayMakerFSM fsm;
    public ItemControllerScript itemController;
    public OpenItemScript openItem;

	// Use this for initialization
	void Start () {
        submitAnswerButton.onClick.AddListener(tryAnswer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setChallenge(int challengeIndex, int itemIndex, int itemLevel){
        this.itemLevel = itemLevel;
        this.itemIndex = itemIndex;
        this.challengeNr = challengeIndex;
        currentChallengeIndex = (itemIndex * 3) + challengeIndex;
        answerInput.text = "";
        question.text = challenges[currentChallengeIndex].question;
    }

    void tryAnswer() {
        Challenge challenge = challenges[currentChallengeIndex];
        // First iteration.
        // Only change answer to lowercase.
        string input = answerInput.text.ToLower();
        string tempAnswer = "";

        foreach(string answer in challenge.answers){
            if(input == answer){
                correctAnswer();
                return;
            }
        }

        // Second iteration.
        // Remove non letters and change to english letters
        input = removeNonLetters(input);
        input = parseIcelandicLetters(input);

        for (int i = 0; i < challenge.answers.Length; i++) {
            tempAnswer = removeNonLetters(challenge.answers[i]);
            tempAnswer = parseIcelandicLetters(tempAnswer);

            if(input == tempAnswer){
                correctAnswer();
                return;
            }
        }

        // Did not find the correct answer
        wrongAnswer();
        return;
    }

    void correctAnswer(){
        UIManager.ShowNotification("MessageNotification", 10f, true, "Vel gert!", "\"" + answerInput.text + "\"\n er rétt svar", completedImage);
        if(challengeNr == itemLevel){
            itemController.updateItem(itemIndex);
            openItem.levelUpItem();
        }
        fsm.SetState("OpenItem");
    }

    void wrongAnswer(){
        UIManager.ShowNotification("NoStarNotification", 10f, true, "Því miður", "rangt svar!\nreyndu aftur!", incompleteImage);
    }

    /// <summary>
    /// Removes all spaces, dots and slashes from a string and returns
    /// the resulting string.
    /// </summary>
    string removeNonLetters(string str) {
        str = str.Replace(" ", string.Empty);
        str = str.Replace(".", string.Empty);
        str = str.Replace(",", string.Empty);
        str = str.Replace("´", string.Empty);
        str = str.Replace("`", string.Empty);
        str = str.Replace("'", string.Empty);
        str = str.Replace("\\", string.Empty);
        return str.Replace("/", string.Empty);
    }

    /// <summary>
    /// Changes icelandic letters to english letters.
    /// </summary>
    string parseIcelandicLetters(string str) {
        StringBuilder strBuilder = new StringBuilder(str);

        for (int i = 0; i < strBuilder.Length; i++) {
            if (strBuilder[i] == 'á') {
                strBuilder[i] = 'a';
            }
            else if (strBuilder[i] == 'é') {
                strBuilder[i] = 'e';
            }
            else if (strBuilder[i] == 'í') {
                strBuilder[i] = 'i';
            }
            else if (strBuilder[i] == 'ó') {
                strBuilder[i] = 'o';
            }
            else if (strBuilder[i] == 'ú') {
                strBuilder[i] = 'u';
            }
            else if (strBuilder[i] == 'ý') {
                strBuilder[i] = 'y';
            }
            else if (strBuilder[i] == 'þ') {
                strBuilder[i] = 't';
                strBuilder.Insert(i+1, 'h');
            }
            else if (strBuilder[i] == 'æ') {
                strBuilder[i] = 'a';
                strBuilder.Insert(i+1, 'e');
            }
            else if (strBuilder[i] == 'ö') {
                strBuilder[i] = 'o';
            }
            else if (strBuilder[i] == 'ð') {
                strBuilder[i] = 'd';
            }
        }

        return strBuilder.ToString();
    }
}

public class Challenge {
    public string question;
    public string[] answers;
}