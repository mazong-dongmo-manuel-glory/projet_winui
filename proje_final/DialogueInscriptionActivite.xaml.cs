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

    public sealed partial class DialogueInscriptionActivite : ContentDialog
    {
        public DialogueInscriptionActivite()
        {
            this.InitializeComponent();
            dateChoisis.MinDate = DateTime.Now.AddDays(1);
            dateChoisis.MaxDate = DateTime.Now.AddDays(365);
            HeureChoisis.MinuteIncrement = 30;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
