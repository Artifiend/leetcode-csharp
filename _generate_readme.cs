
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class ReadmeGenerator
{
    private static void Main()
    {
        var sb = new StringBuilder();
        var dir = Directory.GetCurrentDirectory();
        var problemRe = new Regex(@"^\d+\.[a-zA-Z ]+$");
        var probNumRe = new Regex(@"^\d+");
        var probTitleRe = new Regex(@"[a-zA-Z ]+$");
        
        foreach (string file in Directory.GetFiles(dir))
        {
            string filename = Path.GetFileName(file);
            
            if(Path.GetExtension(filename) != ".cs" || filename.StartsWith("_")) {
                continue;
            }
            
            var tag = filename.Substring(0, filename.Length - 3);
            var prettyName = tag.Split('-').Aggregate("", (acc, s) => acc + ' ' + Char.ToUpper(s[0]) + s.Substring(1));
            
            sb.AppendLine($"## [{ prettyName }](https://leetcode.com/tag/{ tag }/)");
            sb.AppendLine("| # | Title | Source |");
            sb.AppendLine("|:-:| ----- |:------:|");
            
            int lineNum = -1;
            bool linkFlag = false;
            
            foreach (string rawLine in File.ReadAllLines(file))
            {
                ++lineNum;
                string line = rawLine.TrimStart(new char[] { ' ', '*' });
                
                if(linkFlag) {
                    sb.Append($"({ line }) | ");
                    sb.AppendLine($"[(click)](https://github.com/Artifiend/leetcode-csharp/blob/master/{ filename }#L{ lineNum })");
                    linkFlag = false;
                    continue;
                }
                
                if(problemRe.IsMatch(line)) {
                    string probNum = probNumRe.Match(line).Value;
                    string probTitle = probTitleRe.Match(line).Value.TrimStart(' ');
                    
                    sb.Append($"{ probNum } | [{ probTitle }]");
                    linkFlag = true;
                }
            }
        }
        
        File.WriteAllText("README.md", sb.ToString());
    }
}