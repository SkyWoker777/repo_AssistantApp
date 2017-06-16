using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using AssistantApp.Data;
using AssistantApp.Data.Cist;

namespace AssistantApp
{
    public sealed partial class MainPage : Page, IWebAuthenticationContinuable
    {
        public static MainPage Current { get; private set; }
        private UserCredential credential;
        private CalendarService service;
        DbHelper dbHelper;

        public MainPage()
        {
            this.InitializeComponent();
            dbHelper = new DbHelper();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            Current = this;
            this.DataContext = this;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

        }

        public async void ContinueWebAuthentication(Windows.ApplicationModel.Activation.WebAuthenticationBrokerContinuationEventArgs args)
        {
            await PasswordVaultDataStore.Default.StoreAsync<SerializableWebAuthResult>(
                SerializableWebAuthResult.Name,
                new SerializableWebAuthResult(args.WebAuthenticationResult));

            await GetEventsAsync();

            await PasswordVaultDataStore.Default.DeleteAsync<SerializableWebAuthResult>(
                SerializableWebAuthResult.Name);
        }

        private async Task AuthenticateAsync()
        {
            if (service != null)
                return;

                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new Uri("ms-appx:///Assets/client_secret.json"),
                    new[] { CalendarService.Scope.CalendarReadonly },
                    "user",
                    CancellationToken.None);

                var initializer = new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "AssistantApp",
                };

                service = new CalendarService(initializer);
        }

        public async Task GetEventsAsync()
        {
            await AuthenticateAsync();

            try
            {
                // Определяем параметры запроса
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                Events events = request.Execute();

                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        foreach (var eventItem in events.Items)
                        {
                            string whenStart = eventItem.Start.DateTime.ToString();
                            string whenEnd = eventItem.End.DateTime.ToString();
                            if (String.IsNullOrEmpty(whenStart) && String.IsNullOrEmpty(whenEnd))
                            {
                                whenStart = eventItem.Start.Date;
                                whenEnd = eventItem.End.Date;
                            }
                            if(String.IsNullOrWhiteSpace(eventItem.Location))
                            {
                                eventItem.Location = "";
                            }
                            else if (String.IsNullOrWhiteSpace(eventItem.Description))
                            {
                                eventItem.Description = "";
                            }
                            MyEvent entity = new MyEvent
                            {
                                Title = eventItem.Summary,
                                Place = eventItem.Location,
                                Description = eventItem.Description,
                                StartDateTime = Convert.ToDateTime(whenStart),
                                EndTime = Convert.ToDateTime(whenEnd).TimeOfDay,
                                Service_ID = eventItem.Id
                            };
                            if (!dbHelper.CheckExistsInDb(entity.Service_ID))
                            {
                                dbHelper.AddServiceEvent(entity);
                            }
                            else dbHelper.SaveChangesFromService(entity);
                        }
                    });
            }
            catch (NullReferenceException)
            {
                var msd = new MessageDialog("Для синхронизации требуется интернет-соединение.", "Ошибка!");
                await msd.ShowAsync();
            }
        }
        private async void btnGetCalendar_Click(object sender, RoutedEventArgs e)
        {
            await GetEventsAsync();
        }

        private void btnAppBarForward_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoForward)
            {
                Frame.GoForward();
            }
            else Frame.Navigate(typeof(EventsPage));
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        private void btnGetCist_Click(object sender, RoutedEventArgs e)
        {
            uFac.Visibility = Visibility.Visible;
        }
    }
}
