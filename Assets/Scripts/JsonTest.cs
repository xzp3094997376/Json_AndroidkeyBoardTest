using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using UnityEngine.UI;

[ExecuteInEditMode]
public class JsonTest : MonoBehaviour
{
    public bool isTest;

    public RectTransform rt;
    public Text text;

    Vector3 originPos;
    // Start is called before the first frame update
    void Start()
    {
        originPos=rt.anchoredPosition3D;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTest)
        {
            isTest = false;
            JsonToObject();
        }
    }




    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2f,Screen.height/2,100,100), "ObjectToJson"))
        {
            ObjectToJson();
        }
        if (GUI.Button(new Rect(Screen.width / 2f, Screen.height / 2+150, 100, 100), "JsonToObject"))
        {
            JsonToObject();
        }
    }

    void JsonToObject()
    {
        string path =Path.Combine(Application.streamingAssetsPath,"1.txt");
        string jsonStr=File.ReadAllText(path);
        Debug.Log(jsonStr);
        ModelBaseInfo mbi=JsonMapper.ToObject<ModelBaseInfo>(jsonStr);
        Debug.Log(mbi.mp3);
    }

    void ObjectToJson()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "2.txt");      
        path= path.Replace(@"\",@"/");
        ModelBaseInfo mbi = new ModelBaseInfo() { name= "活塞连杆组",code= "huosailianganzu", content= "活塞的基本构造可分顶部，头部和裙部三部分",modeltype= "AB",
             mp3 ="music",tip= "tip",ModuleInforList=new List<ModuleInfo>() { new ModuleInfo { name="123",type= "run",code="code", content= "活塞的基本构造可分顶部",mp3= "讲解音频路径" } } };
        string jsonStr = JsonMapper.ToJson(mbi);
        if (!File.Exists(path))
        {
            File.Create(path);
        }
        

        //byte[] byteContent=Encoding.UTF8.GetBytes(jsonStr);
        File.WriteAllText(path,jsonStr);
#if UNITY_EDITOR
        //UnityEditor.AssetDatabase.ImportAsset("StreamingAssets/2.txt");
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.EditorApplication.isPlaying = false;
#endif
      
       
    }


    public RectTransform canvas;
    public void MouseSelect()
    {
        Debug.Log("点击---"+transform.GetSiblingIndex());
        float h = SystemProperty.GetRelativeKeyboardHeight(canvas,true);
        text.text = h.ToString();
        Vector3 upPos = originPos;
        upPos.y += h;
        rt.anchoredPosition3D = upPos;
    }

    public void MouseDeselect()
    {
        rt.anchoredPosition3D = originPos;
        Debug.Log("出去"+ transform.GetSiblingIndex());
    }
}


[System.Serializable]
public class ModelBaseInfo
{
    public string name;
    public string code;
    public string content;
    public string modeltype;
    public string mp3;
    public string tip;
    public List<ModuleInfo> ModuleInforList = new List<ModuleInfo>();
}

[System.Serializable]
public class ModuleInfo
{
    public string name;
    public string type;
    public string code;
    public string content;
    public string mp3;
}
