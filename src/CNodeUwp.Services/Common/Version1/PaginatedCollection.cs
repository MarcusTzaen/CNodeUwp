using System;
using System.Collections.ObjectModel;
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

        public bool IsLoading { get; private set; } = true;

        public bool IsLoadingCompleted
        {
            get
            {
                return !IsLoading;
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
            IsLoading = true;
            var topicList = await _load(this.Count / 10 + 1);
            foreach (var t in topicList)
            {
                this.Add(t);
            }
            this.HasMoreItems = topicList.Count >= 10;
            IsLoading = false;
            return new LoadMoreItemsResult() { Count = (uint)topicList.Count };
        }
    }
}
