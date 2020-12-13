using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Services.Utilities
{

    public enum NavigationAction
    {
        Push,
        Pop,
        Refresh
    }
    public class NavigationEvent
    {
        public string PageName { get; set; }
        public Object Data { get; set; }
        public Type DataType { get; set; }

        public NavigationAction Action { get; set; }
        public NavigationEvent(string name, NavigationAction action, Object data, Type type)
        {
            PageName = name;
            Data = data;
            DataType = type;
        }

        public NavigationEvent()
        {

        }
    }
    public class NavigationEventService
    {
        Stack<NavigationEvent> PageStack { get; set; }

        public NavigationEventService()
        {
            PageStack = new Stack<NavigationEvent>();
        }

        public void PushToStack(string name, Object data = null, Type type = null)
        {
            NavigationEvent navEvent = new NavigationEvent(name, NavigationAction.Push, data, type);
            PageStack.Push(navEvent);
            MessageBus.Current.SendMessage(navEvent);
        }

        public void PopStack()
        {
            NavigationEvent navEvent = PageStack.Pop();
            navEvent.Action = NavigationAction.Pop;
            MessageBus.Current.SendMessage(navEvent);
        }
    }
}
