using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzIna.GUI
{

    public class CGuiManager
    {
        public delegate void SendMsgToMainFrmDelegate(D.eTagSendMsg a_eTagMsg);
        public event SendMsgToMainFrmDelegate SendMsgEventHandler;


        struct def_stFormInfor
        {
            public Form pForm;
            public int nFormID;
            public int nFormType;


            public void Init()
            {
                Clear();
            }

            public void Clear()
            {
                pForm = null;
                nFormID = -1;
                nFormType = -1;
            }
        }

        Dictionary<int, def_stFormInfor> m_dicFormList; // 모든 폼을 담은 컨테이너.
        def_stFormInfor m_stCurrFormInfor; //현재 폼의 정보.



        int m_iCurrInforPanelFormID; //현재 활성화된 Information Panel Form ID
                                     //int m_iCurrCmdPanelFormID; //현재 활성화된 Command Panel Form ID


        /// <summary>
        /// 폼을 관리하는 클래스이다.
        /// </summary>
        public CGuiManager()
        {
            Initialize();
        }

        ~CGuiManager()
        {


        }

        /// <summary>클래스 초기화 함수</summary>
        /// <returns>Returns true.</returns>
        protected bool Initialize()
        {
            m_dicFormList = new Dictionary<int, def_stFormInfor>(); // 모든 폼을 담은 컨테이너.
            m_stCurrFormInfor = new def_stFormInfor(); //현재 폼의 정보.		
            m_iCurrInforPanelFormID = -1;

            return true;
        }

        /// <summary>
        /// 메인폼 레이아웃 변경 함수
        /// </summary>
        /// <param name="ctrl">메인폼</param>
        /// <param name="a_pTitle">타이틀 패널</param>
        /// <param name="a_pNaVi">네비게이션 패널</param>
        /// <param name="a_pSplitContainerMain">인포메이션 컨테이너</param>
        public void ArrangePositionFullHD(Form a_pFrm, Panel a_pTitle, Panel a_pNaVi, SplitContainer a_pSplitContainerMain)
        {


            a_pFrm.Width = D.GUI_WINDOWS_WIDTH;
            a_pFrm.Height = D.GUI_WINDOWS_HEIGHT;
            a_pFrm.StartPosition = FormStartPosition.Manual;
            a_pFrm.Location = new Point(0, 0);
            a_pFrm.BackColor = D.clFormBackground;

            if (a_pFrm != null)
            {
                a_pTitle.Width = D.GUI_WINDOWS_WIDTH;
                a_pTitle.Height = D.GUI_TITLE_HEIGHT;
                a_pTitle.BorderStyle = BorderStyle.FixedSingle;
                a_pTitle.BackColor = D.clMainTitle;

            }

            if (a_pSplitContainerMain != null)
            {
                a_pSplitContainerMain.Width = D.GUI_WINDOWS_WIDTH;
                a_pSplitContainerMain.Height = D.GUI_INFOR_HEIGHT;
                a_pSplitContainerMain.SplitterDistance = D.GUI_INFOR_WIDTH;
                a_pSplitContainerMain.BorderStyle = BorderStyle.FixedSingle;
                a_pSplitContainerMain.SplitterWidth = 1;
                a_pSplitContainerMain.BackColor = D.clInfo;
            }

            if (a_pNaVi != null)
            {
                a_pNaVi.Width = D.GUI_WINDOWS_WIDTH;
                a_pNaVi.Height = D.GUI_NAVI_HEIGHT;
                a_pNaVi.BorderStyle = BorderStyle.FixedSingle;
                a_pNaVi.BackColor = D.clNavi;
            }
        }

        private int GetCmdFormID(int a_nInforFormID)
        {
            return (a_nInforFormID / 100) * 100;
        }
        /// <summary>
        /// 폼 유무 확인
        /// </summary>
        /// <param name="a_nFormID"></param>
        /// <returns>폼이 존재하면 리턴 true </returns>
        public bool IsExistForm(int a_nFormID)
        {
            return m_dicFormList.ContainsKey(a_nFormID);
        }

        public bool ShowFormWithCMD(int a_nInforFormID)
        {
            if (m_dicFormList.ContainsKey(a_nInforFormID) == false)
                return false;

            int nCmdFormID = -1;
            nCmdFormID = GetCmdFormID(a_nInforFormID);
            if (m_dicFormList.ContainsKey(nCmdFormID) == false)
                return false;

            ShowForm(a_nInforFormID);
            ShowForm(nCmdFormID);
            return true;
        }
        public void HideFormWithCMD(int a_nInforFormID, int a_nFormType)
        {
            if (m_dicFormList.Count <= 0)
                return;

            D.eTagFormType eFrmType = (D.eTagFormType)a_nFormType;
            int nCmdFormID = -1;
            HideForm(a_nInforFormID);
            if (eFrmType == D.eTagFormType.FORM_TYPE_INFOR)
            {
                //Hide a information form
                nCmdFormID = GetCmdFormID(a_nInforFormID);
                HideForm(nCmdFormID);
            }
        }

        /// <summary>
        /// 폼 아이디와 일치하는 폼 Show
        /// </summary>
        /// <param name="a_nFormID"></param>
        /// <returns></returns>
        public bool ShowForm(int a_nFormID)
        {
            if (m_dicFormList.ContainsKey(a_nFormID) == true)
            {
                if (m_dicFormList[a_nFormID].pForm.Visible == false)
                {

                    m_dicFormList[a_nFormID].pForm.Show();
                }

            }
            return true;
        }
        public void HideForm(int a_nFormID)
        {
            if (m_dicFormList.Count <= 0)
                return;

            if (m_dicFormList.ContainsKey(a_nFormID) == true)
            {
                if (m_dicFormList[a_nFormID].pForm.Visible == true)
                {
                    m_dicFormList[a_nFormID].pForm.Hide();
                }
            }

        }

        /// <summary>
        /// 폼 아이디와 일치하는 폼 리턴.
        /// </summary>
        /// <param name="a_nFormID"></param>
        /// <returns></returns>
        public Form GetItem(int a_nFormID)
        {
            Form pRet = null;
            def_stFormInfor stFormInfor = new def_stFormInfor();
            stFormInfor.Init();

            if (m_dicFormList.ContainsKey(a_nFormID) == true)
            {
                if (m_dicFormList.TryGetValue(a_nFormID, out stFormInfor))
                {
                    pRet = stFormInfor.pForm;
                }

            }

            return pRet;
        }
        /// <summary>
        /// 폼을 리스트에 삽입하는 함수.
        /// </summary>
        /// <param name="a_nFormID">폼 식별자</param>
        /// <param name="a_nFormType">폼 타입</param>
        /// <param name="a_pParentForm">메인 폼</param>
        /// <param name="a_pForm">자식 폼</param>
        /// <param name="a_pInterface">인터페이스</param>
        /// <returns></returns>
        public bool AddForm(int a_nFormID, D.eTagFormType a_nFormType, Form a_pParentForm, Form a_pForm, Panel a_pTarget)
        {
            if (IsExistForm(a_nFormID))
                return false;

            HideFormWithCMD(a_nFormID, (int)a_nFormType);
            def_stFormInfor stFormInfor = new def_stFormInfor();
            stFormInfor.Init();

            stFormInfor.pForm = a_pForm;
            stFormInfor.nFormID = a_nFormID;
            stFormInfor.nFormType = (int)a_nFormType;

            stFormInfor.pForm.Owner = a_pParentForm;
            stFormInfor.pForm.TopMost = false;
            stFormInfor.pForm.TopLevel = false;
            stFormInfor.pForm.FormBorderStyle = FormBorderStyle.None;
            stFormInfor.pForm.Dock = DockStyle.Fill;
            a_pTarget.Controls.Add(stFormInfor.pForm);
            D.eTagFormType eFrmType = (D.eTagFormType)a_nFormType;

            switch (eFrmType)
            {
                case D.eTagFormType.FORM_TYPE_TITLE:
                    stFormInfor.pForm.BackColor = D.clMainTitle;
                    //stFormInfor.pForm.BackColor = Color.FromArgb(23, 23, 23);                  
                    //stFormInfor.pForm.Size = new Size(D.GUI_WINDOWS_WIDTH, D.GUI_TITLE_HEIGHT);
                    stFormInfor.pForm.Show();
                    break;
                case D.eTagFormType.FORM_TYPE_INFOR:
                    stFormInfor.pForm.BackColor = SystemColors.Control;//D.clInfo;                    
                                                                       //stFormInfor.pForm.Size = new Size(D.GUI_INFOR_WIDTH, D.GUI_INFOR_HEIGHT);
                    break;

                case D.eTagFormType.FORM_TYPE_CMD:
                    stFormInfor.pForm.BackColor = D.clCmd;
                    //stFormInfor.pForm.Size = new Size(D.GUI_CMD_WIDTH, D.GUI_CMD_HEIGHT);
                    break;
                case D.eTagFormType.FORM_TYPE_NAVI:
                    stFormInfor.pForm.BackColor = D.clNavi;
                    stFormInfor.pForm.Size = new Size(D.GUI_WINDOWS_WIDTH, D.GUI_NAVI_HEIGHT);
                    stFormInfor.pForm.Show();
                    break;
            }

            m_dicFormList.Add(a_nFormID, stFormInfor);
            //ShowForm(a_nFormID);
            return true;
        }
        /// <summary>
        /// 폼을 리스트에 삽입하는 함수.
        /// </summary>
        /// <param name="a_nFormID">폼 식별자</param>
        /// <param name="a_pShowTarget">메인 Target Panel</param>
        /// <param name="a_pTargetForm">메인 폼</param>
        /// <param name="a_pOwner">자식 폼</param>       
        /// <param name="bShow">폼 Show 여부 = false</param> 
        /// <returns></returns>
        ///

        public bool AddForm(int a_nFormID, Panel a_pShowTarget, Form a_pTargetForm, Form a_pOwner, bool bShow = false)
        {
            if (IsExistForm(a_nFormID))
                return false;

            HideForm(a_nFormID);

            def_stFormInfor stFormInfor = new def_stFormInfor();
            stFormInfor.Init();

            stFormInfor.pForm = a_pTargetForm;
            stFormInfor.nFormID = a_nFormID;
            stFormInfor.nFormType = (int)D.eTagFormType.FORM_TYPE_INFOR_WITHOUT_CMD;
            stFormInfor.pForm.Owner = a_pOwner;            
            stFormInfor.pForm.FormBorderStyle = FormBorderStyle.None;
            stFormInfor.pForm.AutoScaleMode = AutoScaleMode.None;
            stFormInfor.pForm.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            if (a_pShowTarget!=null)
            {
                stFormInfor.pForm.TopMost = false;
                stFormInfor.pForm.TopLevel = false;
                stFormInfor.pForm.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                stFormInfor.pForm.Dock = DockStyle.Fill;
                a_pShowTarget.Controls.Add(stFormInfor.pForm);
            }
            m_dicFormList.Add(a_nFormID, stFormInfor);
            if (bShow)
            {
                ChangeForm(stFormInfor.nFormID);
            }
            return true;
        }

        public bool ChangeFormWithCMD(int a_nFormID, int a_nFormType)
        {
            if (IsExistForm(a_nFormID) == false)
                return false;

            if ((D.eTagFormType)a_nFormType == D.eTagFormType.FORM_TYPE_INFOR)
            {

                if (m_iCurrInforPanelFormID != a_nFormID)
                {
                    HideFormWithCMD(m_iCurrInforPanelFormID, (int)D.eTagFormType.FORM_TYPE_INFOR);
                    ShowFormWithCMD(a_nFormID);
                    m_iCurrInforPanelFormID = a_nFormID;
                }
            }
            else if ((D.eTagFormType)a_nFormType == D.eTagFormType.FORM_TYPE_INFOR_WITHOUT_CMD)
            {
                if (m_iCurrInforPanelFormID != a_nFormID)
                {
                    HideFormWithCMD(m_iCurrInforPanelFormID, (int)D.eTagFormType.FORM_TYPE_INFOR_WITHOUT_CMD);
                    ShowFormWithCMD(a_nFormID);
                    m_iCurrInforPanelFormID = a_nFormID;
                }
            }

            return true;
        }

        public bool ChangeForm(int a_nFormID)
        {
            if (IsExistForm(a_nFormID) == false)
                return false;
            def_stFormInfor pInputForm =m_dicFormList[a_nFormID];
            if(pInputForm.pForm.Parent!=null)
            {
                if (m_iCurrInforPanelFormID != a_nFormID)
                {
                    HideForm(m_iCurrInforPanelFormID);
                    ShowForm(a_nFormID);
                    m_iCurrInforPanelFormID = a_nFormID;
                }
            }
            else
            {
                ShowForm(a_nFormID);
            }
            
            return true;
        }

        // MainFrm 해당 Application Program 참조 권장 
        /*
        public Form GetFrmMain(int a_nFormID)
		{
			def_stFormInfor frmRet = new def_stFormInfor();
			frmRet.Init();

			m_dicFormList.TryGetValue(a_nFormID, out frmRet);

			return frmRet.pForm.Owner;			
		}
        */
        public void CreateEvent(Action<D.eTagSendMsg> a_Func)
        {
            SendMsgEventHandler += new SendMsgToMainFrmDelegate(a_Func);
        }
        public void SendMsgToFrmMain(GUI.D.eTagSendMsg a_eTagSendMsg)
        {
            SendMsgEventHandler(a_eTagSendMsg);
        }
    }
}
