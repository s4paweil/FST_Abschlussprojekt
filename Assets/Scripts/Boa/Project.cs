using System.Collections.Generic;
using UnityEngine;

namespace Boa
{
    public class Project
    {
        private string id, name, url;
        private List<Committer> committer;
        private int numberCommitters;
        private int numberRevisions;

        public string Id
        {
            get => id;
            set => id = value;
        }
        
        public List<Committer> Committer
        {
            get => committer;
            set => committer = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Url
        {
            get => url;
            set => url = value;
        }

        public int NumberRevisions
        {
            get => numberRevisions;
            set => numberRevisions = value;
        }
        
        public int NumberCommitters
        {
            get => numberCommitters;
            set => numberCommitters = value;
        }

        public Project(string id, string name, string url, int numberCommitters, int numberRevisions)
        {
            this.id = id;
            this.name = name;
            this.url = url;
            this.committer = new List<Committer>();
            this.numberCommitters = numberCommitters;
            this.numberRevisions = numberRevisions;
        }

        public Project(string id, string name, string url, int numberCommitters, int numberRevisions, List<Committer> committer)
        {
            this.id = id;
            this.name = name;
            this.url = url;
            this.committer = committer;
            this.numberCommitters = numberCommitters;
            this.numberRevisions = numberRevisions;
        }

        public Project()
        {
            this.id = "";
            this.name = "";
            this.url = "";
            this.committer = new List<Committer>();
            this.numberCommitters = 0;
            this.numberRevisions = 0;
        }

        public override string ToString()
        {
            string output = "Id: " + id + ", Name: " + name + ", URL: " + url + ", Number Committers: " +
                            numberCommitters + ", Number Revisions: " + numberRevisions + "\n";

            foreach (Committer committer in committer)
            {
                output += committer.ToString() + "\n";
            }

            return output;
        }


    }
}