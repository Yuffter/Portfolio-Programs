using DG.Tweening;
using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroTextTypewriter : MonoBehaviour
{

    [SerializeField,Multiline(3)] List<string> texts = new List<string>();
    TextAnimatorPlayer typewriter;
    int currentTextIndex = 0;
    bool isTyping = false;
    // Start is called before the first frame update
    void Start()
    {
        typewriter = GetComponent<TextAnimatorPlayer>();
        print(typewriter);
        typewriter.onTypewriterStart.AddListener(StartTypewriter);
        typewriter.onTextShowed.AddListener(FinishTypewriter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartTypewriter()
    {
        isTyping = true;
    }

    void FinishTypewriter()
    {
        isTyping = false;
    }

    public IEnumerator StartTypewrite()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0;i < texts.Count;i++)
        {
            typewriter.GetComponent<TextMeshProUGUI>().color = Color.white;
            typewriter.ShowText(texts[i]);
            while (isTyping) yield return null;
            yield return new WaitForSeconds(1.5f);
            typewriter.GetComponent<TextMeshProUGUI>().DOColor(new Color(1, 1, 1, 0), 1f);
            yield return new WaitForSeconds(2f);
        }
    }
}
