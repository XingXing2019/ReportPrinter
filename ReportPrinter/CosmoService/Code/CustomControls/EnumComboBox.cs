using System;
using System.Windows.Forms;
using ReportPrinterLibrary.Code.Helper;

namespace CosmoService.Code.CustomControls
{
    public partial class EnumComboBox : ComboBox
    {
        private Type _enumType;

        public Type EnumType
        {
            get => _enumType;
            set
            {
                _enumType = value;
                if (_enumType != null)
                {
                    this.DataSource = Enum.GetValues(_enumType);
                }
            }
        }

        public EnumComboBox()
        {
            InitializeComponent();

            this.FormattingEnabled = true;
            this.Format += EnumComboBox_Format;
        }

        private void EnumComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = ((Enum)e.Value).ToDescription();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
