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
using oplata.Properties;

namespace oplata
{
    public partial class authentification : DevExpress.XtraEditors.XtraForm
    {
        public authentification()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                //this.usersTableAdapter.Fill("jjj", "jjj");
                this.users_programmTableAdapter.Fill(this.oplataDataSet.users_programm, textBoxLogin.Text.Trim(), textBoxPass.Text.Trim());
                if (oplataDataSet != null && oplataDataSet.users_programm.Count() > 0)
                {
                    Globals.iii = 1;
                    Settings.Default.UserNowName = textBoxLogin.Text;
                   
                    Globals.name_user = (string)((DataRowView)users_programmBindingSource.Current).Row["fio"];

                   
                            Settings.Default.Save();
                    Close();

                           
                        }

                    

                
                else
                {
                    textBoxLogin.SelectAll();
                    //textBoxPass.();
                    textBoxLogin.Focus();
                    labelControl3.Visible = true;
                    pictureBox1.Visible = false;
                }

            }

            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void textBoxPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton2_Click(null, null);
            }
        }

        private void authentification_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "oplataDataSet.users_programm". При необходимости она может быть перемещена или удалена.
           
            labelControl3.Visible = false;
            textBoxLogin.Text = Settings.Default.UserNowName;
        }

      
    }
}