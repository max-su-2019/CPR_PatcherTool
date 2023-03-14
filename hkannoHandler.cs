using System.Runtime.InteropServices;

internal class hkannoHandler
{
    [DllImport("hkanno64.dll", CallingConvention = CallingConvention.StdCall,
    CharSet = CharSet.Ansi, EntryPoint = "GetAnnotations")]
    private static extern bool GetAnnotations(string ifilename, IntPtr output, int strlen);

    [DllImport("hkanno64.dll", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Ansi, EntryPoint = "GetAnnotations2")]
    private static extern IntPtr GetAnnotations(string ifilename);

    [DllImport("hkanno64.dll", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Ansi, EntryPoint = "UpdateAnnotations")]
    private static extern bool UpdateAnnotations(string anno, string ofilename);

    [DllImport("hkanno64.dll", CallingConvention = CallingConvention.StdCall,
        CharSet = CharSet.Ansi, EntryPoint = "FreeMemory")]
    private static extern void FreeMemory(IntPtr ptr);

    private string? annoStr;

    public string AnnoStr
    {
        protected set { annoStr = value; }
        get { return annoStr; }
    }

    private string? output;

    public string? Output { get => output; }

    public hkannoHandler() { }

    public hkannoHandler(in string a_annoStr)
    {
        annoStr = a_annoStr;
    }

    public hkannoHandler(in hkannoHandler a_annoHandler)
    {
        annoStr = a_annoHandler.annoStr;
    }

    public bool ParseAnnoFromFile(in string ifileName)
    {
        IntPtr output = GetAnnotations(ifileName);
        if (output == IntPtr.Zero)
            return false;

        annoStr = Marshal.PtrToStringAnsi(output);
        FreeMemory(output);
        if (string.IsNullOrEmpty(annoStr))
        {
            annoStr = string.Empty;
            return false;
        }

        return true;
    }

    public bool UpdateFileAnno(in string ofileName)
    {
        if (string.IsNullOrEmpty(annoStr))
            return false;

        return UpdateAnnotations(annoStr, ofileName);
    }

    protected void UpdateLog(in string fileName, in bool result)
    {
        if (result)
        {
            output += (Path.GetFileName(fileName) + ":\r\n" + AnnoStr);
            output += "\r\n";
        }
        else
        {
            StreamReader logReader = new StreamReader("hkanno64.log");
            output = logReader.ReadToEnd();
        }
    }
}

