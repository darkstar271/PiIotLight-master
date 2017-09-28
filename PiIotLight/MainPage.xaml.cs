using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PiIotLight
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    ///  windows 10 UWP APP vid Part 4 time 9:55, just working on toggle methoeds.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InitializGpio();

        }

        DispatcherTimer dispatcherTimer;

        private GpioPin[] AllPins;

 private GpioPin pin0, pin1, pin2, pin3, pin4, pin5, pin6, pin7, pin8;
        void InitializGpio()
        {
  AllPins = new GpioPin[]{ pin0, pin1, pin2, pin3, pin4, pin5, pin6, pin7, pin8};

            var _gpio = GpioController.GetDefault();
            if (_gpio != null)
            {
                AllPins[0] = _gpio.OpenPin(3); // not to be used GPIO Pins 2 or 3 are for I2C
                AllPins[1] = _gpio.OpenPin(26);
                AllPins[2] = _gpio.OpenPin(19);
                AllPins[3] = _gpio.OpenPin(13);
                AllPins[4] = _gpio.OpenPin(6);
                AllPins[5] = _gpio.OpenPin(12);
                AllPins[6] = _gpio.OpenPin(16);
                AllPins[7] = _gpio.OpenPin(20);
                AllPins[8] = _gpio.OpenPin(21);


                foreach (var pin in AllPins)
                {
                    pin.Write(GpioPinValue.Low);
                    pin.SetDriveMode(GpioPinDriveMode.Output);
                }


                //pin1.Write(GpioPinValue.Low);
                //pin2.Write(GpioPinValue.Low);

                //pin1.SetDriveMode(GpioPinDriveMode.Output);
                //pin2.SetDriveMode(GpioPinDriveMode.Output);

            }
            else
            {
                var msg = new MessageDialog("No Gpio found").ShowAsync();

            }

        }

        private void InfinateLoop()
        {
            for (int j = 0; j < 4; j++)
            {
                
            for (int i = 0; i < AllPins.Length; i++)
            {
                Task.Delay(100).Wait();

                    if (AllPins[i].Read() == GpioPinValue.Low)
                {
                    AllPins[i].Write(GpioPinValue.High);
                }
                else
                {
                    AllPins[i].Write(GpioPinValue.Low);
                    }
            }
 }

            //if (IsTimeActivatedNow())
            //{
            //    AllPins[1].Write(GpioPinValue.Low);
            //}

        }

        //run this method to start the timer
        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            //attach it to the method that runs
            dispatcherTimer.Tick += DispatcherTimer_Tick; //methods that runs
//set how many seconds we want it to run​
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
            dispatcherTimer.Start();
        }

//this is the method that is running​

     private   void DispatcherTimer_Tick(object sender, object e)
        {
            //stuff happens in here
            TimePicker ActivateTime = new TimePicker();
            TimePicker StopTime = new TimePicker();

            // TimeSpan Now = new TimeSpan();

            DateTime Now = DateTime.Now;
            ActivateTime.Header = "Activate";

           if (Now.TimeOfDay >= (ActivateTime.Time))
            {
                AllPins[1].Write(GpioPinValue.High); // or set pin to High
                
                AllPins[2].Write(GpioPinValue.High);
                //Task.Delay(1000).Wait();
                AllPins[3].Write(GpioPinValue.High);
                //Task.Delay(1000).Wait();
                AllPins[4].Write(GpioPinValue.Low);
               // Task.Delay(1000).Wait();
                AllPins[5].Write(GpioPinValue.High);
                //Task.Delay(1000).Wait();
                AllPins[6].Write(GpioPinValue.High);
                //Task.Delay(1000).Wait();
                AllPins[7].Write(GpioPinValue.High);
                //Task.Delay(1000).Wait();
                AllPins[8].Write(GpioPinValue.High);
                //Task.Delay(1000).Wait();
                
            }
            if (Now.TimeOfDay >= (StopTime.Time))
            {
                AllPins[1].Write(GpioPinValue.Low); // or set pin to High
               // Task.Delay(1000).Wait();
                AllPins[2].Write(GpioPinValue.Low);
               // Task.Delay(1000).Wait();
                AllPins[3].Write(GpioPinValue.Low);
               // Task.Delay(1000).Wait();
                AllPins[4].Write(GpioPinValue.High);
               // Task.Delay(1000).Wait();
                AllPins[5].Write(GpioPinValue.Low);
                //Task.Delay(1000).Wait();
                AllPins[6].Write(GpioPinValue.Low);
               // Task.Delay(1000).Wait();
                AllPins[7].Write(GpioPinValue.Low);
               // Task.Delay(1000).Wait();
                AllPins[8].Write(GpioPinValue.Low);
                // dispatcherTimer.Stop();
            }


            //  return false;
            //  DateTime picker = TimePicker.d

            //stop timer
            //                    dispatcherTimer.Stop();


            //            var myrandom = new Random();
            //​
            //            for (int j = 0; j < 4; j++)
            //            {
            //​
            //                myMonster[j].movePlayer.X += myrandom.Next(0, 4);
            //​
            //                //if the dog has reached the end of the track
            //                if (myMonster[j].movePlayer.X > 700)
            //                {
            //                    //stop timer
            //                    dispatcherTimer.Stop();
            //                    //winningdog holds the winning dog number j
            //                    WinnerDetails(j);
            //​
            //                }
            //            }
        }


        private void Timetest()
        {
            
        }

        private bool IsTimeActivatedNow()
        {
            TimePicker ActivateTime = new TimePicker();
           // TimeSpan Now = new TimeSpan();

            DateTime Now = DateTime.Now;
            ActivateTime.Header = "Activate";
           
            if (Now  ==  Convert.ToDateTime(ActivateTime.Time))
            {
                return true;
            }

            return false;
            //  DateTime picker = TimePicker.d


        }


        private void SwitchOne_Toggled(object sender, RoutedEventArgs e)
        {
            //ToggleSwitch fakeSwitch = new ToggleSwitch();

            //fakeSwitch = (ToggleSwitch) sender;
            //int number = Convert.ToInt16(fakeSwitch.Tag);

            //if (fakeSwitch.IsOn)
            //{
            //    AllPins[number].Write(GpioPinValue.High);
            //}
            InfinateLoop();






            //if (SwitchOne.IsOn)
            //{
            //    AllPins[1].Write(GpioPinValue.High);
            //}
            //else
            //{
            //    pin1.Write(GpioPinValue.Low);
            //}
        }

        private void switchTwo_Toggled(object sender, RoutedEventArgs e)
        {
            
            //if (SwitchTwo.IsOn)
            //{
            //  //  pin2.Write(GpioPinValue.High);

            //}
            //else
            //{
            //  //  pin2.Write(GpioPinValue.Low);
            //}
        }

        private void SwitchThree_OnToggled(object sender, RoutedEventArgs e)
        {
            DispatcherTimerSetup();
        }
    }
}
