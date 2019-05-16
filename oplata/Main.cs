using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using Microsoft.Reporting.WinForms;
using DevExpress.XtraGrid;

namespace oplata
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
            DateTime dd = DateTime.Now;
        int mes;
       
            int god;
       
        public Main()
        {
            InitializeComponent();
            mes= dd.Month;
            god = dd.Year;
        }

        private void Main_Load(object sender, EventArgs e)
        {
          
            // TODO: данная строка кода позволяет загрузить данные в таблицу "oplataDataSet.analyse". При необходимости она может быть перемещена или удалена.
          
            // TODO: данная строка кода позволяет загрузить данные в таблицу "oplataDataSet.spr_zavis". При необходимости она может быть перемещена или удалена.
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            // TODO: данная строка кода позволяет загрузить данные в таблицу "oplataDataSet.spr_slujbi_bez_zavis". При необходимости она может быть перемещена или удалена.
            this.spr_slujbi_bez_zavisTableAdapter.Fill(this.oplataDataSet.spr_slujbi_bez_zavis);
            this.spr_mesTableAdapter.Fill(this.oplataDataSet.spr_mes);
           
            comboBox1.SelectedValue = mes;
           
            comboBox2.Text =god.ToString();
            this.spr_slujbiTableAdapter.Fill(this.oplataDataSet.spr_slujbi);
           
            // TODO: данная строка кода позволяет загрузить данные в таблицу "oplataDataSet.plan1". При необходимости она может быть перемещена или удалена.
            this.planTableAdapter.Fill(this.oplataDataSet.plan,god,mes);
           int i = planBindingSource.Count;
           

        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            planBindingSource.AddNew();
           
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                Globals.id_plan = (int)gridView1.GetRowCellValue(rowHandle, "id");
                Globals.id_res = (int)gridView1.GetRowCellValue(rowHandle, "id_slujbi");
                this.spr_zavisTableAdapter.Fill(this.oplataDataSet.spr_zavis, Globals.id_res);
                Globals.id_zavis_res = Convert.ToInt32(((DataRowView)spr_zavisBindingSource.Current).Row["id_zavis"]);
                Globals.mes = (int)gridView1.GetRowCellValue(rowHandle, "mes");
                Globals.god = (int)gridView1.GetRowCellValue(rowHandle, "god");
                Globals.dengi = (decimal)gridView1.GetRowCellValue(rowHandle, "plan_deneg");

                if (Globals.id_plan < 0)
                {
                    this.planTableAdapter.FillByDubl(this.oplataDataSet.plan, Globals.god, Globals.mes, Globals.id_res);
                    if (planBindingSource.Count > 0)
                    {
                        MessageBox.Show("Ввод не возможен!!!У этой службы есть план за этот месяц и год!!!");
                        return;
                    }
                    this.planTableAdapter.InsertPlan_deneg(Globals.id_res, Globals.god, Globals.mes, Globals.id_zavis_res, Globals.dengi);
                    this.planTableAdapter.Fill(this.oplataDataSet.plan, god, mes);
                }
                else
                {
                    this.planTableAdapter.UpdatePlan_deneg(Globals.dengi, Globals.id_plan);
                    this.planTableAdapter.Fill(this.oplataDataSet.plan, god, mes);
                }
            }
            catch (Exception ex){}
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
           mes = Convert.ToInt32(comboBox1.SelectedValue);

            god = Convert.ToInt32(comboBox2.Text);
            this.planTableAdapter.Fill(this.oplataDataSet.plan, god, mes);
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mes = Convert.ToInt32(comboBox1.SelectedValue);

            god = Convert.ToInt32(comboBox2.Text);
            this.planTableAdapter.Fill(this.oplataDataSet.plan, god, mes);
        }

        private void windowsUIButtonPanel1_ButtonChecked(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel tt = sender as DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel;


            if (tt.Buttons[0].Properties.Checked == true)
            {
                tt.Buttons[1].Properties.Checked = false;
                tt.Buttons[0].Properties.Checked = false;
                tt.Buttons[2].Properties.Checked = false;


                if (transitionManager1.IsTransition)
                {
                    transitionManager1.EndTransition();
                }

                transitionManager1.StartTransition(xtraTabPage1);
                try
                {
                    xtraTabPage1.PageVisible = true;
                    xtraTabPage2.PageVisible = false;
                  xtraTabPage3.PageVisible = false;
                    xtraTabControl1.SelectedTabPage = xtraTabPage1;
                    //this.pol_instructionsTableAdapter.Fill_pol(deloDataSet1.pol_instructions, Globals.id_sl_pol_ins);
                    this.planTableAdapter.Fill(this.oplataDataSet.plan, god, mes);


                }
                finally
                {
                    transitionManager1.EndTransition();
                }
            }

            if (tt.Buttons[1].Properties.Checked == true)
            {
                tt.Buttons[1].Properties.Checked = false;
                tt.Buttons[0].Properties.Checked = false;
                tt.Buttons[2].Properties.Checked = false;


                if (transitionManager1.IsTransition)
                {
                    transitionManager1.EndTransition();
                }

                transitionManager1.StartTransition(xtraTabPage2);
                try
                {
                    xtraTabPage1.PageVisible = false;
                    xtraTabPage3.PageVisible = false;
                    xtraTabPage2.PageVisible = true;
                    xtraTabControl1.SelectedTabPage = xtraTabPage2;
                   



                }
                finally
                {
                    transitionManager1.EndTransition();
                }
            }

            if (tt.Buttons[2].Properties.Checked == true)
            {

               tt.Buttons[2].Properties.Checked = false;
                tt.Buttons[1].Properties.Checked = false;
                tt.Buttons[0].Properties.Checked = false;



                if (transitionManager1.IsTransition)
                {
                    transitionManager1.EndTransition();
                }

               
                try
                {
                    dateTimePicker1.Text = Convert.ToString(DateTime.Today);

                    xtraTabPage3.PageVisible = true;
                    xtraTabPage1.PageVisible = false;
                    xtraTabPage2.PageVisible = false;
                    DateTime now = Convert.ToDateTime(dateTimePicker1.Value);
                    DateTime mes_naz = now.AddMonths(-1);
                    DateTime god_naz = now.AddYears(-1);
                    DateTime god_mes_naz = now.AddMonths(-1).AddYears(-1);

                    this.analyseTableAdapter.Fill(this.oplataDataSet.analyse, now, now.AddDays(-1));
                    this.analyse1TableAdapter.Fill(this.oplataDataSet.analyse1, mes_naz, mes_naz.AddDays(-1));
                    this.analyse2TableAdapter.Fill(this.oplataDataSet.analyse2, now, now.AddDays(-1));
                    this.analyse3TableAdapter.Fill(this.oplataDataSet.analyse3, mes_naz, mes_naz.AddDays(-1));
                    this.analyse4TableAdapter.Fill(this.oplataDataSet.analyse4, now, now.AddDays(-1));
                    this.analyse5TableAdapter.Fill(this.oplataDataSet.analyse5, mes_naz, mes_naz.AddDays(-1));
                   
                    ReportParameter date1 = new ReportParameter("date1", now.ToString());
                    ReportParameter date2 = new ReportParameter("date2", mes_naz.ToString());
                    ReportParameter date3 = new ReportParameter("date3", god_naz.ToString());
                    ReportParameter date4 = new ReportParameter("date4", god_mes_naz.ToString());
                    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { date1 });
                    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { date2 });
                    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { date3 });
                    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { date4 });
                    this.reportViewer1.RefreshReport();


                }
                finally
                {
                    transitionManager1.EndTransition();
                }
            }

        }

        private void просмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /* XtraReport1 report = new XtraReport1();
            report.Parameters["id_zavis"].Value = Globals.id_res_for_report;
           
            report.RequestParameters = false;
            ReportPrintTool printTool = new ReportPrintTool(report);
           // printTool.AutoShowParametersPanel = false;
            printTool.ShowPreviewDialog();*/
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
DateTime now = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime mes_naz = now.AddMonths(-1);
            DateTime god_naz = now.AddYears(-1);
            DateTime god_mes_naz = now.AddMonths(-1).AddYears(-1);

            this.analyseTableAdapter.Fill(this.oplataDataSet.analyse, now, now.AddDays(-1));
            this.analyse1TableAdapter.Fill(this.oplataDataSet.analyse1, mes_naz, mes_naz.AddDays(-1));
            this.analyse2TableAdapter.Fill(this.oplataDataSet.analyse2, now, now.AddDays(-1));
            this.analyse3TableAdapter.Fill(this.oplataDataSet.analyse3, mes_naz, mes_naz.AddDays(-1));
            this.analyse4TableAdapter.Fill(this.oplataDataSet.analyse4, now, now.AddDays(-1));
            this.analyse5TableAdapter.Fill(this.oplataDataSet.analyse5, mes_naz, mes_naz.AddDays(-1));
            ReportParameter date1 = new ReportParameter("date1", now.ToString());
            ReportParameter date2 = new ReportParameter("date2", mes_naz.ToString());
            ReportParameter date3 = new ReportParameter("date3", god_naz.ToString());
            ReportParameter date4 = new ReportParameter("date4", god_mes_naz.ToString());
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { date1 });
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { date2 });
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { date3 });
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { date4 });
            this.reportViewer1.RefreshReport();
        }
        private void ShowGridPreview(GridControl grid)
        {
            // Check whether the GridControl can be previewed.
            if (!grid.IsPrintingAvailable)
            {
                MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error");
                return;
            }
              
            // Open the Preview window.
            grid.ShowPrintPreview();
        }

       

        private void pictureEdit1_Click_1(object sender, EventArgs e)
        {
 ShowGridPreview(gridControl1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int mes1 = Convert.ToInt32(comboBox1.SelectedValue);
            int god1 = Convert.ToInt32(comboBox2.Text);
            this.planTableAdapter.FillByDubl(this.oplataDataSet.plan, god1, mes1, Globals.id_res2);
            if (planBindingSource.Count > 0)
            {
                MessageBox.Show("Ввод не возможен!!!У этой службы есть план за этот месяц и год!!!");
                this.planTableAdapter.Fill(this.oplataDataSet.plan, god, mes);
                return;
            }
            this.planTableAdapter.InsertPlan_deneg(Globals.id_res2, god1, mes1, Globals.id_res2_zavis, Convert.ToDecimal(textEdit1.Text));
            this.planTableAdapter.Fill(this.oplataDataSet.plan, god, mes);
            lookUpEdit1.Text = "";
            textEdit1.Text = "";
        }

       

        private void lookUpEdit1_Closed_1(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
            {
                Globals.id_res2 = (int)(sender as DevExpress.XtraEditors.LookUpEdit).GetColumnValue("id_slujbi");
                this.spr_zavisTableAdapter.Fill(this.oplataDataSet.spr_zavis, Globals.id_res2);
                Globals.id_res2_zavis = Convert.ToInt32(((DataRowView)spr_zavisBindingSource.Current).Row["id_zavis"]);

            }
        }

     
    }
}