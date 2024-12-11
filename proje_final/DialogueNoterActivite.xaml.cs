using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;



namespace proje_final
{

    public sealed partial class DialogueNoterActivite : ContentDialog
    {
        public int Note { get; private set; }
        public int IdParticipation { get; private set; }

        public DialogueNoterActivite(int idParticipation)
        {
            this.InitializeComponent();
            this.IdParticipation = idParticipation;

           
            Note = (int)sliderNote.Value;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
           
            Note = (int)sliderNote.Value;


            System.Diagnostics.Debug.WriteLine($"Valeur capturée dans le slider : {Note}");
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }
    }
}
