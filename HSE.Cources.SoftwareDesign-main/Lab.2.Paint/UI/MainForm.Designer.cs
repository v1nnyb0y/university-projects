namespace Lab._2.Paint.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewFIle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCloseApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPhotoEffects = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAcceptEffect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSharpness = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEmbrass = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBlur = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSmoothing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRotateOnClock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRotateUnderClock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFlipHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFlipVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiZoomFrom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSortShourcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsZoom = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiChooseColor = new System.Windows.Forms.ToolStripButton();
            this.tsbttnSetting = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbttnPencil = new System.Windows.Forms.ToolStripButton();
            this.tsbttnLine = new System.Windows.Forms.ToolStripButton();
            this.tsbttnTriangle = new System.Windows.Forms.ToolStripButton();
            this.tsbttnRectangle = new System.Windows.Forms.ToolStripButton();
            this.tsbttnRomb = new System.Windows.Forms.ToolStripButton();
            this.tsbttnEllipse = new System.Windows.Forms.ToolStripButton();
            this.tsbttnCornerStar4 = new System.Windows.Forms.ToolStripButton();
            this.tsbttnCornerStar5 = new System.Windows.Forms.ToolStripButton();
            this.tsbttnCornerStar6 = new System.Windows.Forms.ToolStripButton();
            this.tsbttnMagicalEraser = new System.Windows.Forms.ToolStripButton();
            this.tsbttnEraser = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbttnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbttnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslDescription = new System.Windows.Forms.ToolStripLabel();
            this.tsmiPoint = new System.Windows.Forms.ToolStripLabel();
            this.MenuStrip.SuspendLayout();
            this.ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiPhotoEffects,
            this.tsmiWindow});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.MenuStrip.Size = new System.Drawing.Size(1158, 27);
            this.MenuStrip.TabIndex = 1;
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewFIle,
            this.toolStripSeparator1,
            this.tsmiOpenFile,
            this.tsmiSaveFile,
            this.tsmiSaveAsFile,
            this.tsmiCloseApplication});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(53, 23);
            this.tsmiFile.Text = "Файл";
            // 
            // tsmiNewFIle
            // 
            this.tsmiNewFIle.Image = global::Lab._2.Paint.Properties.Resources.EmptyProject;
            this.tsmiNewFIle.Name = "tsmiNewFIle";
            this.tsmiNewFIle.Size = new System.Drawing.Size(183, 26);
            this.tsmiNewFIle.Text = "Создать проект";
            this.tsmiNewFIle.Click += new System.EventHandler(this.NewFileAction);
            this.tsmiNewFIle.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiNewFIle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // tsmiOpenFile
            // 
            this.tsmiOpenFile.Image = global::Lab._2.Paint.Properties.Resources.OpenDocument;
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.Size = new System.Drawing.Size(183, 26);
            this.tsmiOpenFile.Text = "Открыть";
            this.tsmiOpenFile.Click += new System.EventHandler(this.OpenFileProject);
            this.tsmiOpenFile.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiOpenFile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiSaveFile
            // 
            this.tsmiSaveFile.Image = global::Lab._2.Paint.Properties.Resources.Save;
            this.tsmiSaveFile.Name = "tsmiSaveFile";
            this.tsmiSaveFile.Size = new System.Drawing.Size(183, 26);
            this.tsmiSaveFile.Text = "Сохранить";
            this.tsmiSaveFile.Click += new System.EventHandler(this.SaveProject);
            this.tsmiSaveFile.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiSaveFile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiSaveAsFile
            // 
            this.tsmiSaveAsFile.Image = global::Lab._2.Paint.Properties.Resources.saveAs;
            this.tsmiSaveAsFile.Name = "tsmiSaveAsFile";
            this.tsmiSaveAsFile.Size = new System.Drawing.Size(183, 26);
            this.tsmiSaveAsFile.Text = "Сохранить как...";
            this.tsmiSaveAsFile.Click += new System.EventHandler(this.SaveAsProject);
            this.tsmiSaveAsFile.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiSaveAsFile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiCloseApplication
            // 
            this.tsmiCloseApplication.Image = global::Lab._2.Paint.Properties.Resources.Exit;
            this.tsmiCloseApplication.Name = "tsmiCloseApplication";
            this.tsmiCloseApplication.Size = new System.Drawing.Size(183, 26);
            this.tsmiCloseApplication.Text = "Выход";
            this.tsmiCloseApplication.Click += new System.EventHandler(this.ExitAction);
            this.tsmiCloseApplication.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiCloseApplication.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiPhotoEffects
            // 
            this.tsmiPhotoEffects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAcceptEffect,
            this.tsmiRotateOnClock,
            this.tsmiRotateUnderClock,
            this.tsmiFlipHorizontal,
            this.tsmiFlipVertical,
            this.tsmiZoomIn,
            this.tsmiZoomFrom});
            this.tsmiPhotoEffects.Name = "tsmiPhotoEffects";
            this.tsmiPhotoEffects.Size = new System.Drawing.Size(73, 23);
            this.tsmiPhotoEffects.Text = "Рисунок";
            // 
            // tsmiAcceptEffect
            // 
            this.tsmiAcceptEffect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSharpness,
            this.tsmiEmbrass,
            this.tsmiBlur,
            this.tsmiSmoothing});
            this.tsmiAcceptEffect.Name = "tsmiAcceptEffect";
            this.tsmiAcceptEffect.Size = new System.Drawing.Size(255, 26);
            this.tsmiAcceptEffect.Text = "Применить эффекты";
            // 
            // tsmiSharpness
            // 
            this.tsmiSharpness.Image = global::Lab._2.Paint.Properties.Resources.Sharpness;
            this.tsmiSharpness.Name = "tsmiSharpness";
            this.tsmiSharpness.Size = new System.Drawing.Size(165, 26);
            this.tsmiSharpness.Text = "Резкость";
            this.tsmiSharpness.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiSharpness.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiSharpness.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiEmbrass
            // 
            this.tsmiEmbrass.Image = global::Lab._2.Paint.Properties.Resources.Imahe_relief;
            this.tsmiEmbrass.Name = "tsmiEmbrass";
            this.tsmiEmbrass.Size = new System.Drawing.Size(165, 26);
            this.tsmiEmbrass.Text = "Рельеф";
            this.tsmiEmbrass.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiEmbrass.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiEmbrass.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiBlur
            // 
            this.tsmiBlur.Image = global::Lab._2.Paint.Properties.Resources.Image_blur;
            this.tsmiBlur.Name = "tsmiBlur";
            this.tsmiBlur.Size = new System.Drawing.Size(165, 26);
            this.tsmiBlur.Text = "Размытие";
            this.tsmiBlur.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiBlur.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiBlur.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiSmoothing
            // 
            this.tsmiSmoothing.Image = global::Lab._2.Paint.Properties.Resources.Image_smoothing;
            this.tsmiSmoothing.Name = "tsmiSmoothing";
            this.tsmiSmoothing.Size = new System.Drawing.Size(165, 26);
            this.tsmiSmoothing.Text = "Сглаживание";
            this.tsmiSmoothing.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiSmoothing.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiSmoothing.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiRotateOnClock
            // 
            this.tsmiRotateOnClock.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.tsmiRotateOnClock.Image = global::Lab._2.Paint.Properties.Resources.Cicle_arrow_to_right;
            this.tsmiRotateOnClock.Name = "tsmiRotateOnClock";
            this.tsmiRotateOnClock.Size = new System.Drawing.Size(255, 26);
            this.tsmiRotateOnClock.Text = "Повернуть по часовой";
            this.tsmiRotateOnClock.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiRotateOnClock.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiRotateOnClock.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiRotateUnderClock
            // 
            this.tsmiRotateUnderClock.Image = global::Lab._2.Paint.Properties.Resources.Cicle_arrow_to_left;
            this.tsmiRotateUnderClock.Name = "tsmiRotateUnderClock";
            this.tsmiRotateUnderClock.Size = new System.Drawing.Size(255, 26);
            this.tsmiRotateUnderClock.Text = "Повернуть против часовой";
            this.tsmiRotateUnderClock.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiRotateUnderClock.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiRotateUnderClock.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiFlipHorizontal
            // 
            this.tsmiFlipHorizontal.Image = global::Lab._2.Paint.Properties.Resources.Flip_horizontal;
            this.tsmiFlipHorizontal.Name = "tsmiFlipHorizontal";
            this.tsmiFlipHorizontal.Size = new System.Drawing.Size(255, 26);
            this.tsmiFlipHorizontal.Text = "Отразить по горизонтали";
            this.tsmiFlipHorizontal.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiFlipHorizontal.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiFlipHorizontal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiFlipVertical
            // 
            this.tsmiFlipVertical.Image = global::Lab._2.Paint.Properties.Resources.Flip_vertical1;
            this.tsmiFlipVertical.Name = "tsmiFlipVertical";
            this.tsmiFlipVertical.Size = new System.Drawing.Size(255, 26);
            this.tsmiFlipVertical.Text = "Отразить по вертикали";
            this.tsmiFlipVertical.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiFlipVertical.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiFlipVertical.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiZoomIn
            // 
            this.tsmiZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsmiZoomIn.Image")));
            this.tsmiZoomIn.Name = "tsmiZoomIn";
            this.tsmiZoomIn.Size = new System.Drawing.Size(255, 26);
            this.tsmiZoomIn.Text = "Увеличить изображение";
            this.tsmiZoomIn.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiZoomIn.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiZoomIn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiZoomFrom
            // 
            this.tsmiZoomFrom.Image = global::Lab._2.Paint.Properties.Resources.zoom_out;
            this.tsmiZoomFrom.Name = "tsmiZoomFrom";
            this.tsmiZoomFrom.Size = new System.Drawing.Size(255, 26);
            this.tsmiZoomFrom.Text = "Уменьшить изображение";
            this.tsmiZoomFrom.Click += new System.EventHandler(this.RebderEffect);
            this.tsmiZoomFrom.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiZoomFrom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiWindow
            // 
            this.tsmiWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCascade,
            this.tsmiHorizontal,
            this.tsmiVertical,
            this.tsmiSortShourcuts});
            this.tsmiWindow.Name = "tsmiWindow";
            this.tsmiWindow.Size = new System.Drawing.Size(55, 23);
            this.tsmiWindow.Text = "Окно";
            // 
            // tsmiCascade
            // 
            this.tsmiCascade.Image = global::Lab._2.Paint.Properties.Resources.Cascade;
            this.tsmiCascade.Name = "tsmiCascade";
            this.tsmiCascade.Size = new System.Drawing.Size(214, 26);
            this.tsmiCascade.Text = "Каскадом";
            this.tsmiCascade.Click += new System.EventHandler(this.SortWindows);
            this.tsmiCascade.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiCascade.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiHorizontal
            // 
            this.tsmiHorizontal.Image = global::Lab._2.Paint.Properties.Resources.From_left_to_right;
            this.tsmiHorizontal.Name = "tsmiHorizontal";
            this.tsmiHorizontal.Size = new System.Drawing.Size(214, 26);
            this.tsmiHorizontal.Text = "Слева направо";
            this.tsmiHorizontal.Click += new System.EventHandler(this.SortWindows);
            this.tsmiHorizontal.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiHorizontal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiVertical
            // 
            this.tsmiVertical.Image = global::Lab._2.Paint.Properties.Resources.Top_down;
            this.tsmiVertical.Name = "tsmiVertical";
            this.tsmiVertical.Size = new System.Drawing.Size(214, 26);
            this.tsmiVertical.Text = "Сверху вниз";
            this.tsmiVertical.Click += new System.EventHandler(this.SortWindows);
            this.tsmiVertical.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiVertical.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsmiSortShourcuts
            // 
            this.tsmiSortShourcuts.Image = global::Lab._2.Paint.Properties.Resources.Clean_up;
            this.tsmiSortShourcuts.Name = "tsmiSortShourcuts";
            this.tsmiSortShourcuts.Size = new System.Drawing.Size(214, 26);
            this.tsmiSortShourcuts.Text = "Упорядочить значки";
            this.tsmiSortShourcuts.Click += new System.EventHandler(this.SortWindows);
            this.tsmiSortShourcuts.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiSortShourcuts.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // ToolStrip
            // 
            this.ToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolStrip.Font = new System.Drawing.Font("Segoe UI", 20.25F);
            this.ToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsZoom,
            this.toolStripSeparator2,
            this.tsmiChooseColor,
            this.tsbttnSetting,
            this.toolStripButton1,
            this.tsbttnPencil,
            this.tsbttnLine,
            this.tsbttnTriangle,
            this.tsbttnRectangle,
            this.tsbttnRomb,
            this.tsbttnEllipse,
            this.tsbttnCornerStar4,
            this.tsbttnCornerStar5,
            this.tsbttnCornerStar6,
            this.tsbttnMagicalEraser,
            this.tsbttnEraser,
            this.toolStripButton2,
            this.tsbttnZoomOut,
            this.tsbttnZoomIn,
            this.toolStripButton3,
            this.tslDescription,
            this.tsmiPoint});
            this.ToolStrip.Location = new System.Drawing.Point(0, 660);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(1158, 31);
            this.ToolStrip.TabIndex = 2;
            // 
            // tsZoom
            // 
            this.tsZoom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tsZoom.Name = "tsZoom";
            this.tsZoom.Size = new System.Drawing.Size(44, 28);
            this.tsZoom.Text = "100%";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsmiChooseColor
            // 
            this.tsmiChooseColor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiChooseColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsmiChooseColor.Image = global::Lab._2.Paint.Properties.Resources.Palette;
            this.tsmiChooseColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiChooseColor.Name = "tsmiChooseColor";
            this.tsmiChooseColor.Size = new System.Drawing.Size(28, 28);
            this.tsmiChooseColor.Text = "Выбор цвета";
            this.tsmiChooseColor.Click += new System.EventHandler(this.ChooseColorAction);
            this.tsmiChooseColor.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsmiChooseColor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnSetting
            // 
            this.tsbttnSetting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnSetting.Image = global::Lab._2.Paint.Properties.Resources.Pencil_width1;
            this.tsbttnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnSetting.Name = "tsbttnSetting";
            this.tsbttnSetting.Size = new System.Drawing.Size(40, 28);
            this.tsbttnSetting.Text = "Настройка карандаша";
            this.tsbttnSetting.Click += new System.EventHandler(this.SettingPen);
            this.tsbttnSetting.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnSetting.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbttnPencil
            // 
            this.tsbttnPencil.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnPencil.Image = global::Lab._2.Paint.Properties.Resources.pencil__1_;
            this.tsbttnPencil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnPencil.Name = "tsbttnPencil";
            this.tsbttnPencil.Size = new System.Drawing.Size(28, 28);
            this.tsbttnPencil.Text = "Карандаш";
            this.tsbttnPencil.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnPencil.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnPencil.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnLine
            // 
            this.tsbttnLine.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnLine.Image = global::Lab._2.Paint.Properties.Resources.Line;
            this.tsbttnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnLine.Name = "tsbttnLine";
            this.tsbttnLine.Size = new System.Drawing.Size(28, 28);
            this.tsbttnLine.Text = "Прямая";
            this.tsbttnLine.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnLine.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnLine.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnTriangle
            // 
            this.tsbttnTriangle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnTriangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnTriangle.Image = global::Lab._2.Paint.Properties.Resources.Triangle;
            this.tsbttnTriangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnTriangle.Name = "tsbttnTriangle";
            this.tsbttnTriangle.Size = new System.Drawing.Size(28, 28);
            this.tsbttnTriangle.Text = "Треугольник";
            this.tsbttnTriangle.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnTriangle.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnTriangle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnRectangle
            // 
            this.tsbttnRectangle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnRectangle.Image = global::Lab._2.Paint.Properties.Resources.Rectangle;
            this.tsbttnRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnRectangle.Name = "tsbttnRectangle";
            this.tsbttnRectangle.Size = new System.Drawing.Size(28, 28);
            this.tsbttnRectangle.Text = "Прямоугольник";
            this.tsbttnRectangle.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnRectangle.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnRectangle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnRomb
            // 
            this.tsbttnRomb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnRomb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnRomb.Image = global::Lab._2.Paint.Properties.Resources.Romb;
            this.tsbttnRomb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnRomb.Name = "tsbttnRomb";
            this.tsbttnRomb.Size = new System.Drawing.Size(28, 28);
            this.tsbttnRomb.Text = "Ромб";
            this.tsbttnRomb.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnRomb.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnRomb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnEllipse
            // 
            this.tsbttnEllipse.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnEllipse.Image = global::Lab._2.Paint.Properties.Resources.Ellipse;
            this.tsbttnEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnEllipse.Name = "tsbttnEllipse";
            this.tsbttnEllipse.Size = new System.Drawing.Size(28, 28);
            this.tsbttnEllipse.Text = "Элипс";
            this.tsbttnEllipse.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnEllipse.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnEllipse.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnCornerStar4
            // 
            this.tsbttnCornerStar4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnCornerStar4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnCornerStar4.Image = global::Lab._2.Paint.Properties.Resources.CornerStar4;
            this.tsbttnCornerStar4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnCornerStar4.Name = "tsbttnCornerStar4";
            this.tsbttnCornerStar4.Size = new System.Drawing.Size(28, 28);
            this.tsbttnCornerStar4.Text = "Четырехугольная звезда";
            this.tsbttnCornerStar4.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnCornerStar4.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnCornerStar4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnCornerStar5
            // 
            this.tsbttnCornerStar5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnCornerStar5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnCornerStar5.Image = global::Lab._2.Paint.Properties.Resources.CornerStar5;
            this.tsbttnCornerStar5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnCornerStar5.Name = "tsbttnCornerStar5";
            this.tsbttnCornerStar5.Size = new System.Drawing.Size(28, 28);
            this.tsbttnCornerStar5.Text = "Пятиугольная звезда";
            this.tsbttnCornerStar5.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnCornerStar5.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnCornerStar5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnCornerStar6
            // 
            this.tsbttnCornerStar6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnCornerStar6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnCornerStar6.Image = global::Lab._2.Paint.Properties.Resources.CornerStar6;
            this.tsbttnCornerStar6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnCornerStar6.Name = "tsbttnCornerStar6";
            this.tsbttnCornerStar6.Size = new System.Drawing.Size(28, 28);
            this.tsbttnCornerStar6.Text = "Шестиугольная звезда";
            this.tsbttnCornerStar6.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnCornerStar6.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnCornerStar6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnMagicalEraser
            // 
            this.tsbttnMagicalEraser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnMagicalEraser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnMagicalEraser.Image = global::Lab._2.Paint.Properties.Resources.Magical_Eraser;
            this.tsbttnMagicalEraser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnMagicalEraser.Name = "tsbttnMagicalEraser";
            this.tsbttnMagicalEraser.Size = new System.Drawing.Size(28, 28);
            this.tsbttnMagicalEraser.Text = "Магический ластик";
            this.tsbttnMagicalEraser.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnMagicalEraser.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnMagicalEraser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnEraser
            // 
            this.tsbttnEraser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnEraser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnEraser.Image = global::Lab._2.Paint.Properties.Resources.Eraser;
            this.tsbttnEraser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnEraser.Name = "tsbttnEraser";
            this.tsbttnEraser.Size = new System.Drawing.Size(28, 28);
            this.tsbttnEraser.Text = "Ластик";
            this.tsbttnEraser.Click += new System.EventHandler(this.ChangeTypeDrawing);
            this.tsbttnEraser.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnEraser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbttnZoomOut
            // 
            this.tsbttnZoomOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnZoomOut.Image = global::Lab._2.Paint.Properties.Resources.zoom_out;
            this.tsbttnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnZoomOut.Name = "tsbttnZoomOut";
            this.tsbttnZoomOut.Size = new System.Drawing.Size(28, 28);
            this.tsbttnZoomOut.Text = "Уменьшить изображение";
            this.tsbttnZoomOut.Click += new System.EventHandler(this.RebderEffect);
            this.tsbttnZoomOut.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnZoomOut.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // tsbttnZoomIn
            // 
            this.tsbttnZoomIn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbttnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbttnZoomIn.Image = global::Lab._2.Paint.Properties.Resources.Zoom_in1;
            this.tsbttnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbttnZoomIn.Name = "tsbttnZoomIn";
            this.tsbttnZoomIn.Size = new System.Drawing.Size(28, 28);
            this.tsbttnZoomIn.Text = "Увеличить изображение";
            this.tsbttnZoomIn.Click += new System.EventHandler(this.RebderEffect);
            this.tsbttnZoomIn.MouseLeave += new System.EventHandler(this.MouseLeaveAction);
            this.tsbttnZoomIn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveAction);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(6, 31);
            // 
            // tslDescription
            // 
            this.tslDescription.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tslDescription.Name = "tslDescription";
            this.tslDescription.Size = new System.Drawing.Size(72, 28);
            this.tslDescription.Text = "Описание";
            // 
            // tsmiPoint
            // 
            this.tsmiPoint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tsmiPoint.Name = "tsmiPoint";
            this.tsmiPoint.Size = new System.Drawing.Size(35, 28);
            this.tsmiPoint.Text = "X: Y:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 691);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClosingFormAction);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewFIle;
        private System.Windows.Forms.ToolStripLabel tslDescription;
        private System.Windows.Forms.ToolStripMenuItem tsmiPhotoEffects;
        private System.Windows.Forms.ToolStripMenuItem tsmiRotateOnClock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveAsFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiCloseApplication;
        private System.Windows.Forms.ToolStripMenuItem tsmiWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmiCascade;
        private System.Windows.Forms.ToolStripMenuItem tsmiHorizontal;
        private System.Windows.Forms.ToolStripMenuItem tsmiVertical;
        private System.Windows.Forms.ToolStripMenuItem tsmiSortShourcuts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsmiChooseColor;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripButton2;
        private System.Windows.Forms.ToolStripSplitButton tsbttnSetting;
        private System.Windows.Forms.ToolStripButton tsbttnPencil;
        private System.Windows.Forms.ToolStripLabel tsmiPoint;
        private System.Windows.Forms.ToolStripLabel tsZoom;
        private System.Windows.Forms.ToolStripMenuItem tsmiRotateUnderClock;
        private System.Windows.Forms.ToolStripMenuItem tsmiAcceptEffect;
        private System.Windows.Forms.ToolStripMenuItem tsmiSharpness;
        private System.Windows.Forms.ToolStripMenuItem tsmiEmbrass;
        private System.Windows.Forms.ToolStripMenuItem tsmiBlur;
        private System.Windows.Forms.ToolStripMenuItem tsmiSmoothing;
        private System.Windows.Forms.ToolStripMenuItem tsmiFlipHorizontal;
        private System.Windows.Forms.ToolStripMenuItem tsmiFlipVertical;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoomIn;
        private System.Windows.Forms.ToolStripMenuItem tsmiZoomFrom;
        private System.Windows.Forms.ToolStripButton tsbttnZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripButton3;
        private System.Windows.Forms.ToolStripButton tsbttnLine;
        private System.Windows.Forms.ToolStripButton tsbttnZoomOut;
        private System.Windows.Forms.ToolStripButton tsbttnTriangle;
        private System.Windows.Forms.ToolStripButton tsbttnRectangle;
        private System.Windows.Forms.ToolStripButton tsbttnRomb;
        private System.Windows.Forms.ToolStripButton tsbttnEllipse;
        private System.Windows.Forms.ToolStripButton tsbttnCornerStar4;
        private System.Windows.Forms.ToolStripButton tsbttnCornerStar5;
        private System.Windows.Forms.ToolStripButton tsbttnCornerStar6;
        private System.Windows.Forms.ToolStripButton tsbttnEraser;
        private System.Windows.Forms.ToolStripButton tsbttnMagicalEraser;
    }
}