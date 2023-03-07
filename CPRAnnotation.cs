
namespace CPR_PatcherTool
{
    internal class CPRAnnotation
    {
        public readonly string prefix;
        public readonly float localTime;
        public readonly List<float> paramArr = new List<float>();

        public CPRAnnotation(string prefix, float localTime = 0.06F)
        {
            this.prefix = prefix;
            this.localTime = localTime;
        }

        public CPRAnnotation(string prefix, GroupBox groupBox, float localTime = 0.06F)
        {
            this.prefix = prefix;
            this.localTime = localTime;
            foreach (var ctrl in groupBox.Controls)
            {
                if (ctrl is TextBox)
                {
                    var textBox = (TextBox)ctrl;
                    float value;
                    if (!string.IsNullOrEmpty(textBox.Text) && float.TryParse(textBox.Text, out value))
                    {
                        paramArr.Add(value);
                    }
                    else
                        break;
                }
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(prefix) && (paramArr.Count() > 0 || prefix == "CPR.DisableAll"))
            {
                string output = string.Format("{0:F6}", localTime) + " " + prefix;
                foreach (var value in paramArr)
                {
                    output += "|" + value;
                }

                return output;
            }

            return string.Empty;
        }
    }
}
