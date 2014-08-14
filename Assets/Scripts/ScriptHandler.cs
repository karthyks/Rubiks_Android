using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScriptHandler : MonoBehaviour {

    // 3D Matrix to Store Cube Positions
    GameObject[, ,] startRubiks;
    GameObject[, ,] currRubiks;

    public float cubePosition = 0.3048f;

    //For cube Rotation we have to create a Pivot point and the axis of rotation
    GameObject Pivot;
    Vector3 axis;

    // For storing the Pivot's Child we need a List.
    List<GameObject> children;

	void Awake () {
        startRubiks = new GameObject[3, 3, 3];
        currRubiks = new GameObject[3, 3, 3];
        children = new List<GameObject>();
	}

    void Start()
    {
        BuildMatrix(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void RotationType(string rotationType)
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        Pivot = new GameObject("Pivot");
        Pivot.transform.position = Vector3.zero;

        foreach (GameObject cube in cubes)
        {
            if (rotationType == "Rubix")
            {
                cube.transform.parent = Pivot.transform;
            }
            else
            {
                if (rotationType == "Top")
                {
                    if (cube.transform.position.y > 0.1)
                    {
                        cube.transform.parent = Pivot.transform;
                    }
                }
                if(rotationType == "Bottom")
                {
                    if (cube.transform.position.y < -0.1)
                    {
                        cube.transform.parent = Pivot.transform;
                    }
                }

                if (rotationType == "Rhs")
                {
                    if (cube.transform.position.x > 0.1)
                    {
                        cube.transform.parent = Pivot.transform;
                    }
                }
                if(rotationType == "Lhs")
                {
                    if (cube.transform.position.x < -0.1)
                    {
                        cube.transform.parent = Pivot.transform;
                    }
                }
            }
        } 
    }

    void SetAxis(Vector3 setAxis)
    {
        axis = setAxis;
    }
    IEnumerator Rotate(float direction)
    {
        children.Clear();
        for (int i = 0; i < 3; i++)
        {
            Pivot.transform.RotateAround(Pivot.transform.position, axis, direction);
            yield return new WaitForSeconds(0.05f);
        }

        foreach (Transform child in Pivot.transform)
        {
            children.Add(child.gameObject);
        }
        Pivot.transform.DetachChildren();
        Destroy(Pivot);
        ResetParent();
    }

    void ResetParent()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach (GameObject child in children)
        {
            foreach (GameObject cube in cubes)
            {
                if (child.name == cube.name)
                {
                    double x = Mathf.Floor(child.transform.position.x * 100) / 100;
                    double y = Mathf.Floor(child.transform.position.y * 100) / 100;
                    double z = Mathf.Floor(child.transform.position.z * 100) / 100;
                    cube.transform.position = new Vector3((float)(Math.Round(x, 1)), (float)(Math.Round(y, 1)), (float)(Math.Round(z, 1)));
                }
            }
        }

        BuildMatrix(false);
        CheckResult();
    }

    void BuildMatrix(bool reset)
    {
        int l = 0;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Cube");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (reset)
                    {
                        startRubiks[i, j, k] = objects[l];
                        currRubiks[i, j, k] = objects[l];
                        l++;
                    }
                    else
                    {
                        currRubiks[i, j, k] = objects[l];
                        l++;
                    }
                }
            }
        }
    }


    void CheckResult()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (startRubiks[i, j, k].transform.position == currRubiks[i, j, k].transform.position)
                    {
                        Debug.Log("Ok");
                    }
                }
            }
        }
    }
}
