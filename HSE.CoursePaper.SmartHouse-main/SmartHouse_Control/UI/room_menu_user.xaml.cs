#region Libraries

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using SmartHouse_Control.Handlers;
using SmartHouse_Control.InfoS;
using SmartHouse_Control.Session;
using Xbim.Common;
using Xbim.Common.Step21;
using Xbim.Ifc;
using Xbim.IO;
using Xbim.ModelGeometry.Scene;
using Xbim.Presentation.LayerStyling;
using access_lvl = SmartHouse_Control.Properties.Settings;

#endregion

namespace SmartHouse_Control.UI
{
    /// <summary>
    ///     Set new name for the main menu
    /// </summary>
    /// <param name="new_name"></param>
    internal delegate void change_room_name(string new_name);

    /// <summary>
    ///     Логика взаимодействия для room_menu.xaml
    /// </summary>
    public partial class room_menu_user : Window, IRoom, INotifyPropertyChanged
    {
        #region View Settings

        private void SetDefaultModeStyler(object sender, RoutedEventArgs e) {
            DrawingControl.ReloadModel();
        }

        #endregion

        #region Thread

        public void check_model_on_change() {
            while (true) {
                var tuple = subscribe.get_setts(rooms[active_index].get_room_id);
                if (tuple == null)
                    continue;

                if (tuple.Item1 == rooms[active_index].Settings) continue;

                lock (rooms) {
                    rooms[active_index].Settings = tuple.Item1;
                }

                Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    (ThreadStart) delegate { On_ThreadUpdate(); }
                );
            }
        }

        #endregion

        #region Fields

        public Thread thread_check;
        public List<room> rooms { get; set; }

        public int active_index { get; set; }

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

