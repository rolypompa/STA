﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimbresIP
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEditServer_Click(object sender, EventArgs e)
        {
            this.textBoxServer.Enabled = true;
            this.textBoxPort.Enabled = true;
            this.buttonSaveServer.Enabled = true;
            this.buttonEditServer.Enabled = false;
        }

        private void buttonSaveServer_Click(object sender, EventArgs e)
        {
            this.textBoxServer.Enabled = false;
            this.textBoxPort.Enabled = false;
            this.buttonSaveServer.Enabled = false;
            this.buttonEditServer.Enabled = true;
        }

        private void buttonAddHorary_Click(object sender, EventArgs e)
        {
            Dialog dialog = new Dialog();
            HoraryUserControl horaryUserControl = new HoraryUserControl();
            horaryUserControl.Dock = DockStyle.Fill;
            string tabPageName = "Horario";
            if (dialog.Prompt("Crear nuevo Horario", "Ingrese el nombre del horario:", ref tabPageName) == DialogResult.OK)
            {
                TabPage horaryTabPage = new TabPage(tabPageName);
                tabControlHorary.TabPages.Add(horaryTabPage);
                horaryTabPage.ImageIndex = 0;
                horaryTabPage.Controls.Add(horaryUserControl);
            }
        }

        private void buttonDeleteHorary_Click(object sender, EventArgs e)
        {
            if (tabControlHorary.TabPages.Count > 0)
            {
                string tabName = tabControlHorary.SelectedTab.Text;
                if ((MessageBox.Show("Esta seguro que desea eliminar: " + tabName, "Eliminar Horario", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)) == DialogResult.OK)

                    tabControlHorary.TabPages.Remove(tabControlHorary.SelectedTab);
            }
            else { MessageBox.Show("No tiene horarios creados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }

        private void buttonEditHorary_Click(object sender, EventArgs e)
        {
            if (tabControlHorary.TabPages.Count > 0)
            {
                Dialog dialog = new Dialog();
                string tabPageName = "";
                if (dialog.Prompt("Modificar Horario", "Ingrese el nuevo nombre del horario:", ref tabPageName) == DialogResult.OK)
                {
                    TabPage horaryTabPage = new TabPage(tabPageName);
                    tabControlHorary.SelectedTab.Text = tabPageName;
                }
            }
            else { MessageBox.Show("Debe crear horarios primeramente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            GeneralRingUserControl generalRingUserControl = new GeneralRingUserControl();
            this.groupBoxGeneralSound.Controls.Add(generalRingUserControl);
        }
    }

}
