using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text;


public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject level1_setting;
        //[SerializeField] private GameObject level1_2;
        //[SerializeField] private GameObject level1_3;
        //[SerializeField] private GameObject level1_4;
        //[SerializeField] private GameObject level1_5;
        //[SerializeField] private GameObject level1_6;
        [SerializeField] private GameObject level2_setting;
        [SerializeField] private GameObject level3_setting;
        [SerializeField] private GameObject toggleObject; // The object you want to toggle
        [SerializeField] private GameObject levelThreeButton; // assign this in the inspector
        [SerializeField] private TextMeshPro numberText;
        [SerializeField] private TextMeshPro stringText;
        [SerializeField] private GameObject[] buttons;  // Assign the buttons in the inspector
        [SerializeField] private List<Vector3> buttonPositions = new List<Vector3>();


        private float startTime;
        private float totalTime;
        private int buttonClickCount;
        private List<string> csvData = new List<string>();
        private string filePath;

        private void Shuffle(List<Vector3> list)
            {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int rnd = Random.Range(0, i);

                Vector3 temp = list[i];
                list[i] = list[rnd];
                list[rnd] = temp;
            }
        }


    /*       private void ShuffleButtons(int levelIndex)
           {
               for (int i = 0; i < buttons.GetLength(1); i++)
               {
                   int rnd = Random.Range(i, buttons.GetLength(1));
                   GameObject tempGO = buttons[levelIndex, rnd];
                   buttons[levelIndex, rnd] = buttons[levelIndex, i];
                   buttons[levelIndex, i] = tempGO;
               }
           }
    */
    // Start is called before the first frame update
    void Start()
        {
            level1_setting.SetActive(false);
            level2_setting.SetActive(false);
            level3_setting.SetActive(false);
            levelThreeButton.SetActive(false); // initially disable the button
            toggleObject.SetActive(false); // initially disable the object
            buttonClickCount = 0;

        // Set the file path
        filePath = Application.persistentDataPath + "/log.csv";
        // Write the header of CSV
        string header = "NumberText,StringText,ToggleObjectStatus,TotalTime,ButtonClickCount";
        csvData.Add(header);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                Debug.Log("Pressed 1");
                ShowLevelOne();
                startTime = Time.time; // record the start time
            }
        }
/*
        public void ShuffleButtons()
        {
            Shuffle(buttonPositions);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.position = buttonPositions[i];
            }
    }
    */
    public void ShowLevelOne()
        {
            Debug.Log("Entered ShowLevelOne");

            //ShuffleButtons();  // Shuffle the order of the buttons for level one

            level1_setting.SetActive(true);
            level2_setting.SetActive(false);
            level3_setting.SetActive(false);
            levelThreeButton.SetActive(false); // disable the button


            buttonClickCount++; // increment the button click count
        }

        public void ShowLevelTwo()
        {
            //ShuffleButtons(1);  // Shuffle the order of the buttons for level two

            level1_setting.SetActive(false);
            level2_setting.SetActive(true);
            level3_setting.SetActive(false);
            levelThreeButton.SetActive(false); // disable the button
            buttonClickCount++; // increment the button click count
        }

        public void ShowLevelThree()
        {
            //ShuffleButtons(2);  // Shuffle the order of the buttons for level two


            level1_setting.SetActive(false);
            level2_setting.SetActive(false);
            level3_setting.SetActive(true);
            levelThreeButton.SetActive(true); // enable the button

            // Set the button's number text to a random number between 1 and 100
            numberText.text = Random.Range(1, 101).ToString();
            Debug.Log("Number Text Changed: " + numberText.text); //    Log the new text value

            string[] options = { "Mom", "Dad" };// Set the button's string text to either "Mom" or "Dad"
            stringText.text = options[Random.Range(0, options.Length)];
            Debug.Log("String Text Changed: " + stringText.text); // Log the new text value
            buttonClickCount++; // increment the button click count

            if (Random.Range(0, 2) == 1)
            {
                toggleObject.SetActive(true);
                Debug.Log(1);
            }
            else
            {
                toggleObject.SetActive(false);
                Debug.Log(0);
            }
            string data = $"{numberText.text},{stringText.text},{toggleObject.activeSelf},{totalTime},{buttonClickCount}";
            csvData.Add(data);
    }

    public void ButtonLog()
    {
        buttonClickCount++; // increment the button click count


    }

    public void CloseLevelThree()
        {
            levelThreeButton.SetActive(false); // disable the button
            totalTime = Time.time - startTime; // calculate the total time
            Debug.Log("Total time: " + totalTime);
            Debug.Log("Button clicks: " + buttonClickCount);
        string data = $"{numberText.text},{stringText.text},{toggleObject.activeSelf},{totalTime},{buttonClickCount}";
        csvData.Add(data);

        }

    private void OnApplicationQuit()
    {
        WriteDataToFile();
    }

    private void WriteDataToFile()
    {
        // Combine all data into a single string
        //string csvString = string.Join("\n", csvData);

        // Write data to the file
        //File.WriteAllText(filePath, csvString);
    }
}
    



