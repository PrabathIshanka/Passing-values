using System;
using System.Collections.Generic;
using System.Xml;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace DataValues
{
    [FormAttribute("DataValues.Form1", "Data.b1f")]
    class Form1 : UserFormBase
    {
        SAPbobsCOM.Company oCom;
        SAPbobsCOM.Recordset oRec,oRec1;
        SAPbouiCOM.Form oForm;
        SAPbouiCOM.Item btn;
        string Query , Quy;
        decimal rebate =0 , price = 0 , TotRebate , TotPrice , Sum1 , Sum2 , Propit , Flag;
        bool flag ;

        public Form1()
        {
            oCom = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            oRec = (SAPbobsCOM.Recordset)oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            oRec1 = (SAPbobsCOM.Recordset)oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            SAPbouiCOM.Framework.Application.SBO_Application.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(SBO_Application_ItemEvent);

        }

        private void SBO_Application_ItemEvent(string FormUID, ref ItemEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            
            if (pVal.BeforeAction && pVal.FormUID == "143" && pVal.ItemUID== "btnView" && pVal.EventType == SAPbouiCOM.BoEventTypes.et_CLICK)
            {
                Query = "select \"ItemCode\",\"Price\" from \"PCH1\"  ";
                oRec.DoQuery(Query);
                for (int x = 0; x < oRec.RecordCount; x++)
                {
                    Sum1 = Convert.ToDecimal(oRec.Fields.Item("Price").Value);
                    price = price + Sum1;
                    oRec.MoveNext();
                }
                TotPrice = price;
                EditText1.Value = TotPrice.ToString();

                Quy = "select \"ItemCode\", \"U_Rebate\" from \"OITM\" ";
                oRec1.DoQuery(Quy);
                for (int y = 0; y < oRec1.RecordCount; y++)
                {
                    Sum2 = Convert.ToDecimal(oRec1.Fields.Item("U_Rebate").Value);
                    rebate = rebate + Sum2;
                    oRec1.MoveNext();
                }

                TotRebate = rebate;
                EditText0.Value = TotRebate.ToString();

                Propit = (TotPrice - TotRebate);
                EditText2.Value = Propit.ToString();

                flag = false;

                if (flag == false)
                {
                    oForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.Item(pVal.FormUID);
                    btn = oForm.Items.Item("btnView");
                    btn.Visible = false;

                }

            }
       
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("txtRebate").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("txtPrice").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("txtPropit").Specific));
            this.EditText2.KeyDownAfter += new SAPbouiCOM._IEditTextEvents_KeyDownAfterEventHandler(this.EditText2_KeyDownAfter);
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("btnView").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("btnCheck").Specific));
            this.Button1.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button1_ClickBefore);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.Button2.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button2_ClickBefore);
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

        private void Button1_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            Value.Propi = Propit;
            ViewData view = new ViewData();
            view.Show();

        }

        private void Button2_ClickBefore(object sboObject, SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }

        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.Button Button0;

        private void EditText2_KeyDownAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

        }

        private void Form_LoadAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            oForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.Item(pVal.FormUID);
           
        }

        private SAPbouiCOM.Button Button1;
        private Button Button2;
    }
}