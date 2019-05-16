using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace oplata
{
    public partial class US_vvod_mes_dannyh : DevExpress.XtraEditors.XtraUserControl
    {
        DateTime dd = DateTime.Now;
        int mes;

        int god;
        public US_vvod_mes_dannyh()
        {
            InitializeComponent();
            mes = dd.Month;
            god = dd.Year;
        }

        private void US_vvod_mes_dannyh_Load(object sender, EventArgs e)
        {
            this.vvod_postupibwih_denegTableAdapter.Fill(oplataDataSet.vvod_postupibwih_deneg,mes,god);
            this.spr_mesTableAdapter.Fill(this.oplataDataSet.spr_mes);
            this.spr_slujbi_bez_zavisTableAdapter.FillBy(this.oplataDataSet.spr_slujbi_bez_zavis);
            comboBox1.SelectedValue = mes;
            comboBox2.Text = god.ToString();
            this.spr_slujbiTableAdapter.Fill(this.oplataDataSet.spr_slujbi);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mes = Convert.ToInt32(comboBox1.SelectedValue);

            god = Convert.ToInt32(comboBox2.Text);
            this.vvod_postupibwih_denegTableAdapter.Fill(oplataDataSet.vvod_postupibwih_deneg, mes, god);
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mes = Convert.ToInt32(comboBox1.SelectedValue);

            god = Convert.ToInt32(comboBox2.Text);
            this.vvod_postupibwih_denegTableAdapter.Fill(oplataDataSet.vvod_postupibwih_deneg, mes, god);
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            vvodpostupibwihdenegBindingSource.AddNew();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                Globals.id_vvoda = (int)gridView1.GetRowCellValue(rowHandle, "id");
                Globals.id_res1 = (int)gridView1.GetRowCellValue(rowHandle, "id_slujbi");
                this.spr_zavisTableAdapter.Fill(this.oplataDataSet.spr_zavis, Globals.id_res1);
                Globals.id_zavis_res1 = Convert.ToInt32(((DataRowView)spr_zavisBindingSource.Current).Row["id_zavis"]);
                Globals.byt = (decimal)gridView1.GetRowCellValue(rowHandle, "byt");
                Globals.byt_akt = (decimal)gridView1.GetRowCellValue(rowHandle, "byt_akt");
                Globals.prom = (decimal)gridView1.GetRowCellValue(rowHandle, "prom");
                // Globals.prom_akt = (decimal)gridView1.GetRowCellValue(rowHandle, "prom_akt");
                Globals.date_vvod = (DateTime)gridView1.GetRowCellValue(rowHandle, "date_vvod");

                if (Globals.id_vvoda < 0)
                {
                    this.vvod_postupibwih_denegTableAdapter.FillByDubl(this.oplataDataSet.vvod_postupibwih_deneg, Globals.date_vvod, Globals.id_res1);
                    if (vvodpostupibwihdenegBindingSource.Count > 0)
                    {
                        MessageBox.Show("Ввод не возможен!!!За эту дату есть данные!!!");
                        this.vvod_postupibwih_denegTableAdapter.Fill(this.oplataDataSet.vvod_postupibwih_deneg, mes, god);
                        return;
                    }
                    this.vvod_postupibwih_denegTableAdapter.Insert1(Globals.byt, Globals.prom, Globals.byt_akt, 0, Globals.id_res1, Globals.date_vvod, DateTime.Now, Globals.id_zavis_res1);
                    this.vvod_postupibwih_denegTableAdapter.Fill(this.oplataDataSet.vvod_postupibwih_deneg, mes, god);
                }
                else
                {
                    this.vvod_postupibwih_denegTableAdapter.Update1(Globals.byt, Globals.prom, Globals.byt_akt, 0, Globals.date_vvod, Globals.id_vvoda);
                    this.vvod_postupibwih_denegTableAdapter.Fill(this.oplataDataSet.vvod_postupibwih_deneg, mes, god);

                }
            }
            catch(Exception ex){}
           
            
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
        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            ShowGridPreview(gridControl1);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        { 
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {   
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(dateEdit1.SelectedText);
            this.vvod_postupibwih_denegTableAdapter.FillByDubl(this.oplataDataSet.vvod_postupibwih_deneg, date, Globals.id_res3);
            if (vvodpostupibwihdenegBindingSource.Count > 0)
            {
                MessageBox.Show("Ввод не возможен!!!За эту дату есть данные!!!");
                this.vvod_postupibwih_denegTableAdapter.Fill(this.oplataDataSet.vvod_postupibwih_deneg, mes, god);
                return;
            }
            this.vvod_postupibwih_denegTableAdapter.Insert1(Convert.ToDecimal(textEdit1.Text), Convert.ToDecimal(textEdit2.Text), Convert.ToDecimal(textEdit3.Text), 0, Globals.id_res3, date, DateTime.Now, Globals.id_res3_zavis);
            this.vvod_postupibwih_denegTableAdapter.Fill(this.oplataDataSet.vvod_postupibwih_deneg, mes, god);
            textEdit1.Text = "";
            textEdit2.Text = "";
            textEdit3.Text = "";
            lookUpEdit1.SelectedText = "";
        }

        private void lookUpEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.Normal)
            {
                Globals.id_res3 = (int)(sender as DevExpress.XtraEditors.LookUpEdit).GetColumnValue("id_slujbi");
                this.spr_zavisTableAdapter.Fill(this.oplataDataSet.spr_zavis, Globals.id_res3);
                Globals.id_res3_zavis = Convert.ToInt32(((DataRowView)spr_zavisBindingSource.Current).Row["id_zavis"]);

            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
