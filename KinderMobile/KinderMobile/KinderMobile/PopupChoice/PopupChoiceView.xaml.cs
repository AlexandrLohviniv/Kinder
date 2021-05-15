using Rg.Plugins.Popup.Pages;
using System;
using Plugin.InputKit.Shared.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using KinderMobile.DTOs;
using AdvancedRadioButton = Plugin.InputKit.Shared.Controls.RadioButton;

namespace KinderMobile.PopupChoice
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupChoiceView : PopupPage
    {

        public dynamic Result { get; set; }

        public EventHandler selected;

        public PopupChoiceView()
        {
            InitializeComponent();
            this.BindingContext = new PopupChoiceViewModel(this);
        }


        public static PopupChoiceView CreateChoiceView<T>(T alteringOption, InputType inputType = InputType.Default)
        {
            PopupChoiceView popupChoiceView = new PopupChoiceView();


            Button OKButton = new Button()
            {
                Text = "Ok"
            };

            if (alteringOption.GetType().IsEnum)
            {
                int selectedChoice = 0;

                var underlyingType = typeof(T).GetEnumUnderlyingType();
                if (underlyingType == typeof(int))
                {
                    dynamic value = alteringOption;
                    selectedChoice = (int)value;
                }

                foreach (string options in Enum.GetNames(typeof(T)))
                {
                    AdvancedRadioButton btn = new AdvancedRadioButton();
                    btn.Text = options;
                    btn.CircleColor = Color.Green;

                    popupChoiceView.OptionList.Children.Add(btn);
                }

                (popupChoiceView.BindingContext as PopupChoiceViewModel).selectedRadioButton = selectedChoice;
                popupChoiceView.OptionList.SetBinding(RadioButtonGroupView.SelectedIndexProperty, "selectedRadioButton");
                popupChoiceView.OptionList.SetBinding(RadioButtonGroupView.SelectedItemChangedCommandProperty, "MakeChoiceCommand");

            }
            else if (alteringOption.GetType() == typeof(int))
            {
                dynamic value = alteringOption;
                int height = (int)value;

                Plugin.InputKit.Shared.Controls.AdvancedEntry entry = new Plugin.InputKit.Shared.Controls.AdvancedEntry();
                entry.IsRequired = true;
                entry.Title = "Enter your height";
                entry.TextColor = Color.Black;

                (popupChoiceView.BindingContext as PopupChoiceViewModel).InputedText = height.ToString();

                entry.SetBinding(AdvancedEntry.TextProperty, new Binding("InputedText"));

                entry.Placeholder = "height...";

                entry.Annotation = Plugin.InputKit.Shared.AnnotationType.DigitsOnly;
                entry.MaxLength = 3;
                entry.MinLength = 2;
                entry.ValidationMessage = "Enter valid height";
                entry.AnnotationColor = Color.Accent;
                entry.Keyboard = Keyboard.Numeric;
                entry.CompletedCommand = (popupChoiceView.BindingContext as PopupChoiceViewModel).ReadStringCommand;
                popupChoiceView.InputFields.Children.Add(entry);

                OKButton.Command = (popupChoiceView.BindingContext as PopupChoiceViewModel).ReadStringCommand;

                popupChoiceView.Layout.Children.Add(OKButton);
            }
            else if (alteringOption.GetType() == typeof(DateTime))
            {
                dynamic value = alteringOption;

                DateTime zeroTime = new DateTime(1, 1, 1);

                TimeSpan span = (DateTime.Now - (DateTime)value);

                int years = (zeroTime + span).Year - 1;
                (popupChoiceView.BindingContext as PopupChoiceViewModel).Years = years;

                Plugin.InputKit.Shared.Controls.AdvancedSlider slider = new Plugin.InputKit.Shared.Controls.AdvancedSlider();
                slider.MaxValue = 100d;
                slider.MinValue = 0d;
                slider.Title = "Input your age";
                slider.ValuePrefix = "Age: ";
                slider.ValueSuffix = " years";
                slider.TextColor = Color.Black;
                slider.DisplayMinMaxValue = true;

                slider.SetBinding(AdvancedSlider.ValueProperty, "Years");

                slider.Value = years;
                popupChoiceView.Layout.Children.Add(slider);
                OKButton.Command = (popupChoiceView.BindingContext as PopupChoiceViewModel).ReadYearCommand; 

                popupChoiceView.Layout.Children.Add(OKButton);
            }
            else if (alteringOption.GetType() == typeof(string))
            {
                dynamic value = alteringOption;
                string Value = (string)value;


               

                Plugin.InputKit.Shared.Controls.AdvancedEntry entry = new Plugin.InputKit.Shared.Controls.AdvancedEntry();
                entry.IsRequired = true;
                entry.Title = "Enter value";
                entry.TextColor = Color.Black;

                (popupChoiceView.BindingContext as PopupChoiceViewModel).InputedText = Value;

                entry.SetBinding(AdvancedEntry.TextProperty, new Binding("InputedText"));

                entry.Placeholder = "value...";
                entry.Annotation = Plugin.InputKit.Shared.AnnotationType.LettersOnly;
                entry.MaxLength = 30;
                entry.MinLength = 2;
                entry.ValidationMessage = "Enter valid value";
                entry.AnnotationColor = Color.Accent;
                entry.Keyboard = Keyboard.Text;
                
                entry.TextChanged += (object sender, TextChangedEventArgs e) =>
                {
                    OKButton.IsEnabled = entry.IsValidated;
                };

                OKButton.Command = (popupChoiceView.BindingContext as PopupChoiceViewModel).ReadStringCommand;

                popupChoiceView.InputFields.Children.Add(entry);
                popupChoiceView.Layout.Children.Add(OKButton);
            }
            return popupChoiceView;

        }
    }
}