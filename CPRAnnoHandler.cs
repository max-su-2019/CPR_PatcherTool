
namespace CPR_PatcherTool
{
    internal sealed class CPRAnnoHandler : hkannoHandler
    {
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
            if (CPR_annoArr.Count == 0)
                return false;

            if (ParseAnnoFromFile(fileName))
            {
                InsertAnnotations(CPR_annoArr);
                return UpdateFileAnno(fileName);
            }

            return false;
        }
    }
}
