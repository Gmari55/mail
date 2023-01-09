using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user = new User();
        public MainWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            connect(user);
        }



        private void SendBtnClick(object sender, RoutedEventArgs e)
        {
            SendWindow window = new SendWindow(user);
            window.Show();
        }
        void connect(User user)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate(user.Email, user.Password);

               
                foreach (var item in client.GetFolders(client.PersonalNamespaces[0]))
                {
                    this.ForldersListBox.Items.Add(item.Name);

                }

              
                var folder = client.GetFolder(SpecialFolder.Sent);
                folder.Open(FolderAccess.ReadWrite);

                IList<UniqueId> uids = folder.Search(SearchQuery.All);

                List<letter> leters = new List<letter>();
                foreach (var i in uids)
                {
                    MimeMessage message = folder.GetMessage(i);
                    letter letter = new letter(message);
                    leters.Add(letter);
                }
                dg.ItemsSource = leters;

               

                client.Disconnect(true);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string mail;
            letter letter = dg.SelectedItem as letter;
            if (letter.From==user.Email) 
             mail = letter.To; 
            else 
                mail = letter.From;
            SendWindow sendWindow = new SendWindow(user, mail);
            sendWindow.Show();
        }

        private void dgevent(object sender, MouseButtonEventArgs e)
        {

        }
    }
    }
