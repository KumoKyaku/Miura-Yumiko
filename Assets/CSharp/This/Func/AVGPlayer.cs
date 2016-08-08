using UnityEngine;
using System.Collections;
using Poi;
using AVG;
using System;
using System.Collections.Generic;
using System.Linq;

public class AVGPlayer : MonoBehaviour, iAVGPlayer
{
    [SerializeField]
    private Background background;
    [SerializeField]
    private Charactor charactor;
    [SerializeField]
    private Captions captions;
    public SceneEff SceneEff;

    public Background Background
    {
        get
        {
            return background;
        }
    }

    public Charactor Charactor
    {
        get
        {
            return charactor;
        }
    }

    public Captions Captions
    {
        get
        {
            return captions;
        }
    }

    public void Loading()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        Start();
    }

    public void Play(iNode _node)
    {
        switch (_node.Type)
        {
            case NodeType.Sentence:
                PlayNodeTypeSentence(_node);
                break;
            case NodeType.Brunch:
                PlayNodeTypeBrunch(_node);
                break;
            case NodeType.Option:
                PlayNodeTypeOption(_node);
                break;
            case NodeType.End:
                PlayNodeTypeEnd(_node);
                break;
            default:
                break;
        }

    }

    

    private void PlayNodeTypeEnd(iNode _node)
    {
        PlayNodeTypeSentence(_node);
        GameManager.Save.Key.Add(_node.ID);
        var me = UIManager.GetUI<UI_Message>();
        me.Use(new UI_Message_Context()
        {
            Title = Writing.Get(100036),
            Message = System.Text.RegularExpressions.Regex.Unescape(Writing.Get(_node.Sentences.First().Value.DialogueID)),
            CallBack = ui =>
            {

            }
        });
        //UIManager.GetUI<UI_Game>().NoNext = true;
    }

    private void PlayNodeTypeOption(iNode _node)
    {
        UIManager.GetUI<UI_Game>().NoNext = true;
        UI_Choice _c = UIManager.GetUI<UI_Choice>();
        _c.SetFront();

        UI_Choice_Context con = new UI_Choice_Context();
        con.OptionList = new List<iLabel>();
        foreach (var item in _node.Sentences)
        {
            Label l = new Label() { ID = item.Key, Name = Writing.Get(item.Value.DialogueID) };
            con.OptionList.Add(l);
        }

        con.Callback = (res, uichoice) =>
        {
            GameManager.Save.ChoiceResult[_node.ID] = res;
            uichoice.UseDone();
            Libretto.NextNode();
        };

        _c.Use(con);
    }

    private void PlayNodeTypeBrunch(iNode _node)
    {
        UIManager.GetUI<UI_Game>().NoNext = false;
        NextNode();
    }

    private void PlayNodeTypeSentence(iNode _node)
    {
        ///背景处理
        if (!string.IsNullOrEmpty(_node.ChangeBGImage))
        {
            if (_node.ChangeBGImage == "Scene" )
            {
                background.Image.sprite = Loader.Sprite(Libretto.CurrentScene.BGImage);
            }
            else
            {
                background.Image.sprite = Loader.Sprite(_node.ChangeBGImage);
            }

        }

        if (!string.IsNullOrEmpty( _node.ChangeBGM))
        {
            if (_node.ChangeBGImage == "Scene")
            {
                background.Image.sprite = Loader.Sprite(Libretto.CurrentScene.BGAudio);
            }
            else
            {
                background.BGM.clip = Loader.AudioClip(_node.ChangeBGM);
            }
        }

        ///人物处理
        charactor.Play(_node.ActorList);

        ///台词处理

        captions.Play(_node.Sentences);

        UIManager.GetUI<UI_Game>().NoNext = false;
    }

    public void NextNode()
    {
        if (captions.NextPage())
        {
            //表示还有下一条
        }
        else
        {
            //、没有下一条显示下个节点
            Libretto.NextNode();
        }
    }

    public void LastNode()
    {
         Libretto.LastNode();
    }

    // Use this for initialization
    void Start()
    {
        GameManager.AVGPlayer = this;
        Libretto.Player = this;
    }

    public void LoadScene(iScene scene)
    {
        background.Image.sprite = Loader.Sprite(scene.BGImage);
        background.BGM.clip = Loader.AudioClip(scene.BGAudio);
    }

    public void Save()
    {
        UI_Choice ch = UIManager.GetUI<UI_Choice>();
        
        UI_Choice_Context con = new UI_Choice_Context();
        con.OptionList = new List<iLabel>();

        for (int i = 0; i < GameManager.Save.File.Length; i++)
        {
            con.OptionList.Add(new Label()
            {
                ID = i,
                Name = @"["+ Writing.Get(100026) + (i + 1) + "]:" + 
                Writing.Get(Libretto.GetNode(GameManager.Save.File[i]).Sentences.First().Value.DialogueID),
            });
        }

        con.Callback = this.SaveCallbacek;
        ch.Use(con);
        ch.SetFront();
    }

    private void SaveCallbacek(int arg1, UI_Choice arg2)
    {
        GameManager.Save.File[arg1] = Libretto.CurrentNode.ID;
        arg2.UseDone();
        UIManager.GetUI<UI_Game>().SetFront();
        if (Libretto.CurrentNode.Type == NodeType.Option)
        {
            Play(Libretto.CurrentNode);
        }
    }

    private void LoadCallbacek(int arg1, UI_Choice arg2)
    {
        arg2.UseDone();
        UIManager.GetUI<UI_Game>().SetFront();  
        Libretto.PlayNode(GameManager.Save.File[arg1]);
    }

    public void Load()
    {
        UI_Choice ch = UIManager.GetUI<UI_Choice>();

        UI_Choice_Context con = new UI_Choice_Context();
        con.OptionList = new List<iLabel>();

        for (int i = 0; i < GameManager.Save.File.Length; i++)
        {
            con.OptionList.Add(new Label()
            {
                ID = i,
                Name = @"["+Writing.Get(100027) + (i + 1) + "]:" + 
                Writing.Get(Libretto.GetNode(GameManager.Save.File[i]).Sentences.First().Value.DialogueID),
            });
        }

        con.Callback = this.LoadCallbacek;
        ch.Use(con);
        ch.SetFront();
    }
}
