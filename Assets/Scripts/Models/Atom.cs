using System;

public class Atom
{

    public string predicate { get; set; }
    public string[] args { get; set; }

    public Atom(string atom_str)
    {
        // Construct atom from string
        string[] splt = atom_str.Split('(');
        if (splt.Length < 1)
        {
            predicate = "true";
            args = new string[0];
            return;
        }
        else if (splt.Length < 2)
        {
            string pred_str = splt[0].Trim();
            if (pred_str.Length < 1)
            {
                pred_str = "true";
            }
            predicate = pred_str;
            args = new string[0];
            return;
        }
        string rest = splt[1];
        rest = rest.TrimEnd('\r', '\n', ')');
        rest = rest.TrimEnd(')');
        predicate = splt[0];
        args = rest.Split(',');
    }

    public Atom(string npred, string[] nargs)
    {
        predicate = npred;
        args = nargs;
    }

    public override string ToString()
    {
        if (args.Length > 0)
        {
            return predicate + "(" + String.Join(",", args) + ")";
        }
        else
        {
            return predicate;
        }
    }

}