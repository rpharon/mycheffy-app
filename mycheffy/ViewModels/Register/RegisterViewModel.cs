using System;
using System.Net.Http;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using mycheffy.Models.Register;
using mycheffy.Models.Shared.Enums;
using mycheffy.Services;
using mycheffy.Views.Popups;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Navigation;
using ReactiveUI;
using Refit;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Register
{
    public class RegisterViewModel : NavigationViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        public RegisterService _registerService = new RegisterService();
        public LoginService _loginService = new LoginService();

        private bool _isPreRegister;
        public bool IsPreRegister
        {
            get => _isPreRegister;
            set => this.RaiseAndSetIfChanged(ref _isPreRegister, value);
        }

        private bool _isCustomer;
        public bool IsCustomer
        {
            get => _isCustomer;
            set => this.RaiseAndSetIfChanged(ref _isCustomer, value);
        }

        private bool _isVendor;
        public bool IsVendor
        {
            get => _isVendor;
            set => this.RaiseAndSetIfChanged(ref _isVendor, value);
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => this.RaiseAndSetIfChanged(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
        }

        public RegisterViewModel(INavigationService navigationService)
        {
            Console.WriteLine("RegisterViewModel Constructor");

            _navigationService = navigationService;

            ClearFields();

            BacktoSignIn = ReactiveCommand.CreateFromTask(ExecuteSignIn);
            Proceed = ReactiveCommand.CreateFromTask(ExecuteRegister);
        }

        public ReactiveCommand<Unit, Unit> BacktoSignIn { get; set; }
        public ReactiveCommand<Unit, Unit> Proceed { get; set; }

        private async Task ExecuteSignIn()
        {
#if DEBUG
            Console.WriteLine("ExecuteSignIn");
#endif
            var par = new NavigationParameters();

            if (!IsVendor)
            {
                par.Add("LoginAs", 0); //Login as Customer
            }
            else
            {
                par.Add("LoginAs", 1); //Register as Customer
            }

            _navigationService.NavigateAsync("LoginPage", par);
        }

        private async Task ExecuteRegister()
        {
            PopupActivityIndicator popupActivityIndicator;
            popupActivityIndicator = new PopupActivityIndicator();
            await PopupNavigation.PushAsync(popupActivityIndicator, false);

#if DEBUG
            Console.WriteLine("ExecuteRegister");
#endif            

            if (FirstName == string.Empty || LastName == string.Empty || Email == string.Empty || Password == string.Empty || ConfirmPassword == string.Empty)
            {
                ValidateFields(FirstName, LastName, Email, Password, ConfirmPassword);
            }
            else
            {
                try
                {
                    if (!IsVendor)
                    {
                        var response = await _registerService.Register(FirstName,
                                        LastName,
                                        Email,
                                        Password,
                                        ConfirmPassword,
                                        "email",
                                        "",
                                        "",
                                        "",
                                        0); //0 if customer, 1 if vendor

                        if (response.IsSuccessStatusCode)
                        {
#if DEBUG
                            Console.WriteLine("Data: " + JsonConvert.SerializeObject(response.Content));
                            Console.WriteLine("Name: " + response.Content.first_name + " " + response.Content.last_name);
                            Console.WriteLine("Email: " + response.Content.email);
#endif
                            string name = response.Content.first_name + " " + response.Content.last_name;
                            string email = response.Content.email;

                            var alertMsg = "Registration is successful. \n " + name + "\n" + email;
                            Preferences.Set(PreferencesKey.FirstName.ToString(), response.Content.first_name);
                            Preferences.Set(PreferencesKey.LastName.ToString(), response.Content.last_name);
                            Preferences.Set(PreferencesKey.Email.ToString(), response.Content.email);
                            Preferences.Set(PreferencesKey.IsVendor.ToString(), false);
                            await App.Current.MainPage.DisplayAlert("Register Successful", alertMsg, "OK");

                            Preferences.Set("Username", email);
                            Preferences.Set("Password", Password);
                            Preferences.Set("HasCreatedAccount", true);

                            var loginResponse = await _loginService.SignIn("password",
                                                                           NetworkService.client_id,
                                                                           NetworkService.client_secret,
                                                                           email,
                                                                           Password,
                                                                           string.Empty,
                                                                           "email",
                                                                           string.Empty,
                                                                           string.Empty,
                                                                           string.Empty);
                            //Login newly registered account
                            if (loginResponse.IsSuccessStatusCode)
                            {
#if DEBUG
                                Console.WriteLine("Login Successful");
#endif
                                //await SecureStorage.SetAsync(SecureStorageKey.OAuthToken.ToString(), loginResponse.Content.access_token);
                                //await SecureStorage.SetAsync(SecureStorageKey.Password.ToString(), Password);
                                Preferences.Set(PreferencesKey.AccessToken.ToString(), loginResponse.Content.access_token);
                                Preferences.Set(PreferencesKey.Password.ToString(), Password);

                                Preferences.Set(PreferencesKey.FirstName.ToString(), FirstName);
                                Preferences.Set(PreferencesKey.LastName.ToString(), LastName);
                                Preferences.Set(PreferencesKey.FullName.ToString(), FirstName + " " + LastName);
                                Preferences.Set(PreferencesKey.Email.ToString(), email);

                                await _navigationService.NavigateAsync("MasterPage");
                            }
                            else
                            {
                                Console.WriteLine("Login Failed");

                                var errorObj = JObject.Parse(response.Error.Content);

#if DEBUG
                                Console.WriteLine("Error Obj: \n" + errorObj["error"]);
#endif
                                var errorMsg = errorObj["error"];

                                await App.Current.MainPage.DisplayAlert("Login Error", (string)errorMsg, "OK");
                            }
                        }
                        else
                        {
                            var errorObj = JObject.Parse(response.Error.Content);

#if DEBUG
                            Console.WriteLine("Error Obj: \n" + errorObj["error"]);
#endif
                            var errorMsg = String.Empty;

                            foreach (var err in errorObj["error"].Values())
                            {
                                errorMsg += err;
                            }

                            errorMsg = RemoveSpecialCharacters(errorMsg);

                            await App.Current.MainPage.DisplayAlert("Register Error", errorMsg, "OK");
                        }
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Register", "Register as Vendor is not yet available\nAPI is still under development.", "OK");
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine("Register Exception: " + ex.Message);
#endif
                }
            }

            popupActivityIndicator.Pop();
        }

        private void ClearFields()
        {
            _firstName = string.Empty;
            _lastName = string.Empty;
            _email = string.Empty;
            _password = string.Empty;
            _confirmPassword = string.Empty;
        }

        private void ValidateFields(string firstname, string lastname, string email, string pass, string confirmPass)
        {
            if (firstname == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Register", "Please enter your firstname", "OK");
            }
            if (lastname == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Register", "Please enter your lastname", "OK");
            }
            else if (email == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Register", "Please enter your email", "OK");
            }
            else if (pass == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Register", "Please enter your password", "OK");
            }
            else if (confirmPass == string.Empty)
            {
                App.Current.MainPage.DisplayAlert("Register", "Please confirm your password", "OK");
            }
        }

        private string RemoveSpecialCharacters(string errorMsg)
        {
            errorMsg = errorMsg.Replace("\"", string.Empty).Trim(); //remove quotes from string
            errorMsg = errorMsg.Replace("[", string.Empty).Trim(); //remove open bracket from string
            errorMsg = errorMsg.Replace("]", string.Empty).Trim(); //remove close  bracket from string
            errorMsg = errorMsg.Replace(",", string.Empty).Trim(); //remove close  bracket from string

            return errorMsg;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Console.WriteLine("RegisterViewModel OnNavigatedTo");

            var registerAs = parameters["RegisterAs"];
            var isPreRegister = parameters["IsPreRegister"];
            Console.WriteLine("Register As: " + registerAs.ToString());
            Console.WriteLine("IsPreRegister As: " + isPreRegister.ToString());

            RegisterAs((int)registerAs);
            PreRegister((int)isPreRegister);
        }

        public void OnNavigating(INavigationParameters paramaters)
        {
        }

        private void RegisterAs(int registerAs)
        {
            if (registerAs == 0) //Register as Customer
            {
                IsCustomer = true;
                IsVendor = false;
            }
            else if (registerAs == 1) // Register as Vendor
            {
                IsCustomer = false;
                IsVendor = true;

                Application.Current.MainPage.DisplayAlert("Welcome", "Register as Vendor is still under development.", "OK");
            }
        }

        private void PreRegister(int preRegister)
        {
            if (preRegister == 0) //if not pre register, show other details
            {
                IsPreRegister = true;
            }
            else if (preRegister == 1) //if pre register, hide other details
            {
                IsPreRegister = false;
            }
        }
    }
}
