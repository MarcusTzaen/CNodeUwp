using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace CNodeUwp.Services.Common.Version1
{
    public class PaginatedCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private Func<int, Task<ObservableCollection<T>>> _load;
        public bool HasMoreItems { get; protected set; }

        private bool _isLoading = true;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsLoading)));
            }
        }

        public PaginatedCollection(Func<int, Task<ObservableCollection<T>>> load)
        {
            HasMoreItems = true;
            this._load = load;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run((c) =>
            {
                return LoadAsync(count, c);
            });
        }

        private async Task<LoadMoreItemsResult> LoadAsync(uint count, CancellationToken cancellationToken)
        {
            var topicList = await _load(this.Count / 10 + 1);
            foreach (var t in topicList)
            {
                this.Add(t);
            }
            this.HasMoreItems = topicList.Count >= 10;
            IsLoading = topicList.Count <= 0;
            return new LoadMoreItemsResult() { Count = (uint)topicList.Count };
        }
    }
}