        //Open extension
        private void OpenAcceptableExtension(object s, DoWorkEventArgs args) {
            var worker = s as BackgroundWorker;
            var selectedFilename = args.Argument as string;

            try {
                if (worker == null)
                    throw new Exception("Background thread could not be accessed");
                tempFileName = Path.GetTempFileName();

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
        private void dlg_OpenAnyFile(string path) {
            if (path != "") LoadAnyModel(path);
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
            }

            FireLoadingComplete(s, args);
        }

        #endregion

        #region Start visual

        public room_menu_user(List<room> _room_list, int current_active) {
            // So we can use *.xbim files.
            IfcStore.ModelProviderFactory.UseHeuristicModelProvider();

            InitializeComponent();
            rooms = new List<room>();
            foreach (var room in _room_list) {
                if (room.get_access == access_lvl.Default.no_access) continue;
                rooms.Add(new room(room));
            }

            active_index = current_active;
        }

        //Load window
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            if (rooms.Count == 0) {
                var msg = MessageBox.Show(
                    "У вас нет доступа ни к одному помещению",
                    "Уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                if (msg == MessageBoxResult.Cancel || msg == MessageBoxResult.OK)
                    Close();
                return;
            }

            //Fill menu models
            for (var i = 0; i < rooms.Count; ++i)
                if (rooms[i].get_access != access_lvl.Default.no_access) {
                    var mi = new MenuItem
                        {
                            IsCheckable = i != 0,
                            IsChecked = i == 0,
                            Header = rooms[i].get_name
                        };
                    mi.Click += CommandBinding_Open;
                    models.Items.Add(mi);
                }

            Title = rooms[active_index].get_name;
            check_access();

            ev_output += subscribe.output_excel_format_one;
            to_model += subscribe.find_model;
            load_sensors += subscribe.load_room_sensors;


            OnFind(rooms[active_index].get_room_id);
            OnLoadGrid(rooms[active_index].get_room_id);

            thread_check = new Thread(check_model_on_change);
            thread_check.IsBackground = true;
            thread_check.Start();
        }

        //Generate columns
        private void Info_AutoGeneratedColumns(object sender, EventArgs e) {
            for (var i = 0; i < info.Columns.Count; ++i)
                info.Columns[i].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        #endregion

        #region Visual model

        /// <summary>
        ///     If access partly
        /// </summary>
        private void partly_access() {
            DrawingControl.Margin = new Thickness(10, 22, 10, 10);
        }

        /// <summary>
        ///     if access full
        /// </summary>
        private void full_access() {
            DrawingControl.Margin = new Thickness(502, 22, 10, 10);
        }

        /// <summary>
        ///     Change interface due to access
        /// </summary>
        private void check_access() {
            if (rooms[active_index].get_access == access_lvl.Default.partly_access) {
                info.Visibility = Visibility.Hidden;
                partly_access();
            }
            else {
                info.Visibility = Visibility.Visible;
                full_access();
            }
        }

        #endregion

        #region Camera Settings

        private void Camera_Changed(object sender, EventArgs e) {
            _camChanged = true;
        }


        private void MenuItem_ZoomExtents(object sender, RoutedEventArgs e) {
            DrawingControl.ViewHome();
        }

        #endregion

        #region Change active model

        /// <summary>
        ///     Check new model
        /// </summary>
        /// <param name="index">Index in the list</param>
        /// <param name="name">Name of the room</param>
        private void check_model(int index, string name) {
            (models.Items[index] as MenuItem).IsCheckable = false;
            active_index = index;

            OnChange(name);
            check_access();
        }

        /// <summary>
        ///     Uncheck model
        /// </summary>
        /// <param name="index">Index of old active model</param>
        private void uncheck_model(int index) {
            (models.Items[index] as MenuItem).IsCheckable = true;
            (models.Items[index] as MenuItem).IsChecked = false;
        }

        //Set new active model
        private void Models_Checked(object sender, RoutedEventArgs e) {
            uncheck_model(active_index);
            var new_index = (sender as MenuItem).Items.IndexOf(e.OriginalSource as MenuItem);

            check_model(new_index, rooms[new_index].get_name);
            Title = rooms[active_index].get_name;
        }

        #endregion

        #region Delegate

        private event change_room_name change_main_manu; //event for the delegate

        /// <summary>
        ///     Use delegate
        /// </summary>
        /// <param name="name">New room name for the main menu</param>
        private void OnChange(string name) {
            change_main_manu?.Invoke(rooms[active_index].get_name);
        }

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

        /// <summary>
        ///     Find path to room
        /// </summary>
        /// <param name="id_room">Id_room</param>
        /// <returns></returns>
        private delegate object model_load(int id_room, params string[] arr);

        private event model_load to_model;
        private event model_load load_sensors;

        private void OnFind(int id_room) {
            var model = IfcStore.Create(null, XbimSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel);
            ModelProvider.ObjectInstance = model;
            ModelProvider.Refresh();

            var path = to_model?.Invoke(id_room, rooms[active_index].Settings) as string;
            if (path == "") {
                MessageBox.Show("Модель не была найдена");
                return;
            }

            dlg_OpenAnyFile(path);
        }

        private void OnLoadGrid(int id_room) {
            if (rooms[active_index].get_access != access_lvl.Default.full_access) {
                data.Visibility = Visibility.Hidden;
                return;
            }

            data.Visibility = Visibility.Visible;

            var dt = load_sensors?.Invoke(id_room) as DataTable;
            if (dt == null) return;

            info.AutoGenerateColumns = true;
            info.ItemsSource = dt.DefaultView;
            info.CanUserAddRows = false;
            info.CanUserDeleteRows = false;
            info.CanUserReorderColumns = false;
            info.IsReadOnly = true;
        }

        /// <summary>
        ///     Delegate for output
        /// </summary>
        /// <param name="room_name"></param>
        /// <param name="dt"></param>
        private delegate void output(string room_name, DataTable dt);

        private event output ev_output;

        private void OnOutput(string room_name, DataTable dt) {
            ev_output?.Invoke(room_name, dt);
        }

        private void On_ThreadUpdate() {
            OnFind(rooms[active_index].get_room_id);
        }

        #endregion

        #region Exit

        //If window closing
        private void Window_Closing(object sender, CancelEventArgs e) {
            if (rooms.Count != 0) {
                CloseAndDeleteTemporaryFiles();
                thread_check.Abort();
            }

            ((main_menu) Owner).is_active_room_menu = false;
        }

        /// <summary>
        ///     Tidies up any open files and closes any open models
        /// </summary>
        private void CloseAndDeleteTemporaryFiles() {
            try {
                if (_loadFileBackgroundWorker != null && _loadFileBackgroundWorker.IsBusy)
                    _loadFileBackgroundWorker.CancelAsync(); //tell it to stop


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

        #endregion

        #region Output data

        /// <summary>
        ///     Print all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_all_bttn_OnClick(object sender, RoutedEventArgs e) {
            if (is_grid_empty()) return;

            var dt = new DataTable();

            dt.Columns.Add("Наименование");
            dt.Columns.Add("Семейство");
            dt.Columns.Add("Состояние");
            dt.Columns.Add("Последнее значение");

            for (var i = 0; i < info.Items.Count; ++i) {
                var str = new string[4];
                for (var j = 0; j < 4; ++j) {
                    var tx = info.Columns[j].GetCellContent(info.Items[i]) as TextBlock;
                    str[j] = tx.Text;
                }

                dt.Rows.Add(str);
            }

            OnOutput(rooms[active_index].get_name, dt);
        }

        private void Print_one_bttn_OnClick(object sender, RoutedEventArgs e) {
            if (is_grid_empty()) return;

            var dt = new DataTable();

            dt.Columns.Add("Наименование");
            dt.Columns.Add("Семейство");
            dt.Columns.Add("Состояние");
            dt.Columns.Add("Последнее значение");

            for (var i = 0; i < info.SelectedItems.Count; ++i) {
                var str = new string[4];
                for (var j = 0; j < 4; ++j) {
                    var tx = info.Columns[j].GetCellContent(info.SelectedItems[i]) as TextBlock;
                    str[j] = tx.Text;
                }

                dt.Rows.Add(str);
            }

            OnOutput(rooms[active_index].get_name, dt);
        }

        private bool is_grid_empty() {
            if (info.Items.Count == 0) {
                var res = MessageBox.Show(
                    "Вам нечего сохранять",
                    "Уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                if (res == MessageBoxResult.OK)
                    return true;
            }

            return false;
        }

        #endregion

        #region Usual methods

        public XbimDBAccess FileAccessMode { get; set; } = XbimDBAccess.Read;

        private ObjectDataProvider ModelProvider {
            get {
                try {
                    object obj = null;
                    Dispatcher.BeginInvoke(new Action(delegate { obj = main_window.DataContext; }));
                    if (obj != null)
                        return obj as ObjectDataProvider;
                    return main_window.DataContext as ObjectDataProvider;
                }
                catch {
                    return main_window.DataContext as ObjectDataProvider;
                }
            }
        }

        /// <summary>
        /// </summary>
        public IfcStore Model {
            get {
                var op = main_window.DataContext as ObjectDataProvider;
                return op == null ? null : op.ObjectInstance as IfcStore;
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

        //TODO: OPEN FILE FROM DATA BASE
        private void CommandBinding_Open(object sender, RoutedEventArgs e) {
            thread_check.Abort();
            OnFind(rooms[active_index].get_room_id);
            OnLoadGrid(rooms[active_index].get_room_id);
            thread_check = new Thread(check_model_on_change);
            thread_check.IsBackground = true;
            thread_check.Start();
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