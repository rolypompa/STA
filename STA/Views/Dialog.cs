﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using STA.Model;
using STA.Utils;
using utils;

namespace STA
{
    class Dialog
    {
        static public DialogResult Prompt(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            PictureBox pictureBox = new PictureBox();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            pictureBox.Location = new System.Drawing.Point(50, 70);
            pictureBox.Size = new System.Drawing.Size(24, 24);
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.Image = Properties.Resources.information24x24;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { pictureBox, label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            //form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        static public DialogResult SelectSoundFile(string title, string promptText, SoundFileModel soundFile, ref SoundFileModel soundFileRef, string soundDir)
        {
            Form form = new Form();
            Label label = new Label();
            ComboBox comboBox = new ComboBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            PictureBox pictureBox = new PictureBox();

            form.Text = title;
            label.Text = promptText;

            try
            {
                List<SoundFileModel> soundFileList = new List<SoundFileModel>();
                DirectoryInfo dirInfo = new DirectoryInfo(soundDir);

                dirInfo.GetFiles().ForEach(f =>
                {
                    SoundFileModel soundFileModel = new SoundFileModel();
                    soundFileModel.name = ((FileInfo)f).Name;
                    soundFileModel.targetPath = ((FileInfo)f).FullName;
                    soundFileList.Add(soundFileModel);
                });
                comboBox.DataSource = soundFileList;
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                form.Shown += (s, e) =>
                {
                    if (soundFile != null)
                    {
                        SoundFileModel selectedItem = soundFileList.Find(sf => sf.name.Equals(soundFile.name));
                        if (selectedItem != null)
                        {
                            comboBox.SelectedItem = selectedItem;
                        }
                    }
                };
            }
            catch (Exception er)
            {
                BaseUtils.log.Error(er);
            }

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            comboBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            comboBox.Anchor = comboBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, comboBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            soundFileRef = (SoundFileModel)comboBox.SelectedValue;
            return dialogResult;
        }


        static public DialogResult SelectMode(string title, string promptText, ModeModel mode, ref ModeModel modeRef, List<ModeModel> modeList)
        {
            Form form = new Form();
            Label label = new Label();
            ComboBox comboBox = new ComboBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            PictureBox pictureBox = new PictureBox();

            form.Text = title;
            label.Text = promptText;

            comboBox.DataSource = modeList;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            form.Shown += (s, e) =>
            {
                if (mode != null)
                {
                    ModeModel selectedItem = modeList.Find(m => m.value.Equals(mode.value));
                    if (selectedItem != null)
                    {
                        comboBox.SelectedItem = selectedItem;
                    }
                }
            };

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            comboBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            comboBox.Anchor = comboBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, comboBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            modeRef = (ModeModel)comboBox.SelectedValue;
            return dialogResult;
        }
    }
}
