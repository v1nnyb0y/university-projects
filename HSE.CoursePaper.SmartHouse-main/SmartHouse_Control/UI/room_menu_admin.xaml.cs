using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using SmartHouse_Control.Session;
using Xbim.Common;
using Xbim.Common.Step21;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.IO;
using Xbim.ModelGeometry.Scene;
using Xbim.Presentation;
using Xbim.Presentation.LayerStyling;
using Xbim.Presentation.ModelGeomInfo;

namespace SmartHouse_Control.UI
{
    /// <summary>
    ///     Логика взаимодействия для room_menu_admin.xaml
    /// </summary>
    public partial class room_menu_admin : Window, INotifyPropertyChanged
    {
        #region Save to data base

        public bool is_active_saving;
        private readonly user user_obj;

        private void dlg_FileSaveAs(FileInfo fi) {
            var is_model_load = fi != null;

            try {
                if (!is_active_saving) {
                    var sf = new save_file_to_db(
                        is_model_load ? fi.Name : "",
                        is_model_load,
                        user_obj.get_table
                    );
                    sf.Owner = this;
                    sf.Show();

                    is_active_saving = true;
                }

                if (!is_model_load) return;

                if (Model != null) {
                    //TODO: IF FILE WILL BE CHANGED
                    var s = Path.GetExtension(fi.Name);
                    if (string.IsNullOrWhiteSpace(s))
                        return;
                    var extension = s.ToLowerInvariant();
                    if (extension != "xbim" || string.IsNullOrWhiteSpace(tempFileName))
                        return;
                    File.Delete(tempFileName);
                    tempFileName = null;
                }
                else {
                    throw new Exception("Invalid Model Server");
                }
            }
            catch (Exception except) {
                MessageBox.Show(except.Message, "Error Saving as", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion

        #region Fields 

        private BackgroundWorker _loadFileBackgroundWorker;
        private string tempFileName;
        private string _openedModelFileName;

        /// <summary>
        ///     this variable is used to determine when the user is trying again to double click on the selected item
        /// </summary>
        private bool _camChanged;

        /// <summary>
        ///     determines if models need to be meshed on opening
        /// </summary>
        private readonly bool _meshModel = true;


        private readonly double _deflectionOverride = double.NaN;
        private readonly double _angularDeflectionOverride = double.NaN;

        /// <summary>
        ///     determines if the geometry engine will run on parallel threads
        /// </summary>
        private readonly bool _multiThreading = true;

        /// <summary>
        ///     determines if the geometry engine will run on parallel threads
        /// </summary>
        private bool _simpleFastExtrusion = false;

        #endregion

        #region Open File

        /// <summary>
        ///     Deals with the user-defined model file name
        /// </summary>
        /// <returns>String pointing to the file or null if the file is not defined</returns>
        public string GetOpenedModelFileName() {
            return _openedModelFileName;
        }

        //Set new title for the window (after opened file)
        private void SetOpenedModelFileName(string ifcFilename) {
            _openedModelFileName = ifcFilename;
            // try to update the window title through a delegate for multithreading
            Dispatcher.BeginInvoke(new Action(delegate {
                Title = string.IsNullOrEmpty(ifcFilename)
                    ? "Диспетчер администрирования "
                    : "Диспетчер администрирования - [" + ifcFilename + "]";
            }));
        }

        //Open extension
        private void OpenAcceptableExtension(object s, DoWorkEventArgs args) {
            var worker = s as BackgroundWorker;
            var selectedFilename = args.Argument as string;

            #region Set model

            /*
            byte[] arr = File.ReadAllBytes(selectedFilename);
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "192.168.253.175";
            builder.UserID = "root";
            builder.Password = "icJ/Atp3q";
            builder.InitialCatalog = "SM_Control";

            using (SqlConnection sql = new SqlConnection(builder.ConnectionString))
            {
                sql.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO Files ");
                sb.Append($"VALUES (newid(), @a1, 111, 0, 1)");

                var ssql_c = sb.ToString();

                using (SqlCommand sss = new SqlCommand(ssql_c, sql)) {
                    sss.Parameters.AddWithValue("a1", arr);

                    sss.ExecuteNonQuery();
                }
            }*/

            #endregion

            try {
                if (worker == null)
                    throw new Exception("Background thread could not be accessed");
                tempFileName = Path.GetTempFileName();
                SetOpenedModelFileName(selectedFilename);
                var model = IfcStore.Open(selectedFilename, null, null, worker.ReportProgress, FileAccessMode);
                if (_meshModel) {
                    // mesh direct model
                    if (model.GeometryStore.IsEmpty)
                        try {
                            var context = new Xbim3DModelContext(model);

                            if (!_multiThreading)
                                context.MaxThreads = 1;

                            SetDeflection(model);
                            //upgrade to new geometry representation, uses the default 3D model
                            context.CreateContext(worker.ReportProgress, App.ContextWcsAdjustment);
                        }
                        catch (Exception geomEx) {
                            var sb = new StringBuilder();
                            sb.AppendLine(
                                $"Error creating geometry context of '{selectedFilename}' {geomEx.StackTrace}.");
                            var newexception = new Exception(sb.ToString(), geomEx);
                        }

                    // mesh references
                    foreach (var modelReference in model.ReferencedModels) {
                        // creates federation geometry contexts if needed
                        Debug.WriteLine(modelReference.Name);
                        if (modelReference.Model == null)
                            continue;
                        if (!modelReference.Model.GeometryStore.IsEmpty)
                            continue;
                        var context = new Xbim3DModelContext(modelReference.Model);
                        if (!_multiThreading)
                            context.MaxThreads = 1;

                        SetDeflection(modelReference.Model);
                        //upgrade to new geometry representation, uses the default 3D model
                        context.CreateContext(worker.ReportProgress, App.ContextWcsAdjustment);
                    }

                    if (worker.CancellationPending)
                        //if a cancellation has been requested then don't open the resulting file
                    {
                        try {
                            model.Close();
                            if (File.Exists(tempFileName))
                                File.Delete(tempFileName); //tidy up;
                            tempFileName = null;
                            SetOpenedModelFileName(null);
                        }
                        catch (Exception ex) { }

                        return;
                    }
                }

                args.Result = model;
            }
            catch (Exception ex) {
                var sb = new StringBuilder();
                sb.AppendLine($"Error opening '{selectedFilename}' {ex.StackTrace}.");
                var newexception = new Exception(sb.ToString(), ex);
                args.Result = newexception;
            }
        }

        //Set exploration
        private void SetDeflection(IModel model) {
            var mf = model.ModelFactors;
            if (mf == null)
                return;
            if (!double.IsNaN(_angularDeflectionOverride))
                mf.DeflectionAngle = _angularDeflectionOverride;
            if (!double.IsNaN(_deflectionOverride))
                mf.DeflectionTolerance = mf.OneMilliMetre * _deflectionOverride;
        }

        //Open dialog file
        private void dlg_OpenAnyFile(object sender, CancelEventArgs e) {
            var dlg = sender as OpenFileDialog;
            if (dlg != null) LoadAnyModel(dlg.FileName);
        }

        /// <summary>
        ///     Load OpenFileDialog with filters
        /// </summary>
        /// <param name="modelFileName"></param>
        public void LoadAnyModel(string modelFileName) {
            var fInfo = new FileInfo(modelFileName);
            if (!fInfo.Exists) // file does not exist; do nothing
                return;
            if (fInfo.FullName.ToLower() == GetOpenedModelFileName()) //same file do nothing
                return;

            // there's no going back; if it fails after this point the current file should be closed anyway
            CloseAndDeleteTemporaryFiles();
            SetOpenedModelFileName(modelFileName.ToLower());
            ProgressStatusBar.Visibility = Visibility.Visible;
            SetWorkerForFileLoad();

            var ext = fInfo.Extension.ToLower();
            switch (ext) {
                case ".ifc": //it is an Ifc File
                case ".ifcxml": //it is an IfcXml File
                case ".ifczip": //it is a zip file containing xbim or ifc File
                case ".zip": //it is a zip file containing xbim or ifc File
                case ".xbimf":
                case ".xbim":
                    _loadFileBackgroundWorker.RunWorkerAsync(modelFileName);
                    break;
            }
        }

        //Set worker
        private void SetWorkerForFileLoad() {
            _loadFileBackgroundWorker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
            _loadFileBackgroundWorker.ProgressChanged += OnProgressChanged;
            _loadFileBackgroundWorker.DoWork += OpenAcceptableExtension;
            _loadFileBackgroundWorker.RunWorkerCompleted += FileLoadCompleted;
        }

        //Complete load
        private void FileLoadCompleted(object s, RunWorkerCompletedEventArgs args) {
            if (args.Result is IfcStore) //all ok
            {
                //this Triggers the event to load the model into the views 
                ModelProvider.ObjectInstance = args.Result;
                ModelProvider.Refresh();
                ProgressBar.Value = 0;
                StatusMsg.Text = "Готово";
            }
            else //we have a problem
            {
                var errMsg = args.Result as string;
                if (!string.IsNullOrEmpty(errMsg))
                    MessageBox.Show(this, errMsg, "Ошибка открытия файла", MessageBoxButton.OK, MessageBoxImage.Error,
                        MessageBoxResult.None, MessageBoxOptions.None);
                var exception = args.Result as Exception;
                if (exception != null) {
                    var sb = new StringBuilder();

                    var indent = "";
                    while (exception != null) {
                        sb.AppendFormat("{0}{1}\n", indent, exception.Message);
                        exception = exception.InnerException;
                        indent += "\t";
                    }

                    MessageBox.Show(this, sb.ToString(), "Ошибка открытия Ifc файла", MessageBoxButton.OK,
                        MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.None);
                }

                ProgressBar.Value = 0;
                StatusMsg.Text = "Ошибка/Готово";
                SetOpenedModelFileName("");
            }

            FireLoadingComplete(s, args);
        }

        #endregion

        #region Start visual

        public room_menu_admin(bool preventPluginLoad, user _user) {
            // So we can use *.xbim files.
            IfcStore.ModelProviderFactory.UseHeuristicModelProvider();
            user_obj = new user(_user);

            InitializeComponent();

            /*
            // initialise the internal elements of the UI that behave like plugins
            EvaluateXbimUiType(typeof(IfcValidation.ValidationWindow), true);
            EvaluateXbimUiType(typeof(LogViewer.LogViewer), true);
            EvaluateXbimUiType(typeof(Commands.wdwCommands), true);*/


            // attach window managment functions
            Closed += SmartHouse_Closed;
            Loaded += SmartHouse_Loaded;
            Closing += SmartHouse_Closing;

            // notify the user of changes in the measures taken in the 3d viewer.
            DrawingControl.UserModeledDimensionChangedEvent += DrawingControl_MeasureChangedEvent;
        }

        private void SmartHouse_Loaded(object sender, RoutedEventArgs e) {
            var model = IfcStore.Create(null, XbimSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel);
            ModelProvider.ObjectInstance = model;
            ModelProvider.Refresh();
        }

        #endregion

        #region Model work

        //Change coordinators
        private void DrawingControl_MeasureChangedEvent(DrawingControl3D m, PolylineGeomInfo e) {
            if (e == null)
                return;
            EntityLabel.Text = e.ToString();
            Debug.WriteLine("Points:");
            foreach (var pt in e.VisualPoints) Debug.WriteLine("X:{0} Y:{1} Z:{2}", pt.X, pt.Y, pt.Z);
        }

        private void SpatialControl_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            _camChanged = false;
            DrawingControl.Viewport.Camera.Changed += Camera_Changed;
            DrawingControl.ZoomSelected();
            DrawingControl.Viewport.Camera.Changed -= Camera_Changed;
            if (!_camChanged)
                DrawingControl.ClipBaseSelected(0.15);
        }

        /// <summary>
        ///     this event is run after the window is fully rendered.
        /// </summary>
        private void RenderedEvents(object sender, EventArgs e) {
            ConnectStylerFeedBack();
        }

        private void EntityLabel_KeyDown() {
            var input = EntityLabel.Text;
            var re = new Regex(@"#[ \t]*(\d+)");
            var m = re.Match(input);
            IPersistEntity entity = null;
            if (m.Success) {
                int isLabel;
                if (!int.TryParse(m.Groups[1].Value, out isLabel))
                    return;
                entity = Model.Instances[isLabel];
            }
            else {
                entity = Model.Instances.OfType<IIfcRoot>().FirstOrDefault(x => x.GlobalId == input);
            }

            if (entity != null)
                SelectedItem = entity;
        }

        #endregion

        #region Usual Methods

        public XbimDBAccess FileAccessMode { get; set; } = XbimDBAccess.Read;

        private ObjectDataProvider ModelProvider => main_window.DataContext as ObjectDataProvider;

        /// <summary>
        /// </summary>
        public IfcStore Model {
            get {
                var op = main_window.DataContext as ObjectDataProvider;
                return op == null ? null : op.ObjectInstance as IfcStore;
            }
        }

        #endregion

        #region Delegates

        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="args"></param>
        public delegate void LoadingCompleteEventHandler(object s, RunWorkerCompletedEventArgs args);

        /// <summary>
        /// </summary>
        public event LoadingCompleteEventHandler LoadingComplete;

        private void FireLoadingComplete(object s, RunWorkerCompletedEventArgs args) {
            if (LoadingComplete != null) LoadingComplete(s, args);
        }

        //Change progress line
        private void OnProgressChanged(object s, ProgressChangedEventArgs args) {
            if (args.ProgressPercentage < 0 || args.ProgressPercentage > 100)
                return;

            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Send,
                new Action(() => {
                    ProgressBar.Value = args.ProgressPercentage;
                    StatusMsg.Text = (string) args.UserState;
                }));
        }

        //Select items (doing)
        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var mw = d as room_menu_admin;
            if (mw != null && e.NewValue is IPersistEntity) {
                var label = (IPersistEntity) e.NewValue;
                mw.EntityLabel.Text = label != null ? "#" + label.EntityLabel : "";
            }
            else if (mw != null) {
                mw.EntityLabel.Text = "";
            }
        }

