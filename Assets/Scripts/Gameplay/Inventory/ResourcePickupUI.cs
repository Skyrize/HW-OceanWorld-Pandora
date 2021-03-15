using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePickupUI : MonoBehaviour
{
    public static ResourcePickupUI Instance = null;

    public GameObject TextModel;

    [Min(0f)] public float FadeIn = 0.5f;
    [Min(0f)] public float FadeOut = 0.5f;
    [Min(0f)] public float StayTime = 2f;
    [Min(0f)] public float MessageCooldown = 0.5f;
    [Min(0f)] public Vector3 EndPositionOffset = new Vector3(0, 200f, 0);

    private object m_messageLock = new object();
    private float m_lastMessageTime;

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void Awake()
    {
        TextModel.GetComponent<Text>().text = "";
    }

    public void PopMessage(string text)
    {
        StartCoroutine(DoPopMessage(text));
    }

    private IEnumerator DoPopMessage(string text)
    {
        lock (m_messageLock)
        {
            while (m_lastMessageTime + MessageCooldown > Time.time)
                yield return new WaitForSeconds(0.001f);
            m_lastMessageTime = Time.time;
        }

        var obj = Instantiate(TextModel, transform);
        var textComp = obj.GetComponent<Text>();
        var rectComp = obj.GetComponent<RectTransform>();
        var time = 0f;
        var fadingOut = false;

        var posBegin = rectComp.position;
        var posEnd = posBegin + EndPositionOffset;

        obj.name = text;
        textComp.text = text;
        textComp.CrossFadeAlpha(0, 0, false);
        textComp.CrossFadeAlpha(1, FadeIn, false);

        while (time < StayTime)
        {
            yield return new WaitForSeconds(0.001f);
            time += Time.deltaTime;

            if (!fadingOut && time >= StayTime - FadeOut)
            {
                fadingOut = true;
                textComp.CrossFadeAlpha(0, FadeOut, false);
            }

            rectComp.position = Vector3.Lerp(posBegin, posEnd, time / StayTime);
        }

        Destroy(obj);
    }
}