using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Lab._2.Paint.Interfaces.UIInt;
using Lab._2.Paint.Module;
using Lab._2.Paint.Module.DrawingTools;
using Lab._2.Paint.Module.PhotoEffects;

namespace Lab._2.Paint.UI
{
    public partial class MainForm : Form, IDescriptionable, ICanvasProvider, IWorkspace
    {
        public MainForm() {
            InitializeComponent();
            DataCore.Descriptor = this;
            DataCore.Button = tsbttnPencil;
            tsbttnPencil.Checked = true;
            Names = new List<string>();
        }

        public ICanvas ActiveCanvas => ActiveMdiChild as ICanvas;

        public string Description {
            get => tslDescription.Text;
            set {
                tslDescription.Text = value;

                if (IsProcces) Refresh();
            }
        }

        public string Zoom {
            get => tsZoom.Text.Replace("%","");
            set {
                tsZoom.Text = value + "%";
                if (IsProcces) Refresh();
            }
        }

        public string Point {
            get => tsmiPoint.Text;
            set {
                if (!IsProcces)
                    tsmiPoint.Text = value;
            }
        }

        public bool IsProcces { get; set; }

        public int[] Indexes { get; set; }

        public List<string> Names { get; set; }

        private void NewFileAction(object sender, EventArgs e) {
            var dialog = new CanvasNameDialog(this);
            dialog.ShowDialog();
        }

        private void ChooseColorAction(object sender, EventArgs e) {
            var dialog = new ColorDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK) DataCore.Paintbrush.Color = dialog.Color;
        }

        private void SettingPen(object sender, EventArgs e) {
            var settingPen = new SettingPen();
            settingPen.ShowDialog();
            DataCore.CurrentDrawingTool = DefineDrawingType(DataCore.Button);
        }

        private void ExitAction(object sender, EventArgs e) {
            Close();
        }

        private void ClosingFormAction(object sender, FormClosingEventArgs e) {
            if (MdiChildren.Length == 0) return;

            var frmSave = new SaveCanvasesDialog(Names.ToArray(), this);
            var result = frmSave.ShowDialog();

            if (result == DialogResult.OK)
                foreach (var index in Indexes) {
                    var canvas = MdiChildren[index] as ICanvas;
                    SaveCanvas(canvas.Path, canvas.CanvasName, canvas.ShowedPicture);
                }
        }

