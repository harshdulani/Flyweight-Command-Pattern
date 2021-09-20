using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Text countText;
    private void OnEnable()
    {
        TreeGenerator.treesGenerated += OnTreesGenerated;
    }
    
    private void OnDisable()
    {
        TreeGenerator.treesGenerated -= OnTreesGenerated;
    }

    private void OnTreesGenerated(int count)
    {
        countText.text += count;
    }
}
