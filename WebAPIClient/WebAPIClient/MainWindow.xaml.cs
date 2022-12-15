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
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.IO;

namespace WebAPIClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            getContent();
            //Process myProcess = Process.Start("NotePad.exe");
            //int id =myProcess.Id;

        }


        async void postSend(object sender, RoutedEventArgs e)
        {
            try
            {

                //long NewIdleCpuUsage = (long)Process.GetCurrentProcess().TotalProcessorTime.TotalMilliseconds;
                NewEntryWindow entry = new NewEntryWindow();
                if (entry.ShowDialog() == true)
                {
                    MessageBox.Show("Имя: " + entry.name + ", Возраст: " + entry.age);
                    UserPost contact = new UserPost();
                    contact.name = entry.name;
                    contact.age = Int32.Parse(entry.age);
                    using (var http = new HttpClient())
                    {
                        http.DefaultRequestHeaders.Accept.Clear();
                        http.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage x = await http.PostAsJsonAsync<UserPost>("http://automatiomservices.somee.com/api/users/", contact);
                        string xstr = await x.Content.ReadAsStringAsync();
                        MessageBox.Show(xstr);
                        getContent();
                    }
                }
                else
                {
                    MessageBox.Show("Ввод отменен");
                }

                responseDisp.Text = Methods.getTag(sender, e);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            // getContent();         
        }

        async void delSend(object sender, RoutedEventArgs e)
        {
            try
            {
                //var tag = ((Button)sender).Tag;
                // var i = Convert.ToInt32(tag);
                //string strtag = i.ToString();
                string strtag = Methods.getTag(sender, e);
                Console.WriteLine(strtag);
                responseDisp.Text = strtag;
                using (var http = new HttpClient())
                {
                    HttpResponseMessage result = await http.DeleteAsync(new Uri("http://automatiomservices.somee.com/api/users/" + strtag));
                    //var bt = new Button();
                    // var 
                    //getSend(sender, e);
                    getContent();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async void editSend(object sender, RoutedEventArgs e)
        {
            try
            {
                //Contract.Requires(sender != null);
                NewEntryWindow entry = new NewEntryWindow();
                if (entry.ShowDialog() == true)
                {
                    MessageBox.Show("Имя: " + entry.name + ", Возраст: " + entry.age);
                    UserPost contact = new UserPost();
                    User user = new User();
                    user.id = Methods.getTagInt(sender, e);
                    user.name = entry.name;
                    user.age = Int32.Parse(entry.age);
                    contact.name = entry.name;
                    contact.age = Int32.Parse(entry.age);
                    using (var http = new HttpClient())
                    {
                        http.DefaultRequestHeaders.Accept.Clear();
                        http.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        var x = await http.PutAsJsonAsync<User>("http://automatiomservices.somee.com/api/users/", user);
                        string xstr = await x.Content.ReadAsStringAsync();
                        MessageBox.Show(xstr);
                        //getSend(sender, e);
                        getContent();
                    }
                }
                else
                {
                    MessageBox.Show("Ввод отменен");
                }

                responseDisp.Text = Methods.getTag(sender, e);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        async void getContent()
        {
            try
            {
                using (var http = new HttpClient())
                {
                    HttpResponseMessage result = await http.GetAsync(new Uri("http://automatiomservices.somee.com/api/users/"));
                    result.EnsureSuccessStatusCode();
                    string response = await result.Content.ReadAsStringAsync();
                    responseDisp.Text = response;
                    List<User> items = JsonConvert.DeserializeObject<List<User>>(response);
                    IEnumerator<User> enumerator = items.GetEnumerator();

                    lvDataBinding.ItemsSource = items;
                    while (enumerator.MoveNext())
                    {
                        Console.WriteLine(enumerator.Current.name);
                    }
                }
            }
            catch (Exception ex)
            {
                responseDisp.Text = ex.Message;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
