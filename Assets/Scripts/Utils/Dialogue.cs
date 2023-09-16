using System.Collections;
using UnityEngine;
using TMPro;
using Zenject;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI _text;
    public string[] _lines;
    public float _textSpeed;
    private int _index;
    private ITalkableNPC _iTalkableNpc;
    private bool _isConversationStarted;

    [Inject] private InputOneReader InputOneReader;
    [Inject] private InputTwoReader InputTwoReader;

    private void Awake()
    {
        _iTalkableNpc = GetComponentInParent<ITalkableNPC>();
    }

    private void Start()
    {
        _iTalkableNpc.OnPlayerApproached += StartDialogue;
        _text.text = string.Empty;
        gameObject.SetActive(false);
    }
    private void Update() {
        if (_isConversationStarted && InputOneReader.isInteraction || InputTwoReader.isInteraction)
        {
            if (_text.text == _lines[_index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _text.text = _lines[_index];
            }
        }
    }
    private void StartDialogue()
    {
        _index = 0;
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
        _isConversationStarted = true;
    }

    private void NextLine()
    {
        if(_index < _lines.Length -1)
        {
            _index++;
            _text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            _text.text = string.Empty;
            // UIManager.Instance.QuestManager(true);
            
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char l in _lines[_index].ToCharArray())
        {
            _text.text += l;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
}