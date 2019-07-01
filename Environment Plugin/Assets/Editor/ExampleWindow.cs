using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MapWindow : EditorWindow
{
    float mTreeDensity = 0.0f;
    int mMapSize = 0;
    int mRiverCount = 0;
    int[,] mTileMap;

    Stack<GameObject> mTiles = new Stack<GameObject>();
    Stack<GameObject> mTrees = new Stack<GameObject>();
    Stack<GameObject> mRiver = new Stack<GameObject>();
    Stack<GameObject> mDecor = new Stack<GameObject>();
    Stack<GameObject> mTents = new Stack<GameObject>();


    [MenuItem("Window/Map Gen")]
    public static void ShowWindow()
    {
        GetWindow<MapWindow>("Map Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Select Map Size", EditorStyles.boldLabel);
        mMapSize = EditorGUILayout.IntSlider(mMapSize, 1, 10);

        GUILayout.Label("Select River Amount", EditorStyles.boldLabel);
        mRiverCount = EditorGUILayout.IntSlider(mRiverCount, 1, 2);

        GUILayout.Label("Select Tree Density", EditorStyles.boldLabel);
        mTreeDensity = EditorGUILayout.Slider(mTreeDensity, 1, 10);
        
        if (GUILayout.Button("Generate Map"))
        {
            Vector3 currentPos;
            currentPos = new Vector3(0, 0, 0);

            mTileMap = new int[mMapSize * 10, mMapSize * 10];

            for (int i = 0; i < mMapSize * 10; i++) //generate map
            {
                for (int j = 0; j < mMapSize * 10; j++)
                {
                    mTileMap[i, j] = 0;
                }
            }

            int startPos = Random.Range(0, mMapSize*10 - 1);   
            mTileMap[startPos, 0] = 1;
            bool endFound = false;
            Vector2 currentArrPos = new Vector2(startPos, 1);

            //Go to an edge
            while (!endFound)
            {
                int choice = Random.Range(1, 4);
                //mTileMap[(int)currentArrPos.x, (int)currentArrPos.y] = 1;

                switch (choice)
                {
                    case 1:
                        if (currentArrPos.x - 1 > 0)
                        {
                            if (mTileMap[(int)currentArrPos.x - 1, (int)currentArrPos.y] == 0)
                            {
                                currentArrPos.x--;
                                mTileMap[(int)currentArrPos.x, (int)currentArrPos.y] = 1;
                            }
                        }
                        else
                        {
                            endFound = true;
                        }
 
                        break;

                    case 2:
                        if (currentArrPos.y + 1 < mMapSize * 10)
                        {
                            if (mTileMap[(int)currentArrPos.x, (int)currentArrPos.y + 1] == 0)
                            {
                                currentArrPos.y++;
                                mTileMap[(int)currentArrPos.x, (int)currentArrPos.y] = 2;
                            }
                        }
                        else
                        {
                            endFound = true;
                        }
                            
                        break;

                    case 3:
                        if (currentArrPos.x + 1 < mMapSize * 10)
                        {
                            if (mTileMap[(int)currentArrPos.x + 1, (int)currentArrPos.y] == 0)
                            {
                                currentArrPos.x++;
                                mTileMap[(int)currentArrPos.x, (int)currentArrPos.y] = 3;
                            }
                        }
                        else
                        {
                            endFound = true;
                        }
                            

                        break;
                }

                Debug.Log(choice);

               
            }



            //For the base tiles and river // the map starts at 0, 0, 0 and goes out (not centered)
            for (int i = 0; i < mMapSize * 10; i++)
            {
                currentPos.x = i * 3;

                for(int j = 0; j < mMapSize * 10; j++)
                {
                    if(mTileMap[i, j] == 0)
                    {
                        currentPos.z = j * 3;
                        GameObject newTile = Instantiate(Resources.Load("Prefabs/BasePlate", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        mTiles.Push(newTile);
                    }
                    else
                    {
                        currentPos.z = j * 3;
                        GameObject newTile;
                        switch (mTileMap[i, j])
                        {
                            case 1:
                                newTile = Instantiate(Resources.Load("Prefabs/OneRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                                mTiles.Push(newTile);
                                break;

                            case 2:
                                newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                                mTiles.Push(newTile);
                                break;

                            case 3:
                                newTile = Instantiate(Resources.Load("Prefabs/ThreeRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                                mTiles.Push(newTile);
                                break;
                             
                        }
                       
                        
                    }
                    
                }
            }

            
            //For The trees
            for (int k = 0; k < mMapSize * mTreeDensity * 10; k++)
            {
                currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                currentPos.z = Random.Range(0, mMapSize * 30 - 3); 

                foreach (GameObject obj in mTrees)
                {
                    if (Mathf.Abs(obj.transform.position.x - currentPos.x) < 3)
                    {
                        if (Mathf.Abs(obj.transform.position.y - currentPos.y) < 3)
                        {
                            currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                            currentPos.z = Random.Range(0, mMapSize * 30 - 3);
                        }
                    }
                }

                Vector3 scaledPos;
                scaledPos.x = (currentPos.x + 3) / 3;
                scaledPos.y = (currentPos.y + 3) / 3;

                while (mTileMap[(int)scaledPos.x, (int)scaledPos.y] != 0)  
                {
                    currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                    currentPos.z = Random.Range(0, mMapSize * 30 - 3);

                    scaledPos.x = (currentPos.x + 3) / 30;
                    scaledPos.y = (currentPos.y + 3) / 30;
                } 
                
                GameObject newTree = Instantiate(Resources.Load("Prefabs/BaseTree", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                mTrees.Push(newTree);
            }

            //For The river


            //CampFires 1 per 10 x 10 section
            for(int i = 0; i < mMapSize * mMapSize; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                currentPos.z = Random.Range(0, mMapSize * 30 - 3);
                currentPos.y = 0.3f;
                Vector3 scaledPos;
                scaledPos.x = (currentPos.x + 3) / 3;
                scaledPos.y = (currentPos.y + 3) / 3;

                while (mTileMap[(int)scaledPos.x, (int)scaledPos.y] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                    currentPos.z = Random.Range(0, mMapSize * 30 - 3);
                    scaledPos.x = (currentPos.x + 3) / 3;
                    scaledPos.y = (currentPos.y + 3) / 3;
                }

                GameObject newFire = Instantiate(Resources.Load("Prefabs/CampFire", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                mDecor.Push(newFire);
            }

            //Tents
            for (int i = 0; i < mMapSize * mMapSize * 2; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                currentPos.z = Random.Range(0, mMapSize * 30 - 3);

                int yRot = Random.Range(1, 360);

                Vector3 scaledPos;
                scaledPos.x = (currentPos.x + 3) / 3;
                scaledPos.y = (currentPos.y + 3) / 3;

                while (mTileMap[(int)scaledPos.x, (int)scaledPos.y] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                    currentPos.z = Random.Range(0, mMapSize * 30 - 3);
                    scaledPos.x = (currentPos.x + 3) / 3;
                    scaledPos.y = (currentPos.y + 3) / 3;
                }


                GameObject newTent = Instantiate(Resources.Load("Prefabs/Tent", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                newTent.transform.Rotate(new Vector3(0, yRot, 0));

                mDecor.Push(newTent);
            }

            //Rocks
            for (int i = 0; i < mMapSize * mMapSize; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                currentPos.z = Random.Range(0, mMapSize * 30 - 3);

                int yRot = Random.Range(1, 360);


                Vector3 scaledPos;
                scaledPos.x = (currentPos.x + 3) / 3;
                scaledPos.y = (currentPos.y + 3) / 3;

                while (mTileMap[(int)scaledPos.x, (int)scaledPos.y] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                    currentPos.z = Random.Range(0, mMapSize * 30 - 3);
                    scaledPos.x = (currentPos.x + 3) / 3;
                    scaledPos.y = (currentPos.y + 3) / 3;
                }

                GameObject newRock = Instantiate(Resources.Load("Prefabs/BaseRock", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                newRock.transform.Rotate(new Vector3(0, yRot, 0));

                mDecor.Push(newRock);
            }

            //Grass
            for (int i = 0; i < mMapSize * mMapSize * 25; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                currentPos.z = Random.Range(0, mMapSize * 30 - 3);

                int yRot = Random.Range(1, 360);


                Vector3 scaledPos;
                scaledPos.x = (currentPos.x + 3) / 3;
                scaledPos.y = (currentPos.y + 3) / 3;

                while (mTileMap[(int)scaledPos.x, (int)scaledPos.y] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                    currentPos.z = Random.Range(0, mMapSize * 30 - 3);
                    scaledPos.x = (currentPos.x + 3) / 3;
                    scaledPos.y = (currentPos.y + 3) / 3;
                }

                GameObject newGrass = Instantiate(Resources.Load("Prefabs/BaseGrass", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                newGrass.transform.Rotate(new Vector3(0, yRot, 0));

                mDecor.Push(newGrass);
            }

            //Flowers
            for (int i = 0; i < mMapSize * mMapSize * 6; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                currentPos.z = Random.Range(0, mMapSize * 30 - 3);

                int yRot = Random.Range(1, 360);


                Vector3 scaledPos;
                scaledPos.x = (currentPos.x + 3) / 3;
                scaledPos.y = (currentPos.y + 3) / 3;

                while (mTileMap[(int)scaledPos.x, (int)scaledPos.y] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                    currentPos.z = Random.Range(0, mMapSize * 30 - 3);
                    scaledPos.x = (currentPos.x + 3) / 3;
                    scaledPos.y = (currentPos.y + 3) / 3;
                }

                GameObject newFlower = Instantiate(Resources.Load("Prefabs/BaseFlower", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                newFlower.transform.Rotate(new Vector3(0, yRot, 0));

                mDecor.Push(newFlower);
            }

            Debug.Log("Button Pressed");
        }

        if (GUILayout.Button("Reset Map"))
        {
            while(mTiles.Count > 0)
            {
                GameObject obj = mTiles.Pop();
                DestroyImmediate(obj.gameObject);
            }

            while (mTrees.Count > 0)
            {
                GameObject obj = mTrees.Pop();
                DestroyImmediate(obj.gameObject);
            }

            while (mDecor.Count > 0)
            {
                GameObject obj = mDecor.Pop();
                DestroyImmediate(obj.gameObject);
            }
        }
    }
}
