using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace DataValues
{
    [FormAttribute("DataValues.ViewData", "ViewData.b1f")]
    class ViewData : UserFormBase
    {
        SAPbobsCOM.Company oCom;
        SAPbobsCOM.Recordset oRec;
        SAPbouiCOM.Form oForm;
        decimal Buject, validPropit, OldProp;

        public ViewData()
        {
            oCom = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany();
            oRec = (SAPbobsCOM.Recordset)oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("txtBuject").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("txtPropit").Specific));
            this.EditText1.KeyDownAfter += new SAPbouiCOM._IEditTextEvents_KeyDownAfterEventHandler(this.EditText1_KeyDownAfter);
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("btnAdd").Specific));
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button0_ClickBefore);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("btnClose").Specific));
            this.Button1.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button1_ClickBefore);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.LoadAfter += new LoadAfterHandler(this.Form_LoadAfter);

        }

        private SAPbouiCOM.EditText EditText0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.Button Button0;

        private void EditText1_KeyDownAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

        }

        private void Form_LoadAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            oForm = Application.SBO_Application.Forms.Item(pVal.FormUID);
        }

        private void Button1_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;          
            oForm.Close();

        }

        private void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                Buject = Convert.ToDecimal(EditText0.Value);
                OldProp = Value.Propi;
                validPropit = OldProp / 2;

                if (validPropit > Buject)
                {
                    decimal NewPop = OldProp - Buject;
                    EditText1.Value = NewPop.ToString();
                }
                else
                {
                    Application.SBO_Application.SetStatusBarMessage("Buject is High" + oCom.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, true);
                }
            }
            catch(Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message ,SAPbouiCOM.BoMessageTime.bmt_Short,true);
            }

        }

        private SAPbouiCOM.Button Button1;
    }
}
