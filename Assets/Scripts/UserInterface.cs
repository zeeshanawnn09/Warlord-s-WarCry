using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    public WeaponBehavior RocketPrefab;
    public WeaponBehavior CannonPrefab;
    public WeaponBehavior FlamerPrefab;

    public GameObject WeaponMenu;

    public TMPro.TMP_Text Wave_txt;
    public TMPro.TMP_Text Money_txt;
    public TMPro.TMP_Text Lives_txt;
    public TMPro.TMP_Text UpgradeBtn_txt;

    public Button slowBtn;
    public Button fastBtn;
    public Button superfastBtn;

    public AudioSource InsufficientCash_snd;

    private WeaponBehavior _currClickedWeapon;

    GameObject focusObject;
    GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        BtnListener();
    }

    void BtnListener()
    {
        slowBtn.onClick.AddListener(SlowButton);
        fastBtn.onClick.AddListener(FastButton);
        superfastBtn.onClick.AddListener(SuperFastButton);
    }

    public void SlowButton()
    {
        LevelManager.WaveSpeed(1);
    }

    public void FastButton()
    {
        LevelManager.WaveSpeed(5);
    }

    public void SuperFastButton()
    {
        LevelManager.WaveSpeed(10);
    }

    public void RocketButton()
    {
        //check if the player has enough money to build the turret
        if (LevelManager.MoneyGained >= RocketPrefab.WeaponProperties.buildCost)
        {
            itemPrefab = RocketPrefab.gameObject;
            CreateItemOnClick();
            LevelManager.MoneyGained -= RocketPrefab.WeaponProperties.buildCost;
        }
        //if the player doesn't have enough money, play the insufficient cash sound
        else
        {
            InsufficientCash_snd.Play();
        }
    }

    public void CannonButton()
    {
        if (LevelManager.MoneyGained >= CannonPrefab.WeaponProperties.buildCost)
        {
            itemPrefab = CannonPrefab.gameObject;
            CreateItemOnClick();
            LevelManager.MoneyGained -= CannonPrefab.WeaponProperties.buildCost;
        }
        else
        {
            InsufficientCash_snd.Play();
        }
    }

    public void FlamerButton()
    {
        if (LevelManager.MoneyGained >= FlamerPrefab.WeaponProperties.buildCost)
        {
            itemPrefab = FlamerPrefab.gameObject;
            CreateItemOnClick();
            LevelManager.MoneyGained -= FlamerPrefab.WeaponProperties.buildCost;
        }
        else
        {
            InsufficientCash_snd.Play();
        }
    }

    //close the weapon menu
    public void CloseButton_WM()
    {
        WeaponMenu.SetActive(false);
    }

    //destroy the turret
    public void DestroyButton_WM()
    {
        //destroy the turret and give the player half of the money spent on the turret
        Destroy(_currClickedWeapon.gameObject, 0.2f);
        LevelManager.MoneyGained += (int)(_currClickedWeapon.WeaponProperties.buildCost * 0.5f);
        CloseButton_WM();
    }

    //upgrade the turret
    public void UpgradeButton_WM()
    {
        //if the player has enough money, upgrade the turret
        if (LevelManager.MoneyGained >= _currClickedWeapon.WeaponProperties.buildCost)
        {
            //increase the damage of the turret by 20% and double the cost of the turret
            _currClickedWeapon.WeaponProperties.Damage *= 1.2f;
            LevelManager.MoneyGained -= _currClickedWeapon.WeaponProperties.buildCost;
            _currClickedWeapon.WeaponProperties.buildCost *= 2;
            UpgradeBtn_txt.text = "Upgrade: $" + _currClickedWeapon.WeaponProperties.buildCost;
        }
        else
        {
            InsufficientCash_snd.Play();
        }
    }

    //create the turret on the mouse position
    void CreateItemOnClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }
        else
        {
            focusObject = Instantiate(itemPrefab, hit.point, itemPrefab.transform.rotation);
            focusObject.GetComponent<Collider>().enabled = false;
        }
    }

    void MouseBehavior()
    {
        //(if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)) --> for mobile

        //when mouse button is clicked, instantiate the turret
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Weapon"))
            {
                _currClickedWeapon = hit.collider.gameObject.GetComponent<WeaponBehavior>();
                WeaponMenu.transform.position = Input.mousePosition;
                UpgradeBtn_txt.text = "Upgrade: $" + _currClickedWeapon.WeaponProperties.buildCost;
                WeaponMenu.SetActive(true);
            }  
        }

        //(if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))

        //when mouse button is held down, move the turret to the mouse position
        else if (focusObject && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            else
            {
                
                focusObject.transform.position = hit.point;
            }
            }

        //(if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))

        //when mouse button is released make sure the turret is on the platform
        else if (focusObject && Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;

                                                   //(Input.getTouch(0).position)--> for mobile
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if the raycast hits the platform and the normal of the platform is up, place the turret
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Free") && hit.normal.Equals(new Vector3(0, 1, 0)))
            {
                hit.collider.gameObject.tag = "Occupied";
                
                //to place the turret on the centre of the platform
                focusObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x,
                                                             focusObject.transform.position.y,
                                                             hit.collider.gameObject.transform.position.z);

                focusObject.GetComponent<BoxCollider>().enabled = true;
                focusObject.GetComponent<SphereCollider>().enabled = true;
            }
            else
            {
                Destroy(focusObject);
                hit.collider.gameObject.tag = "Free";
                LevelManager.MoneyGained += focusObject.GetComponent<WeaponBehavior>().WeaponProperties.buildCost;
                focusObject = null;
            }
            }
    }

    //display the wave count
    void WaveCount()
    {
        if (LevelManager.WavesEmitted < LevelManager.TotalWaves)
        {
            Wave_txt.text = "Wave: " + (LevelManager.WavesEmitted + 1) + " of " + LevelManager.TotalWaves;
        }
    }

    //display the money gained
    void MoneyUpdater()
    {
        Money_txt.text = "Money: $ " + LevelManager.MoneyGained;
    }

    void LivesUpdater()
    {
        if (LevelManager.MaxLives >= 0)
        {
            Lives_txt.text = "Lives: " + LevelManager.MaxLives;
        }
    }

    public void RestartLevel()
    {
         LevelManager.totalEnemies = 0;
         LevelManager.TotalWaves = 3;
         LevelManager.WavesEmitted = 0;
         LevelManager.MoneyGained = 200;
         LevelManager.MaxLives = 10;

         LevelManager.LvlOver = false;
         LevelManager.nxtWave = false;

         SceneManager.LoadScene("Level 01", LoadSceneMode.Single);
    }

    public void QuitLevel()
    {
        //Stop the game
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {
        LivesUpdater();
        MoneyUpdater();
        WaveCount();
        MouseBehavior();
    }


}
