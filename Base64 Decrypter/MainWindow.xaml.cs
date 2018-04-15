using System;
using System.Text;
using System.Windows;
using swc = System.Windows.Controls;


namespace Base64_Decrypter
{

    public partial class MainWindow : Window
    {
        static int counter = 7;
        public MainWindow()
        {
            InitializeComponent();
            //  PhraseBtn.Content = "Remove Before";

        }

        // For menu item clicks
        private void menuItemClick(object sender, System.EventArgs e)

        {
            swc.MenuItem btn = (swc.MenuItem)sender;
            string btnName = btn.Name;


            switch (btnName)
            {
                case "AboutBtn":

                    About();
                    break;

                case "HelpBtn":
                    Help();
                    break;

                case "DonateBtn":
                    Donate();

                    break;

                case "ExitBtn":
                    Exit();
                    break;

                default:
                    break;
            }

        }

        private void About()
        {
            System.Windows.MessageBox.Show("MegaLink Decrypter - Version 1.0\nMarch 2018\nBy oezk", "About");
        }

        private void Help()
        {
            System.Windows.MessageBox.Show("MegaLink Decrypter is a C# application that can be used to decrypt base64 links.\nEncrypted strings can be pasted into the Link/Key textboxes.\nIf pasted into both boxes, the key text will be appened to the link text.\nYou can also remove characters, words, or other phrases from the links/keys before and/or after their decryption by putting the phrases in separated by spaces in the phrases textbox.\n\nEnjoy and please consider donating! ", "Help");
        }


        // Opens a new window that gives you the option to donate. Opens links with default browser
        private void Donate()
        {
            // Sets owner so new windows opens up relative to MainWindow
            DonateWindow donateWindow = new DonateWindow();
            MainWindow window = (Base64_Decrypter.MainWindow)App.Current.MainWindow;
            donateWindow.Owner = window;

            // ShowDialog is a blocking call so Opacity wont revert instantly
            window.Opacity = 0.8;
            donateWindow.ShowDialog();
            window.Opacity = 1;


        }



        private void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }






        // For button clicks
        private void btnClick(object sender, RoutedEventArgs e)
        {

            swc.Button btn = (swc.Button)sender;
            string btnName = btn.Name;


            switch (btnName)
            {
                case "ProcessBtn":
                    Process();
                    break;

                case "PhraseBtn":
                    Phrase();
                    break;

                case "GoBtn":
                    Go();
                    break;

                default:
                    break;
            }


        }

        private void Go()
        {
            try
            {
                System.Diagnostics.Process.Start(ResultText.Text);
            }
            catch (System.InvalidOperationException e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        // Returns a Base64 decrypted string
        public string decrypt(string encrypted)
        {
            try
            {
                byte[] data = Convert.FromBase64String(encrypted);
                return Encoding.UTF8.GetString(data);
            }

            catch (System.FormatException e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }

        // Removes all characters, words, or phrases specified in string
        public string removePhrases(string result)
        {
            try
            {

                string[] phrases = PhrasesText.Text.Split(' ');

                foreach (var phrase in phrases)
                {

                    result = result.Replace(phrase.ToString(), "");
                    Console.WriteLine(result);
                }
                return result;
            }

            catch (System.ArgumentException e)
            {
                Console.WriteLine(e.ToString());
                return result;
            }
        }




        private void Phrase()
        {
            if (PhraseBtn.Content.ToString() == "Remove Before")
            {
                PhraseBtn.Content = "Remove After";
            }

            else if (PhraseBtn.Content.ToString() == "Remove After")
            {
                PhraseBtn.Content = "Remove Before";
            }
        }

        private void Process()
        {
            string link = "";
            string key = "";
            string phrases = "";
            string result = "";

            link = LinkText.Text;
            key = KeyText.Text;
            phrases = PhrasesText.Text;

            // Append link and key without decryption
            if ((link.StartsWith("/#") && key.StartsWith("!")))
            {
                link = "https://mega.nz" + link;
                result = link + key;
                result = removePhrases(result);
                ResultText.Text = result;
                return;
            }

            // Append link and key without decryption
            if ((link.StartsWith("#") && key.StartsWith("!")))
            {
                link = "https://mega.nz/" + link;
                result = link + key;
                result = removePhrases(result);
                ResultText.Text = result;
                return;
            }


            // Removes phrase(s) before decrypting
            if (PhraseBtn.Content.ToString() == "Remove Before")
            {
                link = removePhrases(link);
                key = removePhrases(key);
            }



            link = decrypt(link);
            key = decrypt(key);

            if (link.StartsWith("/#"))
            {
                link = "https://mega.nz" + link;
            }

            if (link.StartsWith("#"))
            {
                link = "https://mega.nz/" + link;
            }

            result = link + key;

            // Removes phrase(s) after decrypting
            if (PhraseBtn.Content.ToString() == "Remove After")
            {
                result = removePhrases(result);
            }

            ResultText.Text = result;
            countdown();
        }

        // Displays a dialog that asks the user to donate. Sorry. 
        private void countdown()
        {
            counter--;

            if (counter < 1)
            {
                System.Windows.MessageBox.Show("Please consider donating by clicking on File > Donate", "Please donate", MessageBoxButton.OK, MessageBoxImage.Information);
                counter = 7;
            }
        }
    }

}