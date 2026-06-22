using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropPiecesM1 : MonoBehaviour, IDropHandler
{
    [SerializeField] private AudioSource CorrectSound;
    [SerializeField] private AudioSource WrongSound;

    public AudioSource WrongSound1 { get => WrongSound; set => WrongSound = value; }

    public void OnDrop(PointerEventData eventData)
    {
        // Se não houver nada a ser arrastado, ignora
        if (eventData.pointerDrag == null) return;

        // Tenta obter o componente PuzzlePiece do objeto que foi largado
        var collisionElement = eventData.pointerDrag.GetComponent<PuzzlePieceM1>();
        if (collisionElement == null) return;

        // Verifica se o Target Image da peça coincide com o nome desta sombra
        if (collisionElement.targetImage.name == this.gameObject.name)
        {
            // Acertou: Torna a sombra 100% visível (opacidade máxima)
            var currentColor = this.GetComponent<Image>().color;
            currentColor.a = 1f;
            this.GetComponent<Image>().color = currentColor;

            // Toca o som de acerto, se configurado
            if (CorrectSound != null) CorrectSound.Play();

            // Destrói a peça arrastável (já que agora a sombra ficou visível no lugar correto)
            Destroy(collisionElement.gameObject, 0f);
            
            // Incrementa o contador no GameManager
            GameManagerM1.IncrementRightAnswer();
        }
        else
        {
            // Errou: Devolve a peça à posição original
            collisionElement.ResetImage();
            
            // Toca o som de erro, se configurado
            if (WrongSound != null) WrongSound.Play();
            
            // Incrementa os erros
            GameManagerM1.IncrementWrongAnswer();
        }
    }
}