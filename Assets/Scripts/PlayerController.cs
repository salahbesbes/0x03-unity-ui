using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed = 50f;
	public Rigidbody rb;
	private int score = 0;
	public int health = 5;

	public Text scoreText;
	public Text healthText;
	public Image winLoseImage;

	private void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case "Pickup":
				score++;
				SetScoreText();
				Destroy(other.gameObject); // destroy

				break;

			case "Trap":
				health--;
				SetHealthText();
				break;

			case "Goal":
				DisplayWinPanel();
				StartCoroutine(LoadScene(3));
				break;

			default:
				return;
		}
	}

	private void FixedUpdate()
	{
		float currentXPosition = Input.GetAxis("Horizontal");
		float currentZPosition = Input.GetAxis("Vertical");

		Vector3 dir = new Vector3(currentXPosition, 0, currentZPosition).normalized;

		Vector3 force = dir * speed * Time.deltaTime;

		rb.AddForce(force);
	}

	private void Update()
	{
		if (health == 0)
		{
			DisplayLosePanel();
			StartCoroutine(LoadScene(3));
		}
	}

	private void SetScoreText()
	{
		scoreText.text = $"Score {score}";
	}

	private void SetHealthText()
	{
		healthText.text = $"Health: {health}";
	}

	private void DisplayWinPanel()
	{
		Text winLoseText = winLoseImage.GetComponentInChildren<Text>();
		winLoseText.text = "You Win!";
		winLoseText.color = Color.black;

		winLoseImage.color = Color.green;

		winLoseImage.gameObject.SetActive(true);
	}

	private void DisplayLosePanel()
	{
		Text winLoseText = winLoseImage.GetComponentInChildren<Text>();
		winLoseText.text = "Game Over!";
		winLoseText.color = Color.white;

		winLoseImage.color = Color.red;

		winLoseImage.gameObject.SetActive(true);
	}

	private IEnumerator LoadScene(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		health = 5;
		score = 0;
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
}