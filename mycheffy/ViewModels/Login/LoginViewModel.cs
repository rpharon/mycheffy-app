using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using mycheffy.Models.SocialMediaAuthentication;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using Xamarin.Forms;
using Prism.Navigation;
using ReactiveUI;
using mycheffy.Services;
using mycheffy.Service;
using System.Reactive;
using System.Reactive.Linq;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using mycheffy.Models.Shared.Enums;
using mycheffy.Views.Popups;
using Rg.Plugins.Popup.Services;

namespace mycheffy.ViewModels.Login
{
    public class LoginViewModel : NavigationViewModelBase
    {
        private readonly INavigationService _navigationService;

        IFacebookClient _facebookService = CrossFacebookClient.Current;
        IGoogleClientManager _googleService = CrossGoogleClient.Current;

        public LoginService _loginService;
        public ProfileService _profileService;

        public ObservableCollection<AuthNetwork> AuthenticationNetworks { get; set; } = new ObservableCollection<AuthNetwork>()
        {
            new AuthNetwork()
            {
                Name = "Google",
                Icon = "google_logo",
                Foreground = "#000000",
                Background ="#ffffff"
            },
            new AuthNetwork()
            {
                Name = "Facebook",
                Icon = "fb_logo",
                Foreground = "#FFFFFF",
                Background = "#1877F2"
            }
        };

        private string _grantType;
        public string GrantType
        {
            get => _grantType;
            set => this.RaiseAndSetIfChanged(ref _grantType, value);
        }

        private string _clientId;
        public string ClientId
        {
            get => _clientId;
            set => this.RaiseAndSetIfChanged(ref _clientId, value);
        }

        private string _clientSecret;
        public string ClientSecret
        {
            get => _clientSecret;
            set => this.RaiseAndSetIfChanged(ref _clientSecret, value);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _scope;
        public string Scope
        {
            get => _scope;
            set => this.RaiseAndSetIfChanged(ref _scope, value);
        }

        private string _type;
        public string Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        private string _provider;
        public string Provider
        {
            get => _provider;
            set => this.RaiseAndSetIfChanged(ref _provider, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _photo;
        public string Photo
        {
            get => _photo;
            set => this.RaiseAndSetIfChanged(ref _photo, value);
        }

        private bool _isSignUp;
        public bool IsSignUp
        {
            get => _isSignUp;
            set => this.RaiseAndSetIfChanged(ref _isSignUp, value);
        }

        private string _signInButtonColor;
        public string SignInButtonColor
        {
            get => _signInButtonColor;
            set => this.RaiseAndSetIfChanged(ref _signInButtonColor, value);
        }

        private string _signInAs;
        public string SignInAs
        {
            get => _signInAs;
            set => this.RaiseAndSetIfChanged(ref _signInAs, value);
        }

        private string _loginTitle;
        public string LoginTitle
        {
            get => _loginTitle;
            set => this.RaiseAndSetIfChanged(ref _loginTitle, value);
        }

        public LoginViewModel(INavigationService navigationService, ProfileService profileServices, LoginService loginService)
        {
            Console.WriteLine("LoginViewModel Constructor");

            _navigationService = navigationService;
            _loginService = loginService;
            _profileService = profileServices;

            _loginTitle = "Sign In";
            _signInAs = "Sign in as Vendor";

            _grantType = "password";
            _clientId = NetworkService.client_id;
            _clientSecret = NetworkService.client_secret;
            _username = string.Empty;
            _password = string.Empty;
            _scope = string.Empty;
            _type = string.Empty;
            _provider = string.Empty;
            _name = string.Empty;
            _photo = string.Empty;
            _isSignUp = false;
            _signInButtonColor = "#7F7F7F";

            this.WhenAnyValue(x => x.Username)
                .DistinctUntilChanged()
                .ObserveOn(RxApp.MainThreadScheduler).Subscribe(text =>
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        IsSignUp = false;
                        SignInButtonColor = "#7F7F7F";
                    }
                    else
                    {
                        IsSignUp = true;
                        SignInButtonColor = "#CB0000";
                    }
                });

            this.WhenAnyValue(x => x.Password)
                .DistinctUntilChanged()
                .ObserveOn(RxApp.MainThreadScheduler).Subscribe(text =>
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        IsSignUp = false;
                        SignInButtonColor = "#7F7F7F";
                    }
                    else
                    {
                        IsSignUp = true;
                        SignInButtonColor = "#CB0000";
                    }
                });

