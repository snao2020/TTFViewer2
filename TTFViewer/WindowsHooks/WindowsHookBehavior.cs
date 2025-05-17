using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace WindowsHooks
{
    public class WindowsHookCollection : List<WindowsHook>
    {
    }


    public class WindowsHookBehavior : Behavior<Window>
    {
        public WindowsHookCollection WindowsHooks { get; } = new WindowsHookCollection();


        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Initialized += Initialized;
            AssociatedObject.Closed += Closed;

            AddCommandBindings();
        }


        protected override void OnDetaching()
        {
            RemoveCommandBindings();

            AssociatedObject.Closed -= Closed;
            AssociatedObject.Initialized -= Initialized;

            base.OnDetaching();
        }


        private void Initialized(object sender, EventArgs e)
        {
            foreach (var i in WindowsHooks)
                i.Hook(AssociatedObject);
        }


        private void Closed(object sender, EventArgs e)
        {
            foreach (var i in WindowsHooks)
                i.Unhook();
        }


        CommandBinding AddHookCommandBinding;
        CommandBinding RemoveHookCommandBinding;

        void AddCommandBindings()
        {
            if (AssociatedObject.Resources["AddHookCommand"] is ICommand addHookCommand)
            {
                if (AddHookCommandBinding == null)
                    AddHookCommandBinding = new CommandBinding(addHookCommand, AddHookExecuted);
                AssociatedObject.CommandBindings.Add(AddHookCommandBinding);
            }
            if (AssociatedObject.Resources["RemoveHookCommand"] is ICommand removeHookCommand)
            {
                if (RemoveHookCommandBinding == null)
                    RemoveHookCommandBinding = new CommandBinding(removeHookCommand, RemoveHookExecuted);
                AssociatedObject.CommandBindings.Add(RemoveHookCommandBinding);
            }
        }


        void RemoveCommandBindings()
        {
            if (RemoveHookCommandBinding != null)
                AssociatedObject.CommandBindings.Remove(RemoveHookCommandBinding);
            if (AddHookCommandBinding != null)
                AssociatedObject.CommandBindings.Remove(AddHookCommandBinding);
        }


        void AddHookExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is WindowsHook wh)
            {
                WindowsHooks.Add(wh);
                if (AssociatedObject.IsInitialized)
                    wh.Hook(e.Source as Window);
            }
        }


        void RemoveHookExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is WindowsHook wh)
            {
                wh.Unhook();
                WindowsHooks.Remove(wh);
            }
        }
    }
}