        #endregion

        #region Bindings

        //Bind refresh model
        private void CommandBinding_Refresh(object sender, ExecutedRoutedEventArgs e) {
            if (string.IsNullOrEmpty(_openedModelFileName))
                return;
            if (!File.Exists(_openedModelFileName))
                return;
            LoadAnyModel(_openedModelFileName);
        }

        //Binding close
        private void CommandBinding_SaveAs(object sender, ExecutedRoutedEventArgs e) {
            FileInfo dlg = null;
            if (GetOpenedModelFileName() != null) dlg = new FileInfo(GetOpenedModelFileName());

            dlg_FileSaveAs(dlg);
        }

        //Close model bttn
        private void CommandBinding_Close(object sender, ExecutedRoutedEventArgs e) {
            CloseAndDeleteTemporaryFiles();
        }

        //Implement openfiledialog
        private void CommandBinding_Open(object sender, ExecutedRoutedEventArgs e) {
            var corefilters = new[]
                {
                    "Xbim Files|*.xbim;*.xbimf;*.ifc;*.ifcxml;*.ifczip",
                    "Ifc File (*.ifc)|*.ifc",
                    "xBIM File (*.xBIM)|*.xBIM",
                    "IfcXml File (*.IfcXml)|*.ifcxml",
                    "IfcZip File (*.IfcZip)|*.ifczip",
                    "Zipped File (*.zip)|*.zip"
                };

            // Filter files by extension 
            var dlg = new OpenFileDialog
                {
                    Filter = string.Join("|", corefilters)
                };
            dlg.FileOk += dlg_OpenAnyFile;
            dlg.ShowDialog(this);
        }

