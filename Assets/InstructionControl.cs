using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionControl : MonoBehaviour
{
    private int ExpPageNow;
    private GameObject NextPage;
    private GameObject PrevPage;
    private GameObject Subject;
    private GameObject MainText;
    private string Sub;
    private string Main;
    private AudioSource PageChange;
    private AudioSource GotoTitle;
    private GameObject TitleText;
    private GameObject StartButton;
    private GameObject Instruction;
    private GameObject TitleButton;
    // Use this for initialization
    void Start ()
    {
        ExpPageNow = 1;
        TitleText = GameObject.Find("TitleText");
        StartButton = GameObject.Find("Start");
        Instruction = GameObject.Find("Instruction");
        PrevPage = GameObject.Find("PrevPage");
        NextPage = GameObject.Find("NextPage");
        TitleButton = GameObject.Find("TitleButton");
        Subject = GameObject.Find("Subject");
        MainText= GameObject.Find("MainText");
        AudioSource[] audioSources = GetComponents<AudioSource>();
        PageChange = audioSources[0];
        GotoTitle = audioSources[1];
    }
	
	// Update is called once per frame
	void Update ()
    {
        TextChange();
        Subject.GetComponent<Text>().text = Sub;
        MainText.GetComponent<Text>().text = Main;
        switch (ExpPageNow)
        {
            case 1:
                TitleText.SetActive(true);
                StartButton.SetActive(true);
                Instruction.SetActive(true);
                PrevPage.SetActive(false);
                NextPage.SetActive(false);
                TitleButton.SetActive(false);
                Subject.SetActive(false);
                MainText.SetActive(false);
                break;
            case 2:
                TitleText.SetActive(false);
                StartButton.SetActive(false);
                Instruction.SetActive(false);
                PrevPage.SetActive(false);
                NextPage.SetActive(true);
                TitleButton.SetActive(true);
                Subject.SetActive(true);
                MainText.SetActive(true);
                break;
            case 6:
                TitleText.SetActive(false);
                StartButton.SetActive(false);
                Instruction.SetActive(false);
                PrevPage.SetActive(true);
                NextPage.SetActive(false);
                TitleButton.SetActive(true);
                Subject.SetActive(true);
                MainText.SetActive(true);
                break;
            default:
                TitleText.SetActive(false);
                StartButton.SetActive(false);
                Instruction.SetActive(false);
                PrevPage.SetActive(true);
                NextPage.SetActive(true);
                TitleButton.SetActive(true);
                Subject.SetActive(true);
                MainText.SetActive(true);
                break;
        }
    }
    public void GameStartPushed()
    {
        SceneManager.LoadScene("STAGE01");
    }
    public void InstructionPushed()
    {
        GotoTitle.PlayOneShot(GotoTitle.clip);
        ExpPageNow = 2;
    }
    public void PrevPushed()
    {
        ExpPageNow -= 1;
        PageChange.PlayOneShot(PageChange.clip);
    }

    public void NextPushed()
    {
        ExpPageNow += 1;
        PageChange.PlayOneShot(PageChange.clip);
    }

    public void TitlePushed()
    {
        GotoTitle.PlayOneShot(GotoTitle.clip);
        ExpPageNow = 1;
    }

    void TextChange()
    {
        switch (ExpPageNow)
        {
            case 2:
                Sub = "ゲーム説明";
                Main = "画面下部の黄色い棒状のラケットを操作してボールを打ち返し、" + '\n' + "画面上部のブロックにぶつけて破壊するゲームです。" + '\n' + "壊せるブロックをすべて破壊するとステージクリアとなります。" + '\n' + "動いているボールをすべて下に落としてしまうとラケットが１つ減ります。" + '\n' + "ラケットをすべて失うとゲームオーバーとなります。";
                break;
            case 3:
                Sub = "操作説明";
                Main = "左右にスワイプ：ラケットを移動" + '\n' + '\n' + "上にフリック：ボールを発射" + '\n'+ "※ボールが動いている間は上にフリックしてもボールは動きません";
                break;
            case 4:
                Sub = "ブロックとアイテム";
                Main = "ブロックは色の違うものがあり、それぞれ耐久力が異なります。耐久力の高いブロックは何度もボールをぶつけないと壊せません。" + '\n' + "壁と同じ色のブロックは壊すことができません。" + '\n'+ "ブロックを破壊した時にアイテムが出現することがあります。" + '\n' +"ラケットで受けるとアイテムを取得します。中にはマイナスの効果を持つものもあるので注意しましょう。" + '\n' + "残りのブロックが少なくなった時やプレイ時間が長くなった時もアイテムが出現することがあります。";
                break;
            case 5:
                Sub = "アイテムの詳細";
                Main = "[拡]：ラケットの幅が広がります。" + '\n' + "最大で通常の2.5倍まで拡大します。" + '\n'+"[縮]:ラケットの幅が縮みます。" + '\n' + "最大で通常の半分まで縮小します。" + '\n'+"[底]：10秒間、ラケットの下に壁ができます。"+'\n'+"[滅]：ラケットが消えてしまいます。" + '\n' + "ただし5秒後にボールが残っていれば復活します。" + '\n' + "[増]：ラケットの残数が1つ増えます。";
                break;
            case 6:
                Sub = "アイテムの詳細";
                Main = "(裂)：ボールが３つに増えます。それより多くはなりません。" + '\n' + "(貫)：ボールの攻撃力が通常の２倍になり、ブロックを壊した場合は貫通して進みます。" + '\n' + "(軟)：ボールの攻撃力が通常の半分になります。" + '\n' + "(巨)：ボールが巨大化します。" + '\n' + "(普)：通常のボールに戻ります。" + '\n'+ '\n' + "※(貫)(軟)(巨)(普)の効果は同時にはつきません。";
                break;
        }
    }
}


