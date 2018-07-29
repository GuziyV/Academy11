using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Academy11.Services
{
    abstract public class ItemService<T> where T: new()
    {
        public ObservableCollection<T> Items { get; set; } = new ObservableCollection<T>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ItemService()
        {
            SelectedItem = new T();
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }


        public T selectedItem;

        public T SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged(() => SelectedItem);
            }
        }

        abstract protected int GetSelectedId();

        protected HttpClient _httpClient;

        protected string _uri = App.apiUrl;

        public virtual async Task RemoveElem(T elem)
        {
            string uri = _uri + '/' + GetSelectedId();
            await _httpClient.DeleteAsync(uri);
            await UpdateList();
        }


        public virtual async Task<bool> Update(T elem)
        {
            var json = new StringContent(JsonConvert.SerializeObject(elem), Encoding.UTF8, "application/json");
            string uri = _uri + '/' + GetSelectedId();
            var response = await _httpClient.PutAsync(uri, json);
            if (response.IsSuccessStatusCode)
            {
                await UpdateList();
                return true;
            }
            return false;
        }


        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyPropertyChanged(Expression<Func<object>> target)
        {
            if (target != null)
            {
                var body = target.Body as MemberExpression;
                if (body != null)
                {
                    NotifyPropertyChanged(body.Member.Name);
                }
            }
        }

        abstract public bool Validate(T Item);

        virtual public async Task UpdateList()
        {
            var newCollection = new ObservableCollection<T>(await GetAll());
            Items.Clear();
            foreach (var item in newCollection)
            {
                Items.Add(item);
            }
        }


        virtual public async Task<IEnumerable<T>> GetAll()
        {
            string result = await _httpClient.GetStringAsync(_uri).ConfigureAwait(false);
            return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(result));
        }

        virtual public async Task<bool> Add(T f)
        {
            var json = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_uri, json);
            if (response.IsSuccessStatusCode)
            {
                await UpdateList();
                return true;
            }
            return false;

        }

    }
}