            SignUp = ReactiveCommand.CreateFromTask(ExecuteSignUp);
            SignIn = ReactiveCommand.CreateFromTask(ExecuteSignIn);
            SignAs = ReactiveCommand.CreateFromTask(ExecureSignAs);
            SocialMediaLogin = ReactiveCommand.Create<AuthNetwork>(async (data) => await LoginAsync(data));
        }

        public ReactiveCommand<Unit, Unit> SignIn { get; set; }
        public ReactiveCommand<Unit, Unit> SignUp { get; set; }
        public ReactiveCommand<Unit, Unit> SignAs { get; set; }
        public ReactiveCommand<AuthNetwork, Unit> SocialMediaLogin { get; set; }

        private async Task ExecuteSignUp()
        {
#if DEBUG
            Console.WriteLine("ExecuteSignUp");
#endif
            var par = new NavigationParameters();
            int registerAs;
            int isPreRegister = 1;

            if (LoginTitle == "Sign In")
            {
                registerAs = 0;
                Console.WriteLine("Register As from login: " + "Customer");

                par.Add("RegisterAs", registerAs);
                par.Add("IsPreRegister", isPreRegister);
            }
            else if (LoginTitle == "Vendor Sign In")
            {
                registerAs = 1;
                Console.WriteLine("Register As from login: " + "Vendor");

                par.Add("RegisterAs", registerAs);
                par.Add("IsPreRegister", isPreRegister);
            }

            await _navigationService.NavigateAsync("RegisterPage", par);
        }

        private async Task ExecuteSignIn()
        {
#if DEBUG
            Console.WriteLine("ExecuteSignIn");
#endif
            _type = "email";
            _scope = string.Empty;
            _provider = string.Empty;
            _name = string.Empty;
            _photo = string.Empty;

            if (Username != string.Empty && Password != string.Empty)
            {
                SignInMethod();
            }
            else if (Username == string.Empty && Password == string.Empty)
            {
                await App.Current.MainPage.DisplayAlert("Login", "Enter your email & password.", "OK");
            }
            else if (Username == string.Empty || Password == string.Empty)
            {
                if (Username == string.Empty)
                {
                    await App.Current.MainPage.DisplayAlert("Login", "Enter your email.", "OK");
                }
                else if (Password == string.Empty)
                {
                    await App.Current.MainPage.DisplayAlert("Login", "Enter your password.", "OK");
                }
            }
        }

        private async Task ExecureSignAs()
        {
            if (LoginTitle == "Sign In")
            {
                LoginTitle = "Vendor Sign In";
                SignInAs = "Sign in as Customer";

                Application.Current.MainPage.DisplayAlert("Login", "Login as Vendor", "OK");
            }
            else if (LoginTitle == "Vendor Sign In")
            {
                LoginTitle = "Sign In";
                SignInAs = "Sign in as Vendor";

                Application.Current.MainPage.DisplayAlert("Login", "Login as Customer", "OK");
            }
        }

