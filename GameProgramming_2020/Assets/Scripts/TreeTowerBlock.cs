﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIDCITY
{
    public class TreeTowerBlock : MonoBehaviour
    {

        #region Fields
        public BuildingProfile myProfile;
        private GameObject treePrefab;
        public int recursionLevel = -1;
        private int maxLevel = 3;
        private CityManager cityManager;
        private Renderer myRenderer;
        private MeshFilter myMeshFilter;
        private Mesh myMesh;
        private Material myMaterial;
        #endregion

        #region Properties	
        #endregion

        #region Methods

        public void SetProfile(BuildingProfile profile)
        {
            myProfile = profile;
            maxLevel = myProfile.maxHeight;
        }

        public void Initialize(int recLevel, Material mat, Mesh mesh)
        {
            recursionLevel = recLevel;
            myMesh = mesh;
            myMaterial = mat;
            maxLevel = myProfile.maxHeight;
            
        }

        #region Unity Methods

        // Use this for internal initialization
        void Awake()
        {
            myRenderer = GetComponent<MeshRenderer>();
            myMeshFilter = GetComponent<MeshFilter>();
        }

        // Use this for external initialization
        void Start()
        {

            int x = Mathf.RoundToInt(transform.position.x + 7.0f);
            int y = Mathf.RoundToInt(transform.position.y);
            int z = Mathf.RoundToInt(transform.position.z + 7.0f);
            cityManager = CityManager.Instance;
            treePrefab = cityManager.treePrefab;
            GameObject child;
            if (recursionLevel == 0)                     //initial foundation block
            {
                if (!cityManager.CheckSlot(x, y, z))
                {
                    cityManager.SetSlot(x, y, z, true);
                    int meshNum = myProfile.groundBlocks.Length;
                    int matNum = myProfile.groundMaterials.Length;
                    myMesh = myProfile.groundBlocks[Random.Range(0, meshNum)];
                    myMaterial = myProfile.groundMaterials[Random.Range(0, matNum)];
                    
                }
                else
                {
                    GameObject.Destroy(gameObject);
                }
            }

            myMeshFilter.mesh = myMesh;
            myRenderer.material = myMaterial;

            if (recursionLevel < maxLevel)          
            {
                if (recursionLevel == maxLevel - 1)    //the next segment placed will be a roof
                {
                    if (!cityManager.CheckSlot(x, y + 1, z))
                    {
                        cityManager.SetSlot(x, y + 1, z, true);
                        child = Instantiate(treePrefab, transform.position + Vector3.up * 1.01f, Quaternion.identity, this.transform);                      
                        int meshNum = myProfile.roofBlocks.Length;
                        int matNum = myProfile.roofMaterials.Length;
                        Debug.Log(child.GetComponents<TreeTowerBlock>().Length);
                        child.GetComponent<TreeTowerBlock>().Initialize(recursionLevel + 1, myProfile.roofMaterials[Random.Range(0, matNum)], myProfile.roofBlocks[Random.Range(0, meshNum)]);


                    }
                }
                else     //the next segment placed will be a main body segment
                {
                    int random = Random.Range(0, 10);
                    if ((y<5)||(random<6))     //the low altitude vertical tower section
                    {
                        if (!cityManager.CheckSlot(x, y+1, z)) //building upward
                        {
                            cityManager.SetSlot(x, y + 1, z, true);
                            child = Instantiate(treePrefab, transform.position + Vector3.up * 1.01f, Quaternion.identity, this.transform);                        
                            int meshNum = myProfile.mainBlocks.Length;
                            int matNum = myProfile.mainMaterials.Length;
                            child.GetComponent<TreeTowerBlock>().Initialize(recursionLevel + 1, myProfile.mainMaterials[Random.Range(0, matNum)], myProfile.mainBlocks[Random.Range(0, meshNum)]);
                        }
                    }
                    else       //here we start branching off our tree tower
                    {
                        //
                        //
                        //MODIFY THE CODE BELOW


                        random = Random.Range(0, 10);
                        if ((random<5)&&(!cityManager.CheckSlot(x, y+1, z))) //building east (+x direction)
                        {
                            cityManager.SetSlot(x, y+1, z, true);
                            child = Instantiate(treePrefab, transform.position + Vector3.up * 1.01f, Quaternion.identity, this.transform);                        
                            int meshNum = myProfile.mainBlocks.Length;
                            int matNum = myProfile.mainMaterials.Length;
                            child.GetComponent<TreeTowerBlock>().Initialize(recursionLevel + 1, myProfile.mainMaterials[Random.Range(0, matNum)], myProfile.mainBlocks[Random.Range(0, meshNum)]);                        
                        };

                        random = Random.Range(0, 10);
                        if ((random<5)&&(!cityManager.CheckSlot(x, y+1, z))) //building north (+z direction)
                        {
                            cityManager.SetSlot(x, y+1, z, true);
                            child = Instantiate(treePrefab, transform.position + Vector3.up * 1.01f, Quaternion.identity, this.transform);                          
                            int meshNum = myProfile.mainBlocks.Length;
                            int matNum = myProfile.mainMaterials.Length;
                            child.GetComponent<TreeTowerBlock>().Initialize(recursionLevel + 1, myProfile.mainMaterials[Random.Range(0, matNum)], myProfile.mainBlocks[Random.Range(0, meshNum)]);
                        };

                        random = Random.Range(0, 10);
                        if ((random<5)&&(!cityManager.CheckSlot(x-1, y+5, z))) //building west (-x direction)
                        {
                            cityManager.SetSlot(x-1, y+1, z, true);
                            child = Instantiate(treePrefab, transform.position + Vector3.up * 1.01f, Quaternion.identity, this.transform);
                            int meshNum = myProfile.mainBlocks.Length;
                            int matNum = myProfile.mainMaterials.Length;
                            child.GetComponent<TreeTowerBlock>().Initialize(recursionLevel + 1, myProfile.mainMaterials[Random.Range(0, matNum)], myProfile.mainBlocks[Random.Range(0, meshNum)]);
                            if (x < 5)
                            {
                                new GameObject("Tree Child").AddComponent<TreeTowerBlock>().
                                    Initialize(recursionLevel + 5, myProfile.mainMaterials[Random.Range(0, matNum)], myProfile.mainBlocks[Random.Range(0, meshNum)]);
                            }
                        };
                        
                        random = Random.Range(0, 10);
                        if ((random<5)&& (!cityManager.CheckSlot(x, y+1, z))) //building south (-z direction)
                        {
                            cityManager.SetSlot(x, y+1, z, true);
                            child = Instantiate(treePrefab, transform.position + Vector3.up * 1.01f, Quaternion.identity, this.transform);                          
                            int meshNum = myProfile.mainBlocks.Length;
                            int matNum = myProfile.mainMaterials.Length;
                            child.GetComponent<TreeTowerBlock>().Initialize(recursionLevel + 1, myProfile.mainMaterials[Random.Range(0, matNum)], myProfile.mainBlocks[Random.Range(0, meshNum)]);
                        };

                        //END MODIFY CODE
                        //
                        //


                        random = Random.Range(0, 10);
                        if ((random < 5) && (!cityManager.CheckSlot(x, y+1, z))) //building upward (+y direction)
                        {
                            cityManager.SetSlot(x, y + 1, z, true);
                            child = Instantiate(treePrefab, transform.position + Vector3.up * 1.01f, Quaternion.identity, this.transform);
                            int meshNum = myProfile.mainBlocks.Length;
                            int matNum = myProfile.mainMaterials.Length;
                            child.GetComponent<TreeTowerBlock>().Initialize(recursionLevel + 1, myProfile.mainMaterials[Random.Range(0, matNum)], myProfile.mainBlocks[Random.Range(0, meshNum)]);
                            
                        };
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        }

        #endregion
        #endregion

    }
}

