using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerMovement thePlayer;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] platformList;

    private ScoreManager theScoreManager;

    public DeathMenu theDeathScreen;

	public GameObject theDeathOverlay; // overlay na kojem se Ramon smije Damonu

	public int PlayerLives = 10;  // broj života na početku, bazirano na nultom broju, u igri prikazuje kao +1 da se izbjegne Lives: 0 jer to glupo izgleda
	public int LivesFromRewardVideo; // broj života koji igrač dobiva od pogledane reklame
	public Text LivesLeft; // broj života preostalih igraču, ne vidi se u igri, služi za račun u skripti
	public float WaitSecondsOnDeath; // trajanje overlaya kad igrač pogine, mjenjanje trajanja može desinkronizirati animaciju, potrebno prilagoditi novo trajanje u traniziciju između Ramon_idle i Ramon_laugh u animatoru
	public GameObject LivesShow; // GameObject koji sadrži Lives: tekst i prikazuje ga na ekranu, napravljen je tako da ga je lakše isključit/uključit kroz skriptu

	// Use this for initialization
	void Start () 
    {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayer.transform.position;

        theScoreManager = FindObjectOfType<ScoreManager>();
		LivesShow.gameObject.SetActive(true); // pokazuje broj života
		LivesLeft.text = "LIVES: " + (PlayerLives + 1); // mjenja tekst u Lives :, nadodaje 1 da se izbjegne Lives: 0


	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    
    //za restartat igru
    public void restartGame()
    {
        theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive(false);		


		//PlayerLives = PlayerLives - 1;

		if (PlayerLives >= 1) 
		{
			Invoke("DeathWait", WaitSecondsOnDeath); //pokreće blok koda za respawn ali sa zadanim delayem u sekundama
			LivesLeft.text = "LIVES: " + ((PlayerLives + 1) - 1); // ne pitaj me kak ova jednadžba funkcionira jer ni meni nije jasno, ali jedino tako prikazuje točan broj života (my code works, I don't know why)
			theDeathOverlay.gameObject.SetActive(true); // stavlja overlay na kojem Ramon urla Damonu


        //theDeathScreen.gameObject.SetActive(true);
		}
//		else if (PlayerLives == 4) 
//		{
//			Invoke("DeathWait", WaitSecondsOnDeath);
//			LivesLeft.text = "LIVES: 4";
//			theDeathOverlay.gameObject.SetActive(true);
//		}
//
//		else if (PlayerLives == 3) 
//			{
//			Invoke("DeathWait", WaitSecondsOnDeath);
//			LivesLeft.text = "LIVES: 3";
//			theDeathOverlay.gameObject.SetActive(true);
//		}
//		else if (PlayerLives == 2) 
//		{
//			Invoke("DeathWait", WaitSecondsOnDeath);
//			LivesLeft.text = "LIVES: 2";
//			theDeathOverlay.gameObject.SetActive(true);
//		}
//			else if (PlayerLives == 1) 
//			{
//			Invoke("DeathWait", WaitSecondsOnDeath);
//			LivesLeft.text = "LIVES: 1";
//			theDeathOverlay.gameObject.SetActive(true);
//			}
		else if (PlayerLives == 0) 
		{
			theScoreManager.scoreIncreasing = false;
			thePlayer.gameObject.SetActive(false);
			theDeathScreen.gameObject.SetActive(true);
			PlayerLives = PlayerLives; // najgluplja linija koju sam ikad napisao, ali iz nekog razloga potrebna
			LivesLeft.gameObject.SetActive(false); // gasi prikaz Lives:

		}
        //StartCoroutine("RestartGameCo");
    }

    public void Reset()
    {
		theDeathScreen.gameObject.SetActive(false);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;

        thePlayer.gameObject.SetActive(true);
		LivesLeft.gameObject.SetActive(true);
		theDeathOverlay.gameObject.SetActive(false);
		PlayerLives = 10 ; // kad igrač stisne replay, mjenja početan broj života sa životima koje se zaradi pogledanom reklamom i dalje oni vode priču
		LivesLeft.text = "LIVES: " + (PlayerLives); // something.dll


        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }

    /*public IEnumerator RestartGameCo()
    {
        theScoreManager.scoreIncreasing = false;

        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

            thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;

        thePlayer.gameObject.SetActive(true);

        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }*/

	//IEnumerator DeathWait() 
	//{
		//yield return new WaitForSeconds(3);
	//}

	public void DeathWait () 
	{
		theDeathScreen.gameObject.SetActive(false);
		platformList = FindObjectsOfType<PlatformDestroyer>();
		for (int i = 0; i < platformList.Length; i++)
		{
			platformList[i].gameObject.SetActive(false);
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;

		thePlayer.gameObject.SetActive(true);
		theDeathOverlay.gameObject.SetActive(false);

		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
		PlayerLives = PlayerLives -1;
		//that's all folks
	}


}

