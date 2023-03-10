
using System.Diagnostics;
using System.Reflection.Metadata;

namespace CPR_PatcherTool
{
    internal sealed class CPRAnnoHandler : hkannoHandler
    {
        private string? output;

        public string? Output { get => output; }

        private void InsertAnnotations(List<CPRAnnotation> annoArr)
        {
            Func<string, List<CPRAnnotation>, bool> shouldWrite = (string line, List<CPRAnnotation> annoArr) =>
            {
                foreach (var anno in annoArr)
                {
                    if (line.Contains(anno.prefix))
                        return false;
                }

                return true;
            };

            var reader = new StringReader(AnnoStr);
            var writer = new StringWriter();
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (shouldWrite(line, annoArr))
                    writer.WriteLine(line);
            }

            foreach (var anno in annoArr)
            {
                var cprLine = anno.ToString();
                if (!string.IsNullOrEmpty(cprLine))
                    writer.WriteLine(cprLine);
            }

            AnnoStr = writer.ToString();
        }

        public bool UpdateCPRAnnotations(string fileName, CPRAnnotation anno)
        {
            return UpdateCPRAnnotations(fileName, new List<CPRAnnotation> { anno });
        }

        public bool UpdateCPRAnnotations(string fileName, List<CPRAnnotation> CPR_annoArr)
        {
            bool result = false;

            if (CPR_annoArr.Count == 0)
                return result;

            if (ParseAnnoFromFile(fileName))
            {
                InsertAnnotations(CPR_annoArr);
                result = UpdateFileAnno(fileName);
            }

            if (result)
            {
                output += string.Format(Path.GetFileName(fileName) + ":\r\n" + AnnoStr);
                output += "\r\n";
            }
            else
            {
                StreamReader logReader = new StreamReader("hkanno64.log");
                output = logReader.ReadToEnd();
            }

            return result;
        }
    }
}
