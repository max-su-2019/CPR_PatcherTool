
namespace CPR_PatcherTool
{
    internal class SCARAnnoHandler : hkannoHandler
    {
        public List<string> GetSCARActionData(in string ifileName)
        {
            var result = new List<string>();
            if (ParseAnnoFromFile(ifileName) && !string.IsNullOrEmpty(AnnoStr))
            {
                var reader = new StringReader(AnnoStr);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    const string prefix = "SCAR_ActionData";
                    var index = line.IndexOf(prefix);
                    if (index != -1)
                    {
                        result.Add(line.Substring(index + prefix.Length));
                    }
                }
            }

            UpdateLog(ifileName, result.Count > 0);

            return result;
        }
    }
}
