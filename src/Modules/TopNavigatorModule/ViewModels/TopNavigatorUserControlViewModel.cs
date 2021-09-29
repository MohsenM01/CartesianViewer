using Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace TopNavigatorModule.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class TopNavigatorUserControlViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// 
        /// </summary>
        public DelegateCommand<string> NavigateCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventAggregator"></param>
        public TopNavigatorUserControlViewModel(IEventAggregator eventAggregator)
        {

            NavigateCommand = new DelegateCommand<string>(Navigate);

            _eventAggregator = eventAggregator;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigatePath"></param>
        private void Navigate(string navigatePath)
        {
            if (navigatePath == null)
                return;

            _eventAggregator.GetEvent<MessageSentEvent>().Publish(navigatePath);
        }

    }
}