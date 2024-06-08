﻿using System.Collections.ObjectModel;
using System.Windows;

using Ched.Plugins;

namespace Ched.UI.Windows
{
    /// <summary>
    /// Interaction logic for DiagnosticsView.xaml
    /// </summary>
    public partial class DiagnosticsWindow : Window
    {
        public DiagnosticsWindow()
        {
            InitializeComponent();
        }
    }

    public class DiagnosticsWindowViewModel : ViewModel
    {
        private string title;
        private string message;
        private ObservableCollection<Diagnostic> diagnostics;

        public string Title
        {
            get => title;
            set
            {
                if (value == title) return;
                title = value;
                NotifyPropertyChanged();
            }
        }

        public string Message
        {
            get => message;
            set
            {
                if (value == message) return;
                message = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Diagnostic> Diagnostics
        {
            get => diagnostics;
            set
            {
                if (value == diagnostics) return;
                diagnostics = value;
                NotifyPropertyChanged();
            }
        }
    }

    public class DiagnosticViewModel : ViewModel
    {
        private Diagnostic source;

        public DiagnosticViewModel(Diagnostic diagnostic)
        {
            source = diagnostic;
        }

        public DiagnosticSeverity Severity => source.Severity;

        public string Message => source.Message;
    }
}
