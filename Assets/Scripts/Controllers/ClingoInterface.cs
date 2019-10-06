using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class ClingoInterface : MonoBehaviour {

    static public readonly string runClingoCmd = "asp/clingo.exe";
    static public readonly string workingDir = "asp/";
    static public readonly string tmpASPFile = "tmp.lp";

    public static void populateConceptSet()
    {
        string[] filePaths = Directory.GetFiles(workingDir, "*.lp",
                                         SearchOption.TopDirectoryOnly);
        List<string> tmp = new List<string>();
        foreach (string fp in filePaths)
        {
            string[] fpSplt = fp.Split('/');
            string fn = fpSplt[fpSplt.Length - 1];
            if (fn.StartsWith("concept-"))
            {
                string concept = fn.Split('-')[1].Split('.')[0];
                tmp.Add(concept);
                UnityEngine.Debug.Log("Concept " + concept + " detected.");
            }
        }
        GameManager.instance.conceptSet = tmp.ToArray();
    }

    public static Atom[] findPlanWithGivenConcepts(string[] concepts)
    {
        // Construct ASP program
        string ASPProgram = "";
        foreach(string concept in concepts)
        {
            ASPProgram += System.IO.File.ReadAllText(workingDir + "concept-" + concept + ".lp");
            ASPProgram += "\n";
        }

        ASPProgram += System.IO.File.ReadAllText(workingDir + "planner_didnt_do_hw.lp");

        return solveASPCode(ASPProgram);
    }

    public static Atom[] solveASPCode(string aspCode)
    {
        System.IO.File.WriteAllText(workingDir + tmpASPFile, aspCode);
        string clingoOut = runClingoAndGetOutput(tmpASPFile);
        UnityEngine.Debug.Log("Solving ASP code: " + aspCode);
        UnityEngine.Debug.Log("Result: " + clingoOut);
        System.IO.File.Delete(workingDir + tmpASPFile);
        if (clingoOut.Contains("UNSATISFIABLE"))
        {
            return null;
        }
        string[] splited = clingoOut.Split(new string[] { "Answer: 1" }, System.StringSplitOptions.None);
        string atomsStr = splited[1].Split(new string[] { "SATISFIABLE" }, System.StringSplitOptions.None)[0];
        atomsStr = atomsStr.TrimStart('\r', '\n');
        atomsStr = replaceInnerBrackets(atomsStr, '$');
        UnityEngine.Debug.Log(atomsStr);
        return string2Atoms(atomsStr, ' ');
    }

    public static string replaceInnerBrackets(string str, char replaceWith)
    {
        string tmp = "";
        tmp += str.Substring(0, 4);
        for (int i = 4; i < str.Length - 1; i++) {
            if (str[i] == '(' && str.Substring(i-4, 4) != "exec")
            {
                tmp += replaceWith;
            } else if (str[i] == ')' && str[i+1] == ',')
            {
                tmp += replaceWith;
            } else
            {
                tmp += str[i];
            }
        }

        return tmp;
    }

    public static string runClingoAndGetOutput(string argument)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo(runClingoCmd);
        startInfo.WorkingDirectory = workingDir;
        startInfo.Arguments = argument + " -c m=4";
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardInput = true;
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;

        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit();
        UnityEngine.Debug.Log("clingo run with command: " + startInfo.FileName + " " + startInfo.Arguments + "; Exit Code: " + process.ExitCode);
        UnityEngine.Debug.Log(process.StandardError.ReadToEnd());
        return process.StandardOutput.ReadToEnd();
    }

    public static Atom[] string2Atoms(string clingoOpt, char separator)
    {
        string[] optSplt = clingoOpt.Split(separator);
        List<Atom> tmp = new List<Atom>();
        foreach (string res in optSplt)
        {
            if (res.Length > 2)
            {
                Atom atom = new Atom(res);
                tmp.Add(atom);
            }
        }
        return tmp.ToArray();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
