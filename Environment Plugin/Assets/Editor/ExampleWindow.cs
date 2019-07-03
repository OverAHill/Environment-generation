using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MapWindow : EditorWindow
{
    float mTreeDensity = 0.0f;
    int mMapSize = 0;
    int mRiverCount = 0;
    int[,,] mTileMap;

    Stack<GameObject> mTiles = new Stack<GameObject>();
    Stack<GameObject> mTrees = new Stack<GameObject>();
    Stack<GameObject> mDecor = new Stack<GameObject>();
    Stack<GameObject> mTents = new Stack<GameObject>();
    List<int> mRiver = new List<int>();

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

            mTileMap = new int[mMapSize * 10, mMapSize * 10, 2];

            for (int i = 0; i < mMapSize * 10; i++) //generate map
            {
                for (int j = 0; j < mMapSize * 10; j++)
                {
                    mTileMap[i, j, 0] = 0;
                }
            }

            Vector2 startPos;
            Vector2 midPos;
            Vector2 endPos;
            





            GameObject newTile;
            mRiverCount = 1;

            //For the base tiles and river // the map starts at 0, 0, 0 and goes out (not centered)
            for (int i = 0; i < mMapSize * 10; i++)
            {
                currentPos.x = i * 3;

                for(int j = 0; j < mMapSize * 10; j++)
                {
                    currentPos.z = j * 3;

                    if (mTileMap[i, j, 0] == 0)
                    {
                        newTile = Instantiate(Resources.Load("Prefabs/BasePlate", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        mTiles.Push(newTile);
                    }
                    else
                    {
                        newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        mTiles.Push(newTile);

                        //int inFront;
                        //int behind;

                        
                        //if (mTileMap[i, j, 1] == 1 || mTileMap[i, j, 1] == 0) //only one check (in front)
                        //{
                        //    switch (mRiver.IndexOf(mTileMap[i, j, 1] + 1))
                        //    {
                        //        case 1:
                        //            newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //            mTiles.Push(newTile);
                        //            break;

                        //        case 2:
                        //            newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //            mTiles.Push(newTile);
                        //            break;

                        //        case 3:
                        //            newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //            mTiles.Push(newTile);
                        //            break;
                        //    }

                        //}
                        //else // two check (front and back)
                        //{
                        //    inFront = mRiver.IndexOf(mTileMap[i, j, 1] + 1);
                        //    behind = mRiver.IndexOf(mTileMap[i, j, 1] - 1);

                        //    switch (inFront)
                        //    {
                        //        case 1:
                        //            switch (mRiver.IndexOf(mTileMap[i, j, 1]))
                        //            {
                        //                case 1:
                        //                    newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //                    mTiles.Push(newTile);
                        //                    break;

                        //                case 2:
                        //                    newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //                    mTiles.Push(newTile);
                        //                    break;

                        //                case 3:
                        //                    newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //                    mTiles.Push(newTile);
                        //                    break;
                        //            }
                        //            break;

                        //        case 2:
                        //            switch (mRiver.IndexOf(mTileMap[i, j, 1]))
                        //            {
                        //                case 1:
                        //                    newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //                    mTiles.Push(newTile);
                        //                    break;

                        //                case 2:
                        //                    newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //                    mTiles.Push(newTile);
                        //                    break;

                        //                case 3:
                        //                    newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //                    mTiles.Push(newTile);
                        //                    break;
                        //            }
                        //            break;

                        //        case 3:
                        //            switch (mRiver.IndexOf(mTileMap[i, j, 1]))
                        //            {
                        //                case 1:
                        //                    newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //                    mTiles.Push(newTile);
                        //                    break;

                        //                case 2:
                        //                    newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //                    mTiles.Push(newTile);
                        //                    break;

                        //                case 3:
                        //                    newTile = Instantiate(Resources.Load("Prefabs/BaseRiver", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                        //                    mTiles.Push(newTile);
                        //                    break;
                        //            }
                        //            break;
                        //    }

                        //}


                        mRiverCount++;
                    }
                    
                }
            }

           

            
            //For The trees /// now in tile form
            for (int k = 0; k < mTreeDensity; k++)
            {
                currentPos.x = Random.Range(0, mMapSize * 10); // in tile map
                currentPos.z = Random.Range(0, mMapSize * 10); 

                while (mTileMap[(int)currentPos.x, (int)currentPos.y, 0] != 0)  
                {
                    currentPos.x = Random.Range(0, mMapSize * 10);
                    currentPos.z = Random.Range(0, mMapSize * 10);
                }

                mTileMap[(int)currentPos.x, (int)currentPos.z, 0] = 1;

                currentPos *= 3;

                GameObject newTree = Instantiate(Resources.Load("Prefabs/BaseTree", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                mTrees.Push(newTree);
            }

            //CampFires 1 per 10 x 10 section ///tiled
            for(int i = 0; i < mMapSize * mMapSize; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 10);
                currentPos.z = Random.Range(0, mMapSize * 10);
                currentPos.y = 0.3f;

                while (mTileMap[(int)currentPos.x, (int)currentPos.z, 0] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 10);
                    currentPos.z = Random.Range(0, mMapSize * 10);
                }

                mTileMap[(int)currentPos.x, (int)currentPos.z, 0] = 1;
                currentPos *= 3;
                currentPos.y /= 3;
                GameObject newFire = Instantiate(Resources.Load("Prefabs/CampFire", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                mDecor.Push(newFire);
            }

            //Tents ///tiled
            for (int i = 0; i < mMapSize * mMapSize * 2; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 10);
                currentPos.z = Random.Range(0, mMapSize * 10);

                int yRot = Random.Range(1, 360);

                while (mTileMap[(int)currentPos.x, (int)currentPos.z, 0] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 10);
                    currentPos.z = Random.Range(0, mMapSize * 10);
                }

                mTileMap[(int)currentPos.x, (int)currentPos.z, 0] = 1;
                currentPos *= 3;
                currentPos.y /= 3;

                GameObject newTent = Instantiate(Resources.Load("Prefabs/Tent", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                newTent.transform.Rotate(new Vector3(0, yRot, 0));

                mDecor.Push(newTent);
            }

            //Rocks ///tiled
            for (int i = 0; i < mMapSize * mMapSize; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 10);
                currentPos.z = Random.Range(0, mMapSize * 10);

                int yRot = Random.Range(1, 360);

                while (mTileMap[(int)currentPos.x, (int)currentPos.z, 0] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 10);
                    currentPos.z = Random.Range(0, mMapSize * 10);
                }

                mTileMap[(int)currentPos.x, (int)currentPos.z, 0] = 1;
                currentPos *= 3;
                currentPos.y /= 3;

                GameObject newRock = Instantiate(Resources.Load("Prefabs/BaseRock", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                newRock.transform.Rotate(new Vector3(0, yRot, 0));

                mDecor.Push(newRock);
            }

            //Grass //tiled
            for (int i = 0; i < mMapSize * mMapSize * 25; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 10);
                currentPos.z = Random.Range(0, mMapSize * 10);

                int yRot = Random.Range(1, 360);

                while (mTileMap[(int)currentPos.x, (int)currentPos.z, 0] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 10);
                    currentPos.z = Random.Range(0, mMapSize * 10); 
                }

                mTileMap[(int)currentPos.x, (int)currentPos.z, 0] = 1;
                currentPos *= 3;
                currentPos.y /= 3;

                GameObject newGrass = Instantiate(Resources.Load("Prefabs/BaseGrass", typeof(GameObject)) as GameObject, currentPos, Quaternion.identity);
                newGrass.transform.Rotate(new Vector3(0, yRot, 0));

                mDecor.Push(newGrass);
            }

            //Flowers ///tiled
            for (int i = 0; i < mMapSize * mMapSize * 6; i++)
            {
                currentPos.x = Random.Range(0, mMapSize * 30 - 3);
                currentPos.z = Random.Range(0, mMapSize * 30 - 3);

                int yRot = Random.Range(1, 360);

                while (mTileMap[(int)currentPos.x, (int)currentPos.z, 0] != 0)
                {
                    currentPos.x = Random.Range(0, mMapSize * 10);
                    currentPos.z = Random.Range(0, mMapSize * 10);
                }

                mTileMap[(int)currentPos.x, (int)currentPos.z, 0] = 1;
                currentPos *= 3;
                currentPos.y /= 3;

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
