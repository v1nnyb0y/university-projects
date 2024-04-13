namespace CalculatorFromDreamTeam
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.bSignChange = new System.Windows.Forms.Button();
            this.bInverse = new System.Windows.Forms.Button();
            this.bSqroot = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.bComma = new System.Windows.Forms.Button();
            this.bResult = new System.Windows.Forms.Button();
            this.bNine = new System.Windows.Forms.Button();
            this.bSeven = new System.Windows.Forms.Button();
            this.bEight = new System.Windows.Forms.Button();
            this.bSix = new System.Windows.Forms.Button();
            this.bFive = new System.Windows.Forms.Button();
            this.bFour = new System.Windows.Forms.Button();
            this.bThree = new System.Windows.Forms.Button();
            this.bOne = new System.Windows.Forms.Button();
            this.bTwo = new System.Windows.Forms.Button();
            this.bZero = new System.Windows.Forms.Button();
            this.lX = new System.Windows.Forms.Label();
            this.rHistory = new System.Windows.Forms.RichTextBox();
            this.lTwo = new System.Windows.Forms.Label();
            this.tKoefA = new System.Windows.Forms.TextBox();
            this.cMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tClear = new System.Windows.Forms.ToolStripMenuItem();
            this.lLastOperations = new System.Windows.Forms.Label();
            this.lPlus = new System.Windows.Forms.Label();
            this.tKoefB = new System.Windows.Forms.TextBox();
            this.lX1 = new System.Windows.Forms.Label();
            this.lPlus1 = new System.Windows.Forms.Label();
            this.tKoefC = new System.Windows.Forms.TextBox();
            this.lEquals = new System.Windows.Forms.Label();
            this.bNextKoef = new System.Windows.Forms.Button();
            this.ttTip = new System.Windows.Forms.ToolTip(this.components);
            this.bClearHistory = new System.Windows.Forms.Button();
            this.lOne = new System.Windows.Forms.Label();
            this.lDivideLine = new System.Windows.Forms.Label();
            this.lDivideLine1 = new System.Windows.Forms.Label();
            this.lOne1 = new System.Windows.Forms.Label();
            this.lDivideLine2 = new System.Windows.Forms.Label();
            this.lOne2 = new System.Windows.Forms.Label();
            this.lArrow = new System.Windows.Forms.Label();
            this.lArrow1 = new System.Windows.Forms.Label();
            this.lArrow2 = new System.Windows.Forms.Label();
            this.cMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // bSignChange
            // 
            this.bSignChange.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bSignChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSignChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bSignChange.Location = new System.Drawing.Point(16, 392);
            this.bSignChange.Name = "bSignChange";
            this.bSignChange.Size = new System.Drawing.Size(119, 109);
            this.bSignChange.TabIndex = 51;
            this.bSignChange.Text = "±";
            this.bSignChange.UseVisualStyleBackColor = false;
            this.bSignChange.Click += new System.EventHandler(this.bSignChange_Click);
            // 
            // bInverse
            // 
            this.bInverse.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bInverse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bInverse.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bInverse.Location = new System.Drawing.Point(16, 277);
            this.bInverse.Name = "bInverse";
            this.bInverse.Size = new System.Drawing.Size(119, 109);
            this.bInverse.TabIndex = 50;
            this.bInverse.Text = "1/x";
            this.bInverse.UseVisualStyleBackColor = false;
            this.bInverse.Click += new System.EventHandler(this.bInverse_Click);
            // 
            // bSqroot
            // 
            this.bSqroot.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bSqroot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSqroot.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bSqroot.Location = new System.Drawing.Point(141, 622);
            this.bSqroot.Name = "bSqroot";
            this.bSqroot.Size = new System.Drawing.Size(119, 109);
            this.bSqroot.TabIndex = 45;
            this.bSqroot.Text = "√";
            this.bSqroot.UseVisualStyleBackColor = false;
            this.bSqroot.Click += new System.EventHandler(this.bSqroot_Click);
            // 
            // bClear
            // 
            this.bClear.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bClear.Location = new System.Drawing.Point(16, 507);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(119, 109);
            this.bClear.TabIndex = 44;
            this.bClear.Text = "С";
            this.ttTip.SetToolTip(this.bClear, "Нажмите правую кнопку мыши для удаления всех коэффициэнтов.");
            this.bClear.UseVisualStyleBackColor = false;
            this.bClear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.bClear_MouseClick);
            this.bClear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bClear_MouseDown);
            // 
            // bDelete
            // 
            this.bDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bDelete.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bDelete.BackgroundImage")));
            this.bDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bDelete.Location = new System.Drawing.Point(516, 277);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(119, 109);
            this.bDelete.TabIndex = 43;
            this.bDelete.UseVisualStyleBackColor = false;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bComma
            // 
            this.bComma.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bComma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bComma.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bComma.Location = new System.Drawing.Point(391, 622);
            this.bComma.Name = "bComma";
            this.bComma.Size = new System.Drawing.Size(119, 109);
            this.bComma.TabIndex = 42;
            this.bComma.Text = ",";
            this.bComma.UseVisualStyleBackColor = false;
            this.bComma.Click += new System.EventHandler(this.bComma_Click);
            // 
            // bResult
            // 
            this.bResult.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bResult.Location = new System.Drawing.Point(516, 507);
            this.bResult.Name = "bResult";
            this.bResult.Size = new System.Drawing.Size(119, 109);
            this.bResult.TabIndex = 41;
            this.bResult.Text = "=";
            this.bResult.UseVisualStyleBackColor = false;
            this.bResult.Click += new System.EventHandler(this.bResult_Click);
            // 
            // bNine
            // 
            this.bNine.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bNine.Location = new System.Drawing.Point(391, 277);
            this.bNine.Name = "bNine";
            this.bNine.Size = new System.Drawing.Size(119, 109);
            this.bNine.TabIndex = 35;
            this.bNine.Text = "9";
            this.bNine.UseVisualStyleBackColor = true;
            this.bNine.Click += new System.EventHandler(this.bNine_Click);
            // 
            // bSeven
            // 
            this.bSeven.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bSeven.Location = new System.Drawing.Point(141, 277);
            this.bSeven.Name = "bSeven";
            this.bSeven.Size = new System.Drawing.Size(119, 109);
            this.bSeven.TabIndex = 34;
            this.bSeven.Text = "7";
            this.bSeven.UseVisualStyleBackColor = true;
            this.bSeven.Click += new System.EventHandler(this.bSeven_Click);
            // 
            // bEight
            // 
            this.bEight.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bEight.Location = new System.Drawing.Point(266, 277);
            this.bEight.Name = "bEight";
            this.bEight.Size = new System.Drawing.Size(119, 109);
            this.bEight.TabIndex = 33;
            this.bEight.Text = "8";
            this.bEight.UseVisualStyleBackColor = true;
            this.bEight.Click += new System.EventHandler(this.bEight_Click);
            // 
            // bSix
            // 
            this.bSix.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bSix.Location = new System.Drawing.Point(391, 392);
            this.bSix.Name = "bSix";
            this.bSix.Size = new System.Drawing.Size(119, 109);
            this.bSix.TabIndex = 32;
            this.bSix.Text = "6";
            this.bSix.UseVisualStyleBackColor = true;
            this.bSix.Click += new System.EventHandler(this.bSix_Click);
            // 
            // bFive
            // 
            this.bFive.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bFive.Location = new System.Drawing.Point(266, 392);
            this.bFive.Name = "bFive";
            this.bFive.Size = new System.Drawing.Size(119, 109);
            this.bFive.TabIndex = 31;
            this.bFive.Text = "5";
            this.bFive.UseVisualStyleBackColor = true;
            this.bFive.Click += new System.EventHandler(this.bFive_Click);
            // 
            // bFour
            // 
            this.bFour.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bFour.Location = new System.Drawing.Point(141, 392);
            this.bFour.Name = "bFour";
            this.bFour.Size = new System.Drawing.Size(119, 109);
            this.bFour.TabIndex = 30;
            this.bFour.Text = "4";
            this.bFour.UseVisualStyleBackColor = true;
            this.bFour.Click += new System.EventHandler(this.bFour_Click);
            // 
            // bThree
            // 
            this.bThree.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bThree.Location = new System.Drawing.Point(391, 507);
            this.bThree.Name = "bThree";
            this.bThree.Size = new System.Drawing.Size(119, 109);
            this.bThree.TabIndex = 29;
            this.bThree.Text = "3";
            this.bThree.UseVisualStyleBackColor = true;
            this.bThree.Click += new System.EventHandler(this.bThree_Click);
            // 
            // bOne
            // 
            this.bOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOne.Location = new System.Drawing.Point(141, 507);
            this.bOne.Name = "bOne";
            this.bOne.Size = new System.Drawing.Size(119, 109);
            this.bOne.TabIndex = 28;
            this.bOne.Text = "1";
            this.bOne.UseVisualStyleBackColor = true;
            this.bOne.Click += new System.EventHandler(this.bOne_Click);
            // 
            // bTwo
            // 
            this.bTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bTwo.Location = new System.Drawing.Point(266, 507);
            this.bTwo.Name = "bTwo";
            this.bTwo.Size = new System.Drawing.Size(119, 109);
            this.bTwo.TabIndex = 27;
            this.bTwo.Text = "2";
            this.bTwo.UseVisualStyleBackColor = true;
            this.bTwo.Click += new System.EventHandler(this.bTwo_Click);
            // 
            // bZero
            // 
            this.bZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bZero.Location = new System.Drawing.Point(266, 622);
            this.bZero.Name = "bZero";
            this.bZero.Size = new System.Drawing.Size(119, 109);
            this.bZero.TabIndex = 26;
            this.bZero.Text = "0";
            this.bZero.UseVisualStyleBackColor = true;
            this.bZero.Click += new System.EventHandler(this.bZero_Click);
            // 
            // lX
            // 
            this.lX.AutoSize = true;
            this.lX.BackColor = System.Drawing.Color.Transparent;
            this.lX.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lX.Location = new System.Drawing.Point(117, 134);
            this.lX.Name = "lX";
            this.lX.Size = new System.Drawing.Size(49, 55);
            this.lX.TabIndex = 52;
            this.lX.Text = "x";
            // 
            // rHistory
            // 
            this.rHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rHistory.Location = new System.Drawing.Point(671, 37);
            this.rHistory.Name = "rHistory";
            this.rHistory.ReadOnly = true;
            this.rHistory.Size = new System.Drawing.Size(339, 650);
            this.rHistory.TabIndex = 53;
            this.rHistory.Text = "";
            // 
            // lTwo
            // 
            this.lTwo.AutoSize = true;
            this.lTwo.BackColor = System.Drawing.Color.Transparent;
            this.lTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lTwo.Location = new System.Drawing.Point(154, 112);
            this.lTwo.Name = "lTwo";
            this.lTwo.Size = new System.Drawing.Size(42, 44);
            this.lTwo.TabIndex = 55;
            this.lTwo.Text = "2";
            // 
            // tKoefA
            // 
            this.tKoefA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tKoefA.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tKoefA.Location = new System.Drawing.Point(7, 149);
            this.tKoefA.MaxLength = 6;
            this.tKoefA.Name = "tKoefA";
            this.tKoefA.ReadOnly = true;
            this.tKoefA.Size = new System.Drawing.Size(104, 34);
            this.tKoefA.TabIndex = 56;
            this.tKoefA.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tKoefA_MouseClick);
            this.tKoefA.TextChanged += new System.EventHandler(this.tKoefA_TextChanged);
            // 
            // cMenu
            // 
            this.cMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tClear});
            this.cMenu.Name = "cMenu";
            this.cMenu.Size = new System.Drawing.Size(143, 28);
            // 
            // tClear
            // 
            this.tClear.Name = "tClear";
            this.tClear.Size = new System.Drawing.Size(142, 24);
            this.tClear.Text = "Очистить";
            // 
            // lLastOperations
            // 
            this.lLastOperations.AutoSize = true;
            this.lLastOperations.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lLastOperations.Location = new System.Drawing.Point(742, 14);
            this.lLastOperations.Name = "lLastOperations";
            this.lLastOperations.Size = new System.Drawing.Size(206, 20);
            this.lLastOperations.TabIndex = 58;
            this.lLastOperations.Text = "Последние операции";
            // 
            // lPlus
            // 
            this.lPlus.AutoSize = true;
            this.lPlus.BackColor = System.Drawing.Color.Transparent;
            this.lPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lPlus.Location = new System.Drawing.Point(187, 134);
            this.lPlus.Name = "lPlus";
            this.lPlus.Size = new System.Drawing.Size(53, 55);
            this.lPlus.TabIndex = 59;
            this.lPlus.Text = "+";
            // 
            // tKoefB
            // 
            this.tKoefB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tKoefB.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tKoefB.Location = new System.Drawing.Point(229, 149);
            this.tKoefB.MaxLength = 6;
            this.tKoefB.Name = "tKoefB";
            this.tKoefB.ReadOnly = true;
            this.tKoefB.Size = new System.Drawing.Size(115, 34);
            this.tKoefB.TabIndex = 60;
            this.tKoefB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tKoefB_MouseClick);
            this.tKoefB.TextChanged += new System.EventHandler(this.tKoefB_TextChanged);
            // 
            // lX1
            // 
            this.lX1.AutoSize = true;
            this.lX1.BackColor = System.Drawing.Color.Transparent;
            this.lX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lX1.Location = new System.Drawing.Point(350, 134);
            this.lX1.Name = "lX1";
            this.lX1.Size = new System.Drawing.Size(49, 55);
            this.lX1.TabIndex = 61;
            this.lX1.Text = "x";
            // 
            // lPlus1
            // 
            this.lPlus1.AutoSize = true;
            this.lPlus1.BackColor = System.Drawing.Color.Transparent;
            this.lPlus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lPlus1.Location = new System.Drawing.Point(391, 134);
            this.lPlus1.Name = "lPlus1";
            this.lPlus1.Size = new System.Drawing.Size(53, 55);
            this.lPlus1.TabIndex = 62;
            this.lPlus1.Text = "+";
            // 
            // tKoefC
            // 
            this.tKoefC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tKoefC.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tKoefC.Location = new System.Drawing.Point(437, 149);
            this.tKoefC.MaxLength = 8;
            this.tKoefC.Name = "tKoefC";
            this.tKoefC.ReadOnly = true;
            this.tKoefC.Size = new System.Drawing.Size(142, 34);
            this.tKoefC.TabIndex = 63;
            this.tKoefC.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tKoefC_MouseClick);
            this.tKoefC.TextChanged += new System.EventHandler(this.tKoefC_TextChanged);
            // 
            // lEquals
            // 
            this.lEquals.AutoSize = true;
            this.lEquals.BackColor = System.Drawing.Color.Transparent;
            this.lEquals.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lEquals.Location = new System.Drawing.Point(569, 134);
            this.lEquals.Name = "lEquals";
            this.lEquals.Size = new System.Drawing.Size(95, 55);
            this.lEquals.TabIndex = 64;
            this.lEquals.Text = "= 0";
            // 
            // bNextKoef
            // 
            this.bNextKoef.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bNextKoef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNextKoef.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bNextKoef.Location = new System.Drawing.Point(516, 392);
            this.bNextKoef.Name = "bNextKoef";
            this.bNextKoef.Size = new System.Drawing.Size(119, 109);
            this.bNextKoef.TabIndex = 65;
            this.bNextKoef.Text = "→";
            this.bNextKoef.UseVisualStyleBackColor = false;
            this.bNextKoef.Click += new System.EventHandler(this.bNextKoef_Click);
            // 
            // bClearHistory
            // 
            this.bClearHistory.Location = new System.Drawing.Point(807, 699);
            this.bClearHistory.Name = "bClearHistory";
            this.bClearHistory.Size = new System.Drawing.Size(90, 30);
            this.bClearHistory.TabIndex = 66;
            this.bClearHistory.Text = "Очистить";
            this.bClearHistory.UseVisualStyleBackColor = true;
            this.bClearHistory.Click += new System.EventHandler(this.bClearHistory_Click);
            // 
            // lOne
            // 
            this.lOne.AutoSize = true;
            this.lOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOne.Location = new System.Drawing.Point(46, 99);
            this.lOne.Name = "lOne";
            this.lOne.Size = new System.Drawing.Size(33, 36);
            this.lOne.TabIndex = 67;
            this.lOne.Text = "1";
            this.lOne.Visible = false;
            // 
            // lDivideLine
            // 
            this.lDivideLine.AutoSize = true;
            this.lDivideLine.Location = new System.Drawing.Point(27, 129);
            this.lDivideLine.Name = "lDivideLine";
            this.lDivideLine.Size = new System.Drawing.Size(72, 17);
            this.lDivideLine.TabIndex = 68;
            this.lDivideLine.Text = "________";
            this.lDivideLine.Visible = false;
            // 
            // lDivideLine1
            // 
            this.lDivideLine1.AutoSize = true;
            this.lDivideLine1.Location = new System.Drawing.Point(270, 129);
            this.lDivideLine1.Name = "lDivideLine1";
            this.lDivideLine1.Size = new System.Drawing.Size(72, 17);
            this.lDivideLine1.TabIndex = 70;
            this.lDivideLine1.Text = "________";
            this.lDivideLine1.Visible = false;
            // 
            // lOne1
            // 
            this.lOne1.AutoSize = true;
            this.lOne1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOne1.Location = new System.Drawing.Point(289, 99);
            this.lOne1.Name = "lOne1";
            this.lOne1.Size = new System.Drawing.Size(33, 36);
            this.lOne1.TabIndex = 69;
            this.lOne1.Text = "1";
            this.lOne1.Visible = false;
            // 
            // lDivideLine2
            // 
            this.lDivideLine2.AutoSize = true;
            this.lDivideLine2.Location = new System.Drawing.Point(480, 129);
            this.lDivideLine2.Name = "lDivideLine2";
            this.lDivideLine2.Size = new System.Drawing.Size(72, 17);
            this.lDivideLine2.TabIndex = 72;
            this.lDivideLine2.Text = "________";
            this.lDivideLine2.Visible = false;
            // 
            // lOne2
            // 
            this.lOne2.AutoSize = true;
            this.lOne2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOne2.Location = new System.Drawing.Point(499, 99);
            this.lOne2.Name = "lOne2";
            this.lOne2.Size = new System.Drawing.Size(33, 36);
            this.lOne2.TabIndex = 71;
            this.lOne2.Text = "1";
            this.lOne2.Visible = false;
            // 
            // lArrow
            // 
            this.lArrow.AutoSize = true;
            this.lArrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lArrow.Location = new System.Drawing.Point(48, 186);
            this.lArrow.Name = "lArrow";
            this.lArrow.Size = new System.Drawing.Size(31, 36);
            this.lArrow.TabIndex = 73;
            this.lArrow.Text = "↑";
            // 
            // lArrow1
            // 
            this.lArrow1.AutoSize = true;
            this.lArrow1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lArrow1.Location = new System.Drawing.Point(291, 186);
            this.lArrow1.Name = "lArrow1";
            this.lArrow1.Size = new System.Drawing.Size(31, 36);
            this.lArrow1.TabIndex = 74;
            this.lArrow1.Text = "↑";
            this.lArrow1.Visible = false;
            // 
            // lArrow2
            // 
            this.lArrow2.AutoSize = true;
            this.lArrow2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lArrow2.Location = new System.Drawing.Point(501, 186);
            this.lArrow2.Name = "lArrow2";
            this.lArrow2.Size = new System.Drawing.Size(31, 36);
            this.lArrow2.TabIndex = 75;
            this.lArrow2.Text = "↑";
            this.lArrow2.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 741);
            this.Controls.Add(this.lArrow2);
            this.Controls.Add(this.lArrow1);
            this.Controls.Add(this.lArrow);
            this.Controls.Add(this.lDivideLine2);
            this.Controls.Add(this.lOne2);
            this.Controls.Add(this.lDivideLine1);
            this.Controls.Add(this.lOne1);
            this.Controls.Add(this.lDivideLine);
            this.Controls.Add(this.lOne);
            this.Controls.Add(this.bClearHistory);
            this.Controls.Add(this.tKoefC);
            this.Controls.Add(this.tKoefB);
            this.Controls.Add(this.bNextKoef);
            this.Controls.Add(this.lEquals);
            this.Controls.Add(this.lPlus1);
            this.Controls.Add(this.lX1);
            this.Controls.Add(this.lPlus);
            this.Controls.Add(this.lTwo);
            this.Controls.Add(this.lX);
            this.Controls.Add(this.lLastOperations);
            this.Controls.Add(this.tKoefA);
            this.Controls.Add(this.rHistory);
            this.Controls.Add(this.bSignChange);
            this.Controls.Add(this.bInverse);
            this.Controls.Add(this.bSqroot);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bComma);
            this.Controls.Add(this.bResult);
            this.Controls.Add(this.bNine);
            this.Controls.Add(this.bSeven);
            this.Controls.Add(this.bEight);
            this.Controls.Add(this.bSix);
            this.Controls.Add(this.bFive);
            this.Controls.Add(this.bFour);
            this.Controls.Add(this.bThree);
            this.Controls.Add(this.bOne);
            this.Controls.Add(this.bTwo);
            this.Controls.Add(this.bZero);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Калькулятор";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.cMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSignChange;
        private System.Windows.Forms.Button bInverse;
        private System.Windows.Forms.Button bSqroot;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bComma;
        private System.Windows.Forms.Button bResult;
        private System.Windows.Forms.Button bNine;
        private System.Windows.Forms.Button bSeven;
        private System.Windows.Forms.Button bEight;
        private System.Windows.Forms.Button bSix;
        private System.Windows.Forms.Button bFive;
        private System.Windows.Forms.Button bFour;
        private System.Windows.Forms.Button bThree;
        private System.Windows.Forms.Button bOne;
        private System.Windows.Forms.Button bTwo;
        private System.Windows.Forms.Button bZero;
        private System.Windows.Forms.Label lX;
        private System.Windows.Forms.RichTextBox rHistory;
        private System.Windows.Forms.Label lTwo;
        private System.Windows.Forms.TextBox tKoefA;
        private System.Windows.Forms.ContextMenuStrip cMenu;
        private System.Windows.Forms.ToolStripMenuItem tClear;
        private System.Windows.Forms.Label lLastOperations;
        private System.Windows.Forms.Label lPlus;
        private System.Windows.Forms.TextBox tKoefB;
        private System.Windows.Forms.Label lX1;
        private System.Windows.Forms.Label lPlus1;
        private System.Windows.Forms.TextBox tKoefC;
        private System.Windows.Forms.Label lEquals;
        private System.Windows.Forms.Button bNextKoef;
        private System.Windows.Forms.ToolTip ttTip;
        private System.Windows.Forms.Button bClearHistory;
        private System.Windows.Forms.Label lOne;
        private System.Windows.Forms.Label lDivideLine;
        private System.Windows.Forms.Label lDivideLine1;
        private System.Windows.Forms.Label lOne1;
        private System.Windows.Forms.Label lDivideLine2;
        private System.Windows.Forms.Label lOne2;
        private System.Windows.Forms.Label lArrow;
        private System.Windows.Forms.Label lArrow1;
        private System.Windows.Forms.Label lArrow2;
    }
}

