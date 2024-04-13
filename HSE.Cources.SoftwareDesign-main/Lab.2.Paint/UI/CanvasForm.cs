using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Lab._2.Paint.Interfaces.UIInt;
using Lab._2.Paint.Module;
using Lab._2.Paint.Module.DrawingTools.Tools;

namespace Lab._2.Paint.UI
{
    public partial class CanvasForm : Form, ICanvas
    {
        public CanvasForm(string canvasName, IDescriptionable descriptor) {
            InitializeComponent();

            ShowedPictureBox.Image = new Bitmap(ShowedPictureBox.Width, ShowedPictureBox.Height);
            Graphics g = Graphics.FromImage(ShowedPictureBox.Image);
            g.Clear(Color.White);
            CanvasName = canvasName;
            Path = canvasName;
            Descriptor = descriptor;
            g.Dispose();
            CZoom = 100;
        }

        public CanvasForm(string canvasName, Image image, string _Path, IDescriptionable descriptor) {
            InitializeComponent();

            CanvasName = canvasName;
            Path = _Path;

            Descriptor = descriptor;
            OriginPicture = new Bitmap(image, ShowedPictureBox.Width, ShowedPictureBox.Height);
            ShowedPictureBox.Image = new Bitmap(ShowedPictureBox.Width, ShowedPictureBox.Height);
            Graphics g = Graphics.FromImage(ShowedPictureBox.Image);
            g.Clear(Color.White);
            g.DrawImage(OriginPicture,0,0);
            g.Dispose();
            Fit_size();
            CZoom = 100;
        }

        public IDescriptionable Descriptor { get; }

        public Bitmap OriginPicture { get; set; }

        public Bitmap ShowedPicture {
            get => ShowedPictureBox.Image as Bitmap;
            set {
                if (value != null) ShowedPictureBox.Image = value;
            }
        }

        public string CanvasName {
            get => Text;
            set => Text = value;
        }

        public Size GetSize {
            get => Size;
            set => Size = new Size(value.Width, value.Height);
        }

        public string Path { get; set; }

        public int CZoom { get; set; }

        void ICanvas.ExecuteChanging() {
            ShowedPictureBox.Refresh();
        }

        public void _Resize() {
            if (Width != ShowedPicture.Width ||
                Height != ShowedPicture.Height)
                Size = new Size(ShowedPicture.Size.Width + 16,
                    ShowedPicture.Size.Height + 39);
        }

        private void PreShowFigure(object sender, PaintEventArgs e) {
            if (isPreShow())
                DataCore.CurrentDrawingTool.PreShowDrawing(this, e.Graphics);
        }

        private bool isPreShow() {
            return !(DataCore.CurrentDrawingTool is Pencil ||
                     DataCore.CurrentDrawingTool is Eraser ||
                     DataCore.CurrentDrawingTool is MagicalEraser);
        }

        private void DrawPreShowFigures_MouseUpAction(PointF point) {
            DataCore.CurrentDrawingTool.Draw(this, point);
        }

        private void DrawOthersFigures_MouseUpAction(PointF point) {
            DataCore.CurrentDrawingTool.EndDrawing(this, point);
        }

        private void DrawPreShowFigures_MouseMoveAction(PointF point) {
            DataCore.CurrentDrawingTool.PreShowing(this, point);
        }

        private void DrawOthersFigures_MouseMoveAction(PointF point) {
            DataCore.CurrentDrawingTool.Draw(this, point);
        }

        public void Fit_size() {
            if (ShowedPictureBox.Image != null &&
                (ShowedPictureBox.Image.Width < ShowedPictureBox.Width ||
                 ShowedPictureBox.Image.Height < ShowedPictureBox.Height)) {
                if (OriginPicture != null) {
                    var photoBitmap = OriginPicture.Clone() as Bitmap;
                    OriginPicture = new Bitmap(ShowedPictureBox.Width, ShowedPictureBox.Height);
                    var g1 = Graphics.FromImage(OriginPicture);
                    g1.FillRectangle(Brushes.White, 0, 0, ShowedPictureBox.Width, ShowedPictureBox.Height);
                    g1.DrawImage(photoBitmap, 0, 0, photoBitmap.Width, photoBitmap.Height);
                }


                var sketchBitmap = ShowedPicture.Clone() as Bitmap;
                ShowedPicture = new Bitmap(ShowedPictureBox.Width, ShowedPictureBox.Height);
                var g2 = Graphics.FromImage(ShowedPicture);
                g2.DrawImage(sketchBitmap, 0, 0, sketchBitmap.Width, sketchBitmap.Height);

                ShowedPictureBox.Refresh();
                _Resize();
            }
        }

        private void savingProject(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                (ParentForm as IWorkspace).Names.Remove(Text);
                //Ask saving
                var dg = MessageBox.Show("Внимание!\nСохранить ли проект :\"" + Text + "\"",
                    "Внимание!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                //If save
                if (dg == DialogResult.Yes) {
                    //Let's save active children
                    var saveFile = new SaveFileDialog();
                    //Set filters for saveFileDialog
                    saveFile.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
                    saveFile.Title = "Сохранить проект";
                    saveFile.FileName = Text;


                    saveFile.CreatePrompt = true;
                    saveFile.OverwritePrompt = true;

                    dg = saveFile.ShowDialog();

                    if (dg == DialogResult.OK) {
                        //If no fileName
                        if (saveFile.FileName == "") saveFile.FileName = "Новый проект";

                        //What type?
                        switch (saveFile.FilterIndex) {
                            //Type Jpeg
                            case 1:
                                ShowedPictureBox.Image.Save(saveFile.FileName, ImageFormat.Jpeg);
                                break;
                            //Type bmp
                            case 2:
                                ShowedPictureBox.Image.Save(saveFile.FileName, ImageFormat.Bmp);
                                break;
                            //Type Gif
                            case 3:
                                ShowedPictureBox.Image.Save(saveFile.FileName, ImageFormat.Gif);
                                break;
                            //Type Png
                            case 4:
                                ShowedPictureBox.Image.Save(saveFile.FileName, ImageFormat.Png);
                                break;
                        }
                    }
                }
            }
        }

        #region MouseActions

        private void MouseDownAction(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) return;

            if (ShowedPictureBox.Size != ShowedPicture.Size) Fit_size();

            DataCore.CurrentDrawingTool.PrepareToDrawing(this, e.Location);
        }

        private void MouseMoveAction(object sender, MouseEventArgs e) {
            Descriptor.Point = "X: " + e.X + " Y: " + e.Y;
            Descriptor.Zoom = CZoom.ToString();
            if (e.Button != MouseButtons.Left) return;

            if (isPreShow())
                DrawPreShowFigures_MouseMoveAction(e.Location);
            else
                DrawOthersFigures_MouseMoveAction(e.Location);
        }

        private void MouseLeaveAction(object sender, EventArgs e) {
            Descriptor.Point = "X: " + " Y: ";
        }

        private void MouseMoveUp(object sender, MouseEventArgs e) {
            if (isPreShow())
                DrawPreShowFigures_MouseUpAction(e.Location);
            else
                DrawOthersFigures_MouseUpAction(e.Location);
        }

        #endregion
    }
}