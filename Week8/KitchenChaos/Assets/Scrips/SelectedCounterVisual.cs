using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [FormerlySerializedAs("clearCounter")] [SerializeField] private BaseCounter baseCounter;
    [FormerlySerializedAs("visualGameObject")] [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        // this is pretty simple 
        // if it is the same then
        if (e.selectedCounter == baseCounter) {
            // this will show the child which is the faded one 
            Show();
        } else {
            // else it will not show and will stay regular
            Hide();
        }
    }

    private void Show() {
        foreach (GameObject visualGameObject in visualGameObjectArray) {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide() {
        foreach (GameObject visualGameObject in visualGameObjectArray) {
            visualGameObject.SetActive(false);
        }
    }
}
