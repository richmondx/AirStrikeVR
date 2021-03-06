﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AirStrikeKit
{
	public class Mainmeu : MonoBehaviour
	{

		public GUISkin skin;
		public Texture2D Logo;
		public GameObject musicManager;
		private MusicManager musicScript;
        public string[] menuOptions = new string[3];
        public int selectedIndex = 0;
        private bool canInteract = true;
        Camera cam;
        Vector3 screenPos;

        void Start ()
		{
			musicManager = GameObject.Find ("WwiseGlobal");
			musicScript = musicManager.GetComponent<MusicManager> ();
			musicScript.switchMenu ();
			musicScript.playMusic ();
            //AkSoundEngine.SetState ("PlayerLife", "Menu");
            //AkSoundEngine.SetSwitch ("Music", "Menu", uniListener);
            //AkSoundEngine.PostEvent ("PlayMusic", uniListener);
           // cam = GetComponent<Camera>();
            menuOptions[0] = "Classic";
            menuOptions[1] = "Modern";
            menuOptions[2] = "StarFighter";
            /*
            if (FindObjectOfType<EventSystem>() == null)
            {
                var es = new GameObject("EventSystem", typeof(EventSystem));
                es.AddComponent<StandaloneInputModule>();
            }

            var canvasObject = new GameObject("Canvas");
            var canvas = canvasObject.AddComponent<Canvas>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;

            var buttonObject = new GameObject("Button");
            var image = buttonObject.AddComponent<Image>();
            image.transform.parent = canvas.transform;
            image.rectTransform.sizeDelta = new Vector2(180, 50);
            image.rectTransform.anchoredPosition = Vector3.zero;
            image.color = new Color(1f, .3f, .3f, .5f);

            var button = buttonObject.AddComponent<Button>();
            button.targetGraphic = image;
            button.onClick.AddListener(() => Debug.Log(Time.time));

            var textObject = new GameObject("Text");
            textObject.transform.parent = buttonObject.transform;
            var text = textObject.AddComponent<Text>();
            text.rectTransform.sizeDelta = Vector2.zero;
            text.rectTransform.anchorMin = Vector2.zero;
            text.rectTransform.anchorMax = Vector2.one;
            text.rectTransform.anchoredPosition = new Vector2(.5f, .5f);
            text.text = "Button!";
            text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
            text.fontSize = 20;
            text.color = Color.yellow;
            text.alignment = TextAnchor.MiddleCenter;
            */
        }
		private void Awake()
		{
            selectedIndex = 0;
		}
		void DetectUIandHandleCursor()
        {
            if (gameObject.activeSelf == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        int menuSelection(string[] menuItems, int selectedItem, string direction)
        {
            if (direction == "up")
            {
                if (selectedItem == 0)
                {
                    selectedItem = menuItems.Length - 1;
                }
                else
                {
                    selectedItem -= 1;
                }
            }

            if (direction == "down")
            {
                if (selectedItem == menuItems.Length - 1)
                {
                    selectedItem = 0;
                }
                else
                {
                    selectedItem += 1;
                }
            }

            return selectedItem;
        }
        void Update()
        {
            //print("value of joy stick is Y " + Input.GetAxis("Mouse Y"));
            //print("value of joy stick is YVR" + Input.GetAxis("Mouse YVR"));


            if (Input.GetAxis("Mouse Y") == -0.7f && canInteract == true)
            {
                canInteract = false;
                selectedIndex = menuSelection(menuOptions, selectedIndex, "down");

                StartCoroutine(MenuChange(.33f));
            }

            if (Input.GetAxis("Mouse Y") == 0.7f && canInteract == true)
            { 
                canInteract = false;
                selectedIndex = menuSelection(menuOptions, selectedIndex, "up");
                StartCoroutine(MenuChange(.33f));
            }

            if (Input.GetKeyDown("joystick button 0"))
            {
                handleSelection();
               
            } 

        }
        void handleSelection()
        {
            GUI.FocusControl(menuOptions[selectedIndex]);


                switch (selectedIndex)
            {
                case 0:
                    loadClassic();
                    break;
                case 1:
                    loadModern();
                    break;
                case 2:
                    loadStarFighter();
                    break;
                
                default:
                    Debug.Log("None of the above selected..");
                    break;
            }
        }
        void loadClassic()
        {
            musicScript.switchClassic();
            musicScript.currentMode = 1;



            SceneManager.LoadScene("Classic");
        }
        void loadModern()
        {
            musicScript.switchModern();
            musicScript.currentMode = 2;

           

            SceneManager.LoadScene("Modern");
        }
        void loadStarFighter()
        {
            musicScript.switchstarFighter();
            musicScript.currentMode = 3;
           
           

            SceneManager.LoadScene("StarFighter");
        }
		public void OnGUI ()
		{
            cam = Camera.main;
            if (skin)
				GUI.skin = skin;
		
			//GUI.DrawTexture (new Rect (Screen.width / 2 - Logo.width / 2, Screen.height / 2 - 150, Logo.width, Logo.height), Logo);
            //GUI.SetNextControlName("Classic");
            if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 50, 300, 40), "Classic")) {
				//print ("chosen classic mode");
				//AkSoundEngine.SetSwitch ("Music", "Classic", uniListener);
                //Application.LoadLevel ("Classic");
				musicScript.switchClassic ();
				musicScript.currentMode = 1;


              
               SceneManager.LoadScene("Classic");
			}
            GUI.SetNextControlName("Modern");
            if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 40), "Modern")) {
				//AkSoundEngine.SetSwitch ("Music", "Modern", uniListener);
				musicScript.switchModern ();
				musicScript.currentMode = 2;
				//Application.LoadLevel ("Modern");

               
                SceneManager.LoadScene("Modern");
			}

            GUI.SetNextControlName("StarFighter");
            if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 150, 300, 40), "StarFighter")) {
				//AkSoundEngine.SetSwitch ("Music", "Starfighter", uniListener);
				musicScript.switchstarFighter ();
				musicScript.currentMode = 3;


				//Application.LoadLevel ("StarFighter");
                SceneManager.LoadScene("StarFighter");
			}
			/*if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 150, 300, 40), "Invasion")) {
				Application.LoadLevel ("Invasion");
			}*/
			/*if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 200, 300, 40), "Star Fighter")) {
				Application.LoadLevel ("StarFighter");
			} */
		
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect (0, Screen.height - 90, Screen.width, 50), "Air strike starter kit. Visual - Rachan Neamprasert - Audio - Richard PT Lai");
            GUI.FocusControl(menuOptions[selectedIndex]);
			//GUI.Label (new Rect (0, Screen.height - 90, Screen.width, 50), "Air strike starter kit. by Rachan Neamprasert\n www.hardworkerstudio.com");
		}
        IEnumerator MenuChange(float time)
        {
            

            yield return new WaitForSeconds(time);  // I suggest decreasing the time here. One second for each button is quite a long time, which I'm sure you already know.
            canInteract = true;
        } 
	}
}