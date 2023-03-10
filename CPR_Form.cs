using CPR_PatcherTool;

namespace WindowsFormsApp1
{
    public partial class CPR_Form : Form
    {
        public CPR_Form()
        {
            InitializeComponent();
            SCARAnnoHandler handler = new SCARAnnoHandler();
            var refData = handler.GetSCARActionData(Program.files[(int)FileType.SCAR]);
            foreach (var item in refData)
            {
                listBox1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CPRAnnoHandler annoHandler = new CPRAnnoHandler();
            if (annoHandler.UpdateCPRAnnotations(Program.files[(int)FileType.weaponEquip], new List<CPRAnnotation> {
                new("CPR.EnableAdvance", groupBoxAdvance),
                new("CPR.EnableBackoff", groupBoxBackoff),
                new("CPR.EnableCircling", groupBoxCircling),
                new("CPR.EnableFallback", groupBoxFallback)
               })
            )
            {
                if (annoHandler.UpdateCPRAnnotations(Program.files[(int)FileType.weaponUnequip], new CPRAnnotation("CPR.DisableAll")))
                {
                    MessageBoxWithDetails.Show("Patched CPR Moveset Successfully, Click the \"Details\" button to check file annotations", "Result", annoHandler.Output);
                    return;
                }
            }

            MessageBoxWithDetails.Show("Error: Patched CPR Moveset Fail!", "Result", annoHandler.Output);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count > 0)
            {
                this.toolTip1.Active = true;

                this.toolTip1.SetToolTip(this.listBox1, this.listBox1.Items[this.listBox1.SelectedIndex].ToString());
            }
            else
            {
                this.toolTip1.Active = false;
            }
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index != -1 && index < listBox1.Items.Count)
            {

                if (toolTip1.GetToolTip(listBox1) != listBox1.Items[index].ToString())
                {

                    toolTip1.SetToolTip(listBox1, listBox1.Items[index].ToString());
                }
            }
        }

    }
}
