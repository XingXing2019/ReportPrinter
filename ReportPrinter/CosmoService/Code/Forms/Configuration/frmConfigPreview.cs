using System.Windows.Forms;

namespace CosmoService.Code.Forms.Configuration
{
    public partial class frmConfigPreview : Form
    {
        public frmConfigPreview(string preview)
        {
            InitializeComponent();

            txtPreview.Text = preview;
        }
    }
}
