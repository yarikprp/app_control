using app_control.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace app_control
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ValidateTicket_Click(object sender, RoutedEventArgs e)
        {
            string ticketNumber = TicketNumberTextBox.Text;

            if (string.IsNullOrWhiteSpace(ticketNumber))
            {
                DisplayMessage("Пожалуйста, введите номер билета.", Brushes.Red);
                return;
            }

            var ticket = GetTicket(ticketNumber);
            if (ticket == null)
            {
                DisplayMessage("Билет не найден.", Brushes.Red);
                return;
            }

            if (!CanAccess(ticket))
            {
                DisplayMessage("Проход запрещен.", Brushes.Red);
                return;
            }

            DisplayMessage("Проход разрешен.", Brushes.Green);
        }

        private Ticket GetTicket(string ticketNumber)
        {
            using (var context = new ViewpointContext())
            {
                return context.Tickets.FirstOrDefault(t => t.IdTickets == int.Parse(ticketNumber));
            }
        }

        private bool CanAccess(Ticket ticket)
        {
            DateTime currentTime = DateTime.Now;

            if (ticket.PurchaseDate.HasValue)
            {
                DateTime sessionStartTime = ticket.PurchaseDate.Value.AddMinutes(10); 

                return currentTime >= sessionStartTime; 
            }

            return false; 
        }

        private void DisplayMessage(string message, SolidColorBrush color)
        {
            ResultTextBlock.Text = message;
            ResultTextBlock.Foreground = color;
        }

        private void ScanTicket_Click(object sender, RoutedEventArgs e)
        {
            string scannedTicketNumber = SimulateTicketScan();

            if (!string.IsNullOrWhiteSpace(scannedTicketNumber))
            {
                TicketNumberTextBox.Text = scannedTicketNumber;
                ValidateTicket_Click(sender, e);
            }
            else
            {
                DisplayMessage("Не удалось сканировать билет.", Brushes.Red);
            }
        }

        private string SimulateTicketScan()
        {
            MessageBox.Show("Имитация сканирования");
            return "123456"; 
        }
    }
}