        private void SaveProject(object sender, EventArgs e) {
            //If no children
            if (MdiChildren.Length == 0) {
                MessageBox.Show("Вам нечего сохранять",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            //Let's save active children
            var name = ActiveCanvas.CanvasName;
            var path = ActiveCanvas.Path;
            Names.Remove(name);
            SaveCanvas(path, name, ActiveCanvas.ShowedPicture);
        }

        private void SaveAsProject(object sender, EventArgs e) {
            if (MdiChildren.Length == 0) {
                MessageBox.Show("Вам нечего сохранять",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var name = ActiveCanvas.CanvasName;

            var saveFile = new SaveFileDialog();
            //Set filters for saveFileDialog
            saveFile.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
            saveFile.Title = "Сохранить проект";
            saveFile.FileName = name;

            Names.Remove(name);

            saveFile.CreatePrompt = true;
            saveFile.OverwritePrompt = true;

            var dg = saveFile.ShowDialog();

            if (dg == DialogResult.OK) {
                //If no fileName
                if (saveFile.FileName == "") saveFile.FileName = "Новый проект";

                //What type?
                switch (saveFile.FilterIndex) {
                    //Type Jpeg
                    case 1:
                        ActiveCanvas.ShowedPicture.Save(saveFile.FileName, ImageFormat.Jpeg);
                        break;
                    //Type bmp
                    case 2:
                        ActiveCanvas.ShowedPicture.Save(saveFile.FileName, ImageFormat.Bmp);
                        break;
                    //Type Gif
                    case 3:
                        ActiveCanvas.ShowedPicture.Save(saveFile.FileName, ImageFormat.Gif);
                        break;
                    //Type Png
                    case 4:
                        ActiveCanvas.ShowedPicture.Save(saveFile.FileName, ImageFormat.Png);
                        break;
                }
            }
            ActiveMdiChild.Dispose();
        }

        private void SaveCanvas(string path, string name, Image picture) {
            var expansions = "";

            if (path.Contains("."))
                expansions = path.Remove(0, path.Length - 4);

            //What type?
            switch (expansions) {
                //Type Jpeg
                case "jpg":
                    picture.Save(path, ImageFormat.Jpeg);
                    break;
                //Type bmp
                case "bmp":
                    picture.Save(path, ImageFormat.Bmp);
                    break;
                //Type Gif
                case "gif":
                    picture.Save(path, ImageFormat.Gif);
                    break;
                //Type Png
                case "png":
                    picture.Save(path, ImageFormat.Png);
                    break;
                default:
                    picture.Save(path + ".jpg", ImageFormat.Jpeg);
                    break;
            }
        }

        private void SortWindows(object sender, EventArgs e) {
            switch (((ToolStripMenuItem) sender).Name) {
                case "tsmiCascade": {
                    LayoutMdi(MdiLayout.Cascade);
                    break;
                }
                case "tsmiHorizontal": {
                    LayoutMdi(MdiLayout.TileHorizontal);
                    break;
                }
                case "tsmiVertical": {
                    LayoutMdi(MdiLayout.TileVertical);
                    break;
                }
                case "tsmiSortShourcuts": {
                    LayoutMdi(MdiLayout.ArrangeIcons);
                    break;
                }
            }
        }

        private DrawingTool DefineDrawingType(object sender) {
            switch (((ToolStripButton) sender).Name) {
                case "tsbttnPencil": return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.Pencil, ActiveCanvas);
                case "tsbttnLine": return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.Line, ActiveCanvas);
                case "tsbttnTriangle":
                    return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.NCornerStar, ActiveCanvas, 3);
                case "tsbttnRectangle":
                    return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.Rectangle, ActiveCanvas);
                case "tsbttnRomb":
                    return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.NCornerStar, ActiveCanvas, 2);
                case "tsbttnEllipse":
                    return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.Ellipse, ActiveCanvas);
                case "tsbttnCornerStar4":
                    return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.NCornerStar, ActiveCanvas, 4);
                case "tsbttnCornerStar5":
                    return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.NCornerStar, ActiveCanvas, 5);
                case "tsbttnCornerStar6":
                    return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.NCornerStar, ActiveCanvas, 6);
                case "tsbttnEraser": return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.Eraser, ActiveCanvas);
                case "tsbttnMagicalEraser":
                    return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.MagicalEraser, ActiveCanvas);
                default: return DefineTypeDrawing.TypeDrawing(DefineTypeDrawing.Type.None, ActiveCanvas);
            }
        }

        private void ChangeTypeDrawing(object sender, EventArgs e) {
            DataCore.Button.Checked = false;
            DataCore.Button = (ToolStripButton) sender;
            DataCore.Button.Checked = true;
            DataCore.CurrentDrawingTool = DefineDrawingType(sender);
        }

        private void MouseMoveAction(object sender, MouseEventArgs e) {
            if (sender is ToolStripButton) {
                switch (((ToolStripButton) sender).Name) {
                    case "tsmiChooseColor": {
                        Description = "Выбор цвета карандаша";
                        break;
                    }

                    case "tsbttnZoomOut": {
                        Description = "Уменьшить изображение";
                        break;
                    }
                    case "tsbttnZoomIn": {
                        Description = "Увеличить изображение";
                        break;
                    }

                    #region DrawingTools

                    case "tsbttnPencil": {
                        Description = "Выбрать инструмент: Карандаш";
                        break;
                    }
                    case "tsbttnLine": {
                        Description = "Выбрать инструмент: Линия";
                        break;
                    }
                    case "tsbttnTriangle": {
                        Description = "Выбрать инструмент: Треугольник";
                        break;
                    }
                    case "tsbttnRectangle": {
                        Description = "Выбрать инструмент: Прямоугольник";
                        break;
                    }
                    case "tsbttnRomb": {
                        Description = "Выбрать инструмент: Ромб";
                        break;
                    }
                    case "tsbttnEllipse": {
                        Description = "Выбрать инструмент: Элипс";
                        break;
                    }
                    case "tsbttnCornerStar4": {
                        Description = "Выбрать инструмент: Четырехугольная звезда";
                        break;
                    }
                    case "tsbttnCornerStar5": {
                        Description = "Выбрать инструмент: Пятиугольная звезда";
                        break;
                    }
                    case "tsbttnCornerStar6": {
                        Description = "Выбрать инструмент: Шестиугольная звезда";
                        break;
                    }
                    case "tsbttnEraser": {
                        Description = "Выбрать инструмент: Ластик";
                        break;
                    }
                    case "tsbttnMagicalEraser": {
                        Description = "Выбрать инструмент: Магический ластик";
                        break;
                    }

                    #endregion
                }

                return;
            }

            if (sender is ToolStripMenuItem) {
                switch (((ToolStripMenuItem) sender).Name) {
                    #region WindowMenu

                    case "tsmiCascade": {
                        Description = "Расположить окна каскадом";
                        break;
                    }
                    case "tsmiHorizontal": {
                        Description = "Расположить окна по горизонтали";
                        break;
                    }
                    case "tsmiVertical": {
                        Description = "Расположить окна по вертикали";
                        break;
                    }
                    case "tsmiSortShourcuts": {
                        Description = "Расположить окна по упорядоченным значкам";
                        break;
                    }

                    #endregion

                    #region Effects

                    case "tsmiAcceptEffect": {
                        Description = "Меню эффектов";
                        break;
                    }
                    case "tsmiSharpness": {
                        Description = "Применить эффект: Резкость";
                        break;
                    }
                    case "tsmiSmoothing": {
                        Description = "Применить эффект: Сглаживание";
                        break;
                    }
                    case "tsmiBlur": {
                        Description = "Примернить эффект: Размытие";
                        break;
                    }
                    case "tsmiEmbrass": {
                        Description = "Применить эффект: Рельеф";
                        break;
                    }
                    case "tsmiRotateOnClock": {
                        Description = "Повернуть изображение по часовой стрелке";
                        break;
                    }
                    case "tsmiRotateUnderClock": {
                        Description = "Повернуть изображение против часовой стрелке";
                        break;
                    }
                    case "tsmiFlipHorizontal": {
                        Description = "Отобразить изображение относительно оси Х";
                        break;
                    }
                    case "tsmiFlipVertical": {
                        Description = "Отобразить изображение относительно оси Y";
                        break;
                    }
                    case "tsmiZoomIn": {
                        Description = "Увеличить изображение";
                        break;
                    }
                    case "tsmiZoomFrom": {
                        Description = "Уменьшить изображение";
                        break;
                    }

                    #endregion

                    #region File

                    case "tsmiNewFIle": {
                        Description = "Создать новый проект";
                        break;
                    }
                    case "tsmiOpenFile": {
                        Description = "Открыть существующий проект";
                        break;
                    }
                    case "tsmiSaveFile": {
                        Description = "Сохранить проект";
                        break;
                    }
                    case "tsmiSaveAsFile": {
                        Description = "Сохранить проект как...";
                        break;
                    }
                    case "tsmiCloseApplication": {
                        Description = "Закрыть приложение";
                        break;
                    }

                    #endregion
                }

                return;
            }

            Description = "Настройка карандаша";
        }

        private void MouseLeaveAction(object sender, EventArgs e) {
            Description = "Описание";
        }

        private void RebderEffect(object sender, EventArgs e) {
            if (MdiChildren.Length != 0) {
                DataCore.CurrentPhotoTool = Effect(sender);
                DataCore.CurrentPhotoTool.Render(ActiveCanvas);

            }
        }

        private PhotoTool Effect(object sender) {
            if (sender is ToolStripMenuItem) {
                switch (((ToolStripMenuItem) sender).Name) {
                    case "tsmiFlipVertical":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.FlipVertical, this);
                    case "tsmiFlipHorizontal":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.FlipHorizontal, this);
                    case "tsmiRotateUnderClock":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.RotateUnderClock, this);
                    case "tsmiRotateOnClock":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.RotateOnClock, this);
                    case "tsmiSharpness":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.Sharpness, this);
                    case "tsmiEmbrass":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.Embrass, this);
                    case "tsmiBlur":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.Blur, this);
                    case "tsmiSmoothing":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.Smoothing, this);
                    case "tsmiZoomIn":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.ZoomIn, this);
                    case "tsmiZoomFrom":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.ZoomOut, this);
                    default:
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.None, this);
                }
            }
            else {
                switch (((ToolStripButton) sender).Name) {
                    case "tsbttnZoomIn":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.ZoomIn, this);
                    case "tsbttnZoomOut":
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.ZoomOut, this);
                    default:
                        return DefinePhotoEffect.EffectRender(DefinePhotoEffect.Effect.None, this);
                }

                
            }
        }

        private void OpenFileProject(object sender, EventArgs e) {
            var name = "";
            var fileName = "";

            var openFile = new OpenFileDialog();
            //Set filters for openFileDialog
            openFile.Filter = "Images (*.jpg, *.bmp, *.gif, *.png)|*.jpg;*.bmp;*.gif;*.png|" +
                              "All files(*.*)|*.*";
            openFile.Title = "Открыть проект";
            var dr = openFile.ShowDialog();

            //if close openFileDialog
            if (dr == DialogResult.OK) {
                //If fileName is empty or wrong expansion
                if (openFile.FileName == "" &&
                    Path.GetExtension(openFile.FileName) != ".gif" &&
                    Path.GetExtension(openFile.FileName) != ".jpg" &&
                    Path.GetExtension(openFile.FileName) != ".bpm" &&
                    Path.GetExtension(openFile.FileName) != ".png") {
                    MessageBox.Show("Ошибка!\n Открыт файл неверного разрешения или несуществующий файл...",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                //Set name of file
                name = openFile.SafeFileName;
                //Remove expansion
                name = name.Remove(name.Length - 4);

                //Add to list of the names
                Names.Add(openFile.SafeFileName.Remove(openFile.SafeFileName.Length - 4));

                //Set file name
                fileName = openFile.FileName;
            }

            if (fileName != "") {
                var temp = name;
                if (Names.Contains(temp)) {
                    var id = 1;
                    while (true) {
                        temp = name;
                        temp = string.Concat(temp, " (", id, ")");
                        id++;
                        if (!Names.Contains(temp)) break;
                    }
                }

                //Create a child form
                new CanvasForm(name,
                    Image.FromFile(fileName),
                    fileName, this)
                    {
                        MdiParent = this
                    }.Show();
            }
        }
    }
}