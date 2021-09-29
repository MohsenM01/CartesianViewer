using System;
using Common;
using Common.Models;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Cartesian.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;

        private string _title = "Cartesian Viewer";
        
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
        /// <param name="regionManager"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="dialogService"></param>
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;

            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageSentModel"></param>
        private void MessageReceived(string messageSentModel)
        {
            if (messageSentModel.Equals("LoadDataDialog", StringComparison.OrdinalIgnoreCase))
            {
                _dialogService.ShowDialog("LoadDataDialog", new DialogParameters(), r =>
                {
                    switch (r.Result)
                    {
                        case ButtonResult.None:
                            return;
                        case ButtonResult.OK:
                            {
                                var fileData = r.Parameters.GetValue<ShapesReadModel>("FileData");
                                _eventAggregator.GetEvent<PubSubEvent<ShapesReadModel>>().Publish(fileData);
                                return;
                            }
                        case ButtonResult.Cancel:
                            return;
                        default:
                            return;
                    }
                });
                return;
            }

            if (messageSentModel.Equals("CartesianViewer", StringComparison.OrdinalIgnoreCase))
            {
                _eventAggregator.GetEvent<PubSubEvent<string>>().Publish("CartesianViewer");
            }

            _regionManager.RequestNavigate("ContentRegion", messageSentModel);
        }

    }
}
