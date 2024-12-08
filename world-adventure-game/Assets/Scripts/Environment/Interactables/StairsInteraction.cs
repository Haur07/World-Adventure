using UnityEngine;

public class StairsInteraction : MonoBehaviour
{
    // Por enquanto apenas utilizados para objetos do jogo não ficar no player
    [SerializeField] public GameObject interaction;
    [SerializeField] public GameObject blockage;

    private void Awake()
    {
        interaction.SetActive(false);
        blockage.SetActive(false);
    }
}