        //Is load model null or nothing
        private void CanExecuteIfFileOpen(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = Model != null && !string.IsNullOrEmpty(GetOpenedModelFileName());
        }

        //Is model null
        private void CanExecuteIfModelNotNull(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        #endregion

        #region TreeView

        /// <summary>
        /// </summary>
        public IPersistEntity SelectedItem {
            get => (IPersistEntity) GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        ///     SelectedItem from tree view
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(IPersistEntity), typeof(room_menu_admin),
                new UIPropertyMetadata(null, OnSelectedItemChanged));

        #endregion

        #region Camera Settings

        private void Camera_Changed(object sender, EventArgs e) {
            _camChanged = true;
        }


        private void MenuItem_ZoomExtents(object sender, RoutedEventArgs e) {
            DrawingControl.ViewHome();
        }

        #endregion

        #region View Settings

        private void SetDefaultModeStyler(object sender, RoutedEventArgs e) {
            ConnectStylerFeedBack();
            DrawingControl.ReloadModel();
        }

        private void ConnectStylerFeedBack() {
            if (DrawingControl.DefaultLayerStyler is IProgressiveLayerStyler)
                ((IProgressiveLayerStyler) DrawingControl.DefaultLayerStyler).ProgressChanged += OnProgressChanged;
        }

        #endregion

        #region Exit

        //If window closing
        private void SmartHouse_Closing(object sender, CancelEventArgs e) {
            e.Cancel = _loadFileBackgroundWorker != null && _loadFileBackgroundWorker.IsBusy ? true : false;
            ((main_menu) Owner).is_active_room_menu = false;
        }

        private void SmartHouse_Closed(object sender, EventArgs e) {
            CloseAndDeleteTemporaryFiles();
        }

        /// <summary>
        ///     Tidies up any open files and closes any open models
        /// </summary>
        private void CloseAndDeleteTemporaryFiles() {
            try {
                if (_loadFileBackgroundWorker != null && _loadFileBackgroundWorker.IsBusy)
                    _loadFileBackgroundWorker.CancelAsync(); //tell it to stop

                SetOpenedModelFileName(null);
                if (Model != null) {
                    Model.Dispose();
                    ModelProvider.ObjectInstance = null;
                    ModelProvider.Refresh();
                }

                if (!(DrawingControl.DefaultLayerStyler is SurfaceLayerStyler))
                    SetDefaultModeStyler(null, null);
            }
            finally {
                if (!(_loadFileBackgroundWorker != null && _loadFileBackgroundWorker.IsBusy &&
                      _loadFileBackgroundWorker.CancellationPending)) //it is still busy but has been cancelled 
                {
                    if (!string.IsNullOrWhiteSpace(tempFileName) && File.Exists(tempFileName))
                        File.Delete(tempFileName);
                    tempFileName = null;
                } //else do nothing it will be cleared up in the worker thread
            }
        }

        private void Exit(object sender, RoutedEventArgs e) {
            Close();
        }

        #endregion

        #region Property

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}