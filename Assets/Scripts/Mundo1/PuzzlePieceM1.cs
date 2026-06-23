using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePieceM1 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image targetImage;

    private Vector2 _startPosition; // Guarda a posição ancorada inicial
    private RectTransform _rectTransform;
    private Canvas _myCanvas;
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        // CORREÇÃO 1: Usar anchoredPosition em vez de position para interfaces UI (Canvas)
        _startPosition = _rectTransform.anchoredPosition;
        
        _myCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _myCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        
        // SE NÃO ENCAIXOU (se o DropPiecesM1 não a destruir), ela volta automaticamente para trás
        // Isto ajuda caso o utilizador largue a peça no meio do nada (fora de qualquer sombra)
        Invoke(nameof(ResetImage), 0.1f);
    }

    public void ResetImage()
    {
        // Se a peça ainda existir (não foi destruída por acertar na sombra)
        if (this != null && gameObject != null)
        {
            // CORREÇÃO 2: Devolver à anchoredPosition inicial
            _rectTransform.anchoredPosition = _startPosition;
        }
    }
}