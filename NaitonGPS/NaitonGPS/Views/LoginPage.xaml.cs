using NaitonGPS.Helpers;
using NaitonGPS.Services;
using Newtonsoft.Json;
using Plugin.Connectivity;
using SimpleWSA;
using System;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        int taps = 0;
        public LoginPage()
        {
            InitializeComponent();


            entCompany.Text = string.Empty;
            entEmail.Text = string.Empty;
            entPassword.Text = string.Empty;

#if DEBUG
            entCompany.Text = "upstairstest";
            entEmail.Text = "m.aerts@upstairs.com";
            entPassword.Text = "Gromit12";
#endif

            scrollToActivate.IsEnabled = false;
            imgLogo.TranslationY = 100;
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            frameLogin.TranslationY = mainDisplayInfo.Height;
        }

        private async void PopUpLoginFrame(object sender, EventArgs e)
        {
            await imgLogo.TranslateTo(0, -45, 280, Easing.Linear);
            await frameLogin.TranslateTo(0, 0, 330, Easing.Linear);
            scrollToActivate.IsEnabled = true;
        }

        private async void LoginInit(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Preferences.Set("loginCompany", entCompany.Text);

                //Call Web service
                taps++;
                var response = await ApiService.GetWebService(entCompany.Text);

                if (response)
                {
                    if (taps == 1)
                    {
                        var userEmail = entEmail.Text;
                        var userPassword = entPassword.Text;
                        Preferences.Set("loginEmail", entEmail.Text);
                        Preferences.Set("loginPassword", entPassword.Text);

                        var emailPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                        if (CrossConnectivity.Current.IsConnected)
                        {
                            if (string.IsNullOrEmpty(userPassword) || string.IsNullOrWhiteSpace(userPassword))
                            {
                                await DisplayAlert("", "Invalid password", "Ok");
                                entCompany.Text = Preferences.Get("loginCompany", string.Empty);
                                entEmail.Text = Preferences.Get("loginEmail", string.Empty);

                                entPassword.Text = string.Empty;
                                entPassword.Focus();
                            }
                            else if (string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrEmpty(userEmail) || !Regex.IsMatch(userEmail, emailPattern))
                            {
                                await DisplayAlert("", "Invalid email", "Ok");
                                entCompany.Text = Preferences.Get("loginCompany", string.Empty);
                                entPassword.Text = Preferences.Get("loginPassword", string.Empty);

                                entEmail.Text = string.Empty;
                                entEmail.Focus();
                            }
                            else
                            {                                
                                    try
                                    {
                                        string currentAppVersion = VersionTracking.CurrentVersion;
                                        Session session = new Session(userEmail,
                                                                        entPassword.Text,
                                                                        false,
                                                                        6,
                                                                        currentAppVersion,
                                                                        Preferences.Get("loginCompany", string.Empty),
                                                                        null);
                                        await session.CreateByConnectionProviderAddressAsync("https://connectionprovider.naiton.com/");

                                        var user = DataManager.RegistrationServiceSession();


                                        App.Current.Properties["UserDetail"] = JsonConvert.SerializeObject(user);
                                        await Application.Current.SavePropertiesAsync();
                                        App.Current.Properties["IsLoggedIn"] = bool.TrueString;

                                        App.Current.MainPage = new AppShell();
                                    }
                                    catch (RestServiceException exRes)
                                    {
                                        if (exRes.Code == "MI008")
                                        {
                                            await SessionContext.Refresh();                                            
                                        }
                                        else
                                        {
                                            await DisplayAlert("", exRes.Message, "Ok");
                                            entEmail.Text = string.Empty;
                                            entPassword.Text = string.Empty;
                                            entEmail.Focus();                                            
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        await DisplayAlert("", ex.Message, "Ok");
                                        entEmail.Text = string.Empty;
                                        entPassword.Text = string.Empty;
                                        entEmail.Focus();                                        
                                    }
                                }
                        }
                        else
                        {
                            await DisplayAlert("", "Check the Internet connection.", "Ok");
                        }
                        taps = 0;
                    }
                    else if (taps >= 2)
                    {
                        taps = 1;
                        await DisplayAlert("", "Please wait. Your request is being proceeded", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("", "Wrong company name.Please enter valid name", "Ok");
                    entCompany.Text = string.Empty;
                    entCompany.Focus();
                    taps = 0;
                }
            }
            else
            {
                await DisplayAlert("", "Check the Internet connection.", "Ok");
                taps = 0;
            }
        }

        private async void CallNeedHelp(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NeedHelp());
        }

        private async void CallTermsAndService(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermsAndServices());
        }
    }
}