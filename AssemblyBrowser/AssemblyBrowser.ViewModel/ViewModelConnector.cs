using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System;

namespace AssemblyBrowser.ViewModel
{
    public class ViewModelConnector : INotifyPropertyChanged
    {
        protected AssemblyStringProcessor assembly;
        protected string selectedNamespace;
        protected string selectedDatatype;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public AssemblyBrowserCommand ExitCommand
        { get; protected set; }

        public AssemblyBrowserCommand LoadCommand
        { get; protected set; }

        public void ShutdownApp(object o)
        {
            Environment.Exit(0);
        }

        protected void LoadAssembly(object o)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Assemblies|*.dll",
                Title = "Select assembly",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                assembly = new AssemblyStringProcessor(openFileDialog.FileName);
                OnPropertyChanged(nameof(Namespaces));
                Namespace = null;
                Datatype = null;
            }
        }

        public IEnumerable<string> Namespaces => assembly?.GetNamespaces();

        public string Namespace
        {
            get
            {
                return selectedNamespace;
            }
            set
            {
                selectedNamespace = value;
                OnPropertyChanged(nameof(Datatypes));
                Datatype = null;
            }
        }

        public IEnumerable<string> Datatypes
        {
            get
            {
                if (Namespace != null)
                {
                    return assembly?.GetNamespaceDatatypes(Namespace);
                }
                else
                {
                    return null;
                }
            }
        }

        public string Datatype
        {
            get
            {
                return selectedDatatype;
            }
            set
            {
                if (value == null)
                {
                    selectedDatatype = value;
                }
                else
                {
                    string[] splittedDatatype = value.Split(' ');
                    selectedDatatype = splittedDatatype[splittedDatatype.Length - 1];
                }
                OnPropertyChanged(nameof(Fields));
                OnPropertyChanged(nameof(Properties));
                OnPropertyChanged(nameof(Methods));
            }
        }

        public IEnumerable<string> Fields
        {
            get
            {
                if ((Namespace != null) && (Datatype != null))
                {
                    return assembly?.GetDatatypeFields(Namespace, Datatype);
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<string> Properties
        {
            get
            {
                if ((Namespace != null) && (Datatype != null))
                {
                    return assembly?.GetDatatypeProperties(Namespace, Datatype);
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<string> Methods
        {
            get
            {
                if ((Namespace != null) && (Datatype != null))
                {
                    return assembly?.GetDatatypeMethods(Namespace, Datatype);
                }
                else
                {
                    return null;
                }
            }
        }

        public ViewModelConnector()
        {
            assembly = null;
            selectedNamespace = null;
            selectedDatatype = null;
            ExitCommand = new AssemblyBrowserCommand(ShutdownApp);
            LoadCommand = new AssemblyBrowserCommand(LoadAssembly);
        }
    }
}
