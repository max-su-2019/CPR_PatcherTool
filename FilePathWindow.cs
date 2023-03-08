using System.Linq;

namespace CPR_PatcherTool
{
    public partial class FilePathWindow : Form
    {

        private Dictionary<Button, Tuple<ListBox, FileType>> filesSelecterMap;

        public FilePathWindow()
        {
            InitializeComponent();
            filesSelecterMap = new Dictionary<Button, Tuple<ListBox, FileType>> {
                { button1, new (listBox1,FileType.weaponEquip) },
                { button2, new (listBox2,FileType.weaponUnequip) },
                { button3, new (listBox3,FileType.SCAR) },
            };
        }

        private void button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var listBox = filesSelecterMap[button].Item1;
            var fileType = filesSelecterMap[button].Item2;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Please Select Animation Files";
            openFileDialog1.Filter = "havok animation files(*.hkx)|*.hkx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox.Items.Clear();
                listBox.Items.Add(openFileDialog1.SafeFileName);
                Program.files[(int)fileType] = openFileDialog1.FileName;
            }
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            foreach (var filePath in Program.files)
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Not File Select!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Close();
        }
    }
}