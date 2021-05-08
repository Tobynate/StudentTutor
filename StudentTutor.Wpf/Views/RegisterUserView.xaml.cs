using Microsoft.Win32;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using StudentTutor.Core.Models;
using StudentTutor.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentTutor.Wpf.Views
{
    /// <summary>
    /// Interaction logic for RegisterUserView.xaml
    /// </summary>
    public partial class RegisterUserView : MvxWpfView
    {
        public RegisterUserView()
        {
            InitializeComponent();
            var set = this.CreateBindingSet<RegisterUserView, RegisterUserViewModel>();
            set.Bind(this).For(view => view.Interaction).To(viewModel => viewModel.Interaction).OneWay();
            set.Bind(this).For(view => view.SubjectInteraction).To(viewModel => viewModel.SubjectInteraction).OneWay();

            set.Apply();
        }
        private IMvxInteraction<FileDialogInteraction> _interaction;
        public IMvxInteraction<FileDialogInteraction> Interaction
        {
            get => _interaction;
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequested;

                if(value != null)
                {
                    _interaction = value;
                    _interaction.Requested += OnInteractionRequested;
                }
            }
        }
        private async void OnInteractionRequested(object sender, MvxValueEventArgs<FileDialogInteraction> eventArgs)
        {
            var dialogInteraction = eventArgs.Value;
            // show dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //Image files (*.bmp, *.jpg)|*.bmp;*.jpg|All files (*.*)|*.*\"'
            openFileDialog.Filter = "Image files(*.jpg, *.jpeg, *.png)| *.jpg;*.jpeg;*.png";
            openFileDialog.Title = "Pick a passport picture";

            var status = openFileDialog.ShowDialog();

            if ((bool)status)
            {
                dialogInteraction.File = new FileInfo(openFileDialog.FileName);
                dialogInteraction.SelectedFile(status == true);
            }
        }
                
        private IMvxInteraction<RemoveSelectedSubjectInteraction> _subjectInteraction;
        public IMvxInteraction<RemoveSelectedSubjectInteraction> SubjectInteraction
        {
            get => _subjectInteraction;
            set
            {
                if (_subjectInteraction != null)
                    _subjectInteraction.Requested -= Cancel_Click;

                if (value != null)
                {
                    _subjectInteraction = value;
                    _subjectInteraction.Requested += Cancel_Click;
                }
            }
        }

        private async void Cancel_Click(object sender, MvxValueEventArgs<RemoveSelectedSubjectInteraction> e)
        {
            ListBoxItem selectedItem = (ListBoxItem)subjectOfInterestList.ItemContainerGenerator.
                                       ContainerFromItem(((Button)passedObject).DataContext);
            selectedItem.IsSelected = true;

            var cancelInteraction = e.Value;

            cancelInteraction.SubjectSelected(selectedItem.IsSelected);

            passedObject = null;
        }
        private static Object passedObject; 
        private void removeButton_Click(object sender, EventArgs e)
        {
            passedObject = sender;
        }
    }
}
