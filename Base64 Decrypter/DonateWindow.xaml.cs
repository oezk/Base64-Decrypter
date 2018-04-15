using System.Windows;
using System.Windows.Controls;

namespace Base64_Decrypter
{
    public partial class DonateWindow : Window
    {
        public const string PAYPAL = "https://paypal.me/oezk";
        public const string VENMO = "https://venmo.com/omera";

        public DonateWindow()
        {
            InitializeComponent();
        }

        // For button clicks
        private void btnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string btnName = btn.Name;

            switch (btnName)
            {
                case "PaypalBtn":
                    Paypal();
                    break;

                case "VenmoBtn":
                    Venmo();
                    break;

                default:
                    break;
            }
        }

        private void Paypal()
        {
            System.Diagnostics.Process.Start(PAYPAL);
        }

        private void Venmo()
        {
            System.Diagnostics.Process.Start(VENMO);
        }
    }
}
