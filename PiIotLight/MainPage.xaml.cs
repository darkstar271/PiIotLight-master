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

        private GpioPin pin0, pin1, pin2, pin3, pin4, pin5, pin6, pin7, pin8;
        private GpioPin[] AllPins;

        void InitializGpio()
        {
  AllPins = new GpioPin[]{ pin0, pin1, pin2, pin3, pin4, pin5, pin6, pin7, pin8};

            var _gpio = GpioController.GetDefault();
            if (_gpio != null)
            {

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


                pin1.Write(GpioPinValue.Low);
                pin2.Write(GpioPinValue.Low);

                pin1.SetDriveMode(GpioPinDriveMode.Output);
                pin2.SetDriveMode(GpioPinDriveMode.Output);

            }
            else
            {
                var msg = new MessageDialog("No Gpio found").ShowAsync();

            }

        }

        private void InfinateLoop()
        {
            for (int j = 0; j < 2; j++)
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
            if (SwitchTwo.IsOn)
            {
                pin2.Write(GpioPinValue.High);

            }
            else
            {
                pin2.Write(GpioPinValue.Low);
            }
        }

    }
}
