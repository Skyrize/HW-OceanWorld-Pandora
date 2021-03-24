using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairStationUI : MonoBehaviour
{
    [Header("UI Colors")]
    public RepairStation repairStation;
    public Gradient gradient;

    public Text repairText;
    public GameObject floatingText;
    
    [Min(0f)] public float FadeIn = 0.5f;
    [Min(0f)] public float FadeOut = 0.5f;
    [Min(0f)] public float StayTime = 2f;
    [Min(0f)] public float MessageCooldown = 0.5f;
    [Min(0f)] public Vector3 EndPositionOffset = new Vector3(0, 200f, 0);

    private object m_messageLock = new object();
    private float m_lastMessageTime;

    private void Awake()
    {
        floatingText.SetActive(false);
    }

    public void PopMessage(float health, uint amount, Item itemConsummed)
    {
        StartCoroutine(DoPopMessage(health, amount, itemConsummed));
    }

    private WaitForSeconds timer = new WaitForSeconds(0.001f);

    private IEnumerator DoPopMessage(float health, uint amount, Item itemConsummed)
    {
        lock (m_messageLock)
        {
            while (m_lastMessageTime + MessageCooldown > Time.time)
                yield return timer;
            m_lastMessageTime = Time.time;
        }

        var obj = Instantiate(floatingText, transform);
        obj.SetActive(true);
        var rectComp = obj.GetComponent<RectTransform>();
        var time = 0f;
        var fadingOut = false;

        var posBegin = rectComp.position;
        var posEnd = posBegin + EndPositionOffset;
        var healthText = obj.transform.Find("Health").GetComponent<Text>();
        var itemText = obj.transform.Find("Item").GetComponent<Text>();

        string itemName = itemConsummed.Name;
        healthText.text = $"+{health} HP";
        itemText.text = $"-{amount} {itemName.Replace("Item", "")}";

        healthText.CrossFadeAlpha(0, 0, false);
        healthText.CrossFadeAlpha(1, FadeIn, false);
        
        itemText.CrossFadeAlpha(0, 0, false);
        itemText.CrossFadeAlpha(1, FadeIn, false);

        while (time < StayTime)
        {
            yield return new WaitForSeconds(0.001f);
            time += Time.deltaTime;

            if (!fadingOut && time >= StayTime - FadeOut)
            {
                fadingOut = true;
                healthText.CrossFadeAlpha(0, FadeOut, false);
                itemText.CrossFadeAlpha(0, FadeOut, false);
            }

            rectComp.position = Vector3.Lerp(posBegin, posEnd, time / StayTime);
        }
        rectComp.position = posBegin;
        Destroy(obj);
    }


    private void Update()
    {
        var delta = (Mathf.Cos(6f * Time.time) + 1f) / 2f;

        if (repairStation.IsRepairing)
        {
            repairText.text = "Repairing ..";
            repairText.color = gradient.Evaluate(delta);
        }
        else
        {
            repairText.text = "";
        }
    }
}
