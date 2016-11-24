using Models;
using Models.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConsoleGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GUIEchoImpl user;

        public MainWindow()
        {
            InitializeComponent();

            GuiEchoActions actions = new GuiEchoActions
            {
                Debug = ReceiveMessage,
                InvitationReceivedMsg = (a, b) => { ReceiveMessage(a.Username); },
                MyInvitationRejected = MyInvitationRejected,
                ShowInvitation = ShowInvitation
              //  MyInvitationAccepted = MyInvitationAccepted
            };

            var conf = Helper.DeserializeConfig("cluster.config");
            user = new GUIEchoImpl(conf, "Maisie", ReceiveMessage, actions);
        }

        public void ReceiveMessage(string msg)
        {
            Dispatcher.Invoke(() =>
            {
                conversationWindow.Text += Environment.NewLine + msg;
            });
        }

        public void MyInvitationRejected()
        {
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Invitation rejected");
            });
        }

        public void MyInvitationAccepted()
        {
            Dispatcher.Invoke(() =>
            {
                MainGrid.Background = Brushes.Green;
            });
        }

        public void ShowInvitation(ClusterInvitation invitation)
        {
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Your invitaion: " + invitation);
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SendMsg();
        }

        private void SendMsg()
        {
            var msg = inputTextbox.Text;
            user.Send(msg);
            inputTextbox.Text = "";
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var invitation = Helper.TryReadInvitation();
            if (invitation != null)
            {
                user.TryUseInvitation(invitation);
            }
            else
            {
                Console.WriteLine("Invitation missing or invalid.");
            }
        }

        private void inputTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMsg();
            }
            if (e.Key == Key.Oem2)
            {
                if (inputTextbox.Text == "/")
                {
                    ShowCommands();
                }
            }
        }

        private void ShowCommands()
        {
            toolTipKeys.Text = string.Join(Environment.NewLine, user.GetListOfCommands().Select(c => c.Item1 + ": "));
            toolTipDesc.Text = string.Join(Environment.NewLine, user.GetListOfCommands().Select(c => c.Item2));
        }
    }
}
