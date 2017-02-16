using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;

namespace KinesiologiaMiramar.Helpers
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CloseEventArgs> Close;
        public event EventHandler<SetMainContentEventArgs> SetMainContent;

        public abstract string Title { get; }
        protected string _ErrorMessage = "";
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                _ErrorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        public abstract bool IsDirty { get; set; }
        public virtual void Enter() { }
        public void RaisePropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        public void RaiseClose()
        {
            if (Close != null)
            {
                Close(this, new CloseEventArgs());
            }
        }

        public void RaiseSetMainContent(UserControl uc)
        {
            if (SetMainContent != null)
            {
                SetMainContent(this, new SetMainContentEventArgs(uc));
            }
        }
    }
}
