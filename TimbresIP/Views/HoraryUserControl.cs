﻿using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TimbresIP.Model;
using TimbresIP.Utils;

namespace TimbresIP
{
    partial class HoraryUserControl : UserControl
    {
        ValidateEntriesUtils validationEntries = new ValidateEntriesUtils();
        ConfigurationParametersModel configurationParametersModel = new ConfigurationParametersModel();
        JsonHandlerUtils jsonHandlerUtils = new JsonHandlerUtils();

        /// <summary>
        /// Horario.
        /// </summary>
        HoraryModel horary;

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public HoraryUserControl()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Constructor. Para cargar datos en interfaz.
        /// </summary>
        /// <param name="horary"></param>
        public HoraryUserControl(HoraryModel horary)
        {
            this.horary = horary;
            InitializeComponent();
        }

        #region Métodos
        /// <summary>
        /// Cargar datos en interfaz.
        /// </summary>
        private void loadData()
        {
            //Datos de conexión al servidor.
            textBoxHoraryIdExtension.Text = horary.connectionCallServer.displayName;
            textBoxHoraryExtExtension.Text = horary.connectionCallServer.registerName;
            textBoxHoraryPasswordExtension.Text = horary.connectionCallServer.registerPassword;

            //Llamadas.
            horary.callServerList.ForEach(cs =>
            {
                DataTable dataTable = (DataTable)dataGridViewHorary.DataSource;
                DataRow dataRowToAdd = dataTable.NewRow();

                dataRowToAdd["ColumnNo"] = cs.randomId;
                dataRowToAdd["ColumnHoraInicio"] = cs.startAt;
                dataRowToAdd["ColumnSoundTime"] = cs.callTime;
                dataRowToAdd["ColumnTone"] = cs.soundFile.targetPath;
                dataRowToAdd["ColumnCheck"] = cs.enabled;
                dataRowToAdd["ColumnExtension"] = cs.registerName;
                dataRowToAdd["ColumnObservations"] = cs.observations;

                dataTable.Rows.Add(dataRowToAdd);
                dataTable.AcceptChanges();
            });

        }
        #endregion

        #region Eventos
        private void buttonEditExtension_Click(object sender, EventArgs e)
        {
            this.textBoxHoraryExtExtension.Enabled = true;
            this.textBoxHoraryIdExtension.Enabled = true;
            this.textBoxHoraryPasswordExtension.Enabled = true;
            this.buttonHorarySaveExtension.Enabled = true;
            this.buttonHoraryEditExtension.Enabled = false;
        }

        private void buttonSaveExtension_Click(object sender, EventArgs e)
        {
            this.textBoxHoraryExtExtension.Enabled = false;
            this.textBoxHoraryIdExtension.Enabled = false;
            this.textBoxHoraryPasswordExtension.Enabled = false;
            this.buttonHorarySaveExtension.Enabled = false;
            this.buttonHoraryEditExtension.Enabled = true;
        }

        private void dataGridViewHorary_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dataGridViewHorary.Columns[e.ColumnIndex].Name == "ColumnCall" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dataGridViewHorary.Rows[e.RowIndex].Cells["ColumnCall"] as DataGridViewButtonCell;
                Image image = Properties.Resources.call16x16;
                e.Graphics.DrawImage(image, e.CellBounds.Left + 12, e.CellBounds.Top + 3);

                this.dataGridViewHorary.Rows[e.RowIndex].Height = image.Height + 10;
                this.dataGridViewHorary.Columns[e.ColumnIndex].Width = image.Width + 30;

                e.Handled = true;
            }
        }

        private void textBoxHoraryIdExtension_KeyPress(object sender, KeyPressEventArgs e)
        {
            validationEntries.validateNumericEntries(e);
        }

        private void textBoxHoraryExtExtension_KeyPress(object sender, KeyPressEventArgs e)
        {
            validationEntries.validateNumericEntries(e);
        }

        private void dataGridViewHorary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                switch (this.dataGridViewHorary.Columns[e.ColumnIndex].Name)
                {
                    case "ColumnTone":
                        if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                        {
                            DataGridViewComboBoxColumn comboBox = this.dataGridViewHorary.Columns["ColumnTone"] as DataGridViewComboBoxColumn;
                            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\Noslen Martinez\Documents\Visual Studio 2017\Projects\TimbresIP\TimbresIP\Resources\ComboDataExample");
                            FileInfo[] files = dirInfo.GetFiles();
                            comboBox.DataSource = files;
                            comboBox.DisplayMember = nameof(FileInfo.Name);
                        }
                        break;
                    case "ColumnCall":
                        MessageBox.Show("ColumnCall");
                        break;
                }
            }
        }

        private void dataGridViewHorary_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (dataGridViewHorary.Columns[e.ColumnIndex].Name)
            {
                case "ColumnExtension":
                    //Validamos si no es una fila nueva
                    if (!dataGridViewHorary.Rows[e.RowIndex].IsNewRow)
                    {
                        Regex regularExpression = new Regex(validationEntries.NumbersRegularExpression);
                        if (!regularExpression.IsMatch(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show("El dato introducido no es de tipo numerico", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridViewHorary.Rows[e.RowIndex].ErrorText = "El dato introducido no es de tipo numérico";
                            e.Cancel = true;
                        }
                    }
                    break;
                case "ColumnNo":
                    //Validamos si no es una fila nueva
                    if (!dataGridViewHorary.Rows[e.RowIndex].IsNewRow)
                    {
                        Regex regularExpression = new Regex(validationEntries.NumbersRegularExpression);
                        if (!regularExpression.IsMatch(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show("El dato introducido no es de tipo numerico", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridViewHorary.Rows[e.RowIndex].ErrorText = "El dato introducido no es de tipo numérico";
                            e.Cancel = true;
                        }
                    }
                    break;

                case "ColumnHoraInicio":
                    if (!dataGridViewHorary.Rows[e.RowIndex].IsNewRow)
                    {
                        Regex regularExpression = new Regex(validationEntries.TimeRegularExpression);
                        if (!regularExpression.IsMatch(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show("La hora indicada no es correcta", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridViewHorary.Rows[e.RowIndex].ErrorText = "El dato introducido no es de tipo horario";
                            e.Cancel = true;
                        }
                    }
                    break;
                case "ColumnSoundTime":
                    if (!dataGridViewHorary.Rows[e.RowIndex].IsNewRow)
                    {
                        Regex regularExpression = new Regex(validationEntries.TimeRegularExpression);
                        if (!regularExpression.IsMatch(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show("La hora indicada no es correcta", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dataGridViewHorary.Rows[e.RowIndex].ErrorText = "El dato introducido no es de tipo horario";
                            e.Cancel = true;
                        }
                    }
                    break;
            }
        }

        private void dataGridViewHorary_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewHorary.Rows.Count == configurationParametersModel.numberHours + 1)
            {
                this.dataGridViewHorary.AllowUserToAddRows = false;
                MessageBox.Show("No se puede crear más horas, ya exedio el límite licenciado. Por favor póngase en contacto con el proveedor del sistema.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.dataGridViewHorary.AllowUserToAddRows = true;
            }
        }

        private void HoraryUserControl_Load(object sender, EventArgs e)
        {
            jsonHandlerUtils = new JsonHandlerUtils(validationEntries.getJsonConfigurationParametersFilePath(), "TimbresIP.Model.ConfigurationParametersModel");
            configurationParametersModel = (ConfigurationParametersModel)jsonHandlerUtils.deserialize();
            loadData();
        }
        #endregion
    }
}


