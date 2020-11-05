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
using System.Data.SqlClient;
using System.Data;


namespace YahooSignIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        public MainWindow()
        {


            InitializeComponent();
            string constring = "Data Source=DESKTOP-V19M2D9\\sqlexpress;Initial Catalog=yahoopage;Integrated Security=True";
            connection = new SqlConnection(constring);

            String[] s1 = { "United States (+1)", "Pakistan (+3)", "Uganda (+123)", "United Kingdom (+233)", "Germany (+5121)", "Spain (+3123)", "Patoki (+131)" };
            Countries.ItemsSource = s1;

            String[] s2 = { "January", "Feburary", "March", "April", "May", "June", "July", "Augest" };
            birth.ItemsSource = s2;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            string fname = SearchTermTextBox.Text;
            string lname = LastName.Text;
            string pss = PasswordBox.Password;
            string Mob = Mobile.Text;
            string usname = EmailAddress.Text;



            if (string.IsNullOrWhiteSpace(SearchTermTextBox.Text.ToString()))
            {

                SearchTermTextBox.BorderBrush = Brushes.Red;
                required.Visibility = Visibility.Visible;


            }

            else
            {
                SearchTermTextBox.BorderBrush = Brushes.Black;
                required.Visibility = Visibility.Hidden;

            }

            if (string.IsNullOrWhiteSpace(year.Text.ToString()))
            {
                year.BorderBrush = Brushes.Red;
                required.Visibility = Visibility.Visible;

            }
            else
            {
                year.BorderBrush = Brushes.Black;
                required.Visibility = Visibility.Hidden;

            }

            if (string.IsNullOrWhiteSpace(day.Text.ToString()))
            {
                day.BorderBrush = Brushes.Red;
                required.Visibility = Visibility.Visible;

            }
            else
            {
                day.BorderBrush = Brushes.Black;
                required.Visibility = Visibility.Hidden;

            }


            if (string.IsNullOrWhiteSpace(PasswordBox.Password.ToString()))
            {
                PasswordBox.BorderBrush = Brushes.Red;
                required.Visibility = Visibility.Visible;

            }
            else
            {
                PasswordBox.BorderBrush = Brushes.Black;
                required.Visibility = Visibility.Hidden;

            }


            if (string.IsNullOrWhiteSpace(EmailAddress.Text.ToString()))
            {
                EmailAddress.BorderBrush = Brushes.Red;
                required.Visibility = Visibility.Visible;

            }
            else
            {
                EmailAddress.BorderBrush = Brushes.Black;
                required.Visibility = Visibility.Hidden;

            }


            if (string.IsNullOrWhiteSpace(Mobile.Text.ToString())
)
            {
                Mobile.BorderBrush = Brushes.Red;
                required.Visibility = Visibility.Visible;

            }
            else
            {
                Mobile.BorderBrush = Brushes.Black;
                required.Visibility = Visibility.Hidden;

            }


            if (PasswordBox.Password != PasswordBoxConfirm.Password)
            {
                PasswordBox.BorderBrush = Brushes.Red;
                PasswordBoxConfirm.BorderBrush = Brushes.Red;
                confirmPass.Visibility = Visibility.Visible;
                return;

            }
            else
            {
                PasswordBox.BorderBrush = Brushes.Black;
                PasswordBoxConfirm.BorderBrush = Brushes.Black;
                confirmPass.Visibility = Visibility.Hidden;

            }
            if (PasswordBox.Password.Length < 8 && PasswordBox.Password.Length > 0)
            {

                confirmPass1.Visibility = Visibility.Visible;

            }
            else
            {
                confirmPass1.Visibility = Visibility.Hidden;

            }
            bool exists = false;
            
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            using (SqlCommand cmd = new SqlCommand("select count(*) from yahoopage where username = @UserName", connection))
            {
                cmd.Parameters.AddWithValue("UserName", EmailAddress.Text);
                exists = (int)cmd.ExecuteScalar() > 0;
            }
            if (exists)
            {
                already.Visibility = Visibility.Visible;
                Signup.Visibility = Visibility.Hidden;
                return;
            }
            else
            {
                already.Visibility = Visibility.Hidden;
                String query = "INSERT INTO yahoopage (username,fname, lastname,password,mobile)"
                        + "VALUES ('" + usname + "','" + fname + "','" + lname + "', '" + pss + "', '" + Mob + "')";

                int numberOfAffectedRows = 0;
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        numberOfAffectedRows++;
                        command.ExecuteNonQuery();

                        Signup.Visibility = Visibility.Visible;
                        already.Visibility = Visibility.Hidden;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            login log = new login();
            this.Content = log;
        }
    }
}