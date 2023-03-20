using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Boa
{
    public class DataReader
    {
        public static List<Project> readDataFromFile(string path)
        {
            string lines = File.ReadAllText(path);
            return readDataFromString(lines);
        }
        
        public static List<Project> readDataFromString(string s)
        {
            List<Project> ProjectList = new List<Project>();

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            string currentID = null;
            string currentName = null;
            string currentURL = null;
            int currentNumberCommitters = 0;
            int currentNumberRevisions = 0;
            List<Committer> currentCommitters = new List<Committer>();

            foreach (string line in lines)
            {
                if (line.StartsWith("topProjects[]"))
                {
                    // Save current project
                    if (currentID != null)
                    {
                        ProjectList.Add(new Project(currentID, currentName, currentURL, currentNumberCommitters, currentNumberRevisions, currentCommitters));
                    }

                    // Parse new project
                    string[] parts = line.Split(new char[] { ',', '=' }, 5);
                    currentID = parts[1].Trim();
                    currentName = parts[2].Trim();
                    currentURL = parts[3].Trim();
                    currentNumberRevisions = int.Parse(parts[4].Trim());
                    currentCommitters = new List<Committer>();
                }
                else if (line.Length > 0)
                {
                    if (line.StartsWith(","))
                    {
                        currentNumberCommitters = int.Parse(line.Substring(line.IndexOf(',') + 1, line.IndexOf('.') - 1));
                    }
                    else
                    {
                        // Parse committe
                        //Debug.Log(line);
                        string[] parts = line.Split(new string[]{"###"} , System.StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            string name = parts[0].Trim();
                            //Debug.Log(parts[0]);
                            //Debug.Log(parts[1]);
                            int numberCommits = int.Parse(parts[1].Trim());
                            currentCommitters.Add(new Committer(name, numberCommits));
                        }
                    }          
                }
            }

            // Save last project
            if (currentID != null)
            {
                ProjectList.Add(new Project(currentID, currentName, currentURL, currentNumberCommitters, currentNumberRevisions, currentCommitters));
            }

            return ProjectList;
        }
    }
    
}