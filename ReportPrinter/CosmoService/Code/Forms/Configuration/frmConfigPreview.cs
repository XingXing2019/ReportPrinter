using System.Windows.Forms;

namespace CosmoService.Code.Forms.Configuration
{
    public partial class frmConfigPreview : Form
    {
        public frmConfigPreview(string preview, string title = null)
        {
            InitializeComponent();
            Text = !string.IsNullOrEmpty(title) ? title : "Config Preview";
            txtPreview.Text = preview;
        }
    }
}
