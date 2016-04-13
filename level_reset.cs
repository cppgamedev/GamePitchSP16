 using UnityEngine;
 using UnityEngine.SceneManagement;
 using System.Collections;
 
 public class Entity : MonoBehaviour {
 
	public float health;
	public GameObject player;
	
	public void TakeDamage(float dmg) {
		health -= dmg;
		
		if(health <= 0) {
			Die();
		}
	}
	
	public void Die() {
		// add timer so level doesn't reset right away?
		// Destroy(this.GameObject); This will destroy script too?
		// Application.LoadLevel(Application.loadedLevel); Now obsolete
		 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}