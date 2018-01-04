using System.Collections;
using System.Collections.Generic;
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
									"yfir 1500m2", 
									"yfir 1500 m2",
									"yfir 1500",
									"meira en 1500 fermetrar",
									"meira en 1500m2", 
									"meira en 1500 m2",
									"meira en 1500",
									"1500", 
									"1500+", 
									"1500+m2", 
									"1500 m2"}
        },
		new Challenge(){
            question = "Hver leigði klaustrið og allar eignir þess frá danska kónginum sem markaði formleg lok á klausturhaldi að Skrið?",
            answers = new string[] {"sr. einar árnason",
									"sr. einar arnason",
									"séra eina árnason",
									"séra eina arnason",
									"einar árnason",
									"einar arnason"}
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
            answers = new string[] {"8"}
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
									"klukkan 3",
									"kl 3",
									"3 eftir hádegi",
									"klukkan 3 eftir hádegi",
									"kl 3 eftir hádegi",}
        },
		new Challenge(){
            question = "Á hvaða mánaðardegi er allra heilagra messa?",
            answers = new string[] {"1. nóvember",
									"1. nóv",
									"1 nóvember",
									"1 nóv",
									"fyrsti nóvember",
									"fyrsti nóv"}
        },
		new Challenge(){
            question = "Hverskonar stofnun var rekin í klaustrinu á Skriðu?",
            answers = new string[] {"sjúkrahús",
									"hæli sjúkra",
									"hæli",
									"læknamiðstöð"}
        },
		new Challenge(){
            question = "Hvaða príor á Skriðuklaustri gegndi því hlutverki styðst?",
            answers = new string[] {"jón markússon",
									"jon markusson",
									"jon markússon",
									"jón markusson"}
        },
		new Challenge(){
            question = "Hver gaf kirkjunni jörðina að Skriðu?",
            answers = new string[] {"sesselja þorsteinsdóttir",
									"sesselja þorsteins",
									"sesselja þorsteinsd"}
        },
		new Challenge(){
            question = "Hvaða ár er talið að klaustrið á Skriðu hafi verið stofnað?",
            answers = new string[] {"1493"}
        },
		new Challenge(){
            question = "Hvaða synd hafði Sesselja drýgt?",
            answers = new string[] {"kvæntist frænda sínum",
									"giftist frænda sínum"}
        },
		new Challenge(){
            question = "Af hverju gaf Sesselja jörðina Skriðu til stofnunar klausturs?",
            answers = new string[] {"til friðþægingar gagnvart guði",
									"friðþægingar gagnvart guði",
									"friðþæging gagnvart guði",
									"friðþæging",
									"til friðþægingar"}
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
									"áttunda júní árið 1500"}
        },
		new Challenge(){
            question = "Hver var Skálholtsbiskup þegar Skriðuklaustur var stofnað?",
            answers = new string[] {"stefán jónsson"}
        },
		new Challenge(){
            question = "Hvað hét fyrsti príor Skriðuklausturs?",
            answers = new string[] {"narfi jónsson"}
        },
		new Challenge(){
            question = "Eftir hvaða klausturreglu starfaði Skriðuklaustur?",
            answers = new string[] {"ágústínusarreglu",
									"ágústínusarregla"}
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
            answers = new string[] {"skriðuklaustur"}
        },
		new Challenge(){
            question = "Hvað voru margir einstiklingar grafnir upp í klausturkirkjugarðinum?",
            answers = new string[] {"295"}
        },
    };

    public Text title;
    public Text question;
    public Image image;
    public InputField answerInput;
    public string[] answers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void tryAnswer(){
        string input = answerInput.text;
        foreach(string answer in answers){
            if(input == answer){
                correctAnswer();
                return;
            }
        }
        wrongAnswer();
        return;
    }

    void correctAnswer(){
        UIManager.ShowNotification("ItemNotification", 10f, true, "Fjársjóðsfundur!", itemName, item0);
    }

    void wrongAnswer(){
        
    }
}

public class Challenge {
    public string question;
    public string[] answers;
}