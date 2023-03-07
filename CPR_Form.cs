using CPR_PatcherTool;

namespace WindowsFormsApp1
{
    public partial class CPR_Form : Form
    {
        public CPR_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CPRAnnoHandler annoHandler = new CPRAnnoHandler();
            annoHandler.UpdateCPRAnnotations("2hc_equip.hkx", new List<CPRAnnotation> {
                new("CPR.EnableAdvance", groupBoxAdvance),
                new("CPR.EnableBackoff", groupBoxBackoff),
                new("CPR.EnableCircling", groupBoxCircling),
                new("CPR.EnableFallback", groupBoxFallback)
            });

            CPRAnnoHandler annoHandler2 = new CPRAnnoHandler();
            annoHandler2.UpdateCPRAnnotations("2hc_unequip.hkx", new CPRAnnotation("CPR.DisableAll"));
        }


    }
}
