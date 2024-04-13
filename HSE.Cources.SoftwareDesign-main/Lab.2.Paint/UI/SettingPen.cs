using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Lab._2.Paint.Module;

namespace Lab._2.Paint.UI
{
    public partial class SettingPen : Form
    {
        private Graphics graphics;
        private bool isProcess;
        private Pen newPen;

        public SettingPen() {
            InitializeComponent();
        }

        private void Draw() {
            graphics.Clear(Color.White);
            graphics.DrawLine(newPen,
                5,
                5,
                240,
                170);
            preShowPen.Refresh();
        }

        private void LoadForm(object sender, EventArgs e) {
            preShowPen.Image = new Bitmap(preShowPen.Width, preShowPen.Height);
            graphics = Graphics.FromImage(preShowPen.Image);

            newPen = new Pen(DataCore.Paintbrush.Color, DataCore.Paintbrush.Width)
                {
                    StartCap = DataCore.Paintbrush.StartCap,
                    EndCap = DataCore.Paintbrush.EndCap,
                    DashStyle = DataCore.Paintbrush.DashStyle
                };

            graphics.SmoothingMode = DataCore.isSmoothing
                ? SmoothingMode.AntiAlias
                : SmoothingMode.None;

            tsWidth.Items.Add((int) DataCore.Paintbrush.Width + 1);
            tsWidth.Items.Add((int) DataCore.Paintbrush.Width);
            tsWidth.Items.Add((int) DataCore.Paintbrush.Width - 1);


            cbDotted.Checked = DataCore.Paintbrush.DashStyle == DashStyle.Dash;
            cbSmoothing.Checked = DataCore.isSmoothing;


            tsWidth.SelectedIndex = 1;
            Draw();
            isProcess = false;
        }

        private void Cancel(object sender, EventArgs e) {
            Close();
        }

        private void AcceptChanges(object sender, EventArgs e) {
            DataCore.isSmoothing = cbSmoothing.Checked;
            DataCore.Paintbrush = new Pen(DataCore.Paintbrush.Color,
                (int) tsWidth.Items[tsWidth.SelectedIndex])
                {
                    DashStyle = cbDotted.Checked ? DashStyle.Dash : DashStyle.Solid,
                    EndCap = LineCap.Round,
                    StartCap = LineCap.Round
                };

            Close();
        }

        private void ChangeWidth(object sender, EventArgs e) {
            if (!isProcess) {
                isProcess = true;
                if (tsWidth.SelectedIndex == 1) return;

                if ((int) tsWidth.SelectedItem < 0) {
                    tsWidth.Items[2] = (int) newPen.Width + 1;
                    tsWidth.Items[1] = (int) newPen.Width;
                    tsWidth.Items[0] = (int) newPen.Width - 1;
                }

                if (tsWidth.SelectedIndex > 1) {
                    tsWidth.Items[0] = tsWidth.Items[1];
                    tsWidth.Items[1] = tsWidth.Items[2];
                    tsWidth.Items[2] = (int) tsWidth.Items[0] - 2;
                }
                else {
                    tsWidth.Items[2] = tsWidth.Items[1];
                    tsWidth.Items[1] = tsWidth.Items[0];
                    tsWidth.Items[0] = (int) tsWidth.Items[2] + 2;
                }


                tsWidth.SelectedIndex = 1;
                isProcess = false;
                newPen.Width = (int) tsWidth.SelectedItem;
                Draw();
            }
        }

        private void ChangeSmoothing(object sender, EventArgs e) {
            graphics.SmoothingMode = cbSmoothing.Checked ? SmoothingMode.AntiAlias : SmoothingMode.None;
            Draw();
        }

        private void ChangeDock(object sender, EventArgs e) {
            newPen.DashStyle = cbDotted.Checked
                ? DashStyle.Dash
                : DashStyle.Solid;
            Draw();
        }
    }
}