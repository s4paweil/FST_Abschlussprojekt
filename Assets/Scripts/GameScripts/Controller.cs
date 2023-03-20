using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Boa;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace GameScripts
{

    public class Controller: MonoBehaviour
    {
        public TextAsset textFile;
        public GameObject committerPrefab;
        public TextMeshPro textMeshPro;
        private List<Project> _projects;
        private int _index;
        private GameObject _parent;
        private GameObject _prefabEmpty;
        private bool _colored;
        public ToolTip tooltip;

        Controller()
        {
            this._projects = new List<Project>();
            this._index = 0;
            this._colored = true;
        }

        public void Read()
        {
            this._projects = DataReader.readDataFromString(textFile.text);
            Show();

        }
        

        public void Clear()
        {
            GameObject.Destroy(this._parent);
            this._parent = new GameObject();
            this._parent.name = this._projects[_index].Name;
            this._parent.AddComponent<ObjectManipulator>();
        }

        public void Right()
        {
            this._index = ++this._index%this._projects.Count;
            Show();
        }
        
        public void Left()
        {
            this._index = (this._index+this._projects.Count-1)%this._projects.Count;
            Show();
        }

        public void Show()
        {
            var current = this._projects[this._index];
            
            textMeshPro.text = "ID               " + current.Id + "\n" +
                               "Name         " + current.Name + "\n" +
                               "Url              " + current.Url + "\n" +
                               "Ges Rev      " + current.NumberRevisions;
            Clear();
            foreach (var c in current.Committer)
            {
                var commiterP = Instantiate(committerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                commiterP.transform.parent = this._parent.transform;
                commiterP.name = c.Name;
                commiterP.GetComponent<ComitterData>().setCommiter(c);
                commiterP.GetComponent<ComitterData>().setToolTop(tooltip);
                  
                var relativeAmountCommitts = ((float)c.NumberCommitts / (float)this._projects[this._index].NumberRevisions);

                commiterP.transform.localScale = new Vector3(commiterP.transform.localScale.x + relativeAmountCommitts * 10.0f, commiterP.transform.localScale.y + relativeAmountCommitts * 10.0f, commiterP.transform.localScale.z + relativeAmountCommitts * 10.0f);
            }

            positionOnFibSphere();
            
            // Color it

            colorIt();

        }


        public void switchColorize()
        {
            this._colored = !this._colored;
            colorIt();
        }

        private void colorIt()
        {
            for (int i = 0; i < this._parent.transform.childCount; i++)
            {
                //Debug.Log(this._parent.transform.childCount);
                if (this._colored)
                {
                    this._parent.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                }
                else
                {
                    if (this._parent.transform.GetChild(i).GetComponent<ComitterData>().getCommitter().NumberCommitts ==
                        1)
                    {
                        this._parent.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", new Color(117f/255f,215f/255f,255f/255f));
                    }
                    //Debug.Log(((float)this._parent.transform.GetChild(i).GetComponent<ComitterData>().getCommitter().NumberCommitts / (float)this._projects[this._index].NumberRevisions));
                    if (((float)this._parent.transform.GetChild(i).GetComponent<ComitterData>().getCommitter().NumberCommitts / (float)this._projects[this._index].NumberRevisions) > 0.05f)
                    {
                        this._parent.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                    }
                }
            }
        }


        private void Start()
        {
            
            Read();
            
        }

        public void positionOnFibSphere()
        {
            for (int i = 0; i < this._parent.transform.childCount; i++)
            {
                this._parent.transform.GetChild(i).transform.position = FibSphere(i, this._parent.transform.childCount);
            }
        }
        
        private Vector3 FibSphere(int i, int n)
        {
            var radius = 0.5f;
            var k = i + .5f;

            var phi = Mathf.Acos(1f - 2f * k / n);
            var theta = Mathf.PI * (1 + Mathf.Sqrt(5)) * k;

            var x = Mathf.Cos(theta) * Mathf.Sin(phi);
            var y = Mathf.Sin(theta) * Mathf.Sin(phi);
            var z = Mathf.Cos(phi);

            return new Vector3(x, y, z) * radius;
        }
    }
}