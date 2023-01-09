using Microsoft.Win32;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;

namespace mail
{
    public partial class SendWindow : Window
    {

        User user = new User();
        MailMessage Mail = new MailMessage();
        public SendWindow(User user)
        {
            InitializeComponent();
            this.user = user;

        }

        private void SendBtnClick(object sender, RoutedEventArgs e)
        {
            MailAddress mailAddress = new MailAddress(user.Email);
            Mail.From = mailAddress;
            Mail.To.Add(toTxtBox.Text);
            Mail.Subject = subjectTxtBox.Text;
            Mail.Body = bodyTxtBox.Text;



            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(user.Email, user.Password),
                EnableSsl = true
            };

            client.Send(Mail);
            this.Close();
        }



        private void AttachBtnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
                Mail.Attachments.Add(new Attachment(dialog.FileName));
        }



        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = (sender as RadioButton);
            switch (li.Content.ToString())
            {
                case "Low":
                    Mail.Priority = MailPriority.Low;
                    break;
                case "Normal":
                    Mail.Priority = MailPriority.Normal;
                    break;
                case "High":
                    Mail.Priority = MailPriority.High;
                    break;
            }


        }
    }
}
