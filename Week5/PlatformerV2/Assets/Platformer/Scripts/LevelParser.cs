﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * This script is responsible for reading a level layout from a text file and constructing the level
 * in a Unity scene by instantiating block GameObjects. The level file should be placed in the
 * Resources folder, and each line in the file represents a row of blocks (with the bottom row processed first).
 *
 * WHAT YOU NEED TO DO:
 * 1. In the foreach loop that iterates over each character (letter) in the current row, determine
 *    which type of block to create based on the letter (e.g., use 'R' for rock, 'B' for brick, etc.).
 *
 * 2. Instantiate the correct prefab (rockPrefab, brickPrefab, questionBoxPrefab, stonePrefab) corresponding
 *    to the letter.
 *
 * 3. Calculate the position for the new block GameObject using the current 'row' value and its column index.
 *    - You will likely need to maintain a separate column counter as you iterate through the characters.
 *
 * 4. Set the instantiated block’s parent to 'environmentRoot' to keep the hierarchy organized.
 *
 * ADDITIONAL NOTES:
 * - The level reloads when the player presses the 'R' key, which clears all blocks under environmentRoot
 *   and then re-parses the level file.
 * - Ensure that the level file's name (without the extension) matches the 'filename' variable.
 *
 * By completing these TODOs, you will enable the level parser to dynamically create and position
 * the blocks based on the level file data.
 */

public class LevelParser : MonoBehaviour
{
    public string filename;
    public Transform environmentRoot;

    [Header("Block Prefabs")]
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject testPrefab;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ReloadLevel();
    }

    // --------------------------------------------------------------------------
    void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}/Resources/{filename}.txt";
        //Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new(fileToParse))
        {
            while (sr.ReadLine() is { } line)
                levelRows.Push(line);
            sr.Close();
        }

        // Use this variable in the todo code!!
        int row = 0;

        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int col = 0; col < letters.Length; ++col)
            {
                Vector3 pos = new Vector3(col + 0.5f, row + 0.5f, 0f);
                GameObject newObj = null;
                
                switch (letters[col])
                {
                    case 'x': // Test block
                        newObj = Instantiate(testPrefab, environmentRoot);
                        break;
                    case 'r': // Rock block
                        newObj = Instantiate(rockPrefab, environmentRoot);
                        break;
                    case 'b': // Brick block
                        newObj = Instantiate(brickPrefab, environmentRoot);
                        break;
                    case '?': // Question box
                        newObj = Instantiate(questionBoxPrefab, environmentRoot);
                        break;
                    case 's': // Stone block
                        newObj = Instantiate(stonePrefab, environmentRoot);
                        break;
                    case ' ': // Empty space, do nothing
                        continue;
                    default:
                        Debug.LogWarning($"Unrecognized character '{letters[col]}' in level file at row {row}, col {col}");
                        continue;
                }
                
                if (newObj != null)
                {
                    newObj.transform.position = pos;
                }
             
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
           Destroy(child.gameObject);

        LoadLevel();
    }
}
