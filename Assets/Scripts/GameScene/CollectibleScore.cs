using UnityEngine;
using TMPro;

public class CollectibleScore : MonoBehaviour
{
    public int _score;
    public TextMeshProUGUI _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*public void OnTriggerEnter(Collider other)
    {
        Collectible _collectible = other.GetComponent<Collectible>();

        switch (_collectible) {
            case _collectible1:
                print("Placeholder");
                _score += 10;
                _scoreText.text = _score.ToString();
                break;
            case _collectible2:
                print("Placeholder");
                _score += 20;
                _scoreText.text = _score.ToString();
                break;
            case _collectible3:
                print("Placeholder");
                _score += 30;
                _scoreText.text = _score.ToString();
                break;
            case _collectible4:
                print("Placeholder");
                _score += 40;
                _scoreText.text = _score.ToString();
                break;
        }
    }*/
}
