using LoadDataModule.DataSourcesFactory;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.IO;

namespace LoadDataModule.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class FileViewerUserControlViewModel : BindableBase, IDialogAware
    {
        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand OpenFileDialogCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public FileViewerUserControlViewModel()
        {
            OpenFileDialogCommand = new DelegateCommand(OpenFileDialog);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenFileDialog()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                if (!File.Exists(dlg.FileName)) return;
                //TODO validate rules for content

                FilePath = dlg.FileName;
                CloseDialog("true");
            }
        }

        private string _title = "Open File";

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private string _fFilePathError = "";

        /// <summary>
        /// 
        /// </summary>
        public string FilePathError
        {
            get => _fFilePathError;
            set => SetProperty(ref _fFilePathError, value);
        }

        private string _filePath = "No file selected ...";

        /// <summary>
        /// 
        /// </summary>
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private DelegateCommand<string> _closeDialogCommand;

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

        /// <summary>
        /// 
        /// </summary>

        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            DialogParameters dialogParameters = null;
            if (parameter?.ToLower() == "true")
            {
                dialogParameters = new DialogParameters();
                var dataSourceFactory = new DataSourceFactory();
                var jsonFileExtractor = dataSourceFactory.GetJsonDataSource();
                var data = jsonFileExtractor.GetData(FilePath);
                dialogParameters.Add("FileData", data);
                result = ButtonResult.OK;
            }
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result, dialogParameters));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialogResult"></param>
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnDialogClosed()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

    }
}
