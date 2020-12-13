using ReactiveUI;
using System.Reactive;
using Xamarin.Forms;

namespace mycheffy.Models
{
    public class FooterItem
    {
        public string ItemName;
        public string ImageUrl;
        public string ItemLabel;
        public ContentView View;
        public ReactiveCommand<FooterItem, Unit> Command;

        public FooterItem(string name, string url, string label, ContentView view, ReactiveCommand<FooterItem, Unit> command)
        {
            ItemName = name;
            ImageUrl = url;
            ItemLabel = label;
            View = view;
            Command = command;
        }
    }
}
