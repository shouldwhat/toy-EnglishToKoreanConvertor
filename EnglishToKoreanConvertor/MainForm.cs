using System;
using System.Threading;
using System.Windows.Forms;

namespace EnglishToKoreanConvertor
{
    public partial class MainForm : Form
    {
        globalKeyboardHook gkh = new globalKeyboardHook();

        ISearchingService searchingService = SearchingService.getInstance();

        public MainForm()
        {
            InitializeComponent();
            InitializeEventHandler();
        }

        private void InitializeEventHandler()
        {
            gkh.HookedKeys.Add(Keys.F2);
            gkh.KeyDown += Gkh_KeyDown;
        }

        private void Gkh_KeyDown(object sender, KeyEventArgs e)
        {
            this.showTooltip();
        }

        private bool showTooltip()
        {
            string keyword = this.getKeyword();

            if (keyword == null || "".Equals(keyword))
            {
                return false;
            }

            string meaning = searchingService.searchMeaning(keyword);

            return this.showTooltip(meaning);
        }

        private bool showTooltip(string meaning)
        {
            this.Focus();

            toolTip.Hide(this);
            toolTip.Show(meaning, this, MousePosition);
    
            return toolTip.Active;
        }

        private string getKeyword()
        {
            string keyword = Clipboard.GetText();

            return this.validateKeyword(keyword);
        }

        private string validateKeyword(string keyword)
        {
            return ((keyword == null) ? "" : keyword.Trim(new char[] { '.', ',' }));
        }


        #region '사용법' 메뉴 이벤트
        HelpForm helpForm = null;
        private void 사용법ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(helpForm == null)
            {
                helpForm = new HelpForm();
                helpForm.FormClosed += HelpForm_FormClosed;
                helpForm.Show();
            }
        }

        private void HelpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            helpForm = null;
        }
        #endregion
        #region '종료' 메뉴 이벤트
        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

    }
}