        private async void SignInMethod()
        {
            PopupActivityIndicator popupActivityIndicator;
            popupActivityIndicator = new PopupActivityIndicator();
            await PopupNavigation.Instance.PushAsync(popupActivityIndicator, false);

#if DEBUG
            Console.WriteLine("SignInMethod");

            if (Provider != string.Empty)
            {
                Console.WriteLine("Sign in with: " + Provider);
            }
            else
            {
                Console.WriteLine("Sign in with: email");
            }
#endif

            var response = await _loginService.SignIn(GrantType, ClientId, ClientSecret, Username, Password,
                            Scope, Type, Provider, Name, Photo);

            if (response.IsSuccessStatusCode)
            {
#if DEBUG
                Console.WriteLine("Login Successful");
#endif
                Console.WriteLine("AccessToken: " + response.Content.access_token);
                Preferences.Set(PreferencesKey.AccessToken.ToString(), response.Content.access_token);

                try
                {
                    //await SecureStorage.SetAsync(SecureStorageKey.OAuthToken.ToString(), response.Content.access_token);
                    //await SecureStorage.SetAsync(SecureStorageKey.Password.ToString(), Password);

                    var profileResponse = await _profileService.GetCustomerProfile();

                    if (profileResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Get Profile Successful");

                        Console.WriteLine("Email: " + Username);
                        Console.WriteLine("Password: " + Password);
                        Console.WriteLine("FirstName: " + profileResponse.Content.first_name);
                        Console.WriteLine("LastName: " + profileResponse.Content.last_name);
                        Console.WriteLine("PhoneNumber: " + profileResponse.Content.phone_number);
                        Console.WriteLine("AccessToken: " + response.Content.access_token);

                        Preferences.Set(PreferencesKey.Email.ToString(), Username);
                        Preferences.Set(PreferencesKey.Password.ToString(), Password);

                        Preferences.Set(PreferencesKey.FirstName.ToString(), profileResponse.Content.first_name);
                        Preferences.Set(PreferencesKey.LastName.ToString(), profileResponse.Content.last_name);
                        Preferences.Set(PreferencesKey.ProfilePhoto.ToString(), profileResponse.Content.photo);
                        Preferences.Set(PreferencesKey.IsVendor.ToString(), profileResponse.Content.is_vendor);
                        Preferences.Set(PreferencesKey.PhoneNumber.ToString(), profileResponse.Content.phone_number);
                    }
                    else
                    {
                        Console.WriteLine("Get Profile Failed");

                        var errorObj = JObject.Parse(response.Error.Content);

#if DEBUG
                        Console.WriteLine("Error Obj: \n" + errorObj["error"]);
#endif
                        var errorMsg = errorObj["error"];

                        await App.Current.MainPage.DisplayAlert("Login Error", (string)errorMsg, "OK");
                    }

                    await _navigationService.NavigateAsync("MasterPage");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    // Possible that device doesn't support secure storage on device.
                }
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

            popupActivityIndicator.Pop();
        }

        async Task LoginAsync(AuthNetwork authNetwork)
        {
            switch (authNetwork.Name)
            {
                case "Facebook":
                    await LoginFacebookAsync(authNetwork);
                    break;
                case "Google":
                    await LoginGoogleAsync(authNetwork);
                    break;
            }
        }

        async Task LoginFacebookAsync(AuthNetwork authNetwork)
        {
            try
            {
                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;

                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                            var socialLoginData = new NetworkAuthData
                            {
                                Id = facebookProfile.Id,
                                Logo = authNetwork.Icon,
                                Foreground = authNetwork.Foreground,
                                Background = authNetwork.Background,
                                Picture = facebookProfile.Picture.Data.Url,
                                Name = $"{facebookProfile.FirstName} {facebookProfile.LastName}",
                            };

                            Console.WriteLine("Facebook Account: " + socialLoginData.Name);
                            //await App.Current.MainPage.Navigation.PushModalAsync(new HomePage(socialLoginData));
                            break;
                        case FacebookActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Canceled", "Ok");
                            break;
                        case FacebookActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Error", "Ok");
                            break;
                        case FacebookActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _facebookService.OnUserData -= userDataDelegate;
                };

                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "picture", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        async Task LoginGoogleAsync(AuthNetwork authNetwork)
        {
            try
            {
                if (!string.IsNullOrEmpty(_googleService.ActiveToken))
                {
                    //Always require user authentication
                    _googleService.Logout();
                }

                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                {
                    switch (e.Status)
                    {
                        case GoogleActionStatus.Completed:
#if DEBUG
                            var googleUserString = JsonConvert.SerializeObject(e.Data);
                            Debug.WriteLine($"Google Logged in succesfully: {googleUserString}");
#endif

                            var socialLoginData = new NetworkAuthData
                            {
                                Id = e.Data.Id,
                                Logo = authNetwork.Icon,
                                Foreground = authNetwork.Foreground,
                                Background = authNetwork.Background,
                                Picture = e.Data.Picture.AbsoluteUri,
                                Name = e.Data.Name,
                            };

                            Console.WriteLine("Google Account: " + socialLoginData.Name);
                            //await App.Current.MainPage.Navigation.PushModalAsync(new HomePage(socialLoginData));
                            break;
                        case GoogleActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Canceled", "Ok");
                            break;
                        case GoogleActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Error", "Ok");
                            break;
                        case GoogleActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _googleService.OnLogin -= userLoginDelegate;
                };

                _googleService.OnLogin += userLoginDelegate;

                await _googleService.LoginAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
